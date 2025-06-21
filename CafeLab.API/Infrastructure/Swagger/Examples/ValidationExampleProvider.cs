using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using Swashbuckle.AspNetCore.Filters;

namespace CafeLab.API.Infrastructure.Swagger.Examples;

/// <summary>
/// Proveedor de ejemplos para errores de validación.
/// </summary>
public class ValidationErrorExampleProvider : IExamplesProvider<object>
{
    /// <summary>
    /// Obtiene un ejemplo de error de validación.
    /// </summary>
    /// <returns>Un objeto con datos de ejemplo.</returns>
    public object GetExamples()
    {
        return new
        {
            Success = false,
            Message = "Error de validación",
            Errors = new Dictionary<string, string[]>
            {
                { "Name", new[] { "El nombre es requerido", "El nombre debe tener entre 3 y 50 caracteres" } },
                { "Email", new[] { "El correo electrónico es inválido" } },
                { "Password", new[] { "La contraseña debe tener al menos 8 caracteres", "La contraseña debe contener al menos una letra mayúscula" } }
            }
        };
    }
}

/// <summary>
/// Proveedor de ejemplos para advertencias de validación.
/// </summary>
public class ValidationWarningExampleProvider : IExamplesProvider<object>
{
    /// <summary>
    /// Obtiene un ejemplo de advertencia de validación.
    /// </summary>
    /// <returns>Un objeto con datos de ejemplo.</returns>
    public object GetExamples()
    {
        return new
        {
            Success = true,
            Message = "Advertencias de validación",
            Warnings = new Dictionary<string, string[]>
            {
                { "Name", new[] { "Se recomienda usar un nombre más descriptivo" } },
                { "Description", new[] { "La descripción podría ser más detallada" } }
            }
        };
    }
}

/// <summary>
/// Proveedor de ejemplos para información de validación.
/// </summary>
public class ValidationInfoExampleProvider : IExamplesProvider<object>
{
    /// <summary>
    /// Obtiene un ejemplo de información de validación.
    /// </summary>
    /// <returns>Un objeto con datos de ejemplo.</returns>
    public object GetExamples()
    {
        return new
        {
            Success = true,
            Message = "Información de validación",
            Info = new Dictionary<string, string[]>
            {
                { "Name", new[] { "El nombre se utilizará para identificar el recurso en el sistema" } },
                { "Description", new[] { "La descripción ayudará a otros usuarios a entender el propósito del recurso" } }
            }
        };
    }
} 