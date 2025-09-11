using SchoolNexAPI.DTOs.Fee;
using SchoolNexAPI.Models.Fees;

namespace SchoolNexAPI.Services.Abstract.Fee
{
    public interface IPaymentService
    {
        Task<Payment> RecordPaymentAsync(CreatePaymentDto dto, string createdBy, string idempotencyKey);
        Task<List<Payment>> ListByStudentAsync(Guid schoolId, Guid studentId);
    }
}
