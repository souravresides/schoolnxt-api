using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolNexAPI.DTOs;
using SchoolNexAPI.Models;
using SchoolNexAPI.Services.Abstract;

namespace SchoolNexAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AcademicYearController : BaseController
    {
        private readonly IAcademicYearService _service;

        public AcademicYearController(IAcademicYearService service)
        {
            _service = service;
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAll()
        {
            var academicYears = await _service.GetAllAsync();
            return Ok(academicYears);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var academicYear = await _service.GetByIdAsync(id);
            if (academicYear == null) return NotFound();
            return Ok(academicYear);
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] CreateAcademicYearDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var schoolId = GetSchoolId();
            var createdBy = User.Identity?.Name ?? "System";
            var result = await _service.CreateAsync(schoolId,createdBy,dto);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] CreateAcademicYearDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var schoolId = GetSchoolId();
            var updatedBy = User.Identity?.Name ?? "System";
            var updated = await _service.UpdateAsync(id,schoolId,updatedBy,dto);
            if (updated == null) return NotFound();

            return Ok(updated);
        }


        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var deleted = await _service.DeleteAsync(id);
            if (!deleted) return NotFound();
            return NoContent();
        }
    }
}
