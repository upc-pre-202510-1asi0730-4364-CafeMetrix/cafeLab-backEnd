using CafeLab.API.Preparation.Domain.Model.Aggregates;
using CafeLab.API.Preparation.Domain.Model.Queries;

namespace CafeLab.API.Preparation.Domain.Services;

/// <summary>
/// Recipe query service interface
/// </summary>
public interface IRecipeQueryService
{
    /// <summary>
    /// Gets all recipes (without user filtering)
    /// </summary>
    /// <param name="query">Get all recipes query</param>
    /// <returns>List of all recipes</returns>
    Task<IEnumerable<Recipe>> Handle(GetAllRecipesQuery query);
    
    /// <summary>
    /// Gets all recipes by user ID
    /// </summary>
    /// <param name="query">Get all recipes by user ID query</param>
    /// <returns>List of recipes</returns>
    Task<IEnumerable<Recipe>> Handle(GetAllRecipesByUserIdQuery query);
    
    /// <summary>
    /// Gets a recipe by ID
    /// </summary>
    /// <param name="query">Get recipe by ID query</param>
    /// <returns>Recipe if found</returns>
    Task<Recipe?> Handle(GetRecipeByIdQuery query);
    
    /// <summary>
    /// Gets recipes without portfolio by user ID
    /// </summary>
    /// <param name="query">Get recipes without portfolio by user ID query</param>
    /// <returns>List of recipes without portfolio</returns>
    Task<IEnumerable<Recipe>> Handle(GetRecipesWithoutPortfolioByUserIdQuery query);
    
    /// <summary>
    /// Gets recipes by portfolio ID
    /// </summary>
    /// <param name="query">Get recipes by portfolio ID query</param>
    /// <returns>List of recipes in the portfolio</returns>
    Task<IEnumerable<Recipe>> Handle(GetRecipesByPortfolioIdQuery query);
} 