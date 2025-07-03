using System;

namespace CafeLab.API.Application.DTOs
{
    // DTO para transferir datos del módulo de defectos (defects)
    // Utilizado en requests y responses de la API para defectos.
    //

    public class DefectDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public string Severity { get; set; } = string.Empty;
        public string Solution { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool IsActive { get; set; }
    }

    public class CreateDefectDto
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public string Severity { get; set; } = string.Empty;
        public string Solution { get; set; } = string.Empty;
    }

    public class UpdateDefectDto
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public string Severity { get; set; } = string.Empty;
        public string Solution { get; set; } = string.Empty;
    }

    public class DefectResponseDto
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        public DefectDto? Data { get; set; }
    }

    public class DefectsResponseDto
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        public List<DefectDto> Data { get; set; } = new();
    }
} 