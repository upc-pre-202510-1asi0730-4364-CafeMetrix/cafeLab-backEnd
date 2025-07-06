using CafeLab.API.Defects.Domain.Model.Aggregates;
using CafeLab.API.Defects.Domain.Model.Queries;

namespace CafeLab.API.Defects.Domain.Services;

/// <summary>
/// Defect query service interface
/// </summary>
public interface IDefectQueryService
{
    Task<Defect?> GetByIdAsync(GetDefectByIdQuery query);
    Task<IEnumerable<Defect>> GetAllByUserIdAsync(GetAllDefectsByUserIdQuery query);
    Task<IEnumerable<Defect>> SearchByNameAsync(SearchDefectsByNameQuery query);
} 