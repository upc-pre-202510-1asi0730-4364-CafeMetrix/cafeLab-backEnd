using CafeLab.API.Preparation.Domain.Model.Aggregates;
using CafeLab.API.Preparation.Interfaces.REST.Resources;

namespace CafeLab.API.Preparation.Interfaces.REST.Transform;

/// <summary>
/// Assembler to convert Portfolio entity to PortfolioResource
/// </summary>
public static class PortfolioResourceFromEntityAssembler
{
    /// <summary>
    /// Converts Portfolio entity to PortfolioResource
    /// </summary>
    /// <param name="entity">The Portfolio entity</param>
    /// <returns>PortfolioResource</returns>
    public static PortfolioResource ToResourceFromEntity(Portfolio entity)
    {
        var recipes = entity.Recipes.Select(RecipeResourceFromEntityAssembler.ToResourceFromEntity).ToList();

        return new PortfolioResource(
            entity.Id,
            entity.Name,
            entity.CreatedAt,
            entity.UserId,
            recipes
        );
    }
} 