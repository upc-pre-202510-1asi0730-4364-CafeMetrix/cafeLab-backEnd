using CafeLab.API.Defects.Domain.Model.Commands;
using CafeLab.API.Defects.Interfaces.REST.Resources;

namespace CafeLab.API.Defects.Interfaces.REST.Transform;

/// <summary>
/// Assembler to create CreateDefectCommand from resource
/// </summary>
public static class CreateDefectCommandFromResourceAssembler
{
    /// <summary>
    /// Create a CreateDefectCommand from a resource
    /// </summary>
    /// <param name="resource">The CreateDefectResource to create the command from</param>
    /// <param name="userId">The user ID</param>
    /// <returns>The CreateDefectCommand created from the resource</returns>
    public static CreateDefectCommand ToCommandFromResource(CreateDefectResource resource, int userId)
    {
        return new CreateDefectCommand(
            resource.Name,
            resource.Description,
            resource.Category,
            resource.ProbableCauses,
            resource.RecommendedSolutions,
            userId
        );
    }
} 