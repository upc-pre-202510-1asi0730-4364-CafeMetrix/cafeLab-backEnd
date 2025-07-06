namespace CafeLab.API.Defects.Domain.Model.Commands;

/// <summary>
/// Command to create a new defect
/// </summary>
/// <param name="Name">Name of the defect</param>
/// <param name="Description">Description of the defect</param>
/// <param name="Category">Category of the defect</param>
/// <param name="ProbableCauses">Probable causes of the defect</param>
/// <param name="RecommendedSolutions">Recommended solutions for the defect</param>
/// <param name="UserId">ID of the user who created the defect</param>
public record CreateDefectCommand(string Name, string Description, string Category, string ProbableCauses, string RecommendedSolutions, int UserId); 