namespace CafeLab.API.Calibration.Domain.Model.Queries;

/// <summary>
/// Query to get a calibration by ID
/// </summary>
/// <param name="Id">Calibration ID</param>
public record GetCalibrationByIdQuery(int Id); 