using System.Security.Claims;

namespace SchoolNexAPI.Helpers
{
    public class UserHelper
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserHelper(IHttpContextAccessor httpContextAccessor)
        {
            this._httpContextAccessor = httpContextAccessor;
        }

        public bool IsSuperAdmin()
        {
            var user = _httpContextAccessor.HttpContext?.User;
            if (user == null) return false;

            var userRoles = user.FindAll(ClaimTypes.Role).Select(c => c.Value);
            return userRoles.Contains("SuperAdmin");
        }
    }
}
