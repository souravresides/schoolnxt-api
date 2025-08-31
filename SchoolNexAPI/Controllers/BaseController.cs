using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace SchoolNexAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
        protected Guid GetSchoolId()
        {
            var userRoles = User.FindAll(ClaimTypes.Role).Select(c => c.Value);
            if (userRoles.Contains("SuperAdmin"))
            {
                return Guid.Empty;
            }

            var schoolIdClaim = User.FindFirst("school_id")?.Value;
            return Guid.TryParse(schoolIdClaim, out var schoolId)
                ? schoolId
                : throw new UnauthorizedAccessException("Invalid or missing school_id.");
        }

        protected Guid GetUserId()
        {
            var subClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (Guid.TryParse(subClaim, out var userId))
            {
                return userId;
            }
            throw new UnauthorizedAccessException("Invalid or missing user ID (sub claim).");
        }

    }
}
