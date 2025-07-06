using CafeLab.API.Preparation.Domain.Model.Aggregates;
using CafeLab.API.Preparation.Domain.Model.Commands;
using CafeLab.API.Preparation.Domain.Repositories;
using CafeLab.API.Preparation.Domain.Services;
using CafeLab.API.Shared.Domain.Repositories;

namespace CafeLab.API.Preparation.Application.Internal.CommandServices;

/// <summary>
/// Portfolio command service implementation
/// </summary>
public class PortfolioCommandService : IPortfolioCommandService
{
    private readonly IPortfolioRepository _portfolioRepository;
    private readonly IRecipeRepository _recipeRepository;
    private readonly IUnitOfWork _unitOfWork;

    public PortfolioCommandService(
        IPortfolioRepository portfolioRepository,
        IRecipeRepository recipeRepository,
        IUnitOfWork unitOfWork)
    {
        _portfolioRepository = portfolioRepository;
        _recipeRepository = recipeRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Portfolio> Handle(CreatePortfolioCommand command)
    {
        // Create the portfolio
        var portfolio = new Portfolio(command);

        await _portfolioRepository.AddAsync(portfolio);
        await _unitOfWork.CompleteAsync();
        
        return portfolio;
    }

    public async Task<Portfolio?> Handle(UpdatePortfolioCommand command)
    {
        // Get the existing portfolio
        var portfolio = await _portfolioRepository.FindByIdAndUserIdAsync(command.Id, command.UserId);
        if (portfolio == null)
            return null;

        // Update portfolio properties
        portfolio.Update(command.Name);

        _portfolioRepository.Update(portfolio);
        await _unitOfWork.CompleteAsync();
        
        return portfolio;
    }

    public async Task<bool> Handle(DeletePortfolioCommand command)
    {
        // Get the existing portfolio (without userId validation since frontend handles security)
        Console.WriteLine($"Attempting to delete portfolio with ID: {command.Id}");
        var portfolio = await _portfolioRepository.FindByIdAsync(command.Id);
        if (portfolio == null)
        {
            Console.WriteLine($"Portfolio not found with ID: {command.Id}");
            return false;
        }
        Console.WriteLine($"Portfolio found: {portfolio.Name}");

        // Remove portfolio assignment from all recipes in this portfolio
        var recipes = await _recipeRepository.FindRecipesByPortfolioIdAsync(command.Id);
        foreach (var recipe in recipes)
        {
            recipe.RemoveFromPortfolio();
            _recipeRepository.Update(recipe);
        }

        _portfolioRepository.Remove(portfolio);
        await _unitOfWork.CompleteAsync();
        
        return true;
    }
} 