using CafeLab.API.IAM.Domain.Model.Commands;
using CafeLab.API.IAM.Interfaces.REST.Resources;

namespace CafeLab.API.IAM.Interfaces.REST.Transform;

/// <summary>
/// Assembler to create SignInCommand from resource
/// </summary>
public static class SignInCommandFromResourceAssembler
{
    /// <summary>
    /// Create a SignInCommand from a resource
    /// </summary>
    /// <param name="resource">The SignInResource to create the command from</param>
    /// <returns>The SignInCommand created from the resource</returns>
    public static SignInCommand ToCommandFromResource(SignInResource resource)
    {
        return new SignInCommand(
            resource.Email,
            resource.Password
        );
    }
} 