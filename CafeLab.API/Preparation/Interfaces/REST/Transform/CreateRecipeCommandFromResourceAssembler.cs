using CafeLab.API.Preparation.Domain.Model.Commands;
using CafeLab.API.Preparation.Domain.Model.ValueObjects;
using CafeLab.API.Preparation.Interfaces.REST.Resources;

namespace CafeLab.API.Preparation.Interfaces.REST.Transform;

/// <summary>
/// Assembler to convert CreateRecipeResource to CreateRecipeCommand
/// </summary>
public static class CreateRecipeCommandFromResourceAssembler
{
    /// <summary>
    /// Converts CreateRecipeResource to CreateRecipeCommand
    /// </summary>
    /// <param name="resource">The CreateRecipeResource</param>
    /// <param name="userId">The user ID</param>
    /// <returns>CreateRecipeCommand</returns>
    public static CreateRecipeCommand ToCommandFromResource(CreateRecipeResource resource, int userId)
    {
        var ingredients = resource.Ingredients.Select(i => 
            new CreateIngredientCommand(i.Name, i.Amount, i.Unit)).ToList();

        return new CreateRecipeCommand(
            resource.Name,
            resource.ImageUrl,
            ExtractionMethodMapper.FromFrontendValue(resource.ExtractionMethod),
            resource.Ratio,
            resource.CuppingSessionId,
            resource.PortfolioId,
            resource.PreparationTime,
            resource.Steps,
            resource.Tips,
            resource.Cupping,
            resource.GrindSize,
            userId,
            ingredients
        );
    }
} 