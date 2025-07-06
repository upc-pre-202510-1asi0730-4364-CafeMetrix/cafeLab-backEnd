using CafeLab.API.CoffeeProduction.Domain.Model.Aggregates;
using CafeLab.API.CoffeeProduction.Domain.Repositories;
using CafeLab.API.Shared.Infrastructure.Persistence.EFC.Repositories;
using CafeLab.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using Microsoft.EntityFrameworkCore;

namespace CafeLab.API.CoffeeProduction.Infrastructure.Persistance.EFC.Repositories;

public class CoffeeLotRepository : BaseRepository<CoffeeLot>, ICoffeeLotRepository
{
    public CoffeeLotRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<CoffeeLot?> GetByIdAsync(int id)
    {
        return await Context.Set<CoffeeLot>()
            .Include(cl => cl.Supplier)
            .FirstOrDefaultAsync(cl => cl.Id == id);
    }

    public async Task<IEnumerable<CoffeeLot>> GetAllByUserIdAsync(int userId)
    {
        return await Context.Set<CoffeeLot>()
            .Include(cl => cl.Supplier)
            .Where(cl => cl.UserId == userId)
            .ToListAsync();
    }

    public async Task<IEnumerable<CoffeeLot>> SearchByNameAsync(string lotName, int userId)
    {
        return await Context.Set<CoffeeLot>()
            .Include(cl => cl.Supplier)
            .Where(cl => cl.UserId == userId && cl.LotName.Contains(lotName))
            .ToListAsync();
    }

    public async Task<bool> ExistsBySupplierIdAsync(int supplierId)
    {
        return await Context.Set<CoffeeLot>()
            .AnyAsync(cl => cl.SupplierId == supplierId);
    }
} 