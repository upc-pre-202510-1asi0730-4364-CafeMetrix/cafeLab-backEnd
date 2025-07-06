using CafeLab.API.IAM.Domain.Services;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace CafeLab.API.IAM.Interfaces.ASP.Authorization;

/// <summary>
/// JWT Authorization Middleware
/// 
/// Este middleware intercepta todas las peticiones HTTP y valida los tokens JWT
/// para proporcionar autenticación y autorización en el sistema.
/// 
/// Funcionalidades:
/// - Extrae el token JWT del header Authorization
/// - Valida la autenticidad y expiración del token
/// - Extrae el ID del usuario del token
/// - Agrega la información del usuario al contexto HTTP
/// - Establece los claims del usuario para autorización
/// 
/// El middleware se ejecuta en cada petición y permite que los controladores
/// accedan a la información del usuario autenticado.
/// 
/// NOTA: IJwtService se resuelve por request usando RequestServices para evitar problemas de ciclo de vida (scoped).
/// </summary>
public class JwtAuthorizationMiddleware
{
    // Delegado al siguiente middleware en el pipeline
    private readonly RequestDelegate _next;

    // El servicio IJwtService se resuelve por request, no se inyecta aquí
    public JwtAuthorizationMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    /// <summary>
    /// Método principal que intercepta cada petición HTTP
    /// </summary>
    /// <param name="context">Contexto HTTP de la petición</param>
    /// <returns></returns>
    public async Task InvokeAsync(HttpContext context)
    {
        // Resuelve IJwtService usando el ServiceProvider del request actual
        var jwtService = context.RequestServices.GetRequiredService<IJwtService>();

        // Extrae el token JWT del header Authorization (formato: "Bearer <token>")
        var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

        if (!string.IsNullOrEmpty(token))
        {
            try
            {
                // Valida el token JWT (firma, expiración, etc.)
                if (jwtService.ValidateToken(token))
                {
                    // Extrae el ID de usuario del token
                    var userId = jwtService.GetUserIdFromToken(token);
                    // Agrega el ID de usuario al contexto para uso en controladores
                    context.Items["UserId"] = userId;
                    // Crea los claims de usuario para el sistema de autorización de ASP.NET
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.NameIdentifier, userId.ToString())
                    };
                    // Asigna los claims al usuario actual del contexto
                    var identity = new ClaimsIdentity(claims, "Bearer");
                    context.User = new ClaimsPrincipal(identity);
                }
            }
            catch
            {
                // Si el token es inválido, no se lanza excepción aquí
                // El controlador puede decidir si requiere autorización
            }
        }
        // Continúa con el siguiente middleware o controlador
        await _next(context);
    }
} 