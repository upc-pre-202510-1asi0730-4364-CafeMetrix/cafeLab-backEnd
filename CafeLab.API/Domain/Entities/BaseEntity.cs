using System;

namespace CafeLab.Domain.Entities;

/// <summary>
/// Clase base para todas las entidades del dominio.
/// Proporciona propiedades comunes como Id y timestamps.
/// </summary>
public abstract class BaseEntity
{
    /// <summary>
    /// Identificador único de la entidad.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Fecha y hora de creación de la entidad.
    /// </summary>
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// Fecha y hora de la última actualización de la entidad.
    /// </summary>
    public DateTime? UpdatedAt { get; set; }

    /// <summary>
    /// Constructor que inicializa los timestamps de auditoría.
    /// </summary>
    protected BaseEntity()
    {
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }
} 