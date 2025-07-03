using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using Swashbuckle.AspNetCore.Filters;

namespace CafeLab.API.Infrastructure.Swagger.Examples;

public class CreateDefectRequestExampleProvider : IExamplesProvider<object>
{
    public object GetExamples()
    {
        return new
        {
            Name = "Defecto de fermentación",
            Description = "Sabor y aroma a fermentación causado por un procesamiento inadecuado",
            Category = "Fermentación",
            Severity = 3,
            Impact = "Alta",
            MitigationSteps = "Revisar el proceso de fermentación y asegurar tiempos correctos"
        };
    }
}

public class UpdateDefectRequestExampleProvider : IExamplesProvider<object>
{
    public object GetExamples()
    {
        return new
        {
            Name = "Defecto de fermentación actualizado",
            Description = "Sabor y aroma a fermentación causado por un procesamiento inadecuado o tiempos de fermentación excesivos",
            Category = "Fermentación",
            Severity = 4,
            Impact = "Alta",
            MitigationSteps = "Revisar el proceso de fermentación, asegurar tiempos correctos y monitorear la temperatura"
        };
    }
}

public class DefectQueryExampleProvider : IExamplesProvider<object>
{
    public object GetExamples()
    {
        return new
        {
            PageNumber = 1,
            PageSize = 10,
            SearchTerm = "fermentación",
            Category = "Fermentación",
            MinSeverity = 1,
            MaxSeverity = 5,
            SortBy = "Name",
            SortOrder = "asc"
        };
    }
} 