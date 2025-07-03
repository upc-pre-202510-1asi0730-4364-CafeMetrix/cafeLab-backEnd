using CafeLab.API.CoffeeProduction.Domain.Model.Aggregates;
using CafeLab.API.CoffeeProduction.Domain.Model.Queries;
using CafeLab.API.CoffeeProduction.Domain.Repositories;
using CafeLab.API.CoffeeProduction.Domain.Services;

namespace CafeLab.API.CoffeeProduction.Application.Internal.QueryServices;

public class RoastProfileQueryService : IRoastProfileQueryService
{
    private readonly IRoastProfileRepository _roastProfileRepository;

    public RoastProfileQueryService(IRoastProfileRepository roastProfileRepository)
    {
        _roastProfileRepository = roastProfileRepository;
    }

    public async Task<IEnumerable<RoastProfile>> GetAllByUserIdAsync(GetAllRoastProfilesByUserIdQuery query)
    {
        return await _roastProfileRepository.GetAllByUserIdAsync(query.UserId);
    }

    public async Task<RoastProfile?> GetByIdAsync(GetRoastProfileByIdQuery query)
    {
        return await _roastProfileRepository.GetByIdAsync(query.Id);
    }

    public async Task<IEnumerable<RoastProfile>> SearchByNameAsync(SearchRoastProfilesByNameQuery query)
    {
        return await _roastProfileRepository.SearchByNameAsync(query.ProfileName, query.UserId);
    }
} 