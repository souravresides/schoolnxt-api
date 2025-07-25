using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolNexAPI.DTOs;
using SchoolNexAPI.Services.Abstract;

namespace SchoolNexAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class StudentController : BaseController
    {
        private readonly IStudentService _studentService;
        private readonly ILogger<StudentController> _logger;

        public StudentController(IStudentService studentService, ILogger<StudentController> logger)
        {
            _studentService = studentService;
            _logger = logger;
        }

        [HttpGet("getall")]
        public async Task<IActionResult> GetAllStudents()
        {
            var schoolId = GetSchoolId();
            var students = await _studentService.GetAllAsync(schoolId);
            return Ok(students);
        }

        /// <summary>
        /// Get student details by ID.
        /// </summary>
        /// <param name="id">Student ID</param>
        /// <returns>Student details</returns>
        [HttpGet("getstudent/{id}")]
        public async Task<IActionResult> GetStudentById(Guid id)
        {
            var student = await _studentService.GetByIdAsync(id);
            return student == null ? NotFound() : Ok(student);
        }

        [HttpPost("createstudent")]
        public async Task<IActionResult> CreateStudent([FromBody] CreateStudentRequest createStudentRequest)
        {
            var schoolId = GetSchoolId();
            var createdBy = User.Identity?.Name ?? "System";
            var newStudent = await _studentService.CreateAsync(schoolId, createStudentRequest, createdBy);
            return CreatedAtAction(nameof(GetStudentById), new { id = newStudent.Id }, newStudent);
        }

        [HttpPut("updatestudent/{id}")]
        public async Task<IActionResult> UpdateStudent(Guid id, [FromBody] UpdateStudentRequest updateStudentRequest)
        {
            var updatedBy = User.Identity?.Name ?? "System";
            var updated = await _studentService.UpdateAsync(id, updateStudentRequest, updatedBy);
            return updated ? NoContent() : NotFound();
        }

        [HttpDelete("deletestudent/{id}")]
        public async Task<IActionResult> DeleteStudent(Guid id)
        {
            var deleted = await _studentService.DeleteAsync(id);
            return deleted ? NoContent() : NotFound();
        }
    }
}
