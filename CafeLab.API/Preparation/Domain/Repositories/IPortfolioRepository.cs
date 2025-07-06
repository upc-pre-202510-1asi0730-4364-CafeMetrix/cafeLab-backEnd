using CafeLab.API.Preparation.Domain.Model.Aggregates;
using CafeLab.API.Shared.Domain.Repositories;

namespace CafeLab.API.Preparation.Domain.Repositories;

/// <summary>
/// Portfolio repository interface
/// </summary>
public interface IPortfolioRepository : IBaseRepository<Portfolio>
{
    /// <summary>
    /// Get all portfolios (without user filtering)
    /// </summary>
    /// <returns>List of all portfolios</returns>
    Task<IEnumerable<Portfolio>> FindAllAsync();
    
    /// <summary>
    /// Get all portfolios by user ID
    /// </summary>
    /// <param name="userId">User ID</param>
    /// <returns>List of portfolios</returns>
    Task<IEnumerable<Portfolio>> FindAllByUserIdAsync(int userId);
    
    /// <summary>
    /// Get portfolio by ID and user ID
    /// </summary>
    /// <param name="id">Portfolio ID</param>
    /// <param name="userId">User ID</param>
    /// <returns>Portfolio if found and belongs to user</returns>
    Task<Portfolio?> FindByIdAndUserIdAsync(int id, int userId);
    
    /// <summary>
    /// Get portfolio with recipes by ID
    /// </summary>
    /// <param name="id">Portfolio ID</param>
    /// <returns>Portfolio with recipes</returns>
    Task<Portfolio?> FindByIdWithRecipesAsync(int id);
    
    /// <summary>
    /// Get portfolio with recipes by ID and user ID
    /// </summary>
    /// <param name="id">Portfolio ID</param>
    /// <param name="userId">User ID</param>
    /// <returns>Portfolio with recipes if found and belongs to user</returns>
    Task<Portfolio?> FindByIdWithRecipesAndUserIdAsync(int id, int userId);
} 