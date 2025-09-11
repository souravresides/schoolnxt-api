using SchoolNexAPI.DTOs.Fee;
using SchoolNexAPI.Models.Fees;

namespace SchoolNexAPI.Services.Abstract.Fee
{
    public interface IAdjustmentService
    {
        Task<Adjustment> ApplyAsync(CreateAdjustmentDto dto, string createdBy);
        Task<List<Adjustment>> GetByStudentAsync(Guid schoolId, Guid studentId);
    }
}
