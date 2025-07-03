using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace CafeLab.API.Infrastructure.Swagger;

/// <summary>
/// Filtro para agregar encabezados de respuesta en la documentación de Swagger.
/// </summary>
public class AddResponseHeadersFilter : IOperationFilter
{
    /// <summary>
    /// Aplica el filtro a la operación de Swagger.
    /// </summary>
    /// <param name="operation">La operación de Swagger a modificar.</param>
    /// <param name="context">El contexto del filtro.</param>
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        operation.Responses.Add("401", new OpenApiResponse { Description = "No autorizado" });
        operation.Responses.Add("403", new OpenApiResponse { Description = "Prohibido" });
        operation.Responses.Add("404", new OpenApiResponse { Description = "No encontrado" });
        operation.Responses.Add("500", new OpenApiResponse { Description = "Error interno del servidor" });
    }
} 