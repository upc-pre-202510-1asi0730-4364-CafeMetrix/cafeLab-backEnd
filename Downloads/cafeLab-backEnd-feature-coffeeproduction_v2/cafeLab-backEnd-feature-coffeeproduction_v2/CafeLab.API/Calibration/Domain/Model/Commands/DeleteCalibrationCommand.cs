namespace CafeLab.API.Calibration.Domain.Model.Commands;

/// <summary>
/// Command to delete a calibration
/// </summary>
/// <param name="Id">ID of the calibration to delete</param>
public record DeleteCalibrationCommand(int Id); 