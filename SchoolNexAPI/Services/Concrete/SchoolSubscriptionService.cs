using Microsoft.EntityFrameworkCore;
using SchoolNexAPI.Data;
using SchoolNexAPI.DTOs.Subscription;
using SchoolNexAPI.Migrations;
using SchoolNexAPI.Services.Abstract;
using System.Text.Json;

namespace SchoolNexAPI.Services.Concrete
{
    public class SchoolSubscriptionService : ISchoolSubscriptionService
    {
        private readonly AppDbContext _context;
        public SchoolSubscriptionService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<SubscriptionFeaturesDto> GetSchoolFeaturesAsync(Guid schoolId)
        {
            var subscription = await _context.SchoolSubscription
                .Include(s => s.SubscriptionType)
                .FirstOrDefaultAsync(s => s.SchoolId == schoolId);

            if (subscription == null || subscription.SubscriptionExpiresOn <= DateTime.UtcNow)
                throw new UnauthorizedAccessException("Subscription expired or not found.");

            return JsonSerializer.Deserialize<SubscriptionFeaturesDto>(subscription.SubscriptionType.FeaturesJson);
        }
        public async Task<bool> IsSubscriptionValidAsync(Guid schoolId)
        {
            var subscription = await _context.SchoolSubscription
                .FirstOrDefaultAsync(s => s.SchoolId == schoolId);

            if (subscription == null || !subscription.SubscriptionExpiresOn.HasValue)
                return false;

            return subscription.SubscriptionExpiresOn > DateTime.UtcNow;
        }
        public async Task<bool> HasFeatureAccessAsync(Guid schoolId, string featureName)
        {
            var subscription = await _context.SchoolSubscription
                .Include(s => s.SubscriptionType)
                .FirstOrDefaultAsync(s => s.SchoolId == schoolId);

            if (subscription == null || !subscription.SubscriptionExpiresOn.HasValue || subscription.SubscriptionExpiresOn <= DateTime.UtcNow)
                return false;

            var features = JsonSerializer.Deserialize<SubscriptionFeaturesDto>(subscription.SubscriptionType.FeaturesJson);

            // Use reflection to get the property dynamically
            var property = typeof(SubscriptionFeaturesDto).GetProperty(featureName);
            return property != null && (bool)(property.GetValue(features) ?? false);
        }

    }
}
