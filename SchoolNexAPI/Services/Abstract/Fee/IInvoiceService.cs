using SchoolNexAPI.Models.Fees;

namespace SchoolNexAPI.Services.Abstract.Fee
{
    public interface IInvoiceService
    {
        Task<Invoice> GenerateInvoiceFromPlanAsync(Guid schoolId, Guid studentId, Guid academicYearId, DateTime dueDate, string createdBy);
        Task<(int created, int skipped)> GenerateInvoicesForMonthAsync(Guid schoolId, Guid academicYearId, int year, int month, string createdBy);
        Task RecomputeInvoiceAsync(Guid invoiceId);
        Task<List<Invoice>> ListByStudentAsync(Guid schoolId, Guid studentId, Guid academicYearId);
    }
}
