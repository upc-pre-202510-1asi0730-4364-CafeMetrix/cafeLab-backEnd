using CafeLab.API.Defects.Domain.Model.Aggregates;
using CafeLab.API.Defects.Domain.Repositories;
using CafeLab.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using CafeLab.API.Shared.Infrastructure.Persistence.EFC.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CafeLab.API.Defects.Infrastructure.Persistence.EFC.Repositories;

/// <summary>
/// Defect repository implementation
/// </summary>
public class DefectRepository : BaseRepository<Defect>, IDefectRepository
{
    public DefectRepository(AppDbContext context) : base(context) { }

    public async Task<IEnumerable<Defect>> GetAllByUserIdAsync(int userId)
    {
        return await Context.Set<Defect>()
            .Where(d => d.UserId == userId && d.IsActive)
            .ToListAsync();
    }

    public async Task<IEnumerable<Defect>> SearchByNameAsync(string name, int userId)
    {
        return await Context.Set<Defect>()
            .Where(d => d.UserId == userId && d.IsActive && d.Name.Contains(name))
            .ToListAsync();
    }

    public async Task<Defect?> GetByIdAsync(int id)
    {
        return await Context.Set<Defect>()
            .FirstOrDefaultAsync(d => d.Id == id && d.IsActive);
    }

    public async Task UpdateAsync(Defect entity)
    {
        base.Update(entity);
        await Task.CompletedTask;
    }
} 