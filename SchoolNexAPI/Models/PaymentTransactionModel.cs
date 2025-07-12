using System.ComponentModel.DataAnnotations;

namespace SchoolNexAPI.Models
{
    public class PaymentTransactionModel
    {
        [Key]
        public Guid Id { get; set; }
        public Guid SchoolId { get; set; }
        public Guid SubscriptionTypeId { get; set; }
        public SubscriptionTerm SubscriptionTerm { get; set; } // Monthly or Yearly
        public string PaymentId { get; set; }
        public decimal AmountPaid { get; set; }
        public string PaymentGateway { get; set; } // e.g., Razorpay, Stripe
        public string TransactionId { get; set; }
        public string Status { get; set; } // Success, Failed, Pending
        public DateTime CreatedAt { get; set; }
        public DateTime PaidOn { get; set; }
    }
}
