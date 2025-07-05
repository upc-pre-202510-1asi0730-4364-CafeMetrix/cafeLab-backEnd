namespace CafeLab.API.Preparation.Interfaces.REST.Resources;

/// <summary>
/// Create Ingredient Resource
/// </summary>
public record CreateIngredientResource(
    string Name,
    double Amount,
    string Unit
); 