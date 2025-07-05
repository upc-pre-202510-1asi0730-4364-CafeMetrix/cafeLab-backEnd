using CafeLab.API.Preparation.Domain.Model.Aggregates;
using CafeLab.API.Preparation.Domain.Model.ValueObjects;
using CafeLab.API.Preparation.Interfaces.REST.Resources;

namespace CafeLab.API.Preparation.Interfaces.REST.Transform;

/// <summary>
/// Assembler to convert Recipe entity to RecipeResource
/// </summary>
public static class RecipeResourceFromEntityAssembler
{
    /// <summary>
    /// Converts Recipe entity to RecipeResource
    /// </summary>
    /// <param name="entity">The Recipe entity</param>
    /// <returns>RecipeResource</returns>
    public static RecipeResource ToResourceFromEntity(Recipe entity)
    {
        var ingredients = entity.Ingredients.Select(i => 
            new IngredientResource(i.Id, i.RecipeId, i.Name, i.Amount, i.Unit.Value)).ToList();

        return new RecipeResource(
            entity.Id,
            entity.UserId,
            entity.Name,
            entity.ImageUrl,
            ExtractionMethodMapper.ToFrontendValue(entity.ExtractionMethod),
            entity.Ratio.Value,
            entity.CuppingSessionId,
            entity.PortfolioId,
            entity.PreparationTime,
            entity.Steps,
            entity.Tips,
            entity.Cupping,
            entity.GrindSize.Value,
            entity.CreatedAt,
            ingredients
        );
    }
} 