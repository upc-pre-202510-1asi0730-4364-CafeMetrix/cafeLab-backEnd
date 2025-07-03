using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using Swashbuckle.AspNetCore.Filters;

namespace CafeLab.API.Infrastructure.Swagger.Examples;

public class ErrorResponseExampleProvider : IExamplesProvider<ProblemDetails>
{
    public ProblemDetails GetExamples()
    {
        return new ProblemDetails
        {
            Status = 400,
            Title = "Error de validación",
            Detail = "Los datos proporcionados no son válidos",
            Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1",
            Instance = "/api/defects",
            Extensions =
            {
                ["errors"] = new Dictionary<string, string[]>
                {
                    ["Name"] = new[] { "El nombre es requerido" },
                    ["Category"] = new[] { "La categoría es requerida" }
                }
            }
        };
    }
}

public class NotFoundResponseExampleProvider : IExamplesProvider<ProblemDetails>
{
    public ProblemDetails GetExamples()
    {
        return new ProblemDetails
        {
            Status = 404,
            Title = "No encontrado",
            Detail = "El recurso solicitado no existe",
            Type = "https://tools.ietf.org/html/rfc7231#section-6.5.4",
            Instance = "/api/defects/999"
        };
    }
}

public class UnauthorizedResponseExampleProvider : IExamplesProvider<ProblemDetails>
{
    public ProblemDetails GetExamples()
    {
        return new ProblemDetails
        {
            Status = 401,
            Title = "No autorizado",
            Detail = "Se requiere autenticación para acceder a este recurso",
            Type = "https://tools.ietf.org/html/rfc7235#section-3.1",
            Instance = "/api/defects"
        };
    }
}

public class ForbiddenResponseExampleProvider : IExamplesProvider<ProblemDetails>
{
    public ProblemDetails GetExamples()
    {
        return new ProblemDetails
        {
            Status = 403,
            Title = "Prohibido",
            Detail = "No tiene permisos para acceder a este recurso",
            Type = "https://tools.ietf.org/html/rfc7231#section-6.5.3",
            Instance = "/api/defects"
        };
    }
}

public class InternalServerErrorResponseExampleProvider : IExamplesProvider<ProblemDetails>
{
    public ProblemDetails GetExamples()
    {
        return new ProblemDetails
        {
            Status = 500,
            Title = "Error interno del servidor",
            Detail = "Ha ocurrido un error inesperado en el servidor",
            Type = "https://tools.ietf.org/html/rfc7231#section-6.6.1",
            Instance = "/api/defects"
        };
    }
} 