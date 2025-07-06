using CafeLab.API.Defects.Domain.Model.Aggregates;
using CafeLab.API.Shared.Domain.Repositories;

namespace CafeLab.API.Defects.Domain.Repositories;

/// <summary>
/// Defect repository interface
/// </summary>
public interface IDefectRepository : IBaseRepository<Defect>
{
    Task<IEnumerable<Defect>> GetAllByUserIdAsync(int userId);
    Task<IEnumerable<Defect>> SearchByNameAsync(string name, int userId);
    Task<Defect?> GetByIdAsync(int id);
    Task UpdateAsync(Defect entity);
} 