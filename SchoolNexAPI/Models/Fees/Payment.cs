using System.ComponentModel.DataAnnotations;

namespace SchoolNexAPI.Models.Fees
{
    public enum PaymentMethod { Cash = 0, Cheque = 1, BankTransfer = 2, Online = 3 }
    public enum PaymentStatus { Pending = 0, Confirmed = 1, Failed = 2, Refunded = 3 }

    public class Payment
    {
        [Key] public Guid Id { get; set; }
        public Guid SchoolId { get; set; }
        public Guid AcademicYearId { get; set; }
        public Guid StudentId { get; set; }
        public decimal Amount { get; set; }
        public PaymentMethod Method { get; set; } = PaymentMethod.Cash;
        public PaymentStatus Status { get; set; } = PaymentStatus.Confirmed;
        public string? ReferenceNumber { get; set; }
        public string IdempotencyKey { get; set; } // required for offline/online operations

        public DateTime ReceivedAt { get; set; } = DateTime.UtcNow;
        public ICollection<PaymentAllocation> Allocations { get; set; } = new List<PaymentAllocation>();

        [Timestamp] public byte[] RowVersion { get; set; }
    }
    public class PaymentAllocation
    {
        [Key] public Guid Id { get; set; }
        public Guid PaymentId { get; set; }
        public Guid InvoiceId { get; set; }
        public decimal Amount { get; set; }
    }


}
