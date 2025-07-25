using SchoolNexAPI.Services.Abstract;
using SchoolNexAPI.Services.Concrete;
using System.Security.Claims;

namespace SchoolNexAPI.Middleware
{
    public class SubscriptionValidationMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<SubscriptionValidationMiddleware> _logger;

        public SubscriptionValidationMiddleware(RequestDelegate next, ILogger<SubscriptionValidationMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context, ISchoolSubscriptionService subscriptionService)
        {
            var path = context.Request.Path.Value?.ToLower();
            var allowedPaths = new[]
            {
                "/auth/login",
                "/auth/register",
                "/auth/refresh-token",
                "/subscriptiontype/plans",
                "/swagger"
            };
            if (path != null && allowedPaths.Any(p => path.StartsWith(p, StringComparison.OrdinalIgnoreCase)))
            {
                await _next(context);
                return;
            }

            //try
            //{
                var user = context.User;
                var schoolIdClaim = user.FindFirst("school_id")?.Value;

                var userRoles = user.FindAll(ClaimTypes.Role).Select(c => c.Value);
                if (userRoles.Contains("SuperAdmin"))
                {
                    await _next(context);
                    return;
                }

                if (string.IsNullOrWhiteSpace(schoolIdClaim))
                {
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    await context.Response.WriteAsync("SchoolId missing in token.");
                    return;
                }

                var schoolId = Guid.Parse(schoolIdClaim);

                var isSubscriptionValid = await subscriptionService.IsSubscriptionValidAsync(schoolId);
                if (!isSubscriptionValid)
                {
                    context.Response.StatusCode = StatusCodes.Status403Forbidden;
                    await context.Response.WriteAsync("Your subscription has expired or is invalid.");
                    return;
                }

                // Proceed to next middleware
                await _next(context);
            //}
            //catch (Exception ex)
            //{
            //    _logger.LogError(ex, "Error validating subscription");
            //    context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            //    await context.Response.WriteAsync("Internal server error while validating subscription.");
            //}
        }
    }
}
