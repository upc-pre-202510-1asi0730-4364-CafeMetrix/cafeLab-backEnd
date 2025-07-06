namespace CafeLab.API.Profiles.Interfaces.REST.Resources;

/// <summary>
/// Resource for creating a new profile 
/// </summary>
/// <param name="Name">
/// The name of the profile
/// </param>
/// <param name="Email">
/// The email of the profile
/// </param>
/// <param name="Password">
/// The password of the profile
/// </param>
/// <param name="Role">
/// The role of the profile (barista or owner)
/// </param>
/// <param name="CafeteriaName">
/// The name of the cafeteria (if applicable)
/// </param>
/// <param name="Experience">
/// The years of experience of the profile
/// </param>
/// <param name="ProfilePicture">
/// The URL of the profile picture
/// </param>
/// <param name="PaymentMethod">
/// The payment method of the profile
/// </param>
/// <param name="IsFirstLogin">
/// Indicates if it is the first login of the profile
/// </param>
/// <param name="Plan">
/// The name of the subscribed plan
/// </param>
/// <param name="HasPlan">
/// Indicates if the profile has an assigned plan
/// </param>
public record CreateProfileResource(
    string Name,
    string Email,
    string Password,
    string Role,
    string CafeteriaName,
    string Experience,
    string ProfilePicture,
    string PaymentMethod,
    bool IsFirstLogin,
    string Plan,
    bool HasPlan);