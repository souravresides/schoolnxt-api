using System.ComponentModel.DataAnnotations;

namespace SchoolNexAPI.Models.Fees
{
    // PaymentIntent for online gateway (future)
    public enum PaymentIntentStatus { Created = 0, Authorized = 1, Captured = 2, Failed = 3, Cancelled = 4 }
    public class PaymentIntent
    {
        [Key] public Guid Id { get; set; }
        [Required] public Guid SchoolId { get; set; }
        [Required] public Guid AcademicYearId { get; set; }
        [Required] public Guid StudentId { get; set; }
        [Required] public decimal Amount { get; set; }
        [Required] public string Provider { get; set; } // "Razorpay"
        public string? ProviderIntentId { get; set; }
        public string? ProviderOrderId { get; set; }
        public PaymentIntentStatus Status { get; set; } = PaymentIntentStatus.Created;
        public string? MetadataJson { get; set; }
    }

}
