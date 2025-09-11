using SchoolNexAPI.DTOs.Fee;
using SchoolNexAPI.Models.Fees;

namespace SchoolNexAPI.Services.Abstract.Fee
{
    public interface IFeeStructureService
    {
        Task<FeeStructure> CreateAsync(FeeStructureCreateDto dto, string createdBy, Guid effectiveSchoolId);
        Task<List<FeeStructure>> ListByClassAsync(Guid schoolId, Guid ayId, string className);
    }
}
