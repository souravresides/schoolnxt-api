using System.Security.Claims;

namespace SchoolNexAPI.Utilities.Helpers
{
    public class AppHelper : IAppHelper
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AppHelper(IHttpContextAccessor httpContextAccessor)
        {
            this._httpContextAccessor = httpContextAccessor;
        }

        public Guid GetUserId()
        {
            var userId = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return Guid.Parse(userId ?? throw new UnauthorizedAccessException());
        }
        public List<string> GetUserRoles()
        {
            return _httpContextAccessor.HttpContext?.User
                .Claims
                .Where(c => c.Type == ClaimTypes.Role)
                .Select(c => c.Value)
                .ToList() ?? new List<string>();
        }
    }
}
