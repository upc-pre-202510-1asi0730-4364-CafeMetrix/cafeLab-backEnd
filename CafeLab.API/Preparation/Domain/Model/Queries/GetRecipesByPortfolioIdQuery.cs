namespace CafeLab.API.Preparation.Domain.Model.Queries;

/// <summary>
/// Get Recipes By Portfolio ID Query
/// </summary>
/// <param name="PortfolioId">ID del portafolio</param>
/// <param name="UserId">ID del usuario</param>
public record GetRecipesByPortfolioIdQuery(int PortfolioId, int UserId); 