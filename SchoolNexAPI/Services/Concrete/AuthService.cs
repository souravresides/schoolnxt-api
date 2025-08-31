using Azure.Core;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using SchoolNexAPI.DTOs;
using SchoolNexAPI.DTOs.Auth;
using SchoolNexAPI.Models;
using SchoolNexAPI.Repositories.Abstract;
using SchoolNexAPI.Security;
using SchoolNexAPI.Services.Abstract;
using SchoolNexAPI.Utilities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

namespace SchoolNexAPI.Services.Concrete
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<AppUserModel> _userManager;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly EmailSender _emailSender;
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuthService(UserManager<AppUserModel> userManager, IJwtTokenGenerator jwtTokenGenerator,
            EmailSender emailSender, IRefreshTokenRepository refreshTokenRepository, IHttpContextAccessor httpContextAccessor)
        {
            _userManager = userManager;
            _jwtTokenGenerator = jwtTokenGenerator;
            this._emailSender = emailSender;
            this._refreshTokenRepository = refreshTokenRepository;
            this._httpContextAccessor = httpContextAccessor;
        }
        public string GetClientIp()
        {
            var ip = _httpContextAccessor.HttpContext?.Request?.Headers["X-Forwarded-For"].FirstOrDefault()
                  ?? _httpContextAccessor.HttpContext?.Connection?.RemoteIpAddress?.ToString();

            return ip ?? "Unknown";
        }


        public async Task<AuthResponseDto> RegisterAsync(RegisterRequestDto model)
        {
            var user = new AppUserModel
            {
                Name = model.Name,
                Email = model.Email,
                UserName = model.Email,
                SchoolId = model.SchoolId
            };

            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(e => e.Description).ToList();
                return new AuthResponseDto { IsSuccess = false, Errors = errors };
            }

            await _userManager.AddToRoleAsync(user, model.Role); // Assign role during registration

            var roles = await _userManager.GetRolesAsync(user);
            var token = _jwtTokenGenerator.GenerateToken(user, roles);

            return new AuthResponseDto { IsSuccess = true, Token = token };
        }
        public async Task<AuthResponseDto> LoginAsync(LoginRequestDto model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null || !await _userManager.CheckPasswordAsync(user, model.Password))
            {
                return new AuthResponseDto { IsSuccess = false, Errors = new List<string> { "Invalid login attempt." } };
            }

            if (user.TwoFactorEnabled)
            {
                var code = await _userManager.GenerateTwoFactorTokenAsync(user, TokenOptions.DefaultEmailProvider);
                await _emailSender.SendAsync(user.Email, "Your 2FA Code", $"Your login code is: {code}");
                return new AuthResponseDto { Is2FARequired = true, TempUserId = user.Id };
            }

            var roles = await _userManager.GetRolesAsync(user);
            var token = _jwtTokenGenerator.GenerateToken(user, roles);

            // Generate Refresh Token
            var refreshToken = GenerateRefreshToken();

            // Save refresh token in DB
            var refreshTokenEntity = new RefreshTokenModel
            {
                Token = refreshToken,
                Expires = DateTime.UtcNow.AddDays(30),
                Created = DateTime.UtcNow,
                UserId = user.Id
            };
            _httpContextAccessor.HttpContext?.Response.Cookies.Append("refreshToken", refreshToken, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.None,
                Expires = DateTime.UtcNow.AddDays(30)
            });
            await _refreshTokenRepository.AddAsync(refreshTokenEntity);

            return new AuthResponseDto
            {
                IsSuccess = true,
                Token = token,
                User = new UserDto
                {
                    UserId = user.Id,
                    Email = user.Email,
                    Name = user.Name
                },
            };
        }

        public string GenerateRefreshToken()
        {
            var randomBytes = new byte[64];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomBytes);
            return Convert.ToBase64String(randomBytes);
        }
        public async Task<AuthResponseDto> RefreshTokenAsync(string accessToken, string refreshToken)
        {
            string? userId = null;
            RefreshTokenModel oldRefreshToken = null;
            if (!string.IsNullOrEmpty(accessToken))
            {
                var principal = _jwtTokenGenerator.GetPrincipalFromExpiredToken(accessToken);
                userId = principal?.FindFirstValue(ClaimTypes.NameIdentifier);
            }
            else
            {
                oldRefreshToken = await _refreshTokenRepository.GetByTokenAsync(refreshToken);
                userId = oldRefreshToken?.UserId;
            }

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return new AuthResponseDto { IsSuccess = false, Errors = new List<string> { "User not found." } };


            if (oldRefreshToken == null || oldRefreshToken.IsExpired || oldRefreshToken.UserId != user.Id || oldRefreshToken.IsUsed || oldRefreshToken.IsRevoked)
            {
                return new AuthResponseDto { IsSuccess = false, Errors = new List<string> { "Invalid or already used refresh token." } };
            }


            oldRefreshToken.IsUsed = true;
            await _refreshTokenRepository.UpdateAsync(oldRefreshToken);


            var roles = await _userManager.GetRolesAsync(user);
            var newAccessToken = _jwtTokenGenerator.GenerateToken(user, roles);
            var newRefreshToken = GenerateRefreshToken();
            var newTokenEntity = new RefreshTokenModel
            {
                Token = newRefreshToken,
                UserId = user.Id,
                Created = DateTime.UtcNow,
                Expires = DateTime.UtcNow.AddDays(30),
                IsUsed = false,
                IsRevoked = false,
                CreatedByIp = GetClientIp()
            };

            await _refreshTokenRepository.AddAsync(newTokenEntity);

            _httpContextAccessor.HttpContext?.Response.Cookies.Append("refreshToken", newRefreshToken, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.None,
                Expires = DateTime.UtcNow.AddDays(30)
            });

            return new AuthResponseDto
            {
                IsSuccess = true,
                Token = newAccessToken,
                User = new UserDto
                {
                    UserId = user.Id,
                    Email = user.Email,
                    Name = user.Name
                },
            };
        }

        public async Task<AuthResponseDto> ChangePasswordAsync(ChangePasswordDto model)
        {
            var user = await _userManager.FindByIdAsync(model.UserId);
            if (user == null)
            {
                return new AuthResponseDto
                {
                    IsSuccess = false,
                    Errors = new List<string> { "User not found." }
                };
            }

            var result = await _userManager.ChangePasswordAsync(user, model.CurrentPassword, model.NewPassword);
            if (!result.Succeeded)
            {
                return new AuthResponseDto
                {
                    IsSuccess = false,
                    Errors = result.Errors.Select(e => e.Description).ToList()
                };
            }

            return new AuthResponseDto { IsSuccess = true };
        }

        public async Task<AuthResponseDto> ForgotPasswordAsync(ForgotPasswordDto model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                return new AuthResponseDto
                {
                    IsSuccess = false,
                    Errors = new List<string> { "User not found." }
                };
            }

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var resetLink = $"https://yourapp.com/reset-password?email={user.Email}&token={Uri.EscapeDataString(token)}";

            await _emailSender.SendAsync(user.Email, "Reset Password", $"Click here to reset your password: {resetLink}");

            return new AuthResponseDto { IsSuccess = true };
        }

        public async Task<AuthResponseDto> ResetPasswordAsync(ResetPasswordDto model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                return new AuthResponseDto
                {
                    IsSuccess = false,
                    Errors = new List<string> { "User not found." }
                };
            }

            var result = await _userManager.ResetPasswordAsync(user, model.Token, model.NewPassword);
            if (!result.Succeeded)
            {
                return new AuthResponseDto
                {
                    IsSuccess = false,
                    Errors = result.Errors.Select(e => e.Description).ToList()
                };
            }

            return new AuthResponseDto { IsSuccess = true };
        }



        public async Task LogoutAsync(string userId)
        {
            await _refreshTokenRepository.DeleteTokensByUserIdAsync(userId);
        }

        public async Task<AuthResponseDto> VerifyTwoFactorAsync(Verify2FACodeRequestDto model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                return new AuthResponseDto { IsSuccess = false, Errors = new List<string> { "Invalid email." } };
            }

            var isValid = await _userManager.VerifyTwoFactorTokenAsync(user, TokenOptions.DefaultEmailProvider, model.Code);
            if (!isValid)
            {
                return new AuthResponseDto { IsSuccess = false, Errors = new List<string> { "Invalid verification code." } };
            }

            var roles = await _userManager.GetRolesAsync(user);
            var token = _jwtTokenGenerator.GenerateToken(user, roles);

            return new AuthResponseDto
            {
                IsSuccess = true,
                Token = token,
                User = new UserDto
                {
                    UserId = user.Id,
                    Email = user.Email,
                    Name = user.Name
                },
            };
        }
    }
}
