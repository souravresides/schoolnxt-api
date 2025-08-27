using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolNexAPI.DTOs.Employee;
using SchoolNexAPI.Services.Abstract;

namespace SchoolNexAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : BaseController
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateEmployeeDto dto)
        {
            var schoolId = GetSchoolId();
            var createdBy = User.Identity?.Name ?? "system";

            var result = await _employeeService.CreateAsync(dto, schoolId, createdBy);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var schoolId = GetSchoolId();
            var employees = await _employeeService.GetAllAsync(schoolId);
            return Ok(employees);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var schoolId = GetSchoolId();
            var employee = await _employeeService.GetByIdAsync(id, schoolId);
            return employee == null ? NotFound() : Ok(employee);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateEmployeeDto dto)
        {
            var schoolId = GetSchoolId();
            var updatedBy = User.Identity?.Name ?? "system";

            var result = await _employeeService.UpdateAsync(id, dto, schoolId, updatedBy);
            return result == null ? NotFound() : Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var schoolId = GetSchoolId();
            var deletedBy = User.Identity?.Name ?? "system";

            var success = await _employeeService.DeleteAsync(id, schoolId, deletedBy);
            return success ? Ok() : NotFound();
        }
    }
}
