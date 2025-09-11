using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolNexAPI.DTOs;
using SchoolNexAPI.DTOs.Employee;
using SchoolNexAPI.Services.Abstract;

namespace SchoolNexAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : BaseController
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService, ITenantContext tenant, ILogger<EmployeeController> logger) : base(tenant, logger)
        {
            _employeeService = employeeService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateStaff([FromBody] EmployeeDto dto)
        {
            var staff = await _employeeService.CreateStaffAsync(dto, GetSchoolId());
            return Ok(staff);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetStaff(Guid id)
        {
            var staff = await _employeeService.GetStaffByIdAsync(id, GetSchoolId());
            if (staff == null) return NotFound();
            return Ok(staff);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllStaff()
        {
            var staffList = await _employeeService.GetAllStaffAsync(GetSchoolId());
            return Ok(staffList);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStaff(Guid id,[FromBody] EmployeeDto dto)
        {
            var staff = await _employeeService.UpdateStaffAsync(id, dto, GetSchoolId());
            if (staff == null) return NotFound();
            return Ok(staff);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStaff(Guid id)
        {
            var deleted = await _employeeService.DeleteStaffAsync(id, GetSchoolId());
            if (!deleted) return NotFound();
            return NoContent();
        }
    }
}
