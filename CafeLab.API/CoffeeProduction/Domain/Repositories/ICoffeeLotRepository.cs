using CafeLab.API.CoffeeProduction.Domain.Model.Aggregates;
using CafeLab.API.Shared.Domain.Repositories;

namespace CafeLab.API.CoffeeProduction.Domain.Repositories;

public interface ICoffeeLotRepository : IBaseRepository<CoffeeLot>
{
    Task<IEnumerable<CoffeeLot>> GetAllByUserIdAsync(int userId);
    Task<IEnumerable<CoffeeLot>> SearchByNameAsync(string lotName, int userId);
    Task<bool> ExistsBySupplierIdAsync(int supplierId);
    Task<CoffeeLot?> GetByIdAsync(int id);
} 