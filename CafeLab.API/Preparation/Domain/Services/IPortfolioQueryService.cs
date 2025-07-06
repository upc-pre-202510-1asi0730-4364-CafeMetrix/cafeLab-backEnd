using CafeLab.API.Preparation.Domain.Model.Aggregates;
using CafeLab.API.Preparation.Domain.Model.Queries;

namespace CafeLab.API.Preparation.Domain.Services;

/// <summary>
/// Portfolio query service interface
/// </summary>
public interface IPortfolioQueryService
{
    /// <summary>
    /// Gets all portfolios (without user filtering)
    /// </summary>
    /// <param name="query">Get all portfolios query</param>
    /// <returns>List of all portfolios</returns>
    Task<IEnumerable<Portfolio>> Handle(GetAllPortfoliosQuery query);
    
    /// <summary>
    /// Gets all portfolios by user ID
    /// </summary>
    /// <param name="query">Get all portfolios by user ID query</param>
    /// <returns>List of portfolios</returns>
    Task<IEnumerable<Portfolio>> Handle(GetAllPortfoliosByUserIdQuery query);
    
    /// <summary>
    /// Gets a portfolio by ID
    /// </summary>
    /// <param name="query">Get portfolio by ID query</param>
    /// <returns>Portfolio if found</returns>
    Task<Portfolio?> Handle(GetPortfolioByIdQuery query);
} 