using CafeLab.API.Preparation.Domain.Model.Aggregates;
using CafeLab.API.Preparation.Domain.Model.Queries;
using CafeLab.API.Preparation.Domain.Repositories;
using CafeLab.API.Preparation.Domain.Services;

namespace CafeLab.API.Preparation.Application.Internal.QueryServices;

/// <summary>
/// Recipe query service implementation
/// </summary>
public class RecipeQueryService : IRecipeQueryService
{
    private readonly IRecipeRepository _recipeRepository;

    public RecipeQueryService(IRecipeRepository recipeRepository)
    {
        _recipeRepository = recipeRepository;
    }

    public async Task<IEnumerable<Recipe>> Handle(GetAllRecipesQuery query)
    {
        return await _recipeRepository.FindAllAsync();
    }

    public async Task<IEnumerable<Recipe>> Handle(GetAllRecipesByUserIdQuery query)
    {
        return await _recipeRepository.FindAllByUserIdAsync(query.UserId);
    }

    public async Task<Recipe?> Handle(GetRecipeByIdQuery query)
    {
        return await _recipeRepository.FindByIdWithIngredientsAsync(query.Id);
    }

    public async Task<IEnumerable<Recipe>> Handle(GetRecipesWithoutPortfolioByUserIdQuery query)
    {
        return await _recipeRepository.FindRecipesWithoutPortfolioByUserIdAsync(query.UserId);
    }

    public async Task<IEnumerable<Recipe>> Handle(GetRecipesByPortfolioIdQuery query)
    {
        return await _recipeRepository.FindRecipesByPortfolioIdAndUserIdAsync(query.PortfolioId, query.UserId);
    }
} 