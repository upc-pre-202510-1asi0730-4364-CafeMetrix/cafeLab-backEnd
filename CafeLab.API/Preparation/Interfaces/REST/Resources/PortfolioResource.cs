namespace CafeLab.API.Preparation.Interfaces.REST.Resources;

/// <summary>
/// Portfolio Resource
/// </summary>
public record PortfolioResource(
    int Id,
    string Name,
    DateTime CreatedAt,
    int UserId,
    List<RecipeResource> Recipes
); 