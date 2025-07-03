using System;
using System.ComponentModel.DataAnnotations;

namespace CafeLab.API.Domain.Entities
{
    /// <summary>
    /// Representa una calibración de equipos o procesos en el laboratorio de café.
    /// </summary>
    public class Calibration
    {
        /// <summary>
        /// Identificador único de la calibración.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Nombre de la calibración.
        /// </summary>
        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Descripción detallada de la calibración.
        /// </summary>
        [Required]
        [StringLength(500)]
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// Fecha de la calibración.
        /// </summary>
        [Required]
        public DateTime CalibrationDate { get; set; }

        /// <summary>
        /// Resultado de la calibración.
        /// </summary>
        [Required]
        [StringLength(200)]
        public string Result { get; set; } = string.Empty;

        /// <summary>
        /// Estado de la calibración (Pendiente, En Proceso, Completada, Fallida).
        /// </summary>
        [Required]
        [StringLength(20)]
        public string Status { get; set; } = "Pendiente";

        /// <summary>
        /// Tipo de calibración (Equipo, Proceso, Método).
        /// </summary>
        [Required]
        [StringLength(50)]
        public string Type { get; set; } = string.Empty;

        /// <summary>
        /// Notas adicionales sobre la calibración.
        /// </summary>
        [StringLength(1000)]
        public string? Notes { get; set; }

        /// <summary>
        /// Fecha de creación del registro.
        /// </summary>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Fecha de la última modificación.
        /// </summary>
        public DateTime? UpdatedAt { get; set; }

        /// <summary>
        /// Indica si la calibración está activa.
        /// </summary>
        public bool IsActive { get; set; } = true;

        /// <summary>
        /// Constructor por defecto.
        /// </summary>
        public Calibration()
        {
            CreatedAt = DateTime.UtcNow;
            CalibrationDate = DateTime.UtcNow;
            Status = "Pendiente";
            IsActive = true;
        }

        /// <summary>
        /// Constructor con parámetros para crear una calibración.
        /// </summary>
        public Calibration(string name, string description, DateTime calibrationDate, string result, string type)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Name cannot be empty.", nameof(name));
            if (string.IsNullOrWhiteSpace(description))
                throw new ArgumentException("Description cannot be empty.", nameof(description));
            if (string.IsNullOrWhiteSpace(result))
                throw new ArgumentException("Result cannot be empty.", nameof(result));
            if (string.IsNullOrWhiteSpace(type))
                throw new ArgumentException("Type cannot be empty.", nameof(type));

            Name = name;
            Description = description;
            CalibrationDate = calibrationDate;
            Result = result;
            Type = type;
            Status = "Pendiente";
            CreatedAt = DateTime.UtcNow;
            IsActive = true;
        }

        /// <summary>
        /// Actualiza los datos de la calibración.
        /// </summary>
        public void Update(string name, string description, DateTime calibrationDate, string result, string type, string status, string? notes)
        {
            if (!string.IsNullOrWhiteSpace(name))
                Name = name;
            if (!string.IsNullOrWhiteSpace(description))
                Description = description;
            if (!string.IsNullOrWhiteSpace(result))
                Result = result;
            if (!string.IsNullOrWhiteSpace(type))
                Type = type;
            if (!string.IsNullOrWhiteSpace(status))
                Status = status;

            CalibrationDate = calibrationDate;
            Notes = notes;
            UpdatedAt = DateTime.UtcNow;
        }

        /// <summary>
        /// Actualiza el estado de la calibración.
        /// </summary>
        public void UpdateStatus(string status)
        {
            if (!string.IsNullOrWhiteSpace(status))
                Status = status;

            UpdatedAt = DateTime.UtcNow;
        }

        /// <summary>
        /// Desactiva la calibración.
        /// </summary>
        public void Deactivate()
        {
            IsActive = false;
            UpdatedAt = DateTime.UtcNow;
        }

        /// <summary>
        /// Activa la calibración.
        /// </summary>
        public void Activate()
        {
            IsActive = true;
            UpdatedAt = DateTime.UtcNow;
        }
    }
} 