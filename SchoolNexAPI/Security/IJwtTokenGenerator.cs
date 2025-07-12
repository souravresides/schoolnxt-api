using SchoolNexAPI.Models;
using System.Security.Claims;

namespace SchoolNexAPI.Security
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(AppUserModel user, IEnumerable<string> roles);
        ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
    }
}
