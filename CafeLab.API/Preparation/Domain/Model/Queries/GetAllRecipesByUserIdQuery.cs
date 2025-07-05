namespace CafeLab.API.Preparation.Domain.Model.Queries;

/// <summary>
/// Get All Recipes By User ID Query
/// </summary>
/// <param name="UserId">ID del usuario</param>
public record GetAllRecipesByUserIdQuery(int UserId); 