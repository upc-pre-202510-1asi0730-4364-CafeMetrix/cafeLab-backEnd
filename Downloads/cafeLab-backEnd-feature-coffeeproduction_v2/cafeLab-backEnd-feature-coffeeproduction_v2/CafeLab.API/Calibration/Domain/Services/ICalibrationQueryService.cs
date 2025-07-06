using CalibrationEntity = CafeLab.API.Calibration.Domain.Model.Aggregates.Calibration;
using CafeLab.API.Calibration.Domain.Model.Queries;

namespace CafeLab.API.Calibration.Domain.Services;

/// <summary>
/// Calibration query service interface
/// </summary>
public interface ICalibrationQueryService
{
    Task<CalibrationEntity?> GetByIdAsync(GetCalibrationByIdQuery query);
    Task<IEnumerable<CalibrationEntity>> GetAllByUserIdAsync(GetAllCalibrationsByUserIdQuery query);
    Task<IEnumerable<CalibrationEntity>> SearchByEquipmentNameAsync(SearchCalibrationsByEquipmentNameQuery query);
} 