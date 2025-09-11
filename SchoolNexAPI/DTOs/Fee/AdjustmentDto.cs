using SchoolNexAPI.Models.Fees;

namespace SchoolNexAPI.DTOs.Fee
{
    public class CreateAdjustmentDto
    {
        public Guid? SchoolId { get; set; }
        public Guid StudentId { get; set; }
        public Guid AcademicYearId { get; set; }
        public Guid? InvoiceId { get; set; }
        public AdjustmentType Type { get; set; }
        public decimal Amount { get; set; }
        public string? Reason { get; set; }
    }
    public class AdjustmentResponseDto { public Guid Id { get; set; } public decimal Amount { get; set; } public DateTime CreatedAt { get; set; } public string Type { get; set; } }


}
