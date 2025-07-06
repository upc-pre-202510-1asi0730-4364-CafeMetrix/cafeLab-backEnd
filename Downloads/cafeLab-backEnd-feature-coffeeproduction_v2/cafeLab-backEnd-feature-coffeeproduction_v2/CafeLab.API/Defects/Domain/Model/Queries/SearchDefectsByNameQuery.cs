namespace CafeLab.API.Defects.Domain.Model.Queries;

/// <summary>
/// Query to search defects by name
/// </summary>
/// <param name="Name">Name to search for</param>
/// <param name="UserId">User ID</param>
public record SearchDefectsByNameQuery(string Name, int UserId); 