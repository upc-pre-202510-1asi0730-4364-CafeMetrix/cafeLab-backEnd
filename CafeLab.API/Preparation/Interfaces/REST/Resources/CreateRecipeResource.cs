namespace CafeLab.API.Preparation.Interfaces.REST.Resources;

/// <summary>
/// Create Recipe Resource
/// </summary>
public record CreateRecipeResource(
    string Name,
    string ImageUrl,
    string ExtractionMethod,
    string Ratio,
    int? CuppingSessionId,
    int? PortfolioId,
    int PreparationTime,
    string Steps,
    string Tips,
    string Cupping,
    string? GrindSize,
    int? UserId,
    List<CreateIngredientResource> Ingredients
); 