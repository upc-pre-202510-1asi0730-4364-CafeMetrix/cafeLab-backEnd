using CafeLab.API.Preparation.Domain.Model.Aggregates;
using CafeLab.API.Preparation.Domain.Repositories;
using CafeLab.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using CafeLab.API.Shared.Infrastructure.Persistence.EFC.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CafeLab.API.Preparation.Infrastructure.Persistence.EFC.Repositories;

/// <summary>
/// Recipe repository implementation using Entity Framework Core
/// </summary>
public class RecipeRepository : BaseRepository<Recipe>, IRecipeRepository
{
    public RecipeRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Recipe>> FindAllAsync()
    {
        return await Context.Set<Recipe>()
            .Include(r => r.Ingredients)
            .ToListAsync();
    }

    public async Task<IEnumerable<Recipe>> FindAllByUserIdAsync(int userId)
    {
        return await Context.Set<Recipe>()
            .Include(r => r.Ingredients)
            .Where(r => r.UserId == userId)
            .ToListAsync();
    }

    public async Task<IEnumerable<Recipe>> FindRecipesWithoutPortfolioByUserIdAsync(int userId)
    {
        return await Context.Set<Recipe>()
            .Include(r => r.Ingredients)
            .Where(r => r.UserId == userId && r.PortfolioId == null)
            .ToListAsync();
    }

    public async Task<IEnumerable<Recipe>> FindRecipesByPortfolioIdAndUserIdAsync(int portfolioId, int userId)
    {
        return await Context.Set<Recipe>()
            .Include(r => r.Ingredients)
            .Where(r => r.UserId == userId && r.PortfolioId == portfolioId)
            .ToListAsync();
    }

    public async Task<IEnumerable<Recipe>> FindRecipesByPortfolioIdAsync(int portfolioId)
    {
        return await Context.Set<Recipe>()
            .Include(r => r.Ingredients)
            .Where(r => r.PortfolioId == portfolioId)
            .ToListAsync();
    }

    public async Task<Recipe?> FindByIdAndUserIdAsync(int id, int userId)
    {
        return await Context.Set<Recipe>()
            .Include(r => r.Ingredients)
            .FirstOrDefaultAsync(r => r.Id == id && r.UserId == userId);
    }

    public async Task<Recipe?> FindByIdWithIngredientsAsync(int id)
    {
        return await Context.Set<Recipe>()
            .Include(r => r.Ingredients)
            .FirstOrDefaultAsync(r => r.Id == id);
    }
} 