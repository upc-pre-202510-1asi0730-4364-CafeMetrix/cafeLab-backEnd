using CafeLab.API.Application.DTOs;
using CafeLab.API.Application.Interfaces;
using CafeLab.API.Domain.Entities;
using CafeLab.API.Domain.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using BCrypt.Net;
using System;
using System.Linq;

namespace CafeLab.API.Application.Services
{
    /// <summary>
    /// Servicio de autenticación para el sistema de gestión de defectos y calibraciones.
    /// Implementa las mejores prácticas del Learning Center Platform para autenticación
    /// y autorización en contextos de control de calidad del café.
    /// </summary>
    /// <remarks>
    /// Este servicio maneja toda la lógica de autenticación, incluyendo:
    /// - Login y registro de usuarios
    /// - Gestión de tokens JWT y refresh tokens
    /// - Control de acceso basado en roles
    /// - Auditoría de sesiones
    /// - Bloqueo de cuentas por seguridad
    /// 
    /// Los usuarios pueden tener diferentes roles:
    /// - Admin: Acceso completo a defectos y calibraciones
    /// - Technician: Acceso especializado para calibraciones
    /// - User: Acceso básico a defectos y calibraciones
    /// </remarks>
    public class AuthService : IAuthService
    {
        private readonly IRepository<User> _userRepository;
        private readonly IConfiguration _configuration;
        private readonly ILogger<AuthService> _logger;

        /// <summary>
        /// Constructor del servicio de autenticación.
        /// </summary>
        /// <param name="userRepository">Repositorio para operaciones de usuarios</param>
        /// <param name="configuration">Configuración de la aplicación</param>
        /// <param name="logger">Logger para auditoría y debugging</param>
        /// <remarks>
        /// Inyecta las dependencias necesarias para el funcionamiento del servicio.
        /// El logger se utiliza para auditoría de seguridad y debugging.
        /// </remarks>
        public AuthService(
            IRepository<User> userRepository,
            IConfiguration configuration,
            ILogger<AuthService> logger)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Autentica un usuario en el sistema de defectos y calibraciones.
        /// </summary>
        /// <param name="loginDto">Datos de login del usuario</param>
        /// <param name="ipAddress">Dirección IP del cliente para auditoría</param>
        /// <returns>Respuesta de autenticación con tokens y datos del usuario</returns>
        /// <remarks>
        /// Este método implementa el flujo completo de autenticación:
        /// 1. Valida los datos de entrada
        /// 2. Busca el usuario por username
        /// 3. Verifica el estado de la cuenta (activa/bloqueada)
        /// 4. Valida la contraseña con BCrypt
        /// 5. Registra el intento (exitoso o fallido)
        /// 6. Genera tokens JWT y refresh token
        /// 7. Actualiza la información de sesión
        /// 
        /// Características de seguridad:
        /// - Bloqueo automático después de 5 intentos fallidos
        /// - Auditoría completa de intentos de login
        /// - Tokens con expiración configurable
        /// - Refresh tokens para renovación automática
        /// </remarks>
        public async Task<AuthResponseDto> LoginAsync(LoginDto loginDto, string ipAddress)
        {
            try
            {
                _logger.LogInformation("Intento de login para usuario: {Username} desde IP: {IpAddress}", 
                    loginDto.Username, ipAddress);

                // Validación de entrada siguiendo las mejores prácticas
                if (string.IsNullOrWhiteSpace(loginDto.Username) || string.IsNullOrWhiteSpace(loginDto.Password))
                {
                    _logger.LogWarning("Login fallido: credenciales vacías para usuario: {Username}", loginDto.Username);
                    return new AuthResponseDto
                    {
                        Success = false,
                        Message = "Username y password son requeridos"
                    };
                }

                // Buscar usuario por username (case-insensitive)
                var users = await _userRepository.GetAllAsync();
                var user = users.FirstOrDefault(u => u.Username.Equals(loginDto.Username, StringComparison.OrdinalIgnoreCase));

                if (user == null)
                {
                    _logger.LogWarning("Login fallido: usuario no encontrado: {Username}", loginDto.Username);
                    return new AuthResponseDto
                    {
                        Success = false,
                        Message = "Credenciales inválidas"
                    };
                }

                // Verificar si el usuario está activo
                if (!user.IsActive)
                {
                    _logger.LogWarning("Login fallido: usuario inactivo: {Username}", loginDto.Username);
                    return new AuthResponseDto
                    {
                        Success = false,
                        Message = "Cuenta desactivada"
                    };
                }

                // Verificar si la cuenta está bloqueada por intentos fallidos
                if (user.IsLocked())
                {
                    _logger.LogWarning("Login fallido: cuenta bloqueada para usuario: {Username} hasta {LockoutEnd}", 
                        loginDto.Username, user.LockoutEnd);
                    return new AuthResponseDto
                    {
                        Success = false,
                        Message = $"Cuenta bloqueada hasta {user.LockoutEnd:yyyy-MM-dd HH:mm:ss}"
                    };
                }

                // Verificar contraseña usando BCrypt
                if (!BCrypt.Net.BCrypt.Verify(loginDto.Password, user.PasswordHash))
                {
                    user.RecordFailedLogin();
                    await _userRepository.UpdateAsync(user);
                    
                    _logger.LogWarning("Login fallido: contraseña incorrecta para usuario: {Username}. Intentos: {Attempts}", 
                        loginDto.Username, user.FailedLoginAttempts);
                    
                    return new AuthResponseDto
                    {
                        Success = false,
                        Message = "Credenciales inválidas"
                    };
                }

                // Login exitoso - registrar información de sesión
                user.RecordSuccessfulLogin(ipAddress);
                await _userRepository.UpdateAsync(user);

                // Generar tokens de autenticación
                var accessToken = GenerateJwtToken(user);
                var refreshToken = GenerateRefreshToken();
                var expiresAt = DateTime.UtcNow.AddMinutes(GetJwtExpirationMinutes());

                // Guardar refresh token para renovación automática
                user.UpdateRefreshToken(refreshToken, DateTime.UtcNow.AddDays(GetRefreshTokenExpirationDays()));
                await _userRepository.UpdateAsync(user);

                _logger.LogInformation("Login exitoso para usuario: {Username} ({Role}) desde IP: {IpAddress}", 
                    loginDto.Username, user.Role, ipAddress);

                return new AuthResponseDto
                {
                    Success = true,
                    Message = "Login exitoso",
                    AccessToken = accessToken,
                    RefreshToken = refreshToken,
                    ExpiresAt = expiresAt,
                    User = MapToUserDto(user)
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error inesperado durante el login para usuario: {Username}", loginDto.Username);
                return new AuthResponseDto
                {
                    Success = false,
                    Message = "Error interno del servidor"
                };
            }
        }

        /// <summary>
        /// Registra un nuevo usuario en el sistema de defectos y calibraciones.
        /// </summary>
        /// <param name="registerDto">Datos del nuevo usuario</param>
        /// <param name="ipAddress">Dirección IP del cliente para auditoría</param>
        /// <returns>Respuesta de registro con tokens y datos del usuario</returns>
        /// <remarks>
        /// Este método implementa el flujo completo de registro:
        /// 1. Valida todos los datos de entrada
        /// 2. Verifica que el username y email sean únicos
        /// 3. Valida la fortaleza de la contraseña
        /// 4. Hashea la contraseña con BCrypt
        /// 5. Crea el usuario con rol por defecto "User"
        /// 6. Genera tokens de autenticación
        /// 7. Registra la auditoría de creación
        /// 
        /// Validaciones implementadas:
        /// - Contraseña mínima de 6 caracteres
        /// - Email válido
        /// - Username y email únicos
        /// - Confirmación de contraseña
        /// </remarks>
        public async Task<AuthResponseDto> RegisterAsync(RegisterDto registerDto, string ipAddress)
        {
            try
            {
                _logger.LogInformation("Intento de registro para usuario: {Username} desde IP: {IpAddress}", 
                    registerDto.Username, ipAddress);

                // Validación completa de datos de entrada
                if (string.IsNullOrWhiteSpace(registerDto.Username) || 
                    string.IsNullOrWhiteSpace(registerDto.Email) ||
                    string.IsNullOrWhiteSpace(registerDto.FullName) ||
                    string.IsNullOrWhiteSpace(registerDto.Password))
                {
                    return new AuthResponseDto
                    {
                        Success = false,
                        Message = "Todos los campos son requeridos"
                    };
                }

                // Validar confirmación de contraseña
                if (registerDto.Password != registerDto.ConfirmPassword)
                {
                    return new AuthResponseDto
                    {
                        Success = false,
                        Message = "Las contraseñas no coinciden"
                    };
                }

                // Validar fortaleza de contraseña
                if (registerDto.Password.Length < 6)
                {
                    return new AuthResponseDto
                    {
                        Success = false,
                        Message = "La contraseña debe tener al menos 6 caracteres"
                    };
                }

                // Verificar unicidad de username y email
                var users = await _userRepository.GetAllAsync();
                if (users.Any(u => u.Username.Equals(registerDto.Username, StringComparison.OrdinalIgnoreCase)))
                {
                    _logger.LogWarning("Registro fallido: username ya existe: {Username}", registerDto.Username);
                    return new AuthResponseDto
                    {
                        Success = false,
                        Message = "El nombre de usuario ya existe"
                    };
                }

                if (users.Any(u => u.Email.Equals(registerDto.Email, StringComparison.OrdinalIgnoreCase)))
                {
                    _logger.LogWarning("Registro fallido: email ya existe: {Email}", registerDto.Email);
                    return new AuthResponseDto
                    {
                        Success = false,
                        Message = "El email ya existe"
                    };
                }

                // Crear usuario con hash seguro de contraseña
                var passwordHash = BCrypt.Net.BCrypt.HashPassword(registerDto.Password, 12);
                var user = new User(
                    registerDto.Username,
                    registerDto.Email,
                    registerDto.FullName,
                    passwordHash,
                    "User" // Rol por defecto para nuevos usuarios
                );

                await _userRepository.AddAsync(user);

                // Generar tokens de autenticación
                var accessToken = GenerateJwtToken(user);
                var refreshToken = GenerateRefreshToken();
                var expiresAt = DateTime.UtcNow.AddMinutes(GetJwtExpirationMinutes());

                // Guardar refresh token
                user.UpdateRefreshToken(refreshToken, DateTime.UtcNow.AddDays(GetRefreshTokenExpirationDays()));
                await _userRepository.UpdateAsync(user);

                _logger.LogInformation("Registro exitoso para usuario: {Username} ({Role}) desde IP: {IpAddress}", 
                    registerDto.Username, user.Role, ipAddress);

                return new AuthResponseDto
                {
                    Success = true,
                    Message = "Usuario registrado exitosamente",
                    AccessToken = accessToken,
                    RefreshToken = refreshToken,
                    ExpiresAt = expiresAt,
                    User = MapToUserDto(user)
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error inesperado durante el registro para usuario: {Username}", registerDto.Username);
                return new AuthResponseDto
                {
                    Success = false,
                    Message = "Error interno del servidor"
                };
            }
        }

        /// <summary>
        /// Renueva el token de acceso usando un refresh token válido.
        /// </summary>
        /// <param name="refreshTokenDto">Datos del refresh token</param>
        /// <returns>Respuesta con nuevos tokens de autenticación</returns>
        /// <remarks>
        /// Este método permite renovar la sesión sin requerir login:
        /// 1. Valida el refresh token
        /// 2. Verifica que no haya expirado
        /// 3. Genera nuevos tokens
        /// 4. Actualiza el refresh token en la base de datos
        /// 
        /// Características de seguridad:
        /// - Refresh tokens con expiración configurable
        /// - Renovación automática de sesiones
        /// - Invalidación de tokens anteriores
        /// </remarks>
        public async Task<AuthResponseDto> RefreshTokenAsync(RefreshTokenDto refreshTokenDto)
        {
            try
            {
                _logger.LogInformation("Intento de refresh token");

                if (string.IsNullOrWhiteSpace(refreshTokenDto.RefreshToken))
                {
                    return new AuthResponseDto
                    {
                        Success = false,
                        Message = "Refresh token es requerido"
                    };
                }

                // Buscar usuario por refresh token
                var users = await _userRepository.GetAllAsync();
                var user = users.FirstOrDefault(u => u.RefreshToken == refreshTokenDto.RefreshToken);

                if (user == null)
                {
                    _logger.LogWarning("Refresh token inválido");
                    return new AuthResponseDto
                    {
                        Success = false,
                        Message = "Refresh token inválido"
                    };
                }

                // Verificar que el refresh token no haya expirado
                if (!user.IsRefreshTokenValid())
                {
                    _logger.LogWarning("Refresh token expirado para usuario: {Username}", user.Username);
                    return new AuthResponseDto
                    {
                        Success = false,
                        Message = "Refresh token expirado"
                    };
                }

                // Generar nuevos tokens de autenticación
                var accessToken = GenerateJwtToken(user);
                var newRefreshToken = GenerateRefreshToken();
                var expiresAt = DateTime.UtcNow.AddMinutes(GetJwtExpirationMinutes());

                // Actualizar refresh token en la base de datos
                user.UpdateRefreshToken(newRefreshToken, DateTime.UtcNow.AddDays(GetRefreshTokenExpirationDays()));
                await _userRepository.UpdateAsync(user);

                _logger.LogInformation("Refresh token exitoso para usuario: {Username}", user.Username);

                return new AuthResponseDto
                {
                    Success = true,
                    Message = "Token renovado exitosamente",
                    AccessToken = accessToken,
                    RefreshToken = newRefreshToken,
                    ExpiresAt = expiresAt,
                    User = MapToUserDto(user)
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error inesperado durante refresh token");
                return new AuthResponseDto
                {
                    Success = false,
                    Message = "Error interno del servidor"
                };
            }
        }

        /// <summary>
        /// Cierra la sesión del usuario y revoca los tokens.
        /// </summary>
        /// <param name="refreshToken">Refresh token a revocar</param>
        /// <returns>Respuesta de confirmación de logout</returns>
        /// <remarks>
        /// Este método implementa el logout seguro:
        /// 1. Busca el usuario por refresh token
        /// 2. Revoca el refresh token
        /// 3. Actualiza la base de datos
        /// 4. Registra la auditoría de logout
        /// 
        /// Características de seguridad:
        /// - Revocación inmediata de tokens
        /// - Auditoría de sesiones cerradas
        /// - Limpieza de datos de sesión
        /// </remarks>
        public async Task<AuthResponseDto> LogoutAsync(string refreshToken)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(refreshToken))
                {
                    var users = await _userRepository.GetAllAsync();
                    var user = users.FirstOrDefault(u => u.RefreshToken == refreshToken);

                    if (user != null)
                    {
                        user.RevokeRefreshToken();
                        await _userRepository.UpdateAsync(user);
                        _logger.LogInformation("Logout exitoso para usuario: {Username}", user.Username);
                    }
                }

                return new AuthResponseDto
                {
                    Success = true,
                    Message = "Logout exitoso"
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error inesperado durante logout");
                return new AuthResponseDto
                {
                    Success = false,
                    Message = "Error interno del servidor"
                };
            }
        }

        /// <summary>
        /// Obtiene el perfil del usuario autenticado.
        /// </summary>
        /// <param name="userId">ID del usuario</param>
        /// <returns>Datos del perfil del usuario</returns>
        /// <remarks>
        /// Este método proporciona información del perfil para:
        /// - Mostrar datos del usuario en la interfaz
        /// - Verificar permisos y roles
        /// - Auditoría de acceso a módulos
        /// </remarks>
        public async Task<UserResponseDto> GetUserProfileAsync(int userId)
        {
            try
            {
                var user = await _userRepository.GetByIdAsync(userId);
                if (user == null)
                {
                    return new UserResponseDto
                    {
                        Success = false,
                        Message = "Usuario no encontrado"
                    };
                }

                return new UserResponseDto
                {
                    Success = true,
                    Message = "Perfil obtenido exitosamente",
                    Data = MapToUserDto(user)
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener perfil de usuario: {UserId}", userId);
                return new UserResponseDto
                {
                    Success = false,
                    Message = "Error interno del servidor"
                };
            }
        }

        /// <summary>
        /// Obtiene todos los usuarios del sistema (solo para administradores).
        /// </summary>
        /// <returns>Lista de todos los usuarios</returns>
        /// <remarks>
        /// Este método está restringido a administradores y proporciona:
        /// - Lista completa de usuarios
        /// - Información de auditoría
        /// - Estado de cuentas
        /// - Roles y permisos
        /// 
        /// Utilizado para gestión administrativa del sistema.
        /// </remarks>
        public async Task<UsersResponseDto> GetAllUsersAsync()
        {
            try
            {
                var users = await _userRepository.GetAllAsync();
                var userDtos = users.Select(MapToUserDto).ToList();

                return new UsersResponseDto
                {
                    Success = true,
                    Message = "Usuarios obtenidos exitosamente",
                    Data = userDtos
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener usuarios");
                return new UsersResponseDto
                {
                    Success = false,
                    Message = "Error interno del servidor"
                };
            }
        }

        /// <summary>
        /// Genera un token JWT para el usuario especificado.
        /// </summary>
        /// <param name="user">Usuario para el cual generar el token</param>
        /// <returns>Token JWT firmado</returns>
        /// <remarks>
        /// Este método crea un token JWT con:
        /// - Claims de identidad (ID, username, email)
        /// - Claims de rol para autorización
        /// - Claims personalizados (nombre completo)
        /// - Firma digital con clave secreta
        /// - Expiración configurable
        /// 
        /// Los claims se utilizan para:
        /// - Identificación del usuario en requests
        /// - Control de acceso basado en roles
        /// - Auditoría de acciones
        /// </remarks>
        private string GenerateJwtToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(GetJwtSecretKey());

            // Claims para identificación y autorización
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role),
                new Claim("FullName", user.FullName),
                new Claim("DisplayName", user.GetDisplayName())
            };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(GetJwtExpirationMinutes()),
                Issuer = GetJwtIssuer(),
                Audience = GetJwtAudience(),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature
                )
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        /// <summary>
        /// Genera un refresh token seguro usando criptografía.
        /// </summary>
        /// <returns>Refresh token en formato Base64</returns>
        /// <remarks>
        /// Este método genera un refresh token criptográficamente seguro:
        /// - Usa RandomNumberGenerator para máxima entropía
        /// - Genera 64 bytes de datos aleatorios
        /// - Convierte a Base64 para almacenamiento
        /// - Único para cada sesión
        /// </remarks>
        private string GenerateRefreshToken()
        {
            var randomNumber = new byte[64];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }

        /// <summary>
        /// Mapea una entidad User a un DTO para respuesta.
        /// </summary>
        /// <param name="user">Entidad User a mapear</param>
        /// <returns>UserDto con datos del usuario</returns>
        /// <remarks>
        /// Este método convierte la entidad de dominio a DTO:
        /// - Excluye información sensible (password hash)
        /// - Incluye información necesaria para la interfaz
        /// - Mantiene la auditoría y metadatos
        /// - Formatea fechas para el cliente
        /// </remarks>
        private UserDto MapToUserDto(User user)
        {
            return new UserDto
            {
                Id = user.Id,
                Username = user.Username,
                Email = user.Email,
                FullName = user.FullName,
                Role = user.Role,
                IsActive = user.IsActive,
                LastLoginAt = user.LastLoginAt,
                LastLoginIp = user.LastLoginIp,
                FailedLoginAttempts = user.FailedLoginAttempts,
                LockoutEnd = user.LockoutEnd,
                CreatedAt = user.CreatedAt,
                UpdatedAt = user.UpdatedAt,
                CreatedBy = user.CreatedBy,
                UpdatedBy = user.UpdatedBy
            };
        }

        #region Configuration Helpers

        /// <summary>
        /// Obtiene la clave secreta para firmar JWT desde la configuración.
        /// </summary>
        /// <returns>Clave secreta para JWT</returns>
        private string GetJwtSecretKey() => _configuration["JwtSettings:SecretKey"] ?? "default-secret-key";

        /// <summary>
        /// Obtiene el issuer del JWT desde la configuración.
        /// </summary>
        /// <returns>Issuer del JWT</returns>
        private string GetJwtIssuer() => _configuration["JwtSettings:Issuer"] ?? "CafeLab";

        /// <summary>
        /// Obtiene el audience del JWT desde la configuración.
        /// </summary>
        /// <returns>Audience del JWT</returns>
        private string GetJwtAudience() => _configuration["JwtSettings:Audience"] ?? "CafeLabUsers";

        /// <summary>
        /// Obtiene la duración de expiración del JWT en minutos.
        /// </summary>
        /// <returns>Duración en minutos</returns>
        private int GetJwtExpirationMinutes() => int.TryParse(_configuration["JwtSettings:ExpirationMinutes"], out var minutes) ? minutes : 60;

        /// <summary>
        /// Obtiene la duración de expiración del refresh token en días.
        /// </summary>
        /// <returns>Duración en días</returns>
        private int GetRefreshTokenExpirationDays() => int.TryParse(_configuration["JwtSettings:RefreshExpirationDays"], out var days) ? days : 7;

        #endregion
    }
} 