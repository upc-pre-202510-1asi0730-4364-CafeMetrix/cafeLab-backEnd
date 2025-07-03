using System;
using System.ComponentModel.DataAnnotations;

namespace CafeLab.API.Domain.Entities;

/// <summary>
/// Representa un defecto encontrado en una evaluación sensorial de café.
/// Esta entidad define los defectos que pueden afectar la calidad del café.
/// </summary>
public class Defect
{
    /// <summary>
    /// Identificador único del defecto.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Nombre del defecto.
    /// </summary>
    [Required]
    [StringLength(100)]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Descripción detallada del defecto.
    /// </summary>
    [Required]
    [StringLength(500)]
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// Categoría del defecto (por ejemplo: Primario, Secundario).
    /// </summary>
    [Required]
    [StringLength(50)]
    public string Category { get; set; } = string.Empty;

    /// <summary>
    /// Nivel de severidad del defecto (por ejemplo: Bajo, Medio, Alto).
    /// </summary>
    [Required]
    [StringLength(20)]
    public string Severity { get; set; } = string.Empty;

    /// <summary>
    /// Solución recomendada para el defecto.
    /// </summary>
    [Required]
    [StringLength(1000)]
    public string Solution { get; set; } = string.Empty;

    /// <summary>
    /// Fecha de creación del defecto.
    /// </summary>
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// Fecha de la última modificación.
    /// </summary>
    public DateTime? UpdatedAt { get; set; }

    /// <summary>
    /// Indica si el defecto está activo.
    /// </summary>
    public bool IsActive { get; set; } = true;

    /// <summary>
    /// Constructor que inicializa las propiedades.
    /// </summary>
    public Defect()
    {
        CreatedAt = DateTime.UtcNow;
        IsActive = true;
    }

    /// <summary>
    /// Constructor con parámetros para crear un defecto.
    /// </summary>
    public Defect(string name, string description, string category, string severity, string solution)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Name cannot be empty.", nameof(name));
        if (string.IsNullOrWhiteSpace(description))
            throw new ArgumentException("Description cannot be empty.", nameof(description));
        if (string.IsNullOrWhiteSpace(category))
            throw new ArgumentException("Category cannot be empty.", nameof(category));
        if (string.IsNullOrWhiteSpace(severity))
            throw new ArgumentException("Severity cannot be empty.", nameof(severity));
        if (string.IsNullOrWhiteSpace(solution))
            throw new ArgumentException("Solution cannot be empty.", nameof(solution));

        Name = name;
        Description = description;
        Category = category;
        Severity = severity;
        Solution = solution;
        CreatedAt = DateTime.UtcNow;
        IsActive = true;
    }

    /// <summary>
    /// Actualiza los datos del defecto.
    /// </summary>
    public void Update(string name, string description, string category, string severity, string solution)
    {
        if (!string.IsNullOrWhiteSpace(name))
            Name = name;
        if (!string.IsNullOrWhiteSpace(description))
            Description = description;
        if (!string.IsNullOrWhiteSpace(category))
            Category = category;
        if (!string.IsNullOrWhiteSpace(severity))
            Severity = severity;
        if (!string.IsNullOrWhiteSpace(solution))
            Solution = solution;

        UpdatedAt = DateTime.UtcNow;
    }

    /// <summary>
    /// Desactiva el defecto.
    /// </summary>
    public void Deactivate()
    {
        IsActive = false;
        UpdatedAt = DateTime.UtcNow;
    }

    /// <summary>
    /// Activa el defecto.
    /// </summary>
    public void Activate()
    {
        IsActive = true;
        UpdatedAt = DateTime.UtcNow;
    }
} 