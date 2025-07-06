namespace CafeLab.API.Calibration.Domain.Model.Queries;

/// <summary>
/// Query to get all calibrations by user ID
/// </summary>
/// <param name="UserId">User ID</param>
public record GetAllCalibrationsByUserIdQuery(int UserId); 