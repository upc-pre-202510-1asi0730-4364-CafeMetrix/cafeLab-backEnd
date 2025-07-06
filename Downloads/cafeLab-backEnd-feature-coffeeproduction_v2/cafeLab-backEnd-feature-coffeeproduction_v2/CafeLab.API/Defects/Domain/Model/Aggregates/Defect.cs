using CafeLab.API.Defects.Domain.Model.Commands;
using CafeLab.API.Shared.Domain.Model.Aggregates;

namespace CafeLab.API.Defects.Domain.Model.Aggregates;

/// <summary>
/// Defect aggregate root for defects context
/// 
/// Esta entidad representa un defecto del café en el sistema.
/// Es el aggregate root del bounded context Defects.
/// 
/// Un defecto contiene información sobre problemas comunes del café,
/// incluyendo causas probables y soluciones recomendadas para ayudar
/// a los baristas y dueños a identificar y resolver problemas de calidad.
/// 
/// Responsabilidades:
/// - Almacenar información detallada sobre defectos del café
/// - Proporcionar causas probables y soluciones
/// - Categorizar defectos para facilitar la búsqueda
/// - Mantener un historial de defectos por usuario
/// </summary>
public class Defect : AuditableAggregateRoot
{
    /// <summary>
    /// Identificador único del defecto
    /// </summary>
    public int Id { get; private set; }
    
    /// <summary>
    /// Nombre del defecto (ej: "Sabor amargo", "Acidez excesiva")
    /// </summary>
    public string Name { get; private set; }
    
    /// <summary>
    /// Descripción detallada del defecto y sus características
    /// </summary>
    public string Description { get; private set; }
    
    /// <summary>
    /// Categoría del defecto (ej: "Sabor", "Aroma", "Cuerpo", "Acidez")
    /// </summary>
    public string Category { get; private set; }
    
    /// <summary>
    /// Posibles causas que pueden generar este defecto
    /// </summary>
    public string ProbableCauses { get; private set; }
    
    /// <summary>
    /// Soluciones recomendadas para corregir el defecto
    /// </summary>
    public string RecommendedSolutions { get; private set; }
    
    /// <summary>
    /// ID del usuario que creó o registró el defecto
    /// </summary>
    public int UserId { get; private set; }
    
    /// <summary>
    /// Indica si el defecto está activo en el sistema
    /// </summary>
    public bool IsActive { get; private set; }

    public Defect() { }

    public Defect(CreateDefectCommand command)
    {
        Name = command.Name;
        Description = command.Description;
        Category = command.Category;
        ProbableCauses = command.ProbableCauses;
        RecommendedSolutions = command.RecommendedSolutions;
        UserId = command.UserId;
        IsActive = true;
    }

    public void Update(UpdateDefectCommand command)
    {
        Name = command.Name;
        Description = command.Description;
        Category = command.Category;
        ProbableCauses = command.ProbableCauses;
        RecommendedSolutions = command.RecommendedSolutions;
    }

    public void Deactivate()
    {
        IsActive = false;
    }
} 