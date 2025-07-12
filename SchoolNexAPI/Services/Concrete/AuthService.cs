using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using SchoolNexAPI.DTOs;
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

        public AuthService(UserManager<AppUserModel> userManager, IJwtTokenGenerator jwtTokenGenerator, EmailSender emailSender, IRefreshTokenRepository refreshTokenRepository)
        {
            _userManager = userManager;
            _jwtTokenGenerator = jwtTokenGenerator;
            this._emailSender = emailSender;
            this._refreshTokenRepository = refreshTokenRepository;
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
                return new AuthResponseDto { Is2FARequired = true, TempUserId =  user.Id};
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
            await _refreshTokenRepository.AddAsync(refreshTokenEntity);

            return new AuthResponseDto
            {
                IsSuccess = true,
                Token = token,
                RefreshToken = refreshToken
            };
        }

        public string GenerateRefreshToken()
        {
            var randomBytes = new byte[64];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomBytes);
            return Convert.ToBase64String(randomBytes);
        }
        public async Task<AuthResponseDto> RefreshTokenAsync(string token, string refreshToken)
        {
            var principal = _jwtTokenGenerator.GetPrincipalFromExpiredToken(token);
            var userId = principal?.FindFirstValue(ClaimTypes.NameIdentifier);

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return new AuthResponseDto { IsSuccess = false, Errors = new List<string> { "User not found." } };

            var savedRefreshToken = await _refreshTokenRepository.GetByTokenAsync(refreshToken);
            if (savedRefreshToken == null || savedRefreshToken.IsExpired || savedRefreshToken.UserId != user.Id)
                return new AuthResponseDto { IsSuccess = false, Errors = new List<string> { "Invalid refresh token." } };

            var roles = await _userManager.GetRolesAsync(user);
            var newToken = _jwtTokenGenerator.GenerateToken(user, roles);
            var newRefreshToken = GenerateRefreshToken();

            // Update the refresh token
            savedRefreshToken.Token = newRefreshToken;
            savedRefreshToken.Expires = DateTime.UtcNow.AddDays(30);
            savedRefreshToken.Created = DateTime.UtcNow;
            await _refreshTokenRepository.UpdateAsync(savedRefreshToken);

            return new AuthResponseDto
            {
                IsSuccess = true,
                Token = newToken,
                RefreshToken = newRefreshToken
            };
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

            return new AuthResponseDto { IsSuccess = true, Token = token };
        }
    }
}
