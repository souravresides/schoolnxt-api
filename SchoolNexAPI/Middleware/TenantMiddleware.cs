using SchoolNexAPI.Services.Abstract;
using System.Security.Claims;

namespace SchoolNexAPI.Middleware
{
    public class TenantMiddleware
    {
        private readonly RequestDelegate _next;
        public TenantMiddleware(RequestDelegate next) { _next = next; }

        public async Task InvokeAsync(HttpContext context, ITenantContext tenant)
        {
            if (context.User?.Identity?.IsAuthenticated == true)
            {
                var uid = context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (Guid.TryParse(uid, out var guid)) tenant.CurrentUserId = guid;

                tenant.CurrentUserName = context.User.Identity?.Name;
                tenant.CurrentUserRoles = context.User.FindAll(ClaimTypes.Role).Select(c => c.Value).ToList();

                // Optional school claim; SuperAdmin tokens won't have it
                var schoolClaim = context.User.FindFirst("school_id")?.Value;
                if (Guid.TryParse(schoolClaim, out var schoolId)) tenant.CurrentSchoolId = schoolId;

                // Optional header for AY
                if (context.Request.Headers.TryGetValue("X-AcademicYear-Id", out var ayVals))
                {
                    if (Guid.TryParse(ayVals.FirstOrDefault(), out var ayId)) tenant.CurrentAcademicYearId = ayId;
                }
            }

            await _next(context);
        }
    }
}
