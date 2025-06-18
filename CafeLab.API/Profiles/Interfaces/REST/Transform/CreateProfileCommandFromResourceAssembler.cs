using CafeLab.API.Profiles.Domain.Model.Commands;
using CafeLab.API.Profiles.Interfaces.REST.Resources;

namespace CafeLab.API.Profiles.Interfaces.REST.Transform;

/// <summary>
/// Assembler to create a CreateProfileCommand command from a resource 
/// </summary>
public static class CreateProfileCommandFromResourceAssembler
{
    /// <summary>
    /// Create a CreateProfileCommand command from a resource 
    /// </summary>
    /// <param name="resource">
    /// The <see cref="CreateProfileResource"/> resource to create the command from
    /// </param>
    /// <returns>
    /// The <see cref="CreateProfileCommand"/> command created from the resource
    /// </returns>
    public static CreateProfileCommand ToCommandFromResource(CreateProfileResource resource)
    {
        return new CreateProfileCommand(
            resource.Name,
            resource.Email,
            resource.Password,
            resource.Role,
            resource.CafeteriaName,
            resource.Experience,
            resource.ProfilePicture,
            resource.PaymentMethod,
            resource.IsFirstLogin,
            resource.Plan,
            resource.HasPlan
        );
    }
}