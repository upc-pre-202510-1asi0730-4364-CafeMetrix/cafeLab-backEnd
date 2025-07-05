namespace CafeLab.API.Preparation.Domain.Model.Commands;

/// <summary>
/// Update Portfolio Command
/// </summary>
/// <param name="Id">ID del portafolio</param>
/// <param name="Name">Nombre del portafolio</param>
/// <param name="UserId">ID del usuario</param>
public record UpdatePortfolioCommand(int Id, string Name, int UserId); 