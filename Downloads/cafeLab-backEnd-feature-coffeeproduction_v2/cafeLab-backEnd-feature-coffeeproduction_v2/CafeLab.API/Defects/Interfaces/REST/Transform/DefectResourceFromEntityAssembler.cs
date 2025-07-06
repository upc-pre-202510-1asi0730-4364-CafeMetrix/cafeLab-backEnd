using CafeLab.API.Defects.Domain.Model.Aggregates;
using CafeLab.API.Defects.Interfaces.REST.Resources;

namespace CafeLab.API.Defects.Interfaces.REST.Transform;

/// <summary>
/// Assembler to create DefectResource from entity
/// </summary>
public static class DefectResourceFromEntityAssembler
{
    /// <summary>
    /// Create a DefectResource from an entity
    /// </summary>
    /// <param name="entity">The Defect entity to create the resource from</param>
    /// <returns>The DefectResource created from the entity</returns>
    public static DefectResource ToResourceFromEntity(Defect entity)
    {
        return new DefectResource(
            entity.Id,
            entity.Name,
            entity.Description,
            entity.Category,
            entity.ProbableCauses,
            entity.RecommendedSolutions,
            entity.UserId,
            entity.IsActive,
            entity.CreatedAt,
            entity.UpdatedAt
        );
    }
} 