using CalibrationEntity = CafeLab.API.Calibration.Domain.Model.Aggregates.Calibration;
using CafeLab.API.Calibration.Domain.Model.Commands;
using CafeLab.API.Calibration.Domain.Repositories;
using CafeLab.API.Calibration.Domain.Services;
using CafeLab.API.Shared.Domain.Repositories;

namespace CafeLab.API.Calibration.Application.Internal.CommandServices;

/// <summary>
/// Calibration command service implementation
/// </summary>
public class CalibrationCommandService : ICalibrationCommandService
{
    private readonly ICalibrationRepository _calibrationRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CalibrationCommandService(ICalibrationRepository calibrationRepository, IUnitOfWork unitOfWork)
    {
        _calibrationRepository = calibrationRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<CalibrationEntity> CreateAsync(CreateCalibrationCommand command)
    {
        var calibration = new CalibrationEntity(command);
        await _calibrationRepository.AddAsync(calibration);
        await _unitOfWork.CompleteAsync();
        return calibration;
    }

    public async Task<CalibrationEntity> UpdateAsync(UpdateCalibrationCommand command)
    {
        var calibration = await _calibrationRepository.FindByIdAsync(command.Id);
        if (calibration == null)
            throw new InvalidOperationException($"Calibration with ID {command.Id} not found");

        calibration.Update(command);
        await _calibrationRepository.UpdateAsync(calibration);
        await _unitOfWork.CompleteAsync();
        return calibration;
    }

    public async Task DeleteAsync(DeleteCalibrationCommand command)
    {
        var calibration = await _calibrationRepository.FindByIdAsync(command.Id);
        if (calibration == null)
            throw new InvalidOperationException($"Calibration with ID {command.Id} not found");

        calibration.Deactivate();
        await _calibrationRepository.UpdateAsync(calibration);
        await _unitOfWork.CompleteAsync();
    }
} 