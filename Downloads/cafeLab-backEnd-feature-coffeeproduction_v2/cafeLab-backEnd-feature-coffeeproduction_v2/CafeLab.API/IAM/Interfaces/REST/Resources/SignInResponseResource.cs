namespace CafeLab.API.IAM.Interfaces.REST.Resources;

/// <summary>
/// Resource for sign in response
/// </summary>
/// <param name="Token">JWT token</param>
/// <param name="User">User information</param>
public record SignInResponseResource(string Token, UserResource User); 