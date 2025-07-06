using CafeLab.API.Calibration.Domain.Model.Commands;
using CafeLab.API.Calibration.Interfaces.REST.Resources;

namespace CafeLab.API.Calibration.Interfaces.REST.Transform;

/// <summary>
/// Assembler to create CreateCalibrationCommand from resource
/// </summary>
public static class CreateCalibrationCommandFromResourceAssembler
{
    /// <summary>
    /// Create a CreateCalibrationCommand from a resource
    /// </summary>
    /// <param name="resource">The CreateCalibrationResource to create the command from</param>
    /// <param name="userId">The user ID</param>
    /// <returns>The CreateCalibrationCommand created from the resource</returns>
    public static CreateCalibrationCommand ToCommandFromResource(CreateCalibrationResource resource, int userId)
    {
        return new CreateCalibrationCommand(
            resource.EquipmentName,
            resource.EquipmentType,
            resource.CalibrationMethod,
            resource.TargetValue,
            resource.MeasuredValue,
            resource.Tolerance,
            resource.Status,
            resource.Notes,
            resource.CalibrationDate,
            resource.NextCalibrationDate,
            userId
        );
    }
} 