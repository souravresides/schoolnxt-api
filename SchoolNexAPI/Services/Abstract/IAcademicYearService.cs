using SchoolNexAPI.DTOs;
using SchoolNexAPI.Models;

namespace SchoolNexAPI.Services.Abstract
{
    public interface IAcademicYearService
    {
        Task<AcademicYearModel> CreateAsync(Guid schoolId, string name, DateTime start, DateTime end, string userId);
        Task<AcademicYearModel> ActivateAsync(Guid schoolId, Guid ayId, string userId);
        Task<AcademicYearModel> CloseAsync(Guid schoolId, Guid ayId, string userId);
        Task<List<AcademicYearModel>> GetAllAsync(Guid schoolId);
        Task<AcademicYearModel?> GetCurrentAsync(Guid schoolId);
    }
}
