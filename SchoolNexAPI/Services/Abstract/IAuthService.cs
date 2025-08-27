using SchoolNexAPI.DTOs;
using SchoolNexAPI.DTOs.Auth;

namespace SchoolNexAPI.Services.Abstract
{
    public interface IAuthService
    {
        Task<AuthResponseDto> RegisterAsync(RegisterRequestDto model);
        Task<AuthResponseDto> LoginAsync(LoginRequestDto model);
        Task<AuthResponseDto> VerifyTwoFactorAsync(Verify2FACodeRequestDto model);
        Task LogoutAsync(string userId);
        Task<AuthResponseDto> RefreshTokenAsync(string token, string refreshToken);
    }
}
