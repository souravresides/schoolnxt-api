using SchoolNexAPI.Models;
using System.Security.Claims;

namespace SchoolNexAPI.Utilities
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(AppUserModel user, IEnumerable<string> roles);
        ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
    }
}
