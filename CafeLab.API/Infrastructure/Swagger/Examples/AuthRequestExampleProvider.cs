using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using Swashbuckle.AspNetCore.Filters;

namespace CafeLab.API.Infrastructure.Swagger.Examples;

public class LoginRequestExampleProvider : IExamplesProvider<object>
{
    public object GetExamples()
    {
        return new
        {
            Email = "usuario@ejemplo.com",
            Password = "Contraseña123!"
        };
    }
}

public class RegisterRequestExampleProvider : IExamplesProvider<object>
{
    public object GetExamples()
    {
        return new
        {
            Email = "usuario@ejemplo.com",
            Password = "Contraseña123!",
            ConfirmPassword = "Contraseña123!",
            Name = "Juan Pérez"
        };
    }
}

public class ForgotPasswordRequestExampleProvider : IExamplesProvider<object>
{
    public object GetExamples()
    {
        return new
        {
            Email = "usuario@ejemplo.com"
        };
    }
}

public class ResetPasswordRequestExampleProvider : IExamplesProvider<object>
{
    public object GetExamples()
    {
        return new
        {
            Email = "usuario@ejemplo.com",
            Token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIxMjM0NTY3ODkwIiwibmFtZSI6Ikp1YW4gUGVyZXoiLCJpYXQiOjE1MTYyMzkwMjJ9.SflKxwRJSMeKKF2QT4fwpMeJf36POk6yJV_adQssw5c",
            NewPassword = "NuevaContraseña123!",
            ConfirmPassword = "NuevaContraseña123!"
        };
    }
}

public class ChangePasswordRequestExampleProvider : IExamplesProvider<object>
{
    public object GetExamples()
    {
        return new
        {
            CurrentPassword = "Contraseña123!",
            NewPassword = "NuevaContraseña123!",
            ConfirmPassword = "NuevaContraseña123!"
        };
    }
} 