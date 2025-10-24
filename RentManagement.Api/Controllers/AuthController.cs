using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RentManagement.Api.DTOs;
using RentManagement.Api.Interfaces;
using System.Security.Claims;

namespace RentManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto model)
        {
            var (success, message) = await _authService.RegisterUserAsync(model);
            if (!success) return BadRequest(new { Message = message });
            return Ok(new { Message = message });
        }

        [HttpPost("login")]
        public async Task<ActionResult<LoginResponseDto>> Login([FromBody] LoginDto model)
        {
            if (!ModelState.IsValid) return ValidationProblem(ModelState);

            var result = await _authService.LoginUserAsync(model);
            if (result is null) return Unauthorized("Invalid credentials");

            return Ok(result);
        }

        [HttpPost("confirm-email")]
        public async Task<IActionResult> ConfirmEmail([FromQuery] string userId, [FromQuery] string token)
        {
            var succeeded = await _authService.ConfirmEmailAsync(userId, token);
            if (!succeeded) return BadRequest(new { Message = "Email confirmation failed. Invalid link or user." });
            return Ok(new { Message = "Email confirmed successfully. You can now log in." });
        }

        [HttpGet("TestAuth")]
        [Authorize]
        public IActionResult TestAuth()
        {
            var userEmail = User.Identity?.Name;
            var roles = string.Join(", ", User.Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value));

            return Ok(new { Message = $"Hello, {userEmail}! You are successfully authenticated.", Roles = roles });
        }
    }
}