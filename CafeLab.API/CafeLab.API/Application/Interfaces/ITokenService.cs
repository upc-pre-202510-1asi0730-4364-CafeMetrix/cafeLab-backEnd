using CafeLab.API.Domain.Model.Aggregates;

namespace CafeLab.API.Application.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(User user);
    }
} 