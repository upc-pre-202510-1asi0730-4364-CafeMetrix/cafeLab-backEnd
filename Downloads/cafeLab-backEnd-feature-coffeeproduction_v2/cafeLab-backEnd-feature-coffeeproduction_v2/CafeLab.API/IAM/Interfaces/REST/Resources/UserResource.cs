namespace CafeLab.API.IAM.Interfaces.REST.Resources;

/// <summary>
/// Resource for user information
/// </summary>
/// <param name="Id">User ID</param>
/// <param name="Username">Username</param>
/// <param name="Email">Email address</param>
/// <param name="Role">User role</param>
/// <param name="IsActive">Whether the user is active</param>
/// <param name="LastLoginAt">Last login timestamp</param>
/// <param name="CreatedAt">Creation timestamp</param>
/// <param name="UpdatedAt">Last update timestamp</param>
public record UserResource(int Id, string Username, string Email, string Role, bool IsActive, DateTime LastLoginAt, DateTime CreatedAt, DateTime UpdatedAt); 