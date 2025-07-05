using CafeLab.API.Profiles.Domain.Model.Commands;
using CafeLab.API.Profiles.Interfaces.REST.Resources;

namespace CafeLab.API.Profiles.Interfaces.REST.Transform;

public static class UpdateProfileCommandFromResourceAssembler
{
    public static UpdateProfileCommand ToCommandFromResource(int id, UpdateProfileResource resource)
    {
        return new UpdateProfileCommand(
            id,
            resource.Name,
            resource.Email,
            resource.Role,
            resource.CafeteriaName,
            resource.Experience,
            resource.ProfilePicture,
            resource.PaymentMethod,
            resource.Plan,
            resource.HasPlan
        );
    }
}