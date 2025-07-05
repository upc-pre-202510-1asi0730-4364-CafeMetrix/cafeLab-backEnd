namespace CafeLab.API.Preparation.Interfaces.REST.Resources;

/// <summary>
/// Ingredient Resource
/// </summary>
public record IngredientResource(
    int Id,
    int RecipeId,
    string Name,
    double Amount,
    string Unit
); 