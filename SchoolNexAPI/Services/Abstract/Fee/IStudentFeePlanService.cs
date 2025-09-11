using SchoolNexAPI.Models.Fees;

namespace SchoolNexAPI.Services.Abstract.Fee
{
    public interface IStudentFeePlanService
    {
        Task<StudentFeePlan> GenerateOrRefreshAsync(Guid schoolId, Guid studentId, Guid academicYearId, string createdBy);

        Task<StudentFeePlan?> GetAsync(Guid schoolId, Guid studentId, Guid academicYearId);
    }
}
