namespace CafeLab.API.IAM.Domain.Model.Commands;

/// <summary>
/// Command to sign in a user
/// </summary>
/// <param name="Email">Email address</param>
/// <param name="Password">Password</param>
public record SignInCommand(string Email, string Password); 