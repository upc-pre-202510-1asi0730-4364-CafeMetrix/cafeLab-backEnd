namespace CafeLab.API.Calibration.Interfaces.REST.Resources;

/// <summary>
/// Resource for creating a new calibration
/// </summary>
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
public record CreateCalibrationResource(string EquipmentName, string EquipmentType, string CalibrationMethod, decimal TargetValue, decimal MeasuredValue, decimal Tolerance, string Status, string Notes, DateTime CalibrationDate, DateTime NextCalibrationDate); 