namespace CafeLab.API.Profiles.Interfaces.REST.Resources;

/// <summary>
/// Profile resource for REST API 
/// </summary>
/// <param name="Id">
/// The unique identifier of the profile
/// </param>
/// <param name="Name">
/// The name of the profile
/// </param>
/// <param name="Email">
/// The email address of the profile
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
/// <param name="Plan">
/// The name of the subscribed plan
/// </param>
/// <param name="HasPlan">
/// Indicates if the profile has an assigned plan
/// </param>
public record ProfileResource(string Id, string Name, string Email, string Role, string CafeteriaName, string Experience, string ProfilePicture, string PaymentMethod, string Plan, bool HasPlan);