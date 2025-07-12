using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolNexAPI.DTOs;
using SchoolNexAPI.Services.Abstract;

namespace SchoolNexAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomFieldController : ControllerBase
    {
        private readonly ICustomFieldService _customFieldService;

        public CustomFieldController(ICustomFieldService customFieldService)
        {
            _customFieldService = customFieldService;
        }

        [HttpGet("{schoolId}/{targetEntity}")]
        public async Task<IActionResult> GetAll(Guid schoolId, string targetEntity)
        {
            var fields = await _customFieldService.GetAllAsync(schoolId, targetEntity);
            return Ok(fields);
        }


        [HttpGet("field/{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var field = await _customFieldService.GetByIdAsync(id);
            if (field == null) return NotFound();
            return Ok(field);
        }

        [HttpPost("{schoolId}")]
        public async Task<IActionResult> Create(Guid schoolId, [FromBody] CreateCustomFieldRequest request)
        {
            var createdBy = User.Identity?.Name ?? "System"; // Customize as needed
            var result = await _customFieldService.CreateAsync(schoolId, request, createdBy);
            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateCustomFieldRequest request)
        {
            var updatedBy = User.Identity?.Name ?? "System";
            var result = await _customFieldService.UpdateAsync(request, updatedBy);
            return result ? Ok() : NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _customFieldService.DeleteAsync(id);
            return result ? Ok() : NotFound();
        }
    }
}
