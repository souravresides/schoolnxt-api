using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolNexAPI.Attributes;
using SchoolNexAPI.DTOs.School;
using SchoolNexAPI.DTOs.Subscription;
using SchoolNexAPI.Services.Abstract;

namespace SchoolNexAPI.Controllers
{
    //[Authorize(Roles ="SuperAdmin")]
    [Route("api/[controller]")]
    public class SchoolController : BaseController
    {
        private readonly ISchoolService _schoolService;

        public SchoolController(ISchoolService schoolService)
        {
            _schoolService = schoolService;
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] SchoolRequestDto dto)
        {
            var userId = GetUserId();
            dto.CreatedBy = userId.ToString();
            var id = await _schoolService.CreateSchoolAsync(dto);
            return Ok(id);
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAll()
        {
            var schools = await _schoolService.GetAllSchoolsAsync();
            return Ok(schools);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var school = await _schoolService.GetSchoolByIdAsync(id);
            return Ok(school);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] SchoolRequestDto dto)
        {
            await _schoolService.UpdateSchoolAsync(id, dto);
            return NoContent();
        }

        [HttpPut("{schoolId}/settings")]
        public async Task<IActionResult> UpdateSettings(Guid schoolId, [FromBody] SchoolSettingsRequestDto dto)
        {
            await _schoolService.UpdateSchoolSettingsAsync(schoolId, dto.Timezone, dto.Locale, dto.Currency);
            return NoContent();
        }

        [HttpPut("{schoolId}/upgrade-subscription")]
        public async Task<IActionResult> UpgradeSubscription(Guid schoolId, [FromBody] UpgradeSubscriptionDto dto)
        {
            await _schoolService.UpgradeSchoolSubscriptionAsync(schoolId, dto.SubscriptionTypeId, dto.SubscriptionTerm);
            return NoContent();
        }

    }
}
