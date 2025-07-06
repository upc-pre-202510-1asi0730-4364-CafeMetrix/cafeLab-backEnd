namespace CafeLab.API.Defects.Interfaces.REST.Resources;

/// <summary>
/// Resource for defect information
/// </summary>
/// <param name="Id">Defect ID</param>
/// <param name="Name">Name of the defect</param>
/// <param name="Description">Description of the defect</param>
/// <param name="Category">Category of the defect</param>
/// <param name="ProbableCauses">Probable causes of the defect</param>
/// <param name="RecommendedSolutions">Recommended solutions for the defect</param>
/// <param name="UserId">ID of the user who created the defect</param>
/// <param name="IsActive">Whether the defect is active</param>
/// <param name="CreatedAt">Creation timestamp</param>
/// <param name="UpdatedAt">Last update timestamp</param>
public record DefectResource(int Id, string Name, string Description, string Category, string ProbableCauses, string RecommendedSolutions, int UserId, bool IsActive, DateTime CreatedAt, DateTime UpdatedAt); 