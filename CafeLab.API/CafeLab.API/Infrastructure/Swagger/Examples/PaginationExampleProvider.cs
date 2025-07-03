using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using Swashbuckle.AspNetCore.Filters;

namespace CafeLab.API.Infrastructure.Swagger.Examples;

/// <summary>
/// Proveedor de ejemplos para respuestas paginadas.
/// </summary>
public class PaginationExampleProvider : IExamplesProvider<object>
{
    /// <summary>
    /// Obtiene un ejemplo de respuesta paginada.
    /// </summary>
    /// <returns>Un objeto con datos de ejemplo.</returns>
    public object GetExamples()
    {
        return new
        {
            PageNumber = 1,
            PageSize = 10,
            TotalPages = 5,
            TotalCount = 48,
            HasPrevious = false,
            HasNext = true,
            FirstPage = 1,
            LastPage = 5,
            NextPage = 2,
            PreviousPage = -1
        };
    }
}

/// <summary>
/// Proveedor de ejemplos para consultas paginadas.
/// </summary>
public class PaginationQueryExampleProvider : IExamplesProvider<object>
{
    /// <summary>
    /// Obtiene un ejemplo de consulta paginada.
    /// </summary>
    /// <returns>Un objeto con datos de ejemplo.</returns>
    public object GetExamples()
    {
        return new
        {
            PageNumber = 1,
            PageSize = 10,
            SortBy = "Name",
            SortOrder = "asc"
        };
    }
}

/// <summary>
/// Proveedor de ejemplos para respuestas paginadas con datos.
/// </summary>
public class PaginationResponseExampleProvider : IExamplesProvider<object>
{
    /// <summary>
    /// Obtiene un ejemplo de respuesta paginada con datos.
    /// </summary>
    /// <returns>Un objeto con datos de ejemplo.</returns>
    public object GetExamples()
    {
        return new
        {
            Success = true,
            Data = new[]
            {
                new { Id = 1, Name = "Ejemplo 1", CreatedAt = DateTime.UtcNow },
                new { Id = 2, Name = "Ejemplo 2", CreatedAt = DateTime.UtcNow }
            },
            Pagination = new
            {
                PageNumber = 1,
                PageSize = 10,
                TotalPages = 5,
                TotalCount = 48,
                HasPrevious = false,
                HasNext = true,
                FirstPage = 1,
                LastPage = 5,
                NextPage = 2,
                PreviousPage = -1
            }
        };
    }
} 