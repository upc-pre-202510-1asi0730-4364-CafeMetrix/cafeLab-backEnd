using CalibrationEntity = CafeLab.API.Calibration.Domain.Model.Aggregates.Calibration;
using CafeLab.API.Shared.Domain.Repositories;

namespace CafeLab.API.Calibration.Domain.Repositories;

/// <summary>
/// Calibration repository interface
/// </summary>
public interface ICalibrationRepository : IBaseRepository<CalibrationEntity>
{
    Task<IEnumerable<CalibrationEntity>> GetAllByUserIdAsync(int userId);
    Task<IEnumerable<CalibrationEntity>> SearchByEquipmentNameAsync(string equipmentName, int userId);
    Task<CalibrationEntity?> GetByIdAsync(int id);
    Task UpdateAsync(CalibrationEntity entity);
} 