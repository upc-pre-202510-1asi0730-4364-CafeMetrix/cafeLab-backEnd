using CafeLab.API.Preparation.Domain.Model.Aggregates;
using CafeLab.API.Shared.Domain.Repositories;

namespace CafeLab.API.Preparation.Domain.Repositories;

/// <summary>
/// Recipe repository interface
/// </summary>
public interface IRecipeRepository : IBaseRepository<Recipe>
{
    /// <summary>
    /// Get all recipes (without user filtering)
    /// </summary>
    /// <returns>List of all recipes</returns>
    Task<IEnumerable<Recipe>> FindAllAsync();
    
    /// <summary>
    /// Get all recipes by user ID
    /// </summary>
    /// <param name="userId">User ID</param>
    /// <returns>List of recipes</returns>
    Task<IEnumerable<Recipe>> FindAllByUserIdAsync(int userId);
    
    /// <summary>
    /// Get recipes without portfolio by user ID
    /// </summary>
    /// <param name="userId">User ID</param>
    /// <returns>List of recipes without portfolio</returns>
    Task<IEnumerable<Recipe>> FindRecipesWithoutPortfolioByUserIdAsync(int userId);
    
    /// <summary>
    /// Get recipes by portfolio ID and user ID
    /// </summary>
    /// <param name="portfolioId">Portfolio ID</param>
    /// <param name="userId">User ID</param>
    /// <returns>List of recipes in the portfolio</returns>
    Task<IEnumerable<Recipe>> FindRecipesByPortfolioIdAndUserIdAsync(int portfolioId, int userId);
    
    /// <summary>
    /// Get recipes by portfolio ID (without user filtering)
    /// </summary>
    /// <param name="portfolioId">Portfolio ID</param>
    /// <returns>List of recipes in the portfolio</returns>
    Task<IEnumerable<Recipe>> FindRecipesByPortfolioIdAsync(int portfolioId);
    
    /// <summary>
    /// Get recipe by ID and user ID
    /// </summary>
    /// <param name="id">Recipe ID</param>
    /// <param name="userId">User ID</param>
    /// <returns>Recipe if found and belongs to user</returns>
    Task<Recipe?> FindByIdAndUserIdAsync(int id, int userId);
    
    /// <summary>
    /// Get recipe with ingredients by ID
    /// </summary>
    /// <param name="id">Recipe ID</param>
    /// <returns>Recipe with ingredients</returns>
    Task<Recipe?> FindByIdWithIngredientsAsync(int id);
} 