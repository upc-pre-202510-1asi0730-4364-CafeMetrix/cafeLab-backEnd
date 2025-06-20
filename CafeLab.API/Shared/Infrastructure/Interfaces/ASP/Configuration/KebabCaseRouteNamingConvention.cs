using Microsoft.AspNetCore.Mvc.ApplicationModels;
using CafeLab.API.Shared.Infrastructure.Interfaces.ASP.Configuration.Extensions;

namespace CafeLab.API.Shared.Infrastructure.Interfaces.ASP.Configuration;

/// <summary>
/// Convención para convertir rutas de controladores y acciones a kebab-case.
/// </summary>
public class KebabCaseRouteNamingConvention : IControllerModelConvention, IActionModelConvention
{
    public void Apply(ControllerModel controller)
    {
        controller.ControllerName = controller.ControllerName.ToKebabCase();
    }

    public void Apply(ActionModel action)
    {
        action.ActionName = action.ActionName.ToKebabCase();
    }
} 