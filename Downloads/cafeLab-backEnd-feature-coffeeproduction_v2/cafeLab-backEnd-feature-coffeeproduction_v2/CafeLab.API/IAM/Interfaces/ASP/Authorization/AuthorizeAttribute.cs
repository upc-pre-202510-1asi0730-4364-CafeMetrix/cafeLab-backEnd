using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CafeLab.API.IAM.Interfaces.ASP.Authorization;

/// <summary>
/// Authorization attribute for JWT-based authentication
/// </summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class AuthorizeAttribute : Attribute, IAuthorizationFilter
{
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var userId = context.HttpContext.Items["UserId"];
        
        if (userId == null)
        {
            context.Result = new UnauthorizedResult();
        }
    }
} 