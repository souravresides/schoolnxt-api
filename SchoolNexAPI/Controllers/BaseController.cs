using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolNexAPI.Services.Abstract;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace SchoolNexAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
        protected readonly ITenantContext _tenant;
        protected readonly ILogger _log;
        public BaseController(ITenantContext tenant, ILogger logger)
        {
            _tenant = tenant;
            _log = logger;
        }
        protected Guid GetSchoolIdFromClaims()
        {
            var schoolIdClaim = User.FindFirst("school_id")?.Value;
            if (Guid.TryParse(schoolIdClaim, out var schoolId))
                return schoolId;

            throw new UnauthorizedAccessException("Invalid or missing school_id.");
        }
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

        protected Guid RequireSchoolId(Guid? suppliedSchoolId = null)
        {
            var isSuper = _tenant.CurrentUserRoles?.Contains("SuperAdmin") ?? false;
            if (isSuper)
            {
                if (suppliedSchoolId.HasValue) return suppliedSchoolId.Value;
                if (_tenant.CurrentSchoolId.HasValue) return _tenant.CurrentSchoolId.Value;
                throw new InvalidOperationException("SuperAdmin must supply schoolId in request when acting on a school resource.");
            }

            if (!_tenant.CurrentSchoolId.HasValue) throw new UnauthorizedAccessException("School scope missing in token.");
            if (suppliedSchoolId.HasValue && suppliedSchoolId.Value != _tenant.CurrentSchoolId.Value) throw new UnauthorizedAccessException("Cannot act on other school's data.");
            return _tenant.CurrentSchoolId.Value;
        }

    }
}
