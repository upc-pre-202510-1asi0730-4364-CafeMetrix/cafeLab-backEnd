using CafeLab.API.Defects.Domain.Model.Aggregates;
using CafeLab.API.Defects.Domain.Model.Queries;
using CafeLab.API.Defects.Domain.Repositories;
using CafeLab.API.Defects.Domain.Services;

namespace CafeLab.API.Defects.Application.Internal.QueryServices;

/// <summary>
/// Defect query service implementation
/// </summary>
public class DefectQueryService : IDefectQueryService
{
    private readonly IDefectRepository _defectRepository;

    public DefectQueryService(IDefectRepository defectRepository)
    {
        _defectRepository = defectRepository;
    }

    public async Task<Defect?> GetByIdAsync(GetDefectByIdQuery query)
    {
        return await _defectRepository.GetByIdAsync(query.Id);
    }

    public async Task<IEnumerable<Defect>> GetAllByUserIdAsync(GetAllDefectsByUserIdQuery query)
    {
        return await _defectRepository.GetAllByUserIdAsync(query.UserId);
    }

    public async Task<IEnumerable<Defect>> SearchByNameAsync(SearchDefectsByNameQuery query)
    {
        return await _defectRepository.SearchByNameAsync(query.Name, query.UserId);
    }
} 