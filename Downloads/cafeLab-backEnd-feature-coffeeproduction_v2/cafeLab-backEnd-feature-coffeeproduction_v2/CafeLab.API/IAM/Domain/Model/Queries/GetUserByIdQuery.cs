namespace CafeLab.API.IAM.Domain.Model.Queries;

/// <summary>
/// Query to get a user by ID
/// </summary>
/// <param name="Id">User ID</param>
public record GetUserByIdQuery(int Id); 