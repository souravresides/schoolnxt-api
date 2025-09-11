using System.ComponentModel.DataAnnotations;

namespace SchoolNexAPI.Models.Fees
{
    public enum InvoiceStatus { Draft = 0, Issued = 1, PartiallyPaid = 2, Paid = 3, Cancelled = 4 }

    public class Invoice
    {
        [Key] public Guid Id { get; set; }
        public Guid SchoolId { get; set; }
        public Guid AcademicYearId { get; set; }
        public Guid StudentId { get; set; }
        [Required] public string InvoiceNumber { get; set; }
        public DateTime IssueDate { get; set; } = DateTime.UtcNow;
        public DateTime DueDate { get; set; }
        public InvoiceStatus Status { get; set; } = InvoiceStatus.Issued;

        public ICollection<InvoiceLine> Lines { get; set; } = new List<InvoiceLine>();

        public decimal TotalAmount { get; set; }
        public decimal TotalTax { get; set; }
        public decimal TotalAdjustments { get; set; } // sum of waivers negative, credits negative, debits positive
        public decimal TotalLateFee { get; set; }
        public decimal AmountDue { get; set; } // computed/stored for performance

        [Timestamp] public byte[] RowVersion { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public string CreatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string? UpdatedBy { get; set; }
    }
    public class InvoiceLine
    {
        [Key] public Guid Id { get; set; }
        public Guid InvoiceId { get; set; }
        public Guid FeeHeadId { get; set; }
        public string? Description { get; set; }
        public decimal Amount { get; set; }
        public decimal TaxAmount { get; set; }
        public decimal AmountPaid { get; set; } = 0m;
    }

}
