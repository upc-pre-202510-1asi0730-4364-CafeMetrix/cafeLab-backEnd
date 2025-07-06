namespace CafeLab.API.Preparation.Domain.Model.Commands;

/// <summary>
/// Create Portfolio Command
/// </summary>
/// <param name="Name">Nombre del portafolio</param>
/// <param name="UserId">ID del usuario</param>
public record CreatePortfolioCommand(string Name, int UserId); 