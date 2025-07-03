using CafeLab.API.Application.DTOs;
using CafeLab.API.Application.Interfaces;
using CafeLab.API.Application.Exceptions;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;
using Microsoft.Extensions.Localization;
using Microsoft.AspNetCore.Authorization;
using CafeLab.API.Domain.Interfaces;
using CafeLab.API.Domain.Model.Aggregates;
using System.Security.Claims;

namespace CafeLab.API.Interfaces.REST.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IStringLocalizer<SharedResource> _localizer;
        private readonly IUserRepository _userRepository;

        public UsersController(
            IAuthService authService, 
            IStringLocalizer<SharedResource> localizer,
            IUserRepository userRepository)
        {
            _authService = authService;
            _localizer = localizer;
            _userRepository = userRepository;
        }

        [HttpPost("sign-up")]
        public async Task<IActionResult> Register(RegisterDto registerDto)
        {
            try
            {
                var ipAddress = HttpContext.Connection.RemoteIpAddress?.ToString() ?? "Unknown";
                var response = await _authService.RegisterAsync(registerDto, ipAddress);
                return Ok(response);
            }
            catch (UserAlreadyExistsException ex)
            {
                return BadRequest(new { 
                    success = false, 
                    message = ex.Message 
                });
            }
            catch (Exception)
            {
                return StatusCode(500, new { 
                    success = false, 
                    message = _localizer["UnexpectedError"] 
                });
            }
        }

        [HttpPost("sign-in")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            try
            {
                var ipAddress = HttpContext.Connection.RemoteIpAddress?.ToString() ?? "Unknown";
                var response = await _authService.LoginAsync(loginDto, ipAddress);
                return Ok(response);
            }
            catch (InvalidCredentialsException ex)
            {
                return Unauthorized(new { 
                    success = false, 
                    message = ex.Message ?? _localizer["InvalidCredentials"] 
                });
            }
            catch (Exception)
            {
                return StatusCode(500, new { 
                    success = false, 
                    message = _localizer["UnexpectedError"] 
                });
            }
        }

        [HttpGet("profile")]
        [Authorize]
        public async Task<IActionResult> GetProfile()
        {
            try
            {
                var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
                if (userIdClaim == null || !int.TryParse(userIdClaim.Value, out int userId))
                {
                    return Unauthorized(new { 
                        success = false, 
                        message = "Invalid token" 
                    });
                }

                var user = await _userRepository.GetByIdAsync(userId);
                if (user == null)
                {
                    return NotFound(new { 
                        success = false, 
                        message = "User not found" 
                    });
                }

                var userDto = new UserDto
                {
                    Id = user.Id,
                    Username = user.Username,
                    Email = user.Email,
                    FullName = user.FullName,
                    Role = user.Role,
                    IsActive = user.IsActive,
                    LastLoginAt = user.LastLoginDate,
                    CreatedAt = user.CreatedAt
                };

                return Ok(new { 
                    success = true, 
                    data = userDto 
                });
            }
            catch (Exception)
            {
                return StatusCode(500, new { 
                    success = false, 
                    message = _localizer["UnexpectedError"] 
                });
            }
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllUsers()
        {
            try
            {
                var users = await _userRepository.GetAllAsync();
                var userDtos = users.Select(u => new UserDto
                {
                    Id = u.Id,
                    Username = u.Username,
                    Email = u.Email,
                    FullName = u.FullName,
                    Role = u.Role,
                    IsActive = u.IsActive,
                    LastLoginAt = u.LastLoginDate,
                    CreatedAt = u.CreatedAt
                });

                return Ok(new { 
                    success = true, 
                    data = userDtos 
                });
            }
            catch (Exception)
            {
                return StatusCode(500, new { 
                    success = false, 
                    message = _localizer["UnexpectedError"] 
                });
            }
        }
    }
} 