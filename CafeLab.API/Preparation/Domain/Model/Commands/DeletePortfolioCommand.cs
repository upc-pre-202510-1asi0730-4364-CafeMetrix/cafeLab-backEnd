namespace CafeLab.API.Preparation.Domain.Model.Commands;

/// <summary>
/// Delete Portfolio Command
/// </summary>
/// <param name="Id">ID del portafolio a eliminar</param>
/// <param name="UserId">ID del usuario que solicita la eliminación</param>
public record DeletePortfolioCommand(int Id, int UserId); 