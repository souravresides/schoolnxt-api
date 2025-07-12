using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using SchoolNexAPI.Services.Abstract;
using System.Security.Claims;

namespace SchoolNexAPI.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
    public class AuthorizeFeatureAttribute : Attribute, IAsyncAuthorizationFilter
    {
        private readonly string _featureName;

        public AuthorizeFeatureAttribute(string featureName)
        {
            _featureName = featureName;
        }

        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            var subscriptionService = context.HttpContext.RequestServices.GetService(typeof(ISchoolSubscriptionService)) as ISchoolSubscriptionService;
            var userRoles = context.HttpContext.User.FindAll(ClaimTypes.Role).Select(c => c.Value);
            if (userRoles.Contains("SuperAdmin"))
            {
                return; // SuperAdmin bypasses feature restrictions
            }
            var user = context.HttpContext.User;
            var schoolIdClaim = user.FindFirst("school_id")?.Value;

            if (string.IsNullOrWhiteSpace(schoolIdClaim))
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            var schoolId = Guid.Parse(schoolIdClaim);
            
            var hasFeature = await subscriptionService.HasFeatureAccessAsync(schoolId, _featureName);

            if (!hasFeature)
            {
                //context.Result = new ForbidResult();
                context.Result = new JsonResult(new
                {
                    StatusCode = 403,
                    Error = "Feature access denied",
                    Message = $"Your subscription does not allow to access this feature."
                })
                {
                    StatusCode = StatusCodes.Status403Forbidden
                };
            }
        }
    }
}
