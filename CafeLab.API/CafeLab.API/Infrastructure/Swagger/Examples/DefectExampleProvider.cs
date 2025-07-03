using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using CafeLab.API.Domain.Entities;
using Swashbuckle.AspNetCore.Filters;

namespace CafeLab.API.Infrastructure.Swagger.Examples;

public class DefectExampleProvider : IExamplesProvider<Defect>
{
    public Defect GetExamples()
    {
        return new Defect
        {
            Id = 1,
            Name = "Defecto de fermentación",
            Description = "Sabor y aroma a fermentación causado por un procesamiento inadecuado",
            Category = "Fermentación",
            Severity = "Alta",
            Solution = "Revisar el proceso de fermentación y asegurar tiempos correctos",
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };
    }
}

public class DefectListExampleProvider : IExamplesProvider<List<Defect>>
{
    public List<Defect> GetExamples()
    {
        return new List<Defect>
        {
            new Defect
            {
                Id = 1,
                Name = "Defecto de fermentación",
                Description = "Sabor y aroma a fermentación causado por un procesamiento inadecuado",
                Category = "Fermentación",
                Severity = "Alta",
                Solution = "Revisar el proceso de fermentación y asegurar tiempos correctos",
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            },
            new Defect
            {
                Id = 2,
                Name = "Defecto de tueste",
                Description = "Sabor a quemado causado por un tueste excesivo",
                Category = "Tueste",
                Severity = "Media",
                Solution = "Ajustar el perfil de tueste y monitorear la temperatura",
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            }
        };
    }
} 