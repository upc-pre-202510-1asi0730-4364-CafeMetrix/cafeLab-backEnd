using CafeLab.API.Profiles.Domain.Model.Commands;
using CafeLab.API.Profiles.Domain.Model.Queries;
using CafeLab.API.Profiles.Domain.Model.ValueObjects;
using CafeLab.API.Profiles.Domain.Services;
using CafeLab.API.Profiles.Interfaces.ACL;

namespace CafeLab.API.Profiles.Application.ACL;

/// <summary>
/// Facade for the profiles context 
/// </summary>
/// <param name="profileCommandService">
/// The profile command service
/// </param>
/// <param name="profileQueryService">
/// The profile query service
/// </param>
public class ProfilesContextFacade(IProfileCommandService profileCommandService, IProfileQueryService profileQueryService) : IProfilesContextFacade
{
   // inheritedDoc
   public async Task<string> CreateProfile(string name, string email, string password, string role, string cafeteriaName,
      string experience, string profilePicture, string paymentMethod, bool isFirstLogin, string plan, bool hasPlan)
   {
      var createProfileCommand = new CreateProfileCommand(name, email, password, role, cafeteriaName, experience,
         profilePicture, paymentMethod, isFirstLogin, plan, hasPlan);
      var profile = await profileCommandService.Handle(createProfileCommand);
      return profile != null ? profile.Id : string.Empty;
   }

   // inheritedDoc
   public async Task<string> FetchProfileIdByEmail(string email)
   {
      var getProfileByEmailQuery = new GetProfileByEmailQuery(new EmailAddress(email));
      var profile = await profileQueryService.Handle(getProfileByEmailQuery);
      return profile?.Id ?? string.Empty;
   }
}