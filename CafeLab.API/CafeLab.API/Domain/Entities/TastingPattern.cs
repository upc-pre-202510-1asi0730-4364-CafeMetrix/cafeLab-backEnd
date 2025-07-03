using System;
using System.Collections.Generic;
using CafeLab.API.Shared.Entities;

namespace CafeLab.API.Domain.Entities;

/// <summary>
/// Representa un patrón de cata para evaluar el café.
/// Esta entidad define la metodología y pasos para realizar una cata de café.
/// </summary>
public class TastingPattern : BaseEntity
{
    /// <summary>
    /// Nombre del patrón de cata.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Descripción detallada del patrón de cata.
    /// </summary>
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// Pasos a seguir durante la cata.
    /// </summary>
    public virtual ICollection<string> Steps { get; set; }

    /// <summary>
    /// Sistema de puntuación utilizado en la cata.
    /// </summary>
    public string ScoringSystem { get; set; } = string.Empty;

    /// <summary>
    /// Constructor que inicializa las colecciones.
    /// </summary>
    public TastingPattern()
    {
        Steps = new HashSet<string>();
        CreatedAt = DateTime.UtcNow;
    }

    public decimal Aroma { get; set; }
    public decimal Flavor { get; set; }
    public decimal Aftertaste { get; set; }
    public decimal Acidity { get; set; }
    public decimal Body { get; set; }
    public decimal Uniformity { get; set; }
    public decimal Balance { get; set; }
    public decimal CleanCup { get; set; }
    public decimal Sweetness { get; set; }
    public decimal Overall { get; set; }
    public string Notes { get; set; } = string.Empty;
    public string Recommendations { get; set; } = string.Empty;
    public string CreatedBy { get; set; } = string.Empty;
}