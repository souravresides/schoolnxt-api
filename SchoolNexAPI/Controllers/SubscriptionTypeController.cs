using Microsoft.AspNetCore.Mvc;
using SchoolNexAPI.Models;
using SchoolNexAPI.Services.Abstract;

namespace SchoolNexAPI.Controllers
{
    [Route("SubscriptionType")]
    public class SubscriptionTypeController : BaseController
    {
        private readonly ISubscriptionTypeService _subscriptionService;

        public SubscriptionTypeController(ISubscriptionTypeService subscriptionService)
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
