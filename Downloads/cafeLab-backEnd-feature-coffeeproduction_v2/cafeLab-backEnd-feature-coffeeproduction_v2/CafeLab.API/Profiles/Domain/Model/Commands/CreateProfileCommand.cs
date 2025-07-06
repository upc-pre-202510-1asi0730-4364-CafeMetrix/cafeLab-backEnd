namespace CafeLab.API.Profiles.Domain.Model.Commands;

/// <summary>
/// Create Profile Command
/// </summary>
/// <param name="Name">          Nombre del usuario</param>
/// <param name="Email">         Correo del usuario</param>
/// <param name="Password">      Contraseña</param>
/// <param name="Role">          Rol del usuario (barista o dueño)</param>
/// <param name="CafeteriaName"> Nombre de la cafetería (si aplica)</param>
/// <param name="Experience">    Años de experiencia</param>
/// <param name="ProfilePicture">URL de la foto de perfil</param>
/// <param name="PaymentMethod"> Método de pago</param>
/// <param name="IsFirstLogin">  ¿Es el primer inicio de sesión?</param>
/// <param name="Plan">          Nombre del plan suscrito</param>
/// <param name="HasPlan">       ¿Tiene plan asignado?</param>

public record CreateProfileCommand(string Name, string Email, string Password, string Role, string CafeteriaName, string Experience, string ProfilePicture, string PaymentMethod, bool IsFirstLogin, string Plan, bool HasPlan);