using CafeLab.API.Application.DTOs;
using System.Threading.Tasks;

namespace CafeLab.API.Application.Interfaces
{
    /// <summary>
    /// Interfaz para el servicio de autenticación.
    /// </summary>
    public interface IAuthService
    {
        /// <summary>
        /// Autentica un usuario con username y password.
        /// </summary>
        Task<AuthResponseDto> LoginAsync(LoginDto loginDto, string ipAddress);

        /// <summary>
        /// Registra un nuevo usuario.
        /// </summary>
        Task<AuthResponseDto> RegisterAsync(RegisterDto registerDto, string ipAddress);

        /// <summary>
        /// Renueva el token de acceso usando un refresh token.
        /// </summary>
        Task<AuthResponseDto> RefreshTokenAsync(RefreshTokenDto refreshTokenDto);

        /// <summary>
        /// Cierra la sesión del usuario.
        /// </summary>
        Task<AuthResponseDto> LogoutAsync(string refreshToken);

        /// <summary>
        /// Obtiene el perfil de un usuario.
        /// </summary>
        Task<UserResponseDto> GetUserProfileAsync(int userId);

        /// <summary>
        /// Obtiene todos los usuarios del sistema.
        /// </summary>
        Task<UsersResponseDto> GetAllUsersAsync();
    }
} 