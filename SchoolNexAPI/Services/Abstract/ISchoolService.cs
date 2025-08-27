using SchoolNexAPI.DTOs.School;
using SchoolNexAPI.Models;

namespace SchoolNexAPI.Services.Abstract
{
    public interface ISchoolService
    {
        Task<Guid> CreateSchoolAsync(SchoolRequestDto dto);
        Task UpdateSchoolAsync(Guid id, SchoolRequestDto dto);
        Task UpdateSchoolSettingsAsync(Guid schoolId, string timezone, string locale, string currency);
        Task UpgradeSchoolSubscriptionAsync(Guid schoolId, Guid subscriptionTypeId, SubscriptionTerm term);

        Task<List<SchoolModel>> GetAllSchoolsAsync();
        Task<SchoolModel> GetSchoolByIdAsync(Guid id);
    }
}
