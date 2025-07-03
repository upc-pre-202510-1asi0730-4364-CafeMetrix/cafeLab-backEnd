using CafeLab.API.Application.DTOs;
using CafeLab.API.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Security.Claims;

namespace CafeLab.API.Controllers;

/// <summary>
/// Controlador para la autenticación y gestión de usuarios en el sistema de defectos y calibraciones.
/// Implementa las mejores prácticas del Learning Center Platform para APIs RESTful con autenticación JWT.
/// </summary>
/// <remarks>
/// Este controlador maneja todas las operaciones de autenticación y autorización:
/// - Login y registro de usuarios
/// - Gestión de tokens JWT y refresh tokens
/// - Perfiles de usuario
/// - Gestión administrativa de usuarios
/// 
/// Características de seguridad implementadas:
/// - Validación de entrada en todos los endpoints
/// - Logging detallado para auditoría
/// - Manejo de errores consistente
/// - Respuestas HTTP apropiadas
/// - Documentación automática con Swagger
/// 
/// Roles soportados:
/// - Admin: Acceso completo a defectos y calibraciones
/// - Technician: Acceso especializado para calibraciones
/// - User: Acceso básico a defectos y calibraciones
/// </remarks>
[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;
    private readonly ILogger<AuthController> _logger;

    /// <summary>
    /// Constructor del controlador de autenticación.
    /// </summary>
    /// <param name="authService">Servicio de autenticación</param>
    /// <param name="logger">Logger para auditoría y debugging</param>
    /// <remarks>
    /// Inyecta las dependencias necesarias para el funcionamiento del controlador.
    /// El logger se utiliza para auditoría de seguridad y debugging de operaciones.
    /// </remarks>
    public AuthController(IAuthService authService, ILogger<AuthController> logger)
    {
        _authService = authService ?? throw new ArgumentNullException(nameof(authService));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    /// <summary>
    /// Autentica un usuario en el sistema de defectos y calibraciones.
    /// </summary>
    /// <param name="loginDto">Credenciales de login del usuario</param>
    /// <returns>Token de acceso y información del usuario</returns>
    /// <response code="200">Login exitoso - Retorna tokens y datos del usuario</response>
    /// <response code="400">Credenciales inválidas o datos faltantes</response>
    /// <response code="500">Error interno del servidor</response>
    /// <remarks>
    /// Este endpoint implementa el flujo de autenticación completo:
    /// 
    /// **Flujo de autenticación:**
    /// 1. Valida los datos de entrada (username y password)
    /// 2. Busca el usuario en la base de datos
    /// 3. Verifica el estado de la cuenta (activa/bloqueada)
    /// 4. Valida la contraseña usando BCrypt
    /// 5. Registra el intento de login (exitoso o fallido)
    /// 6. Genera tokens JWT y refresh token
    /// 7. Actualiza la información de sesión
    /// 
    /// **Características de seguridad:**
    /// - Bloqueo automático después de 5 intentos fallidos
    /// - Auditoría completa con IP y timestamp
    /// - Tokens con expiración configurable
    /// - Refresh tokens para renovación automática
    /// 
    /// **Ejemplo de uso:**
    /// ```json
    /// {
    ///   "username": "admin",
    ///   "password": "password123"
    /// }
    /// ```
    /// 
    /// **Respuesta exitosa:**
    /// ```json
    /// {
    ///   "success": true,
    ///   "message": "Login exitoso",
    ///   "accessToken": "eyJhbGciOiJIUzI1NiIs...",
    ///   "refreshToken": "refresh_token_here",
    ///   "expiresAt": "2024-01-15T10:30:00Z",
    ///   "user": {
    ///     "id": 1,
    ///     "username": "admin",
    ///     "fullName": "Administrador",
    ///     "role": "Admin"
    ///   }
    /// }
    /// ```
    /// </remarks>
    [HttpPost("login")]
    [ProducesResponseType(typeof(AuthResponseDto), 200)]
    [ProducesResponseType(typeof(AuthResponseDto), 400)]
    [ProducesResponseType(typeof(AuthResponseDto), 500)]
    public async Task<ActionResult<AuthResponseDto>> Login([FromBody] LoginDto loginDto)
    {
        try
        {
            _logger.LogInformation("Solicitud de login recibida para usuario: {Username}", loginDto.Username);

            // Obtener IP del cliente para auditoría
            var ipAddress = GetClientIpAddress();
            
            // Procesar la autenticación
            var result = await _authService.LoginAsync(loginDto, ipAddress);

            if (!result.Success)
            {
                _logger.LogWarning("Login fallido para usuario: {Username}. Razón: {Message}", 
                    loginDto.Username, result.Message);
                return BadRequest(result);
            }

            _logger.LogInformation("Login exitoso para usuario: {Username} ({Role}) desde IP: {IpAddress}", 
                loginDto.Username, result.User?.Role, ipAddress);
            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error inesperado durante login para usuario: {Username}", loginDto.Username);
            return StatusCode(500, new AuthResponseDto
            {
                Success = false,
                Message = "Error interno del servidor"
            });
        }
    }

    /// <summary>
    /// Registra un nuevo usuario en el sistema de defectos y calibraciones.
    /// </summary>
    /// <param name="registerDto">Datos del nuevo usuario</param>
    /// <returns>Token de acceso y información del usuario registrado</returns>
    /// <response code="201">Usuario registrado exitosamente - Retorna tokens y datos del usuario</response>
    /// <response code="400">Datos inválidos o usuario ya existe</response>
    /// <response code="500">Error interno del servidor</response>
    /// <remarks>
    /// Este endpoint implementa el flujo de registro completo:
    /// 
    /// **Flujo de registro:**
    /// 1. Valida todos los datos de entrada
    /// 2. Verifica que el username y email sean únicos
    /// 3. Valida la fortaleza de la contraseña
    /// 4. Hashea la contraseña con BCrypt
    /// 5. Crea el usuario con rol por defecto "User"
    /// 6. Genera tokens de autenticación
    /// 7. Registra la auditoría de creación
    /// 
    /// **Validaciones implementadas:**
    /// - Contraseña mínima de 6 caracteres
    /// - Email válido
    /// - Username y email únicos
    /// - Confirmación de contraseña
    /// 
    /// **Ejemplo de uso:**
    /// ```json
    /// {
    ///   "username": "nuevo_usuario",
    ///   "email": "usuario@cafelab.com",
    ///   "fullName": "Nuevo Usuario",
    ///   "password": "password123",
    ///   "confirmPassword": "password123"
    /// }
    /// ```
    /// 
    /// **Respuesta exitosa:**
    /// ```json
    /// {
    ///   "success": true,
    ///   "message": "Usuario registrado exitosamente",
    ///   "accessToken": "eyJhbGciOiJIUzI1NiIs...",
    ///   "refreshToken": "refresh_token_here",
    ///   "expiresAt": "2024-01-15T10:30:00Z",
    ///   "user": {
    ///     "id": 2,
    ///     "username": "nuevo_usuario",
    ///     "fullName": "Nuevo Usuario",
    ///     "role": "User"
    ///   }
    /// }
    /// ```
    /// </remarks>
    [HttpPost("register")]
    [ProducesResponseType(typeof(AuthResponseDto), 201)]
    [ProducesResponseType(typeof(AuthResponseDto), 400)]
    [ProducesResponseType(typeof(AuthResponseDto), 500)]
    public async Task<ActionResult<AuthResponseDto>> Register([FromBody] RegisterDto registerDto)
    {
        try
        {
            _logger.LogInformation("Solicitud de registro recibida para usuario: {Username}", registerDto.Username);

            // Obtener IP del cliente para auditoría
            var ipAddress = GetClientIpAddress();
            
            // Procesar el registro
            var result = await _authService.RegisterAsync(registerDto, ipAddress);

            if (!result.Success)
            {
                _logger.LogWarning("Registro fallido para usuario: {Username}. Razón: {Message}", 
                    registerDto.Username, result.Message);
                return BadRequest(result);
            }

            _logger.LogInformation("Registro exitoso para usuario: {Username} ({Role}) desde IP: {IpAddress}", 
                registerDto.Username, result.User?.Role, ipAddress);
            return CreatedAtAction(nameof(GetProfile), result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error inesperado durante registro para usuario: {Username}", registerDto.Username);
            return StatusCode(500, new AuthResponseDto
            {
                Success = false,
                Message = "Error interno del servidor"
            });
        }
    }

    /// <summary>
    /// Renueva el token de acceso usando un refresh token válido.
    /// </summary>
    /// <param name="refreshTokenDto">Refresh token para renovación</param>
    /// <returns>Nuevo token de acceso y refresh token</returns>
    /// <response code="200">Token renovado exitosamente - Retorna nuevos tokens</response>
    /// <response code="400">Refresh token inválido o expirado</response>
    /// <response code="500">Error interno del servidor</response>
    /// <remarks>
    /// Este endpoint permite renovar la sesión sin requerir login:
    /// 
    /// **Flujo de renovación:**
    /// 1. Valida el refresh token proporcionado
    /// 2. Verifica que no haya expirado
    /// 3. Genera nuevos tokens JWT y refresh token
    /// 4. Actualiza el refresh token en la base de datos
    /// 5. Retorna los nuevos tokens
    /// 
    /// **Características de seguridad:**
    /// - Refresh tokens con expiración configurable (7 días por defecto)
    /// - Renovación automática de sesiones
    /// - Invalidación de tokens anteriores
    /// - Auditoría de renovaciones
    /// 
    /// **Ejemplo de uso:**
    /// ```json
    /// {
    ///   "refreshToken": "refresh_token_here"
    /// }
    /// ```
    /// 
    /// **Respuesta exitosa:**
    /// ```json
    /// {
    ///   "success": true,
    ///   "message": "Token renovado exitosamente",
    ///   "accessToken": "eyJhbGciOiJIUzI1NiIs...",
    ///   "refreshToken": "new_refresh_token_here",
    ///   "expiresAt": "2024-01-15T11:30:00Z"
    /// }
    /// ```
    /// </remarks>
    [HttpPost("refresh")]
    [ProducesResponseType(typeof(AuthResponseDto), 200)]
    [ProducesResponseType(typeof(AuthResponseDto), 400)]
    [ProducesResponseType(typeof(AuthResponseDto), 500)]
    public async Task<ActionResult<AuthResponseDto>> RefreshToken([FromBody] RefreshTokenDto refreshTokenDto)
    {
        try
        {
            _logger.LogInformation("Solicitud de refresh token recibida");

            // Procesar la renovación del token
            var result = await _authService.RefreshTokenAsync(refreshTokenDto);

            if (!result.Success)
            {
                _logger.LogWarning("Refresh token fallido. Razón: {Message}", result.Message);
                return BadRequest(result);
            }

            _logger.LogInformation("Refresh token exitoso para usuario: {Username}", result.User?.Username);
            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error inesperado durante refresh token");
            return StatusCode(500, new AuthResponseDto
            {
                Success = false,
                Message = "Error interno del servidor"
            });
        }
    }

    /// <summary>
    /// Cierra la sesión del usuario actual y revoca los tokens.
    /// </summary>
    /// <param name="refreshTokenDto">Refresh token a revocar</param>
    /// <returns>Confirmación de logout exitoso</returns>
    /// <response code="200">Logout exitoso - Sesión cerrada correctamente</response>
    /// <response code="401">No autorizado - Token inválido</response>
    /// <response code="500">Error interno del servidor</response>
    /// <remarks>
    /// Este endpoint implementa el logout seguro:
    /// 
    /// **Flujo de logout:**
    /// 1. Valida el token de autenticación del usuario
    /// 2. Busca el usuario por refresh token
    /// 3. Revoca el refresh token en la base de datos
    /// 4. Registra la auditoría de logout
    /// 5. Retorna confirmación de logout exitoso
    /// 
    /// **Características de seguridad:**
    /// - Revocación inmediata de tokens
    /// - Auditoría de sesiones cerradas
    /// - Limpieza de datos de sesión
    /// - Prevención de reutilización de tokens
    /// 
    /// **Ejemplo de uso:**
    /// ```json
    /// {
    ///   "refreshToken": "refresh_token_here"
    /// }
    /// ```
    /// 
    /// **Respuesta exitosa:**
    /// ```json
    /// {
    ///   "success": true,
    ///   "message": "Logout exitoso"
    /// }
    /// ```
    /// </remarks>
    [HttpPost("logout")]
    [Authorize]
    [ProducesResponseType(typeof(AuthResponseDto), 200)]
    [ProducesResponseType(401)]
    [ProducesResponseType(typeof(AuthResponseDto), 500)]
    public async Task<ActionResult<AuthResponseDto>> Logout([FromBody] RefreshTokenDto refreshTokenDto)
    {
        try
        {
            var userId = GetCurrentUserId();
            _logger.LogInformation("Solicitud de logout recibida para usuario ID: {UserId}", userId);

            // Procesar el logout
            var result = await _authService.LogoutAsync(refreshTokenDto.RefreshToken);

            if (!result.Success)
            {
                _logger.LogWarning("Logout fallido para usuario ID: {UserId}. Razón: {Message}", 
                    userId, result.Message);
                return StatusCode(500, result);
            }

            _logger.LogInformation("Logout exitoso para usuario ID: {UserId}", userId);
            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error inesperado durante logout");
            return StatusCode(500, new AuthResponseDto
            {
                Success = false,
                Message = "Error interno del servidor"
            });
        }
    }

    /// <summary>
    /// Obtiene el perfil del usuario autenticado.
    /// </summary>
    /// <returns>Información del perfil del usuario</returns>
    /// <response code="200">Perfil obtenido exitosamente - Retorna datos del usuario</response>
    /// <response code="401">No autorizado - Token inválido</response>
    /// <response code="404">Usuario no encontrado</response>
    /// <response code="500">Error interno del servidor</response>
    /// <remarks>
    /// Este endpoint proporciona información del perfil del usuario autenticado:
    /// 
    /// **Información retornada:**
    /// - Datos básicos del usuario (ID, username, email, nombre completo)
    /// - Rol y permisos en el sistema
    /// - Estado de la cuenta (activa/inactiva)
    /// - Información de auditoría (último login, IP)
    /// - Metadatos de creación y modificación
    /// 
    /// **Utilización:**
    /// - Mostrar datos del usuario en la interfaz
    /// - Verificar permisos y roles
    /// - Auditoría de acceso a módulos
    /// - Gestión de sesiones
    /// 
    /// **Respuesta exitosa:**
    /// ```json
    /// {
    ///   "success": true,
    ///   "message": "Perfil obtenido exitosamente",
    ///   "data": {
    ///     "id": 1,
    ///     "username": "admin",
    ///     "email": "admin@cafelab.com",
    ///     "fullName": "Administrador",
    ///     "role": "Admin",
    ///     "isActive": true,
    ///     "lastLoginAt": "2024-01-15T10:00:00Z",
    ///     "lastLoginIp": "192.168.1.100",
    ///     "createdAt": "2024-01-01T00:00:00Z",
    ///     "updatedAt": "2024-01-15T10:00:00Z"
    ///   }
    /// }
    /// ```
    /// </remarks>
    [HttpGet("profile")]
    [Authorize]
    [ProducesResponseType(typeof(UserResponseDto), 200)]
    [ProducesResponseType(401)]
    [ProducesResponseType(typeof(UserResponseDto), 404)]
    [ProducesResponseType(typeof(UserResponseDto), 500)]
    public async Task<ActionResult<UserResponseDto>> GetProfile()
    {
        try
        {
            var userId = GetCurrentUserId();
            _logger.LogInformation("Solicitud de perfil recibida para usuario ID: {UserId}", userId);

            // Obtener el perfil del usuario
            var result = await _authService.GetUserProfileAsync(userId);

            if (!result.Success)
            {
                if (result.Message.Contains("no encontrado"))
                {
                    _logger.LogWarning("Perfil no encontrado para usuario ID: {UserId}", userId);
                    return NotFound(result);
                }

                _logger.LogError("Error al obtener perfil para usuario ID: {UserId}. Razón: {Message}", 
                    userId, result.Message);
                return StatusCode(500, result);
            }

            _logger.LogInformation("Perfil obtenido exitosamente para usuario ID: {UserId}", userId);
            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error inesperado al obtener perfil");
            return StatusCode(500, new UserResponseDto
            {
                Success = false,
                Message = "Error interno del servidor"
            });
        }
    }

    /// <summary>
    /// Obtiene todos los usuarios del sistema (solo para administradores).
    /// </summary>
    /// <returns>Lista de todos los usuarios del sistema</returns>
    /// <response code="200">Usuarios obtenidos exitosamente - Retorna lista completa</response>
    /// <response code="401">No autorizado - Token inválido</response>
    /// <response code="403">Acceso denegado - Se requiere rol Admin</response>
    /// <response code="500">Error interno del servidor</response>
    /// <remarks>
    /// Este endpoint está restringido a administradores y proporciona:
    /// 
    /// **Información retornada:**
    /// - Lista completa de usuarios del sistema
    /// - Información de auditoría de cada usuario
    /// - Estado de cuentas (activas/inactivas/bloqueadas)
    /// - Roles y permisos de cada usuario
    /// - Metadatos de creación y modificación
    /// 
    /// **Utilización:**
    /// - Gestión administrativa de usuarios
    /// - Auditoría de actividad del sistema
    /// - Control de acceso y permisos
    /// - Análisis de uso del sistema
    /// 
    /// **Respuesta exitosa:**
    /// ```json
    /// {
    ///   "success": true,
    ///   "message": "Usuarios obtenidos exitosamente",
    ///   "data": [
    ///     {
    ///       "id": 1,
    ///       "username": "admin",
    ///       "email": "admin@cafelab.com",
    ///       "fullName": "Administrador",
    ///       "role": "Admin",
    ///       "isActive": true,
    ///       "lastLoginAt": "2024-01-15T10:00:00Z",
    ///       "createdAt": "2024-01-01T00:00:00Z"
    ///     },
    ///     {
    ///       "id": 2,
    ///       "username": "usuario1",
    ///       "email": "usuario1@cafelab.com",
    ///       "fullName": "Usuario Uno",
    ///       "role": "User",
    ///       "isActive": true,
    ///       "lastLoginAt": "2024-01-14T15:30:00Z",
    ///       "createdAt": "2024-01-02T00:00:00Z"
    ///     }
    ///   ]
    /// }
    /// ```
    /// </remarks>
    [HttpGet("users")]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(typeof(UsersResponseDto), 200)]
    [ProducesResponseType(401)]
    [ProducesResponseType(403)]
    [ProducesResponseType(typeof(UsersResponseDto), 500)]
    public async Task<ActionResult<UsersResponseDto>> GetAllUsers()
    {
        try
        {
            var userId = GetCurrentUserId();
            _logger.LogInformation("Solicitud de lista de usuarios recibida por usuario ID: {UserId}", userId);

            // Obtener todos los usuarios
            var result = await _authService.GetAllUsersAsync();

            if (!result.Success)
            {
                _logger.LogError("Error al obtener usuarios. Razón: {Message}", result.Message);
                return StatusCode(500, result);
            }

            _logger.LogInformation("Lista de usuarios obtenida exitosamente por usuario ID: {UserId}. Total: {Count}", 
                userId, result.Data.Count);
            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error inesperado al obtener usuarios");
            return StatusCode(500, new UsersResponseDto
            {
                Success = false,
                Message = "Error interno del servidor"
            });
        }
    }

    #region Helper Methods

    /// <summary>
    /// Obtiene la dirección IP del cliente que realiza la petición.
    /// </summary>
    /// <returns>Dirección IP del cliente</returns>
    /// <remarks>
    /// Este método extrae la IP real del cliente considerando:
    /// - Headers de proxy (X-Forwarded-For)
    /// - Conexión directa
    /// - Casos especiales de red
    /// 
    /// Utilizado para auditoría de seguridad y logging.
    /// </remarks>
    private string GetClientIpAddress()
    {
        // Verificar header de proxy (común en entornos con load balancer)
        var forwardedHeader = Request.Headers["X-Forwarded-For"].FirstOrDefault();
        if (!string.IsNullOrEmpty(forwardedHeader))
        {
            // Tomar la primera IP del header (cliente original)
            return forwardedHeader.Split(',')[0].Trim();
        }

        // IP directa de la conexión
        return HttpContext.Connection.RemoteIpAddress?.ToString() ?? "Unknown";
    }

    /// <summary>
    /// Obtiene el ID del usuario actual desde el token JWT.
    /// </summary>
    /// <returns>ID del usuario autenticado</returns>
    /// <exception cref="InvalidOperationException">Se lanza cuando no se puede obtener el ID del usuario</exception>
    /// <remarks>
    /// Este método extrae el ID del usuario del claim NameIdentifier del token JWT.
    /// Se utiliza para identificar al usuario en operaciones que requieren autenticación.
    /// 
    /// El ID se utiliza para:
    /// - Auditoría de acciones
    /// - Control de acceso
    /// - Personalización de respuestas
    /// - Logging de operaciones
    /// </remarks>
    private int GetCurrentUserId()
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (int.TryParse(userIdClaim, out var userId))
        {
            return userId;
        }

        throw new InvalidOperationException("No se pudo obtener el ID del usuario del token JWT");
    }

    #endregion
} 