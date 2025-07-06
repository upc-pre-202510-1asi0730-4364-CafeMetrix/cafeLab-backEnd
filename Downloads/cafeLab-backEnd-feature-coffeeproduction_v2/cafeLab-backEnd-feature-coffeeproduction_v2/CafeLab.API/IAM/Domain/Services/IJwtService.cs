using CafeLab.API.IAM.Domain.Model.Aggregates;

namespace CafeLab.API.IAM.Domain.Services;

/// <summary>
/// JWT service interface
/// </summary>
public interface IJwtService
{
    string GenerateToken(User user);
    bool ValidateToken(string token);
    int GetUserIdFromToken(string token);
} 