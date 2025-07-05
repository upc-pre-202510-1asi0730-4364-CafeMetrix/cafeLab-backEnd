using CafeLab.API.Profiles.Domain.Model.Aggregates;
using CafeLab.API.Profiles.Domain.Model.Commands;
using CafeLab.API.Profiles.Domain.Repositories;
using CafeLab.API.Profiles.Domain.Services;
using CafeLab.API.Shared.Domain.Repositories;

namespace CafeLab.API.Profiles.Application.Internal.CommandServices;

/// <summary>
/// Profile command service 
/// </summary>
/// <param name="profileRepository">
/// Profile repository
/// </param>
/// <param name="unitOfWork">
/// Unit of work
/// </param>
public class ProfileCommandService(
    IProfileRepository profileRepository, 
    IUnitOfWork unitOfWork) 
    : IProfileCommandService
{
    /// <inheritdoc />
    public async Task<Profile?> Handle(CreateProfileCommand command)
    {
        var profile = new Profile(command);
        try
        {
            await profileRepository.AddAsync(profile);
            await unitOfWork.CompleteAsync();
            return profile;
        } catch (Exception e)
        {
            // Log error
            return null;
        }
    }
    
    public async Task<Profile?> Handle(UpdateProfileCommand command)
    {
        var profile = await profileRepository.FindByIdAsync(command.Id);
        if (profile == null)
            return null;
    
        // Esto sirve para actualizar los campos necesarios del perfil
        profile.Update(
            command.Name,
            command.Email,
            command.Role,
            command.CafeteriaName,
            command.Experience,
            command.ProfilePicture,
            command.PaymentMethod,
            command.Plan,
            command.HasPlan
        );
        await unitOfWork.CompleteAsync();
        return profile;
    }
}