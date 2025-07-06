using CafeLab.API.IAM.Domain.Model.ValueObjects;
using CafeLab.API.IAM.Domain.Model.Commands;
using CafeLab.API.Shared.Domain.Model.Aggregates;

namespace CafeLab.API.IAM.Domain.Model.Aggregates;

/// <summary>
/// User aggregate root for IAM context
/// 
/// Esta entidad representa un usuario del sistema con capacidades de autenticación y autorización.
/// Es el aggregate root del bounded context IAM (Identity and Access Management).
/// 
/// Responsabilidades:
/// - Gestionar la información de autenticación del usuario
/// - Validar credenciales de acceso
/// - Mantener el estado de la sesión del usuario
/// - Controlar el acceso basado en roles
/// </summary>
public class User : AuditableAggregateRoot
{
    /// <summary>
    /// Identificador único del usuario
    /// </summary>
    public int Id { get; private set; }
    
    /// <summary>
    /// Nombre de usuario único para autenticación
    /// </summary>
    public string Username { get; private set; }
    
    /// <summary>
    /// Dirección de correo electrónico del usuario (Value Object)
    /// </summary>
    public EmailAddress Email { get; private set; }
    
    /// <summary>
    /// Hash de la contraseña del usuario (encriptada con BCrypt)
    /// </summary>
    public string PasswordHash { get; private set; }
    
    /// <summary>
    /// Rol del usuario en el sistema (barista, owner, admin)
    /// </summary>
    public string Role { get; private set; }
    
    /// <summary>
    /// Indica si la cuenta del usuario está activa
    /// </summary>
    public bool IsActive { get; private set; }
    
    /// <summary>
    /// Fecha y hora del último inicio de sesión
    /// </summary>
    public DateTime LastLoginAt { get; private set; }

    /// <summary>
    /// Constructor por defecto requerido por Entity Framework
    /// </summary>
    public User() { }

    /// <summary>
    /// Constructor que crea un nuevo usuario a partir de un comando
    /// 
    /// Este constructor encapsula la lógica de creación de un usuario,
    /// incluyendo el hashing de la contraseña y la inicialización de valores por defecto.
    /// </summary>
    /// <param name="command">Comando con los datos del usuario a crear</param>
    public User(CreateUserCommand command)
    {
        Username = command.Username;
        Email = new EmailAddress(command.Email);
        PasswordHash = BCrypt.Net.BCrypt.HashPassword(command.Password);
        Role = command.Role;
        IsActive = true;
        LastLoginAt = DateTime.UtcNow;
    }

    /// <summary>
    /// Actualiza la fecha del último inicio de sesión
    /// 
    /// Se llama automáticamente cuando el usuario se autentica exitosamente.
    /// </summary>
    public void UpdateLastLogin()
    {
        LastLoginAt = DateTime.UtcNow;
    }

    /// <summary>
    /// Valida si la contraseña proporcionada coincide con el hash almacenado
    /// 
    /// Utiliza BCrypt para verificar la contraseña de forma segura.
    /// </summary>
    /// <param name="password">Contraseña en texto plano a validar</param>
    /// <returns>True si la contraseña es correcta, false en caso contrario</returns>
    public bool ValidatePassword(string password)
    {
        return BCrypt.Net.BCrypt.Verify(password, PasswordHash);
    }

    /// <summary>
    /// Actualiza la contraseña del usuario con un nuevo hash
    /// 
    /// La nueva contraseña se hashea usando BCrypt antes de almacenarla.
    /// </summary>
    /// <param name="newPassword">Nueva contraseña en texto plano</param>
    public void UpdatePassword(string newPassword)
    {
        PasswordHash = BCrypt.Net.BCrypt.HashPassword(newPassword);
    }
} 