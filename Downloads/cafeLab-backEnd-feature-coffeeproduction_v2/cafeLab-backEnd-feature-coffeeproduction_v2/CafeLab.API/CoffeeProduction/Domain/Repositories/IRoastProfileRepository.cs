using CafeLab.API.CoffeeProduction.Domain.Model.Aggregates;
using CafeLab.API.Shared.Domain.Repositories;

namespace CafeLab.API.CoffeeProduction.Domain.Repositories;

public interface IRoastProfileRepository : IBaseRepository<RoastProfile>
{
    Task<IEnumerable<RoastProfile>> GetAllByUserIdAsync(int userId);
    Task<IEnumerable<RoastProfile>> SearchByNameAsync(string profileName, int userId);
    Task<RoastProfile?> GetByIdAsync(int id);
    Task<bool> ExistsByCoffeeLotIdAsync(int coffeeLotId);
} 