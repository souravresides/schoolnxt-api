using Microsoft.AspNetCore.Mvc;
using SchoolNexAPI.DTOs;
using SchoolNexAPI.Services.Abstract;

namespace SchoolNexAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AcademicYearController : BaseController
    {
        private readonly IAcademicYearService _service;
        private readonly ILogger<AcademicYearController> _logger;

        public AcademicYearController(
            IAcademicYearService service,
            ITenantContext tenant,
            ILogger<AcademicYearController> logger) : base(tenant, logger)
        {
            _service = service;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] Guid schoolId)
        {
            var result = await _service.GetAllAsync(GetSchoolId());
            _logger.LogInformation("Fetched {Count} academic years for SchoolId {SchoolId}", result.Count, schoolId);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateAcademicYearDto dto)
        {
            try
            {
                dto.SchoolId = GetSchoolId();
                var result = await _service.CreateAsync(dto.SchoolId, dto.Name, dto.StartDate, dto.EndDate, User.Identity?.Name ?? "system");

                _logger.LogInformation(
                    "Academic year {AcademicYearId} ({Name}) created successfully for SchoolId {SchoolId}",
                    result.Id, result.Name, dto.SchoolId);

                return CreatedAtAction(nameof(GetAll), new { schoolId = dto.SchoolId }, result);
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogWarning(ex, "Validation failed while creating academic year for SchoolId {SchoolId}", GetSchoolId());
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error while creating academic year for SchoolId {SchoolId}", GetSchoolId());
                return StatusCode(500, new { message = "Internal server error" });
            }
        }

        [HttpPut("{id}/activate")]
        public async Task<IActionResult> Activate(Guid id, [FromQuery] Guid schoolId)
        {
            try
            {
                var result = await _service.ActivateAsync(GetSchoolId(), id, User.Identity?.Name ?? "system");

                _logger.LogInformation(
                    "Academic year {AcademicYearId} activated successfully for SchoolId {SchoolId}",
                    id, schoolId);

                return Ok(result);
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogWarning(ex, "Activation failed for AcademicYear {AcademicYearId} in SchoolId {SchoolId}", id, schoolId);
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error while activating AcademicYear {AcademicYearId} in SchoolId {SchoolId}", id, schoolId);
                return StatusCode(500, new { message = "Internal server error" });
            }
        }

        [HttpPut("{id}/close")]
        public async Task<IActionResult> Close(Guid id, [FromQuery] Guid schoolId)
        {
            try
            {
                var result = await _service.CloseAsync(GetSchoolId(), id, User.Identity?.Name ?? "system");

                _logger.LogInformation(
                    "Academic year {AcademicYearId} closed successfully for SchoolId {SchoolId}",
                    id, schoolId);

                return Ok(result);
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogWarning(ex, "Closing failed for AcademicYear {AcademicYearId} in SchoolId {SchoolId}", id, schoolId);
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error while closing AcademicYear {AcademicYearId} in SchoolId {SchoolId}", id, schoolId);
                return StatusCode(500, new { message = "Internal server error" });
            }
        }

        [HttpGet("current")]
        public async Task<IActionResult> GetCurrent([FromQuery] Guid schoolId)
        {
            try
            {
                var result = await _service.GetCurrentAsync(GetSchoolId());
                if (result == null)
                {
                    _logger.LogWarning("No active academic year found for SchoolId {SchoolId}", schoolId);
                    return NotFound(new { message = "No active academic year found." });
                }

                _logger.LogInformation(
                    "Fetched current academic year {AcademicYearId} for SchoolId {SchoolId}",
                    result.Id, schoolId);

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching current academic year for SchoolId {SchoolId}", schoolId);
                return StatusCode(500, new { message = "Internal server error" });
            }
        }
    }
}
