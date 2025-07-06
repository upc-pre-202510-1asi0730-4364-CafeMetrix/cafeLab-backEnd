using CafeLab.API.Preparation.Domain.Model.Aggregates;
using CafeLab.API.Preparation.Domain.Model.Commands;
using CafeLab.API.Preparation.Domain.Repositories;
using CafeLab.API.Preparation.Domain.Services;
using CafeLab.API.Shared.Domain.Repositories;

namespace CafeLab.API.Preparation.Application.Internal.CommandServices;

/// <summary>
/// Recipe command service implementation
/// </summary>
public class RecipeCommandService : IRecipeCommandService
{
    private readonly IRecipeRepository _recipeRepository;
    private readonly IPortfolioRepository _portfolioRepository;
    private readonly IUnitOfWork _unitOfWork;

    public RecipeCommandService(
        IRecipeRepository recipeRepository,
        IPortfolioRepository portfolioRepository,
        IUnitOfWork unitOfWork)
    {
        _recipeRepository = recipeRepository;
        _portfolioRepository = portfolioRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Recipe> Handle(CreateRecipeCommand command)
    {
        // Validate portfolio if specified
        if (command.PortfolioId.HasValue)
        {
            var portfolio = await _portfolioRepository.FindByIdAndUserIdAsync(command.PortfolioId.Value, command.UserId);
            if (portfolio == null)
                throw new InvalidOperationException("Portfolio not found or does not belong to user");
        }

        // Create the recipe
        var recipe = new Recipe(command);
        
        // Add ingredients
        foreach (var ingredientCommand in command.Ingredients)
        {
            recipe.AddIngredient(ingredientCommand.Name, ingredientCommand.Amount, ingredientCommand.Unit);
        }

        await _recipeRepository.AddAsync(recipe);
        await _unitOfWork.CompleteAsync();
        
        return recipe;
    }

    public async Task<Recipe?> Handle(UpdateRecipeCommand command)
    {
        // Get the existing recipe
        var recipe = await _recipeRepository.FindByIdAndUserIdAsync(command.Id, command.UserId);
        if (recipe == null)
            return null;

        // Validate portfolio if specified
        if (command.PortfolioId.HasValue)
        {
            var portfolio = await _portfolioRepository.FindByIdAndUserIdAsync(command.PortfolioId.Value, command.UserId);
            if (portfolio == null)
                throw new InvalidOperationException("Portfolio not found or does not belong to user");
        }

        // Update recipe properties
        recipe.Update(
            command.Name,
            command.ImageUrl,
            command.ExtractionMethod,
            command.Ratio,
            command.CuppingSessionId,
            command.PortfolioId,
            command.PreparationTime,
            command.Steps,
            command.Tips,
            command.Cupping,
            command.GrindSize
        );

        // Clear existing ingredients and add new ones
        recipe.Ingredients.Clear();
        foreach (var ingredientCommand in command.Ingredients)
        {
            recipe.AddIngredient(ingredientCommand.Name, ingredientCommand.Amount, ingredientCommand.Unit);
        }

        _recipeRepository.Update(recipe);
        await _unitOfWork.CompleteAsync();
        
        return recipe;
    }

    public async Task<bool> Handle(DeleteRecipeCommand command)
    {
        // Get the existing recipe (without userId validation since frontend handles security)
        Console.WriteLine($"Attempting to delete recipe with ID: {command.Id}");
        var recipe = await _recipeRepository.FindByIdWithIngredientsAsync(command.Id);
        if (recipe == null)
        {
            Console.WriteLine($"Recipe not found with ID: {command.Id}");
            return false;
        }
        Console.WriteLine($"Recipe found: {recipe.Name}");

        _recipeRepository.Remove(recipe);
        await _unitOfWork.CompleteAsync();
        
        return true;
    }
} 