using CafeLab.API.CoffeeProduction.Domain.Model.Aggregates;
using CafeLab.API.CoffeeProduction.Domain.Model.Queries;
using CafeLab.API.CoffeeProduction.Domain.Repositories;
using CafeLab.API.CoffeeProduction.Domain.Services;

namespace CafeLab.API.CoffeeProduction.Application.Internal.QueryServices;

public class CoffeeLotQueryService : ICoffeeLotQueryService
{
    private readonly ICoffeeLotRepository _coffeeLotRepository;

    public CoffeeLotQueryService(ICoffeeLotRepository coffeeLotRepository)
    {
        _coffeeLotRepository = coffeeLotRepository;
    }

    public async Task<IEnumerable<CoffeeLot>> GetAllByUserIdAsync(GetAllCoffeeLotsByUserIdQuery query)
    {
        return await _coffeeLotRepository.GetAllByUserIdAsync(query.UserId);
    }

    public async Task<CoffeeLot?> GetByIdAsync(GetCoffeeLotByIdQuery query)
    {
        return await _coffeeLotRepository.GetByIdAsync(query.Id);
    }

    public async Task<IEnumerable<CoffeeLot>> SearchByNameAsync(SearchCoffeeLotsByNameQuery query)
    {
        return await _coffeeLotRepository.SearchByNameAsync(query.LotName, query.UserId);
    }
} 