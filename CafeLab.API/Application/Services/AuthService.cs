using CafeLab.API.Application.DTOs;
using CafeLab.API.Application.Interfaces;
using CafeLab.API.Domain.Model.Aggregates;
using CafeLab.API.Domain.Repositories;
using System.Threading.Tasks;
using BCrypt.Net;
using CafeLab.API.Application.Exceptions;

namespace CafeLab.API.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITokenService _tokenService;

        public AuthService(IUserRepository userRepository, IUnitOfWork unitOfWork, ITokenService tokenService)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
            _tokenService = tokenService;
        }

        public async Task<string> LoginAsync(LoginDto loginDto)
        {
            var user = await _userRepository.FindByUsernameAsync(loginDto.Username);

            if (user == null || !BCrypt.Net.BCrypt.Verify(loginDto.Password, user.PasswordHash))
            {
                throw new InvalidCredentialsException();
            }
            
            user.SetLastLoginDate();
            _userRepository.UpdateAsync(user);
            await _unitOfWork.CompleteAsync();

            return _tokenService.CreateToken(user);
        }

        public async Task<string> RegisterAsync(RegisterDto registerDto)
        {
            var existingUser = await _userRepository.FindByUsernameAsync(registerDto.Username);
            if (existingUser != null)
            {
                throw new UserAlreadyExistsException(registerDto.Username);
            }
            
            var passwordHash = BCrypt.Net.BCrypt.HashPassword(registerDto.Password);
            var user = new User(
                registerDto.Username,
                registerDto.Email,
                passwordHash,
                registerDto.FullName,
                Domain.Entities.Roles.User
            );

            await _userRepository.AddAsync(user);
            await _unitOfWork.CompleteAsync();
            
            return _tokenService.CreateToken(user);
        }
    }
} 