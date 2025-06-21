using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using Swashbuckle.AspNetCore.Filters;
using CafeLab.API.Domain.Entities;

namespace CafeLab.API.Infrastructure.Swagger.Examples;

public class CreateTastingPatternRequestExampleProvider : IExamplesProvider<object>
{
    public object GetExamples()
    {
        return new
        {
            Name = "Patrón de cata estándar SCA",
            Description = "Protocolo de cata según los estándares de la Specialty Coffee Association",
            Steps = new[]
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
            ScoringSystem = "Sistema de puntuación de 0-10"
        };
    }
}

public class UpdateTastingPatternRequestExampleProvider : IExamplesProvider<object>
{
    public object GetExamples()
    {
        return new
        {
            Name = "Patrón de cata estándar SCA actualizado",
            Description = "Protocolo de cata según los estándares de la Specialty Coffee Association (versión 2023)",
            Steps = new[]
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
                "Evaluación de puntaje total",
                "Evaluación de sostenibilidad"
            },
            ScoringSystem = "Sistema de puntuación de 0-10 con criterios de sostenibilidad"
        };
    }
}

public class TastingPatternQueryExampleProvider : IExamplesProvider<object>
{
    public object GetExamples()
    {
        return new
        {
            PageNumber = 1,
            PageSize = 10,
            SearchTerm = "SCA",
            ScoringSystem = "0-10",
            SortBy = "Name",
            SortOrder = "asc"
        };
    }
} 