using CafeLab.API.IAM.Domain.Model.Aggregates;
using CafeLab.API.IAM.Interfaces.REST.Resources;

namespace CafeLab.API.IAM.Interfaces.REST.Transform;

/// <summary>
/// Assembler to create UserResource from entity
/// </summary>
public static class UserResourceFromEntityAssembler
{
    /// <summary>
    /// Create a UserResource from an entity
    /// </summary>
    /// <param name="entity">The User entity to create the resource from</param>
    /// <returns>The UserResource created from the entity</returns>
    public static UserResource ToResourceFromEntity(User entity)
    {
        return new UserResource(
            entity.Id,
            entity.Username,
            entity.Email.Address,
            entity.Role,
            entity.IsActive,
            entity.LastLoginAt,
            entity.CreatedAt,
            entity.UpdatedAt
        );
    }
} 