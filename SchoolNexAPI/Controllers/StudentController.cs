using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolNexAPI.DTOs;
using SchoolNexAPI.Services.Abstract;

namespace SchoolNexAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : BaseController
    {
        private readonly IStudentService _studentService;

        public StudentController(IStudentService studentService, ITenantContext tenant, ILogger<PaymentsController> logger) : base(tenant, logger)
        {
            _studentService = studentService;
        }

        [HttpGet("getall")]
        public async Task<IActionResult> GetAllStudents([FromQuery] string status = "all")
        {
            var schoolId = GetSchoolId();
            var students = await _studentService.GetAllAsync(schoolId, status);
            return Ok(students);
        }

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
            var updated = await _studentService.UpdateAsync(id, GetSchoolId(), updateStudentRequest, updatedBy);
            return updated ? NoContent() : NotFound();
        }

        [HttpDelete("deletestudent/{id}")]
        public async Task<IActionResult> DeleteStudent(Guid id)
        {
            var deleted = await _studentService.DeleteAsync(id);
            return deleted ? NoContent() : NotFound();
        }

        [HttpPost("uploadphoto/{id}")]
        public async Task<IActionResult> UploadPhoto(Guid id, IFormFile photo)
        {
            if (photo == null || photo.Length == 0)
                return BadRequest("Invalid photo file.");

            var schoolId = GetSchoolId();
            var updatedBy = User.Identity?.Name ?? "System";

            var photoPath = await _studentService.UploadPhotoAsync(id, schoolId, photo, updatedBy);

            return photoPath != null
                ? Ok(new { Message = "Photo uploaded successfully", PhotoUrl = photoPath })
                : StatusCode(500, "Error uploading photo");
        }

        [HttpGet("classes-sections")]
        public async Task<IActionResult> GetClassesAndSections()
        {
            var schoolId = GetSchoolId(); 
            var classSectionList = await _studentService.GetAllClassesAndSectionsAsync();

            return classSectionList != null
                ? Ok(new { Message = "Classes and sections fetched successfully", Data = classSectionList })
                : NotFound(new { Message = "No classes or sections found" });
        }

        [HttpPost("toggle-active/{id}")]
        public async Task<IActionResult> ToggleActive(Guid id)
        {
            var student = await _studentService.ToggleActiveStatusAsync(id);
            return Ok(new
            {
                Message = student.IsActive ? "Student activated successfully" : "Student deactivated successfully",
                Data = new { student.Id, student.IsActive }
            });
        }

    }
}
