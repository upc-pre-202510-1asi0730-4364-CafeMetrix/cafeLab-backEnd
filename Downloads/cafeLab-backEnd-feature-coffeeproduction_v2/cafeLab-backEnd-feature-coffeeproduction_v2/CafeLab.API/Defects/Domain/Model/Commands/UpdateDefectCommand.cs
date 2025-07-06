namespace CafeLab.API.Defects.Domain.Model.Commands;

/// <summary>
/// Command to update an existing defect
/// </summary>
/// <param name="Id">ID of the defect to update</param>
/// <param name="Name">Name of the defect</param>
/// <param name="Description">Description of the defect</param>
/// <param name="Category">Category of the defect</param>
/// <param name="ProbableCauses">Probable causes of the defect</param>
/// <param name="RecommendedSolutions">Recommended solutions for the defect</param>
public record UpdateDefectCommand(int Id, string Name, string Description, string Category, string ProbableCauses, string RecommendedSolutions); 