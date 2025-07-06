using CafeLab.API.IAM.Domain.Model.Commands;
using CafeLab.API.IAM.Interfaces.REST.Resources;

namespace CafeLab.API.IAM.Interfaces.REST.Transform;

/// <summary>
/// Assembler to create CreateUserCommand from resource
/// </summary>
public static class CreateUserCommandFromResourceAssembler
{
    /// <summary>
    /// Create a CreateUserCommand from a resource
    /// </summary>
    /// <param name="resource">The CreateUserResource to create the command from</param>
    /// <returns>The CreateUserCommand created from the resource</returns>
    public static CreateUserCommand ToCommandFromResource(CreateUserResource resource)
    {
        return new CreateUserCommand(
            resource.Username,
            resource.Email,
            resource.Password,
            resource.Role
        );
    }
} 