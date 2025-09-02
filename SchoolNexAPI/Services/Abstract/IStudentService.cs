using SchoolNexAPI.DTOs;

namespace SchoolNexAPI.Services.Abstract
{
    public interface IStudentService
    {
        Task<StudentDto> CreateAsync(Guid schoolId, CreateStudentRequest request, string createdBy);
        Task<StudentDto> GetByIdAsync(Guid studentId);
        Task<IEnumerable<StudentDto>> GetAllAsync(Guid schoolId, string status = "all");
        Task<bool> UpdateAsync(Guid studentId, Guid schoolId, UpdateStudentRequest request, string updatedBy);
        Task<bool> DeleteAsync(Guid studentId);
        Task<string?> UploadPhotoAsync(Guid studentId, Guid schoolId, IFormFile photo, string updatedBy);
    }
}
