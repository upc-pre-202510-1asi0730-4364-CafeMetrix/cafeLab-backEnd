using CafeLab.API.CoffeeProduction.Domain.Model.Aggregates;
using CafeLab.API.CoffeeProduction.Domain.Model.Queries;

namespace CafeLab.API.CoffeeProduction.Domain.Services;

public interface IRoastProfileQueryService
{
    Task<IEnumerable<RoastProfile>> GetAllByUserIdAsync(GetAllRoastProfilesByUserIdQuery query);
    Task<RoastProfile?> GetByIdAsync(GetRoastProfileByIdQuery query);
    Task<IEnumerable<RoastProfile>> SearchByNameAsync(SearchRoastProfilesByNameQuery query);
} 