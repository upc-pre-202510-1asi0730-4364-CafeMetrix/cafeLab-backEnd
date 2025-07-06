namespace CafeLab.API.IAM.Interfaces.REST.Resources;

/// <summary>
/// Resource for user sign in
/// </summary>
/// <param name="Email">Email address</param>
/// <param name="Password">Password</param>
public record SignInResource(string Email, string Password); 