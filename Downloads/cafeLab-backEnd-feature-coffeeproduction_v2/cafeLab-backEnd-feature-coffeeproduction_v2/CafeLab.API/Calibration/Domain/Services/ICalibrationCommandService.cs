using CalibrationEntity = CafeLab.API.Calibration.Domain.Model.Aggregates.Calibration;
using CafeLab.API.Calibration.Domain.Model.Commands;

namespace CafeLab.API.Calibration.Domain.Services;

/// <summary>
/// Calibration command service interface
/// </summary>
public interface ICalibrationCommandService
{
    Task<CalibrationEntity> CreateAsync(CreateCalibrationCommand command);
    Task<CalibrationEntity> UpdateAsync(UpdateCalibrationCommand command);
    Task DeleteAsync(DeleteCalibrationCommand command);
} 