namespace CafeLab.API.Defects.Domain.Model.Commands;

/// <summary>
/// Command to delete a defect
/// </summary>
/// <param name="Id">ID of the defect to delete</param>
public record DeleteDefectCommand(int Id); 