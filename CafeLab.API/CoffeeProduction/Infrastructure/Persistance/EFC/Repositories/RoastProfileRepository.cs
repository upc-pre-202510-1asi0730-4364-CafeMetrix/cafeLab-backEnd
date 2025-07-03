using CafeLab.API.CoffeeProduction.Domain.Model.Aggregates;
using CafeLab.API.CoffeeProduction.Domain.Repositories;
using CafeLab.API.Shared.Infrastructure.Persistence.EFC.Repositories;
using CafeLab.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using Microsoft.EntityFrameworkCore;

namespace CafeLab.API.CoffeeProduction.Infrastructure.Persistance.EFC.Repositories;

public class RoastProfileRepository : BaseRepository<RoastProfile>, IRoastProfileRepository
{
    public RoastProfileRepository(AppDbContext context) : base(context) { }

    public async Task<IEnumerable<RoastProfile>> GetAllByUserIdAsync(int userId)
    {
        return await Context.Set<RoastProfile>()
            .Include(rp => rp.CoffeeLot)
            .Where(rp => rp.UserId == userId)
            .ToListAsync();
    }

    public async Task<IEnumerable<RoastProfile>> SearchByNameAsync(string profileName, int userId)
    {
        return await Context.Set<RoastProfile>()
            .Include(rp => rp.CoffeeLot)
            .Where(rp => rp.UserId == userId && rp.ProfileName.Contains(profileName))
            .ToListAsync();
    }

    public async Task<RoastProfile?> GetByIdAsync(int id)
    {
        return await Context.Set<RoastProfile>()
            .Include(rp => rp.CoffeeLot)
            .FirstOrDefaultAsync(rp => rp.Id == id);
    }

    public async Task<bool> ExistsByCoffeeLotIdAsync(int coffeeLotId)
    {
        return await Context.Set<RoastProfile>()
            .AnyAsync(rp => rp.CoffeeLotId == coffeeLotId);
    }
} 