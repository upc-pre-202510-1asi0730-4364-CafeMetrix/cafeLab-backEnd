using CafeLab.API.CoffeeProduction.Domain.Model.Aggregates;
using CafeLab.API.CoffeeProduction.Domain.Repositories;
using CafeLab.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using CafeLab.API.Shared.Infrastructure.Persistence.EFC.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CafeLab.API.CoffeeProduction.Infrastructure.Persistance.EFC.Repositories;

public class SupplierRepository(AppDbContext context)
    : BaseRepository<Supplier>(context), ISupplierRepository
{
    public async Task<IEnumerable<Supplier>> FindAllByUserIdAsync(int userId)
        => await Context.Set<Supplier>().Where(s => s.UserId == userId).ToListAsync();

    public async Task<Supplier?> FindByIdAndUserIdAsync(int id, int userId)
        => await Context.Set<Supplier>().FirstOrDefaultAsync(s => s.Id == id && s.UserId == userId);

    public async Task<IEnumerable<Supplier>> SearchByNameAndUserIdAsync(string name, int userId)
        => await Context.Set<Supplier>().Where(s => s.UserId == userId && s.Name.Contains(name)).ToListAsync();

    public async Task DeleteByIdAndUserIdAsync(int id, int userId)
    {
        var supplier = await FindByIdAndUserIdAsync(id, userId);
        if (supplier != null)
        {
            Context.Set<Supplier>().Remove(supplier);
        }
    }

    public async Task<Supplier?> GetByIdAsync(int id)
    {
        return await Context.Set<Supplier>().FirstOrDefaultAsync(s => s.Id == id);
    }
} 