using SchoolNexAPI.DTOs.Subscription;
using SchoolNexAPI.Models;

namespace SchoolNexAPI.Services.Abstract
{
    public interface ISubscriptionService
    {
        Task<SubscriptionPurchaseResponse> CreateSubscriptionOrderAsync(Guid schoolId, SubscriptionPurchaseRequest request);
        Task<IEnumerable<SubscriptionTypeModel>> GetPlansAsync();
        Task<SubscriptionPurchaseResponse> PurchaseAsync(Guid schoolId, SubscriptionPurchaseRequest req);
        Task VerifyAsync(SubscriptionVerifyRequest req);
    }
}
