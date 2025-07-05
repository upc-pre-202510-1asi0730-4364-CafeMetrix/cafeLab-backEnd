namespace CafeLab.API.Preparation.Interfaces.REST.Resources;

/// <summary>
/// Recipe Resource
/// </summary>
public record RecipeResource(
    int Id,
    int UserId,
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
    string GrindSize,
    DateTime CreatedAt,
    List<IngredientResource> Ingredients
); 