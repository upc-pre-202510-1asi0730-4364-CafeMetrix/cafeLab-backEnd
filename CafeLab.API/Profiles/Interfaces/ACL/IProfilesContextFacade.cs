namespace CafeLab.API.Profiles.Interfaces.ACL;

/// <summary>
/// Facade for the profiles context 
/// </summary>
public interface IProfilesContextFacade
{
    /// <summary>
    /// Create a profile 
    /// </summary>
    /// <param name="name">
    /// Name of the profile
    /// </param>
    /// <param name="email">
    /// Email of the profile
    /// </param>
    /// <param name="password">
    /// Password of the profile
    /// </param>
    /// <param name="role">
    /// Role of the profile (barista or owner)
    /// </param>
    /// <param name="cafeteriaName">
    /// Name of the cafeteria (if applicable)
    /// </param>
    /// <param name="experience">
    /// Years of experience of the profile
    /// </param>
    /// <param name="profilePicture">
    /// URL of the profile picture
    /// </param>
    /// <param name="paymentMethod">
    /// Payment method of the profile
    /// </param>
    /// <param name="isFirstLogin">
    /// Indicates if it is the first login of the profile
    /// </param>
    /// <param name="plan">
    /// Name of the subscribed plan
    /// </param>
    /// <param name="hasPlan">
    /// Indicates if the profile has an assigned plan
    /// </param>
    /// <returns>
    /// The id of the created profile if successful, 0 otherwise
    /// </returns>
    Task<string> CreateProfile(string name, 
        string email, 
        string password, 
        string role, 
        string cafeteriaName, 
        string experience,
        string profilePicture,
        string paymentMethod,
        bool isFirstLogin,
        string plan,
        bool hasPlan);
    
    /// <summary>
    /// Fetch the profile id by email     
    /// </summary>
    /// <param name="email">
    /// Email of the profile to fetch
    /// </param>
    /// <returns>
    /// The id of the profile if found, 0 otherwise
    /// </returns>
    Task<string> FetchProfileIdByEmail(string email);
}