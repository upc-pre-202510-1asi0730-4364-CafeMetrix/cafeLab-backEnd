using CafeLab.API.CoffeeProduction.Domain.Model.Aggregates;
using CafeLab.API.CoffeeProduction.Domain.Model.Queries;

namespace CafeLab.API.CoffeeProduction.Domain.Services;

public interface ICoffeeLotQueryService
{
    Task<IEnumerable<CoffeeLot>> GetAllByUserIdAsync(GetAllCoffeeLotsByUserIdQuery query);
    Task<CoffeeLot?> GetByIdAsync(GetCoffeeLotByIdQuery query);
    Task<IEnumerable<CoffeeLot>> SearchByNameAsync(SearchCoffeeLotsByNameQuery query);
} 