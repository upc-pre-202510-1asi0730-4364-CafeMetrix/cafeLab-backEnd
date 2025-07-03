using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using Swashbuckle.AspNetCore.Filters;

namespace CafeLab.API.Infrastructure.Swagger.Examples;

public class SuccessResponseExampleProvider : IExamplesProvider<object>
{
    public object GetExamples()
    {
        return new
        {
            Success = true,
            Message = "Operación completada exitosamente",
            Data = new
            {
                Id = 1,
                Name = "Ejemplo",
                CreatedAt = DateTime.UtcNow
            }
        };
    }
}

public class CreatedResponseExampleProvider : IExamplesProvider<object>
{
    public object GetExamples()
    {
        return new
        {
            Success = true,
            Message = "Recurso creado exitosamente",
            Data = new
            {
                Id = 1,
                Name = "Ejemplo",
                CreatedAt = DateTime.UtcNow
            }
        };
    }
}

public class UpdatedResponseExampleProvider : IExamplesProvider<object>
{
    public object GetExamples()
    {
        return new
        {
            Success = true,
            Message = "Recurso actualizado exitosamente",
            Data = new
            {
                Id = 1,
                Name = "Ejemplo actualizado",
                UpdatedAt = DateTime.UtcNow
            }
        };
    }
}

public class DeletedResponseExampleProvider : IExamplesProvider<object>
{
    public object GetExamples()
    {
        return new
        {
            Success = true,
            Message = "Recurso eliminado exitosamente"
        };
    }
}

public class PaginatedResponseExampleProvider : IExamplesProvider<object>
{
    public object GetExamples()
    {
        return new
        {
            Success = true,
            Data = new[]
            {
                new
                {
                    Id = 1,
                    Name = "Ejemplo 1",
                    CreatedAt = DateTime.UtcNow
                },
                new
                {
                    Id = 2,
                    Name = "Ejemplo 2",
                    CreatedAt = DateTime.UtcNow
                }
            },
            Pagination = new
            {
                PageNumber = 1,
                PageSize = 10,
                TotalPages = 5,
                TotalCount = 50,
                HasPrevious = false,
                HasNext = true
            }
        };
    }
} 