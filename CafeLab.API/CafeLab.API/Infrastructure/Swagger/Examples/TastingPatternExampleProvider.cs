using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using CafeLab.API.Domain.Entities;
using Swashbuckle.AspNetCore.Filters;

namespace CafeLab.API.Infrastructure.Swagger.Examples;

public class TastingPatternExampleProvider : IExamplesProvider<TastingPattern>
{
    public TastingPattern GetExamples()
    {
        return new TastingPattern
        {
            Id = 1,
            Name = "Patrón de cata estándar SCA",
            Description = "Protocolo de cata según los estándares de la Specialty Coffee Association",
            Steps = new List<string>
            {
                "Evaluación de fragancia/aroma",
                "Evaluación de sabor",
                "Evaluación de retrogusto",
                "Evaluación de acidez",
                "Evaluación de cuerpo",
                "Evaluación de uniformidad",
                "Evaluación de balance",
                "Evaluación de taza limpia",
                "Evaluación de dulzor",
                "Evaluación de puntaje total"
            },
            ScoringSystem = "Sistema de puntuación de 0-10",
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };
    }
}

public class TastingPatternListExampleProvider : IExamplesProvider<List<TastingPattern>>
{
    public List<TastingPattern> GetExamples()
    {
        return new List<TastingPattern>
        {
            new TastingPattern
            {
                Id = 1,
                Name = "Patrón de cata estándar SCA",
                Description = "Protocolo de cata según los estándares de la Specialty Coffee Association",
                Steps = new List<string>
                {
                    "Evaluación de fragancia/aroma",
                    "Evaluación de sabor",
                    "Evaluación de retrogusto",
                    "Evaluación de acidez",
                    "Evaluación de cuerpo",
                    "Evaluación de uniformidad",
                    "Evaluación de balance",
                    "Evaluación de taza limpia",
                    "Evaluación de dulzor",
                    "Evaluación de puntaje total"
                },
                ScoringSystem = "Sistema de puntuación de 0-10",
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            },
            new TastingPattern
            {
                Id = 2,
                Name = "Patrón de cata simplificado",
                Description = "Protocolo de cata simplificado para catadores principiantes",
                Steps = new List<string>
                {
                    "Evaluación de aroma",
                    "Evaluación de sabor",
                    "Evaluación de acidez",
                    "Evaluación de cuerpo",
                    "Evaluación de balance"
                },
                ScoringSystem = "Sistema de puntuación de 0-5",
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            }
        };
    }
} 