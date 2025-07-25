using SchoolNexAPI.DTOs;
using SchoolNexAPI.Models;

namespace SchoolNexAPI.Services.Abstract
{
    public interface IAcademicYearService
    {
        Task<IEnumerable<AcademicYearModel>> GetAllAsync();
        Task<AcademicYearModel> GetByIdAsync(Guid id);
        Task<AcademicYearModel> CreateAsync(Guid SchoolId, string createdBy, CreateAcademicYearDto model);
        Task<AcademicYearModel> UpdateAsync(Guid id, Guid SchoolId,string updatedBy,CreateAcademicYearDto model);
        Task<bool> DeleteAsync(Guid id);
    }
}
