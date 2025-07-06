namespace CafeLab.API.Defects.Interfaces.REST.Resources;

/// <summary>
/// Resource for creating a new defect
/// </summary>
/// <param name="Name">Name of the defect</param>
/// <param name="Description">Description of the defect</param>
/// <param name="Category">Category of the defect</param>
/// <param name="ProbableCauses">Probable causes of the defect</param>
/// <param name="RecommendedSolutions">Recommended solutions for the defect</param>
public record CreateDefectResource(string Name, string Description, string Category, string ProbableCauses, string RecommendedSolutions); 