using System.ComponentModel.DataAnnotations;

namespace SchoolNexAPI.Models.Fees
{
    public enum AdjustmentType { Waiver = 0, Scholarship = 1, Credit = 2, Debit = 3, LateFee = 4 }

    public class Adjustment
    {
        [Key] public Guid Id { get; set; }
        public Guid SchoolId { get; set; }
        public Guid AcademicYearId { get; set; }
        public Guid StudentId { get; set; }
        public Guid? InvoiceId { get; set; } // null => open credit/debit
        public AdjustmentType Type { get; set; }
        public decimal Amount { get; set; } // positive
        public string? Reason { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }


}
