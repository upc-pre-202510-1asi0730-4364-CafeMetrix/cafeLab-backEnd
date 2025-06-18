namespace CafeLab.API.Profiles.Domain.Model.Commands;

/// <summary>
/// Update Profile Command
/// </summary>
/// <param name="Id">            Identificador del perfil</param>
/// <param name="Name">          Nombre del usuario</param>
/// <param name="Email">         Correo del usuario</param>
/// <param name="Role">          Rol del usuario (barista o dueño)</param>
/// <param name="CafeteriaName"> Nombre de la cafetería (si aplica)</param>
/// <param name="Experience">    Años de experiencia</param>
/// <param name="ProfilePicture">URL de la foto de perfil</param>
/// <param name="PaymentMethod"> Método de pago</param>
/// <param name="Plan">          Nombre del plan suscrito</param>
/// <param name="HasPlan">       ¿Tiene plan asignado?</param>

public record UpdateProfileCommand(string Id, string Name, string Email, string Role, string CafeteriaName, string Experience, string ProfilePicture, string PaymentMethod, string Plan, bool HasPlan);