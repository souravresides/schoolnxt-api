using SchoolNexAPI.DTOs;

namespace SchoolNexAPI.Services.Abstract
{
    public interface IStudentService
    {
        Task<StudentDto> CreateAsync(Guid schoolId, CreateStudentRequest request, string createdBy);
        Task<StudentDto> GetByIdAsync(Guid studentId);
        Task<IEnumerable<StudentDto>> GetAllAsync(Guid schoolId);
        Task<bool> UpdateAsync(Guid studentId, UpdateStudentRequest request, string updatedBy);
        Task<bool> DeleteAsync(Guid studentId);
    }
}
