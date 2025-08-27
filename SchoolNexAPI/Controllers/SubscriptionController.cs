using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolNexAPI.DTOs.Subscription;
using SchoolNexAPI.Services.Abstract;

namespace SchoolNexAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubscriptionController : BaseController
    {
        private readonly ISubscriptionService _subscriptionService;

        public SubscriptionController(ISubscriptionService subscriptionService)
        {
            _subscriptionService = subscriptionService;
        }

        [HttpPost("purchase")]
        //[Authorize(Roles = "SchoolAdmin")]
        public async Task<IActionResult> PurchaseSubscription([FromBody] SubscriptionPurchaseRequest request)
        {
            var schoolId = GetSchoolId();
            var result = await _subscriptionService.CreateSubscriptionOrderAsync(schoolId, request);
            return Ok(result);
        }
    }
}
