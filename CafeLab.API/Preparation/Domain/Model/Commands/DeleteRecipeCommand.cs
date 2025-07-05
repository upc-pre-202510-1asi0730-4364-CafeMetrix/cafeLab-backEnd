namespace CafeLab.API.Preparation.Domain.Model.Commands;

/// <summary>
/// Delete Recipe Command
/// </summary>
/// <param name="Id">ID de la receta a eliminar</param>
/// <param name="UserId">ID del usuario que solicita la eliminación</param>
public record DeleteRecipeCommand(int Id, int UserId); 