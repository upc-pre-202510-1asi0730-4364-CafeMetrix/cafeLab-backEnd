using CafeLab.API.Preparation.Domain.Model.ValueObjects;

namespace CafeLab.API.Preparation.Domain.Model.Commands;

/// <summary>
/// Update Recipe Command
/// </summary>
/// <param name="Id">ID de la receta</param>
/// <param name="Name">Nombre de la receta</param>
/// <param name="ImageUrl">URL de la imagen</param>
/// <param name="ExtractionMethod">Método de extracción</param>
/// <param name="Ratio">Ratio de la receta</param>
/// <param name="CuppingSessionId">ID de la sesión de cata (opcional)</param>
/// <param name="PortfolioId">ID del portafolio (opcional)</param>
/// <param name="PreparationTime">Tiempo de preparación en minutos</param>
/// <param name="Steps">Pasos de preparación</param>
/// <param name="Tips">Consejos adicionales</param>
/// <param name="Cupping">Notas de cata</param>
/// <param name="GrindSize">Tamaño de molienda</param>
/// <param name="UserId">ID del usuario</param>
/// <param name="Ingredients">Lista de ingredientes</param>
public record UpdateRecipeCommand(
    int Id,
    string Name,
    string ImageUrl,
    EExtractionMethod ExtractionMethod,
    string Ratio,
    int? CuppingSessionId,
    int? PortfolioId,
    int PreparationTime,
    string Steps,
    string Tips,
    string Cupping,
    string? GrindSize,
    int UserId,
    List<CreateIngredientCommand> Ingredients
); 