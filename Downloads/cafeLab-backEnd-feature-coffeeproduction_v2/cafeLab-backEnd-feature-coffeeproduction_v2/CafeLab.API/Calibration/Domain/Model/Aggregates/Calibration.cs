using CafeLab.API.Calibration.Domain.Model.Commands;
using CafeLab.API.Shared.Domain.Model.Aggregates;

namespace CafeLab.API.Calibration.Domain.Model.Aggregates;

/// <summary>
/// Calibration aggregate root for calibration context
/// 
/// Esta entidad representa una calibración de equipo en el sistema.
/// Es el aggregate root del bounded context Calibration.
/// 
/// Una calibración contiene información sobre el proceso de verificación
/// y ajuste de equipos para asegurar mediciones precisas en la producción
/// de café. Incluye valores objetivo, medidos y tolerancias aceptables.
/// 
/// Responsabilidades:
/// - Registrar procesos de calibración de equipos
/// - Validar que las mediciones estén dentro de tolerancias
/// - Programar próximas calibraciones
/// - Mantener historial de calibraciones por usuario
/// </summary>
public class Calibration : AuditableAggregateRoot
{
    /// <summary>
    /// Identificador único de la calibración
    /// </summary>
    public int Id { get; private set; }
    
    /// <summary>
    /// Nombre del equipo calibrado (ej: "Báscula Principal", "Termómetro Digital")
    /// </summary>
    public string EquipmentName { get; private set; }
    
    /// <summary>
    /// Tipo de equipo (ej: "Báscula", "Termómetro", "Manómetro", "Tostadora")
    /// </summary>
    public string EquipmentType { get; private set; }
    
    /// <summary>
    /// Método utilizado para la calibración
    /// </summary>
    public string CalibrationMethod { get; private set; }
    
    /// <summary>
    /// Valor objetivo esperado durante la calibración
    /// </summary>
    public decimal TargetValue { get; private set; }
    
    /// <summary>
    /// Valor real medido durante la calibración
    /// </summary>
    public decimal MeasuredValue { get; private set; }
    
    /// <summary>
    /// Tolerancia aceptable para la calibración (±)
    /// </summary>
    public decimal Tolerance { get; private set; }
    
    /// <summary>
    /// Estado de la calibración: "Pass", "Fail", "Pending"
    /// </summary>
    public string Status { get; private set; }
    
    /// <summary>
    /// Notas adicionales sobre la calibración
    /// </summary>
    public string Notes { get; private set; }
    
    /// <summary>
    /// Fecha en que se realizó la calibración
    /// </summary>
    public DateTime CalibrationDate { get; private set; }
    
    /// <summary>
    /// Fecha programada para la próxima calibración
    /// </summary>
    public DateTime NextCalibrationDate { get; private set; }
    
    /// <summary>
    /// ID del usuario que realizó la calibración
    /// </summary>
    public int UserId { get; private set; }
    
    /// <summary>
    /// Indica si la calibración está activa en el sistema
    /// </summary>
    public bool IsActive { get; private set; }

    public Calibration() { }

    public Calibration(CreateCalibrationCommand command)
    {
        EquipmentName = command.EquipmentName;
        EquipmentType = command.EquipmentType;
        CalibrationMethod = command.CalibrationMethod;
        TargetValue = command.TargetValue;
        MeasuredValue = command.MeasuredValue;
        Tolerance = command.Tolerance;
        Status = command.Status;
        Notes = command.Notes;
        CalibrationDate = command.CalibrationDate;
        NextCalibrationDate = command.NextCalibrationDate;
        UserId = command.UserId;
        IsActive = true;
    }

    public void Update(UpdateCalibrationCommand command)
    {
        EquipmentName = command.EquipmentName;
        EquipmentType = command.EquipmentType;
        CalibrationMethod = command.CalibrationMethod;
        TargetValue = command.TargetValue;
        MeasuredValue = command.MeasuredValue;
        Tolerance = command.Tolerance;
        Status = command.Status;
        Notes = command.Notes;
        CalibrationDate = command.CalibrationDate;
        NextCalibrationDate = command.NextCalibrationDate;
    }

    public void Deactivate()
    {
        IsActive = false;
    }

    public bool IsWithinTolerance()
    {
        var difference = Math.Abs(MeasuredValue - TargetValue);
        return difference <= Tolerance;
    }
} 