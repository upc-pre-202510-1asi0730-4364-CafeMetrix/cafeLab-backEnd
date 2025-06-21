using System;
using System.Collections.Generic;
using CafeLab.API.Shared.Entities;

namespace CafeLab.API.Domain.Entities;

/// <summary>
/// Representa un defecto encontrado en una evaluación sensorial de café.
/// Esta entidad define los defectos que pueden afectar la calidad del café.
/// </summary>
public class Defect : BaseEntity
{
    /// <summary>
    /// Nombre del defecto.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Descripción detallada del defecto.
    /// </summary>
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// Categoría del defecto (por ejemplo: Primario, Secundario).
    /// </summary>
    public string Category { get; set; } = string.Empty;

    /// <summary>
    /// Nivel de severidad del defecto (por ejemplo: Bajo, Medio, Alto).
    /// </summary>
    public string Severity { get; set; } = string.Empty;

    /// <summary>
    /// Solución recomendada para el defecto.
    /// </summary>
    public string Solution { get; set; } = string.Empty;

    /// <summary>
    /// Usuario que creó el defecto.
    /// </summary>
    public string CreatedBy { get; set; } = string.Empty;

    /// <summary>
    /// Constructor que inicializa las colecciones.
    /// </summary>
    public Defect()
    {
        CreatedAt = DateTime.UtcNow;
    }
} 