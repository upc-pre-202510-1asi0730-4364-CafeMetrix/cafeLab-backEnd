using CafeLab.API.Preparation.Domain.Model.Commands;
using CafeLab.API.Preparation.Interfaces.REST.Resources;

namespace CafeLab.API.Preparation.Interfaces.REST.Transform;

/// <summary>
/// Assembler to convert CreatePortfolioResource to CreatePortfolioCommand
/// </summary>
public static class CreatePortfolioCommandFromResourceAssembler
{
    /// <summary>
    /// Converts CreatePortfolioResource to CreatePortfolioCommand
    /// </summary>
    /// <param name="resource">The CreatePortfolioResource</param>
    /// <param name="userId">The user ID</param>
    /// <returns>CreatePortfolioCommand</returns>
    public static CreatePortfolioCommand ToCommandFromResource(CreatePortfolioResource resource, int userId)
    {
        return new CreatePortfolioCommand(resource.Name, userId);
    }
} 