using CafeLab.API.CoffeeProduction.Domain.Model.Aggregates;
using CafeLab.API.Shared.Domain.Repositories;

namespace CafeLab.API.CoffeeProduction.Domain.Repositories;

public interface ISupplierRepository : IBaseRepository<Supplier>
{
    Task<IEnumerable<Supplier>> FindAllByUserIdAsync(int userId);
    Task<Supplier?> FindByIdAndUserIdAsync(int id, int userId);
    Task<IEnumerable<Supplier>> SearchByNameAndUserIdAsync(string name, int userId);
    Task DeleteByIdAndUserIdAsync(int id, int userId);
    Task<Supplier?> GetByIdAsync(int id);
} 