using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolNexAPI.DTOs;
using SchoolNexAPI.DTOs.Auth;
using SchoolNexAPI.Services.Abstract;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace SchoolNexAPI.Controllers
{
    [Route("api/[controller]")]    
    public class AuthController : BaseController
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

            return Ok(new { Message = "User account created successfully." });
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

            Response.Cookies.Delete("accessToken");
            Response.Cookies.Delete("refreshToken");
            await _authService.LogoutAsync(userId);

            return Ok(new { Message = "Logged out successfully." });
        }

        [AllowAnonymous]
        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken()
        {
            var accessToken = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            var refreshToken = Request.Cookies["refreshToken"];

            if (string.IsNullOrEmpty(refreshToken))
                return BadRequest(new { Message = "Invalid session" });

            var response = await _authService.RefreshTokenAsync(accessToken, refreshToken);
            if (!response.IsSuccess)
                return Unauthorized(response);

            return Ok(response);
        }

        [AllowAnonymous]
        [HttpPost("verify-2fa")]
        public async Task<IActionResult> VerifyTwoFactor([FromBody] Verify2FACodeRequestDto model)
        {
            var response = await _authService.VerifyTwoFactorAsync(model);

            if (!response.IsSuccess)
                return BadRequest(response);

            Response.Cookies.Append("refreshToken", response.RefreshToken, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict,
                Expires = DateTime.UtcNow.AddDays(30)
            });

            return Ok(new { token = response.Token });
        }

        [Authorize]
        [HttpGet("me")]
        public IActionResult Me()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return Ok(new { userId });
        }
    }
}
