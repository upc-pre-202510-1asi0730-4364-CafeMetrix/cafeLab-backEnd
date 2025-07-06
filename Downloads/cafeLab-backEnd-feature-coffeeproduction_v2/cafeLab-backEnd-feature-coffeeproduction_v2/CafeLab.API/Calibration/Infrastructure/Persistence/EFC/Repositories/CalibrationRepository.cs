using CalibrationEntity = CafeLab.API.Calibration.Domain.Model.Aggregates.Calibration;
using CafeLab.API.Calibration.Domain.Repositories;
using CafeLab.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using CafeLab.API.Shared.Infrastructure.Persistence.EFC.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CafeLab.API.Calibration.Infrastructure.Persistence.EFC.Repositories;

/// <summary>
/// Calibration repository implementation
/// </summary>
public class CalibrationRepository : BaseRepository<CalibrationEntity>, ICalibrationRepository
{
    public CalibrationRepository(AppDbContext context) : base(context) { }

    public async Task<IEnumerable<CalibrationEntity>> GetAllByUserIdAsync(int userId)
    {
        return await Context.Set<CalibrationEntity>()
            .Where(c => c.UserId == userId && c.IsActive)
            .ToListAsync();
    }

    public async Task<IEnumerable<CalibrationEntity>> SearchByEquipmentNameAsync(string equipmentName, int userId)
    {
        return await Context.Set<CalibrationEntity>()
            .Where(c => c.UserId == userId && c.IsActive && c.EquipmentName.Contains(equipmentName))
            .ToListAsync();
    }

    public async Task<CalibrationEntity?> GetByIdAsync(int id)
    {
        return await Context.Set<CalibrationEntity>()
            .FirstOrDefaultAsync(c => c.Id == id && c.IsActive);
    }

    public async Task UpdateAsync(CalibrationEntity entity)
    {
        base.Update(entity);
        await Task.CompletedTask;
    }
} 