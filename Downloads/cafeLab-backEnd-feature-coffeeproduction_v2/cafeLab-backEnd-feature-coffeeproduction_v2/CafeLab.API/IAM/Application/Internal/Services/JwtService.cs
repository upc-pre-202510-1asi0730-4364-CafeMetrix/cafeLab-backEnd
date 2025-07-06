using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using CafeLab.API.IAM.Domain.Model.Aggregates;
using CafeLab.API.IAM.Domain.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace CafeLab.API.IAM.Application.Internal.Services;

/// <summary>
/// JWT service implementation
/// 
/// Este servicio maneja todas las operaciones relacionadas con JWT (JSON Web Tokens)
/// para la autenticación y autorización en el sistema.
/// 
/// Responsabilidades:
/// - Generar tokens JWT seguros para usuarios autenticados
/// - Validar tokens JWT existentes
/// - Extraer información del usuario desde tokens
/// - Configurar parámetros de seguridad del token
/// 
/// El servicio utiliza la configuración de JWT desde appsettings.json
/// para mantener la flexibilidad y seguridad.
/// </summary>
public class JwtService : IJwtService
{
    private readonly IConfiguration _configuration;

    public JwtService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string GenerateToken(User user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_configuration["JwtSettings:SecretKey"] ?? "your-secret-key-here-make-it-long-enough-for-security");
        
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Email, user.Email.Address),
                new Claim(ClaimTypes.Role, user.Role)
            }),
            Expires = DateTime.UtcNow.AddHours(24),
            Issuer = _configuration["JwtSettings:Issuer"] ?? "CafeLab",
            Audience = _configuration["JwtSettings:Audience"] ?? "CafeLabUsers",
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

    public bool ValidateToken(string token)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_configuration["JwtSettings:SecretKey"] ?? "your-secret-key-here-make-it-long-enough-for-security");

        try
        {
            tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = true,
                ValidIssuer = _configuration["JwtSettings:Issuer"] ?? "CafeLab",
                ValidateAudience = true,
                ValidAudience = _configuration["JwtSettings:Audience"] ?? "CafeLabUsers",
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero
            }, out SecurityToken validatedToken);

            return true;
        }
        catch
        {
            return false;
        }
    }

    public int GetUserIdFromToken(string token)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_configuration["JwtSettings:SecretKey"] ?? "your-secret-key-here-make-it-long-enough-for-security");

        try
        {
            var principal = tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = true,
                ValidIssuer = _configuration["JwtSettings:Issuer"] ?? "CafeLab",
                ValidateAudience = true,
                ValidAudience = _configuration["JwtSettings:Audience"] ?? "CafeLabUsers",
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero
            }, out SecurityToken validatedToken);

            var userIdClaim = principal.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim != null && int.TryParse(userIdClaim.Value, out int userId))
                return userId;

            throw new InvalidOperationException("Invalid token: User ID not found");
        }
        catch
        {
            throw new InvalidOperationException("Invalid token");
        }
    }
} 