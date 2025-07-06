using CafeLab.API.Profiles.Domain.Model.Aggregates;
using CafeLab.API.Profiles.Interfaces.REST.Resources;

namespace CafeLab.API.Profiles.Interfaces.REST.Transform;

/// <summary>
/// Assembler class to convert Profile entity to ProfileResource 
/// </summary>
public static class ProfileResourceFromEntityAssembler
{
    /// <summary>
    /// Convert Profile entity to ProfileResource 
    /// </summary>
    /// <param name="entity">
    /// <see cref="Profile"/> entity to convert
    /// </param>
    /// <returns>
    /// <see cref="ProfileResource"/> converted from <see cref="Profile"/> entity
    /// </returns>
    public static ProfileResource ToResourceFromEntity(Profile entity)
    {
        return new ProfileResource(entity.Id, entity.Name, entity.EmailAddress, entity.Role, entity.CafeteriaName, entity.Experience, entity.ProfilePicture, entity.PaymentMethod, entity.Plan, entity.HasPlan);
    }
}