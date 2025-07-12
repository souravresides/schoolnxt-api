using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolNexAPI.DTOs;
using SchoolNexAPI.Services.Abstract;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace SchoolNexAPI.Controllers
{
    [Route("Auth")]    
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }
        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDto model)
        {
            var response = await _authService.RegisterAsync(model);
            if (!response.IsSuccess)
                return BadRequest(response);

            return Ok(response);
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto model)
        {
            var response = await _authService.LoginAsync(model);
            if (response.Is2FARequired)
                return Ok(response);

            if (!response.IsSuccess)
                return BadRequest(response);

            return Ok(response);
        }

        [Authorize]
        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
                return Unauthorized();

            await _authService.LogoutAsync(userId);

            return Ok(new { Message = "Logged out successfully." });
        }

        [AllowAnonymous]
        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenRequestDto model)
        {
            var response = await _authService.RefreshTokenAsync(model.Token, model.RefreshToken);
            if (!response.IsSuccess)
                return BadRequest(response);

            return Ok(response);
        }

        [AllowAnonymous]
        [HttpPost("verify-2fa")]
        public async Task<IActionResult> VerifyTwoFactor([FromBody] Verify2FACodeRequestDto model)
        {
            var response = await _authService.VerifyTwoFactorAsync(model);

            if (!response.IsSuccess)
                return BadRequest(response);

            return Ok(response);
        }


        [HttpGet("test")]
        public IActionResult Test()
        {
            return Ok("Test successful");
        }
    }
}
