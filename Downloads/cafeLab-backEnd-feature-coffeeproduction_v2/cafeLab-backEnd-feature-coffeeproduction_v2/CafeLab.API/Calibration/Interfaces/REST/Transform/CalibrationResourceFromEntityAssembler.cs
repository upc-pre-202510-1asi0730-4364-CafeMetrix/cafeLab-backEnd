using CalibrationEntity = CafeLab.API.Calibration.Domain.Model.Aggregates.Calibration;
using CafeLab.API.Calibration.Interfaces.REST.Resources;

namespace CafeLab.API.Calibration.Interfaces.REST.Transform;

/// <summary>
/// Assembler to create CalibrationResource from entity
/// </summary>
public static class CalibrationResourceFromEntityAssembler
{
    /// <summary>
    /// Create a CalibrationResource from an entity
    /// </summary>
    /// <param name="entity">The Calibration entity to create the resource from</param>
    /// <returns>The CalibrationResource created from the entity</returns>
    public static CalibrationResource ToResourceFromEntity(CalibrationEntity entity)
    {
        return new CalibrationResource(
            entity.Id,
            entity.EquipmentName,
            entity.EquipmentType,
            entity.CalibrationMethod,
            entity.TargetValue,
            entity.MeasuredValue,
            entity.Tolerance,
            entity.Status,
            entity.Notes,
            entity.CalibrationDate,
            entity.NextCalibrationDate,
            entity.UserId,
            entity.IsActive,
            entity.CreatedAt,
            entity.UpdatedAt
        );
    }
} 