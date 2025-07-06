namespace CafeLab.API.Calibration.Interfaces.REST.Resources;

/// <summary>
/// Resource for calibration information
/// </summary>
/// <param name="Id">Calibration ID</param>
/// <param name="EquipmentName">Name of the equipment</param>
/// <param name="EquipmentType">Type of equipment</param>
/// <param name="CalibrationMethod">Method used for calibration</param>
/// <param name="TargetValue">Target value for calibration</param>
/// <param name="MeasuredValue">Measured value during calibration</param>
/// <param name="Tolerance">Acceptable tolerance</param>
/// <param name="Status">Calibration status (Pass, Fail, Pending)</param>
/// <param name="Notes">Additional notes</param>
/// <param name="CalibrationDate">Date of calibration</param>
/// <param name="NextCalibrationDate">Next calibration due date</param>
/// <param name="UserId">ID of the user who performed the calibration</param>
/// <param name="IsActive">Whether the calibration is active</param>
/// <param name="CreatedAt">Creation timestamp</param>
/// <param name="UpdatedAt">Last update timestamp</param>
public record CalibrationResource(int Id, string EquipmentName, string EquipmentType, string CalibrationMethod, decimal TargetValue, decimal MeasuredValue, decimal Tolerance, string Status, string Notes, DateTime CalibrationDate, DateTime NextCalibrationDate, int UserId, bool IsActive, DateTime CreatedAt, DateTime UpdatedAt); 