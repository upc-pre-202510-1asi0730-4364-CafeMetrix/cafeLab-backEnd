using CafeLab.API.Preparation.Domain.Model.Aggregates;
using CafeLab.API.Preparation.Domain.Model.Queries;
using CafeLab.API.Preparation.Domain.Repositories;
using CafeLab.API.Preparation.Domain.Services;

namespace CafeLab.API.Preparation.Application.Internal.QueryServices;

/// <summary>
/// Portfolio query service implementation
/// </summary>
public class PortfolioQueryService : IPortfolioQueryService
{
    private readonly IPortfolioRepository _portfolioRepository;

    public PortfolioQueryService(IPortfolioRepository portfolioRepository)
    {
        _portfolioRepository = portfolioRepository;
    }

    public async Task<IEnumerable<Portfolio>> Handle(GetAllPortfoliosQuery query)
    {
        return await _portfolioRepository.FindAllAsync();
    }

    public async Task<IEnumerable<Portfolio>> Handle(GetAllPortfoliosByUserIdQuery query)
    {
        return await _portfolioRepository.FindAllByUserIdAsync(query.UserId);
    }

    public async Task<Portfolio?> Handle(GetPortfolioByIdQuery query)
    {
        return await _portfolioRepository.FindByIdWithRecipesAsync(query.Id);
    }
} 