using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace CafeLab.API.Infrastructure.Swagger;

public class AppendAuthorizeToSummaryOperationFilter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        var authAttributes = context.MethodInfo.DeclaringType.GetCustomAttributes(true)
            .Union(context.MethodInfo.GetCustomAttributes(true))
            .OfType<Microsoft.AspNetCore.Authorization.AuthorizeAttribute>();

        if (authAttributes.Any())
        {
            operation.Summary = $"{operation.Summary} [Requiere autenticación]";
        }
    }
} 