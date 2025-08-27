using Razorpay.Api;
using SchoolNexAPI.Models;

namespace SchoolNexAPI.DTOs.Subscription
{
    public class SubscriptionVerifyRequest
    {
        public string OrderId { get; set; }
        public string PaymentId { get; set; }
        public string Signature { get; set; }
        public SubscriptionTerm SubscriptionTerm { get; set; }
    }
}
