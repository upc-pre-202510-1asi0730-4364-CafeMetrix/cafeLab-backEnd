namespace CafeLab.API.IAM.Interfaces.REST.Resources;

/// <summary>
/// Resource for creating a new user
/// </summary>
/// <param name="Username">Username for the user</param>
/// <param name="Email">Email address</param>
/// <param name="Password">Password</param>
/// <param name="Role">User role (barista, owner, admin)</param>
public record CreateUserResource(string Username, string Email, string Password, string Role); 