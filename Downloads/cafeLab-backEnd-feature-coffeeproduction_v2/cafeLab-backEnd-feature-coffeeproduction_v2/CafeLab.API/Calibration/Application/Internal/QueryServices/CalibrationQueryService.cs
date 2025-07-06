using CalibrationEntity = CafeLab.API.Calibration.Domain.Model.Aggregates.Calibration;
using CafeLab.API.Calibration.Domain.Model.Queries;
using CafeLab.API.Calibration.Domain.Repositories;
using CafeLab.API.Calibration.Domain.Services;

namespace CafeLab.API.Calibration.Application.Internal.QueryServices;

/// <summary>
/// Calibration query service implementation
/// </summary>
public class CalibrationQueryService : ICalibrationQueryService
{
    private readonly ICalibrationRepository _calibrationRepository;

    public CalibrationQueryService(ICalibrationRepository calibrationRepository)
    {
        _calibrationRepository = calibrationRepository;
    }

    public async Task<CalibrationEntity?> GetByIdAsync(GetCalibrationByIdQuery query)
    {
        return await _calibrationRepository.GetByIdAsync(query.Id);
    }

    public async Task<IEnumerable<CalibrationEntity>> GetAllByUserIdAsync(GetAllCalibrationsByUserIdQuery query)
    {
        return await _calibrationRepository.GetAllByUserIdAsync(query.UserId);
    }

    public async Task<IEnumerable<CalibrationEntity>> SearchByEquipmentNameAsync(SearchCalibrationsByEquipmentNameQuery query)
    {
        return await _calibrationRepository.SearchByEquipmentNameAsync(query.EquipmentName, query.UserId);
    }
} 