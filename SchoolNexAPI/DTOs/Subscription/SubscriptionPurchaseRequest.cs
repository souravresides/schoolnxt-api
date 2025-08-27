using SchoolNexAPI.Models;

namespace SchoolNexAPI.DTOs.Subscription
{
    public class SubscriptionPurchaseRequest
    {
        public Guid SubscriptionTypeId { get; set; }
        public SubscriptionTerm SubscriptionTerm { get; set; } // "Monthly" or "Yearly"
    }

}
