using Microsoft.AspNetCore.Mvc;
using SchoolNexAPI.Models;
using SchoolNexAPI.Services.Abstract;

namespace SchoolNexAPI.Controllers
{
    [Route("api/[controller]")]
    public class SubscriptionTypeController : BaseController
    {
        private readonly ISubscriptionTypeService _subscriptionService;

        public SubscriptionTypeController(ISubscriptionTypeService subscriptionService, ITenantContext tenant, ILogger<SubscriptionTypeController> logger) : base(tenant, logger)
        {
            _subscriptionService = subscriptionService;
        }

        [HttpGet("plans")]
        public async Task<IActionResult> GetAll()
        {
            var plans = await _subscriptionService.GetAllAsync();
            return Ok(plans);
        }
    }
}
