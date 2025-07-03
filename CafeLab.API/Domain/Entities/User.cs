using System;
using System.ComponentModel.DataAnnotations;
using CafeLab.API.Shared.Entities;

namespace CafeLab.API.Domain.Entities;

/// <summary>
/// Representa un usuario del sistema de gestión de defectos y calibraciones.
/// Esta entidad es el agregado raíz para la gestión de usuarios en el contexto
/// de defectos y calibraciones del café, siguiendo las mejores prácticas del
/// Learning Center Platform.
/// </summary>
/// <remarks>
/// La entidad User maneja la autenticación y autorización para los módulos
/// de defectos y calibraciones. Incluye auditoría completa, gestión de
/// sesiones y control de acceso basado en roles.
/// </remarks>
public class User : BaseEntity
{
    /// <summary>
    /// Nombre de usuario único para autenticación en el sistema.
    /// Utilizado para el login y identificación del usuario.
    /// </summary>
    /// <remarks>
    /// Debe ser único en el sistema y no puede estar vacío.
    /// Máximo 50 caracteres para evitar problemas de rendimiento.
    /// </remarks>
    [Required(ErrorMessage = "El nombre de usuario es requerido")]
    [StringLength(50, ErrorMessage = "El nombre de usuario no puede exceder 50 caracteres")]
    public string Username { get; set; } = string.Empty;

    /// <summary>
    /// Email único del usuario para comunicación y recuperación de cuenta.
    /// </summary>
    /// <remarks>
    /// Debe ser un email válido y único en el sistema.
    /// Utilizado para notificaciones sobre defectos y calibraciones.
    /// </remarks>
    [Required(ErrorMessage = "El email es requerido")]
    [EmailAddress(ErrorMessage = "El formato del email no es válido")]
    [StringLength(100, ErrorMessage = "El email no puede exceder 100 caracteres")]
    public string Email { get; set; } = string.Empty;

    /// <summary>
    /// Nombre completo del usuario para identificación en reportes.
    /// </summary>
    /// <remarks>
    /// Utilizado en auditoría de defectos y calibraciones para
    /// identificar quién realizó cada acción.
    /// </remarks>
    [Required(ErrorMessage = "El nombre completo es requerido")]
    [StringLength(100, ErrorMessage = "El nombre completo no puede exceder 100 caracteres")]
    public string FullName { get; set; } = string.Empty;

    /// <summary>
    /// Hash de la contraseña utilizando BCrypt para máxima seguridad.
    /// </summary>
    /// <remarks>
    /// La contraseña se hashea con BCrypt usando un factor de costo de 12.
    /// Nunca se almacena en texto plano por seguridad.
    /// </remarks>
    [Required(ErrorMessage = "La contraseña es requerida")]
    public string PasswordHash { get; set; } = string.Empty;

    /// <summary>
    /// Rol del usuario en el sistema de defectos y calibraciones.
    /// </summary>
    /// <remarks>
    /// Roles disponibles:
    /// - "Admin": Acceso completo a todos los módulos
    /// - "User": Acceso básico a defectos y calibraciones
    /// - "Technician": Acceso especializado para calibraciones
    /// </remarks>
    [Required(ErrorMessage = "El rol es requerido")]
    [StringLength(20, ErrorMessage = "El rol no puede exceder 20 caracteres")]
    public string Role { get; set; } = "User";

    /// <summary>
    /// Indica si el usuario está activo y puede acceder al sistema.
    /// </summary>
    /// <remarks>
    /// Los usuarios inactivos no pueden crear o modificar defectos
    /// ni realizar calibraciones. Útil para desactivar cuentas
    /// temporalmente sin eliminarlas.
    /// </remarks>
    public bool IsActive { get; set; } = true;

    /// <summary>
    /// Número de intentos fallidos de login consecutivos.
    /// </summary>
    /// <remarks>
    /// Se incrementa con cada intento fallido y se resetea en login exitoso.
    /// Después de 5 intentos fallidos, la cuenta se bloquea por 30 minutos.
    /// </remarks>
    public int FailedLoginAttempts { get; set; } = 0;

    /// <summary>
    /// Fecha hasta la cual la cuenta está bloqueada por intentos fallidos.
    /// </summary>
    /// <remarks>
    /// Null cuando la cuenta no está bloqueada.
    /// Se establece automáticamente después de 5 intentos fallidos.
    /// </remarks>
    public DateTime? LockoutEnd { get; set; }

    /// <summary>
    /// Fecha del último login exitoso del usuario.
    /// </summary>
    /// <remarks>
    /// Utilizado para auditoría y análisis de actividad del usuario
    /// en el sistema de defectos y calibraciones.
    /// </remarks>
    public DateTime? LastLoginAt { get; set; }

    /// <summary>
    /// Dirección IP del último login exitoso.
    /// </summary>
    /// <remarks>
    /// Utilizado para auditoría de seguridad y detección de
    /// accesos no autorizados al sistema.
    /// </remarks>
    [StringLength(45, ErrorMessage = "La IP no puede exceder 45 caracteres")]
    public string? LastLoginIp { get; set; }

    /// <summary>
    /// Token de refresh para renovación automática de sesiones.
    /// </summary>
    /// <remarks>
    /// Permite renovar el token de acceso sin requerir login.
    /// Se revoca automáticamente en logout o expiración.
    /// </remarks>
    [StringLength(500, ErrorMessage = "El refresh token no puede exceder 500 caracteres")]
    public string? RefreshToken { get; set; }

    /// <summary>
    /// Fecha de expiración del refresh token.
    /// </summary>
    /// <remarks>
    /// Por defecto 7 días desde la creación.
    /// Después de esta fecha, el usuario debe hacer login nuevamente.
    /// </remarks>
    public DateTime? RefreshTokenExpiry { get; set; }

    /// <summary>
    /// Usuario que creó este registro en el sistema.
    /// </summary>
    /// <remarks>
    /// Utilizado para auditoría de creación de usuarios.
    /// Null para usuarios creados por el sistema o migraciones.
    /// </remarks>
    [StringLength(100, ErrorMessage = "El nombre del creador no puede exceder 100 caracteres")]
    public string? CreatedBy { get; set; }

    /// <summary>
    /// Usuario que modificó por última vez este registro.
    /// </summary>
    /// <remarks>
    /// Utilizado para auditoría de modificaciones de usuarios.
    /// Se actualiza automáticamente en cada modificación.
    /// </remarks>
    [StringLength(100, ErrorMessage = "El nombre del modificador no puede exceder 100 caracteres")]
    public string? UpdatedBy { get; set; }

    /// <summary>
    /// Constructor por defecto requerido por Entity Framework.
    /// </summary>
    /// <remarks>
    /// Inicializa el usuario con valores por defecto seguros.
    /// Establece la fecha de creación y activa la cuenta.
    /// </remarks>
    public User()
    {
        CreatedAt = DateTime.UtcNow;
        IsActive = true;
        Role = "User";
    }

    /// <summary>
    /// Constructor con parámetros para crear un usuario con datos específicos.
    /// </summary>
    /// <param name="username">Nombre de usuario único</param>
    /// <param name="email">Email único del usuario</param>
    /// <param name="fullName">Nombre completo del usuario</param>
    /// <param name="passwordHash">Hash de la contraseña</param>
    /// <param name="role">Rol del usuario (por defecto "User")</param>
    /// <exception cref="ArgumentException">Se lanza cuando los parámetros son inválidos</exception>
    /// <remarks>
    /// Valida todos los parámetros antes de crear el usuario.
    /// Establece automáticamente la fecha de creación y activa la cuenta.
    /// </remarks>
    public User(string username, string email, string fullName, string passwordHash, string role = "User")
    {
        // Validaciones de entrada siguiendo las mejores prácticas
        if (string.IsNullOrWhiteSpace(username))
            throw new ArgumentException("El nombre de usuario no puede estar vacío.", nameof(username));
        if (string.IsNullOrWhiteSpace(email))
            throw new ArgumentException("El email no puede estar vacío.", nameof(email));
        if (string.IsNullOrWhiteSpace(fullName))
            throw new ArgumentException("El nombre completo no puede estar vacío.", nameof(fullName));
        if (string.IsNullOrWhiteSpace(passwordHash))
            throw new ArgumentException("El hash de la contraseña no puede estar vacío.", nameof(passwordHash));

        Username = username;
        Email = email;
        FullName = fullName;
        PasswordHash = passwordHash;
        Role = role;
        CreatedAt = DateTime.UtcNow;
        IsActive = true;
        FailedLoginAttempts = 0;
    }

    /// <summary>
    /// Actualiza los datos básicos del usuario.
    /// </summary>
    /// <param name="email">Nuevo email del usuario</param>
    /// <param name="fullName">Nuevo nombre completo</param>
    /// <param name="updatedBy">Usuario que realiza la actualización</param>
    /// <remarks>
    /// Solo actualiza los campos proporcionados que no estén vacíos.
    /// Registra automáticamente la fecha de actualización y el usuario modificador.
    /// </remarks>
    public void Update(string email, string fullName, string updatedBy)
    {
        if (!string.IsNullOrWhiteSpace(email))
            Email = email;
        if (!string.IsNullOrWhiteSpace(fullName))
            FullName = fullName;

        UpdatedBy = updatedBy;
        UpdatedAt = DateTime.UtcNow;
    }

    /// <summary>
    /// Actualiza la contraseña del usuario con un nuevo hash.
    /// </summary>
    /// <param name="newPasswordHash">Nuevo hash de la contraseña</param>
    /// <param name="updatedBy">Usuario que realiza la actualización</param>
    /// <exception cref="ArgumentException">Se lanza cuando el hash está vacío</exception>
    /// <remarks>
    /// Utilizado para cambio de contraseña y reset de contraseña.
    /// Registra la auditoría de la modificación.
    /// </remarks>
    public void UpdatePassword(string newPasswordHash, string updatedBy)
    {
        if (string.IsNullOrWhiteSpace(newPasswordHash))
            throw new ArgumentException("El nuevo hash de contraseña no puede estar vacío.", nameof(newPasswordHash));

        PasswordHash = newPasswordHash;
        UpdatedBy = updatedBy;
        UpdatedAt = DateTime.UtcNow;
    }

    /// <summary>
    /// Registra un intento de login exitoso del usuario.
    /// </summary>
    /// <param name="ipAddress">Dirección IP del login</param>
    /// <remarks>
    /// Resetea los intentos fallidos y desbloquea la cuenta si estaba bloqueada.
    /// Actualiza la fecha del último login y la IP.
    /// </remarks>
    public void RecordSuccessfulLogin(string ipAddress)
    {
        LastLoginAt = DateTime.UtcNow;
        LastLoginIp = ipAddress;
        FailedLoginAttempts = 0;
        LockoutEnd = null;
        UpdatedAt = DateTime.UtcNow;
    }

    /// <summary>
    /// Registra un intento de login fallido del usuario.
    /// </summary>
    /// <remarks>
    /// Incrementa el contador de intentos fallidos.
    /// Bloquea la cuenta automáticamente después de 5 intentos fallidos
    /// por un período de 30 minutos.
    /// </remarks>
    public void RecordFailedLogin()
    {
        FailedLoginAttempts++;
        
        // Bloquear cuenta después de 5 intentos fallidos por 30 minutos
        if (FailedLoginAttempts >= 5)
        {
            LockoutEnd = DateTime.UtcNow.AddMinutes(30);
        }
        
        UpdatedAt = DateTime.UtcNow;
    }

    /// <summary>
    /// Verifica si la cuenta del usuario está bloqueada.
    /// </summary>
    /// <returns>True si la cuenta está bloqueada, False en caso contrario</returns>
    /// <remarks>
    /// Una cuenta está bloqueada si tiene una fecha de bloqueo futura.
    /// Se utiliza para prevenir ataques de fuerza bruta.
    /// </remarks>
    public bool IsLocked()
    {
        return LockoutEnd.HasValue && LockoutEnd.Value > DateTime.UtcNow;
    }

    /// <summary>
    /// Desbloquea la cuenta del usuario manualmente.
    /// </summary>
    /// <remarks>
    /// Resetea los intentos fallidos y elimina la fecha de bloqueo.
    /// Utilizado por administradores para desbloquear cuentas.
    /// </remarks>
    public void Unlock()
    {
        FailedLoginAttempts = 0;
        LockoutEnd = null;
        UpdatedAt = DateTime.UtcNow;
    }

    /// <summary>
    /// Desactiva el usuario en el sistema.
    /// </summary>
    /// <param name="updatedBy">Usuario que realiza la desactivación</param>
    /// <remarks>
    /// Un usuario desactivado no puede acceder al sistema ni realizar
    /// operaciones en defectos o calibraciones.
    /// </remarks>
    public void Deactivate(string updatedBy)
    {
        IsActive = false;
        UpdatedBy = updatedBy;
        UpdatedAt = DateTime.UtcNow;
    }

    /// <summary>
    /// Activa el usuario en el sistema.
    /// </summary>
    /// <param name="updatedBy">Usuario que realiza la activación</param>
    /// <remarks>
    /// Permite que el usuario vuelva a acceder al sistema y realizar
    /// operaciones en defectos y calibraciones.
    /// </remarks>
    public void Activate(string updatedBy)
    {
        IsActive = true;
        UpdatedBy = updatedBy;
        UpdatedAt = DateTime.UtcNow;
    }

    /// <summary>
    /// Actualiza el refresh token del usuario.
    /// </summary>
    /// <param name="refreshToken">Nuevo refresh token</param>
    /// <param name="expiry">Fecha de expiración del token</param>
    /// <remarks>
    /// Utilizado para renovar sesiones automáticamente.
    /// El token anterior se invalida al crear uno nuevo.
    /// </remarks>
    public void UpdateRefreshToken(string refreshToken, DateTime expiry)
    {
        RefreshToken = refreshToken;
        RefreshTokenExpiry = expiry;
        UpdatedAt = DateTime.UtcNow;
    }

    /// <summary>
    /// Revoca el refresh token del usuario.
    /// </summary>
    /// <remarks>
    /// Utilizado en logout para invalidar la sesión.
    /// El usuario deberá hacer login nuevamente para obtener un nuevo token.
    /// </remarks>
    public void RevokeRefreshToken()
    {
        RefreshToken = null;
        RefreshTokenExpiry = null;
        UpdatedAt = DateTime.UtcNow;
    }

    /// <summary>
    /// Verifica si el refresh token del usuario es válido.
    /// </summary>
    /// <returns>True si el token es válido, False en caso contrario</returns>
    /// <remarks>
    /// Un token es válido si existe, no está vacío y no ha expirado.
    /// Se utiliza para determinar si se puede renovar la sesión automáticamente.
    /// </remarks>
    public bool IsRefreshTokenValid()
    {
        return !string.IsNullOrEmpty(RefreshToken) && 
               RefreshTokenExpiry.HasValue && 
               RefreshTokenExpiry.Value > DateTime.UtcNow;
    }

    /// <summary>
    /// Verifica si el usuario tiene un rol específico.
    /// </summary>
    /// <param name="role">Rol a verificar</param>
    /// <returns>True si el usuario tiene el rol, False en caso contrario</returns>
    /// <remarks>
    /// La comparación es case-insensitive para mayor flexibilidad.
    /// Utilizado para control de acceso en defectos y calibraciones.
    /// </remarks>
    public bool HasRole(string role)
    {
        return Role.Equals(role, StringComparison.OrdinalIgnoreCase);
    }

    /// <summary>
    /// Verifica si el usuario es administrador del sistema.
    /// </summary>
    /// <returns>True si el usuario es admin, False en caso contrario</returns>
    /// <remarks>
    /// Los administradores tienen acceso completo a todos los módulos
    /// de defectos y calibraciones, incluyendo gestión de usuarios.
    /// </remarks>
    public bool IsAdmin()
    {
        return HasRole("Admin");
    }

    /// <summary>
    /// Verifica si el usuario es técnico especializado.
    /// </summary>
    /// <returns>True si el usuario es técnico, False en caso contrario</returns>
    /// <remarks>
    /// Los técnicos tienen acceso especializado para calibraciones
    /// y pueden realizar operaciones técnicas avanzadas.
    /// </remarks>
    public bool IsTechnician()
    {
        return HasRole("Technician");
    }

    /// <summary>
    /// Obtiene el nombre para mostrar del usuario.
    /// </summary>
    /// <returns>Nombre completo o username como fallback</returns>
    /// <remarks>
    /// Utilizado en interfaces de usuario para mostrar la identidad
    /// del usuario de forma amigable.
    /// </remarks>
    public string GetDisplayName()
    {
        return !string.IsNullOrWhiteSpace(FullName) ? FullName : Username;
    }
} 