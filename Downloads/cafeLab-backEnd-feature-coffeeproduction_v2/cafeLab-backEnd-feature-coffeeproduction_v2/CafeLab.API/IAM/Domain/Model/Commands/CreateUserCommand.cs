namespace CafeLab.API.IAM.Domain.Model.Commands;

/// <summary>
/// Command to create a new user
/// </summary>
/// <param name="Username">Username for the user</param>
/// <param name="Email">Email address</param>
/// <param name="Password">Password (will be hashed)</param>
/// <param name="Role">User role (barista, owner, admin)</param>
public record CreateUserCommand(string Username, string Email, string Password, string Role); 