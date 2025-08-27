using SchoolNexAPI.DTOs.Employee;

namespace SchoolNexAPI.Services.Abstract
{
    public interface IEmployeeService
    {
        Task<EmployeeDto> CreateAsync(CreateEmployeeDto dto, Guid schoolId, string createdBy);
        Task<EmployeeDto> UpdateAsync(Guid id, UpdateEmployeeDto dto, Guid schoolId, string updatedBy);
        Task<EmployeeDto> GetByIdAsync(Guid id, Guid schoolId);
        Task<List<EmployeeDto>> GetAllAsync(Guid schoolId);
        Task<bool> DeleteAsync(Guid id, Guid schoolId, string deletedBy);
    }

}
