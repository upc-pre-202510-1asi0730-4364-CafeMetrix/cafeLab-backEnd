using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using Swashbuckle.AspNetCore.Filters;

namespace CafeLab.API.Infrastructure.Swagger.Examples;

public class LoginResponseExampleProvider : IExamplesProvider<object>
{
    public object GetExamples()
    {
        return new
        {
            Success = true,
            Data = new
            {
                Token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIxMjM0NTY3ODkwIiwibmFtZSI6Ikp1YW4gUGVyZXoiLCJpYXQiOjE1MTYyMzkwMjJ9.SflKxwRJSMeKKF2QT4fwpMeJf36POk6yJV_adQssw5c",
                ExpiresIn = 3600,
                User = new
                {
                    Id = 1,
                    Email = "usuario@ejemplo.com",
                    Name = "Juan Pérez",
                    Role = "Admin"
                }
            }
        };
    }
}

public class RegisterResponseExampleProvider : IExamplesProvider<object>
{
    public object GetExamples()
    {
        return new
        {
            Success = true,
            Message = "Usuario registrado exitosamente",
            Data = new
            {
                Id = 1,
                Email = "usuario@ejemplo.com",
                Name = "Juan Pérez",
                Role = "User",
                CreatedAt = DateTime.UtcNow
            }
        };
    }
}

public class ForgotPasswordResponseExampleProvider : IExamplesProvider<object>
{
    public object GetExamples()
    {
        return new
        {
            Success = true,
            Message = "Se ha enviado un correo con instrucciones para restablecer la contraseña"
        };
    }
}

public class ResetPasswordResponseExampleProvider : IExamplesProvider<object>
{
    public object GetExamples()
    {
        return new
        {
            Success = true,
            Message = "Contraseña restablecida exitosamente"
        };
    }
}

public class ChangePasswordResponseExampleProvider : IExamplesProvider<object>
{
    public object GetExamples()
    {
        return new
        {
            Success = true,
            Message = "Contraseña actualizada exitosamente"
        };
    }
} 