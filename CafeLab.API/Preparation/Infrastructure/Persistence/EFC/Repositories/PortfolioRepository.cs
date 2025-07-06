using CafeLab.API.Preparation.Domain.Model.Aggregates;
using CafeLab.API.Preparation.Domain.Repositories;
using CafeLab.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using CafeLab.API.Shared.Infrastructure.Persistence.EFC.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CafeLab.API.Preparation.Infrastructure.Persistence.EFC.Repositories;

/// <summary>
/// Portfolio repository implementation using Entity Framework Core
/// </summary>
public class PortfolioRepository : BaseRepository<Portfolio>, IPortfolioRepository
{
    public PortfolioRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Portfolio>> FindAllAsync()
    {
        return await Context.Set<Portfolio>()
            .ToListAsync();
    }

    public async Task<IEnumerable<Portfolio>> FindAllByUserIdAsync(int userId)
    {
        return await Context.Set<Portfolio>()
            .Where(p => p.UserId == userId)
            .ToListAsync();
    }

    public async Task<Portfolio?> FindByIdAndUserIdAsync(int id, int userId)
    {
        return await Context.Set<Portfolio>()
            .FirstOrDefaultAsync(p => p.Id == id && p.UserId == userId);
    }

    public async Task<Portfolio?> FindByIdWithRecipesAsync(int id)
    {
        return await Context.Set<Portfolio>()
            .Include(p => p.Recipes)
                .ThenInclude(r => r.Ingredients)
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<Portfolio?> FindByIdWithRecipesAndUserIdAsync(int id, int userId)
    {
        return await Context.Set<Portfolio>()
            .Include(p => p.Recipes)
                .ThenInclude(r => r.Ingredients)
            .FirstOrDefaultAsync(p => p.Id == id && p.UserId == userId);
    }
} 