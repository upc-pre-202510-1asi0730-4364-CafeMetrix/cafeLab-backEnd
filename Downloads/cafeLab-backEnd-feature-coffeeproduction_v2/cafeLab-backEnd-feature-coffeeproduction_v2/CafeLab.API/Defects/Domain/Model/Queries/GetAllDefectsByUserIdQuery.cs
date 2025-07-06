namespace CafeLab.API.Defects.Domain.Model.Queries;

/// <summary>
/// Query to get all defects by user ID
/// </summary>
/// <param name="UserId">User ID</param>
public record GetAllDefectsByUserIdQuery(int UserId); 