using SchoolNexAPI.Models.Fees;

namespace SchoolNexAPI.DTOs.Fee
{
    public class CreatePaymentDto
    {
        public Guid? SchoolId { get; set; }
        public Guid StudentId { get; set; }
        public Guid AcademicYearId { get; set; }
        public decimal Amount { get; set; }
        public PaymentMethod Method { get; set; }
        public string? ReferenceNumber { get; set; }
        // optional allocations: if not provided, system auto-FIFO
        public List<PaymentAllocationDto> Allocations { get; set; } = new();
    }
    public class PaymentAllocationDto { public Guid InvoiceId { get; set; } public decimal Amount { get; set; } }
    public class PaymentResponseDto { public Guid Id { get; set; } public decimal Amount { get; set; } public DateTime ReceivedAt { get; set; } }


}
