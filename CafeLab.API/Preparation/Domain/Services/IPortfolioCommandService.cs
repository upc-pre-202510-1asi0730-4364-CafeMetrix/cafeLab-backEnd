using CafeLab.API.Preparation.Domain.Model.Aggregates;
using CafeLab.API.Preparation.Domain.Model.Commands;

namespace CafeLab.API.Preparation.Domain.Services;

/// <summary>
/// Portfolio command service interface
/// </summary>
public interface IPortfolioCommandService
{
    /// <summary>
    /// Creates a new portfolio
    /// </summary>
    /// <param name="command">Create portfolio command</param>
    /// <returns>Created portfolio</returns>
    Task<Portfolio> Handle(CreatePortfolioCommand command);
    
    /// <summary>
    /// Updates an existing portfolio
    /// </summary>
    /// <param name="command">Update portfolio command</param>
    /// <returns>Updated portfolio</returns>
    Task<Portfolio?> Handle(UpdatePortfolioCommand command);
    
    /// <summary>
    /// Deletes a portfolio
    /// </summary>
    /// <param name="command">Delete portfolio command</param>
    /// <returns>True if deleted successfully</returns>
    Task<bool> Handle(DeletePortfolioCommand command);
} 