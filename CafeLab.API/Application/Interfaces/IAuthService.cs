using CafeLab.API.Application.DTOs;
using System.Threading.Tasks;

namespace CafeLab.API.Application.Interfaces
{
    public interface IAuthService
    {
        Task<string> RegisterAsync(RegisterDto registerDto);
        Task<string> LoginAsync(LoginDto loginDto);
    }
} 