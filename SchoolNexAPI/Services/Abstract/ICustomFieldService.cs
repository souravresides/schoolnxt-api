using SchoolNexAPI.DTOs;

namespace SchoolNexAPI.Services.Abstract
{
    public interface ICustomFieldService
    {
        Task<IEnumerable<CustomFieldDto>> GetAllAsync(Guid schoolId, string targetEntity);
        Task<CustomFieldDto> GetByIdAsync(Guid id);
        Task<CustomFieldDto> CreateAsync(Guid schoolId, CreateCustomFieldRequest request, string createdBy);
        Task<bool> UpdateAsync(UpdateCustomFieldRequest request, string updatedBy);
        Task<bool> DeleteAsync(Guid id);
    }
}
