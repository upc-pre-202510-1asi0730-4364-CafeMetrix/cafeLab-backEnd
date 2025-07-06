namespace CafeLab.API.Shared.Domain.Model.Aggregates;

/// <summary>
/// Base class for aggregate roots that need audit information
/// 
/// Esta clase base proporciona funcionalidad de auditoría automática
/// para todas las entidades que la hereden.
/// 
/// Características:
/// - CreatedAt: Fecha y hora de creación automática
/// - UpdatedAt: Fecha y hora de última actualización automática
/// - Integración con Entity Framework para auditoría automática
/// 
/// Todas las entidades que necesiten auditoría deben heredar de esta clase
/// para mantener consistencia en el seguimiento de cambios.
/// </summary>
public abstract class AuditableAggregateRoot
{
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
} 