namespace CafeLab.API.Calibration.Domain.Model.Queries;

/// <summary>
/// Query to search calibrations by equipment name
/// </summary>
/// <param name="EquipmentName">Equipment name to search for</param>
/// <param name="UserId">User ID</param>
public record SearchCalibrationsByEquipmentNameQuery(string EquipmentName, int UserId); 