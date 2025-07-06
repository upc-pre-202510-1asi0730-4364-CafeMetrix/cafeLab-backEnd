namespace CafeLab.API.Defects.Domain.Model.Queries;

/// <summary>
/// Query to get a defect by ID
/// </summary>
/// <param name="Id">Defect ID</param>
public record GetDefectByIdQuery(int Id); 