namespace CafeLab.API.Preparation.Domain.Model.Commands;

/// <summary>
/// Create Ingredient Command
/// </summary>
/// <param name="Name">Nombre del ingrediente</param>
/// <param name="Amount">Cantidad del ingrediente</param>
/// <param name="Unit">Unidad de medida</param>
public record CreateIngredientCommand(string Name, double Amount, string Unit); 