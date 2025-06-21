using CafeLab.API.Application.DTOs;
using CafeLab.API.Application.Interfaces;
using CafeLab.API.Application.Exceptions;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;
using Microsoft.Extensions.Localization;

namespace CafeLab.API.Interfaces.REST.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")] // Added versioning
    public class UsersController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IStringLocalizer<SharedResource> _localizer;

        public UsersController(IAuthService authService, IStringLocalizer<SharedResource> localizer)
        {
            _authService = authService;
            _localizer = localizer;
        }

        [HttpPost("sign-up")] // More conventional name
        public async Task<IActionResult> Register(RegisterDto registerDto)
        {
            try
            {
                var token = await _authService.RegisterAsync(registerDto);
                return Ok(new { token });
            }
            catch (UserAlreadyExistsException)
            {
                return BadRequest(new { message = _localizer["UserAlreadyExists"] });
            }
            catch (Exception)
            {
                return StatusCode(500, new { message = _localizer["UnexpectedError"] });
            }
        }

        [HttpPost("sign-in")] // More conventional name
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            try
            {
                var token = await _authService.LoginAsync(loginDto);
                return Ok(new { token });
            }
            catch (InvalidCredentialsException)
            {
                return Unauthorized(new { message = _localizer["InvalidCredentials"] });
            }
            catch (Exception)
            {
                return StatusCode(500, new { message = _localizer["UnexpectedError"] });
            }
        }
    }
} 