using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SchoolNexAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
        protected Guid GetSchoolId()
        {
            var schoolIdClaim = User.FindFirst("school_id")?.Value;
            return Guid.TryParse(schoolIdClaim, out var schoolId)
                ? schoolId
                : throw new UnauthorizedAccessException("Invalid or missing school_id.");
        }
    }
}
