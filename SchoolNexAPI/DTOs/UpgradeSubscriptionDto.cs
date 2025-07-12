using SchoolNexAPI.Models;

namespace SchoolNexAPI.DTOs
{
    public class UpgradeSubscriptionDto
    {
        public Guid SubscriptionTypeId { get; set; }
        public SubscriptionTerm SubscriptionTerm { get; set; }
    }
}
