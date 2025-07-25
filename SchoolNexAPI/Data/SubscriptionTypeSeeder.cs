using Microsoft.EntityFrameworkCore;
using SchoolNexAPI.Models;

namespace SchoolNexAPI.Data
{
    public class SubscriptionTypeSeeder : ISubscriptionTypeSeeder
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public SubscriptionTypeSeeder(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public async Task SeedAsync()
        {
            var basePath = Path.Combine(_env.ContentRootPath, "Data/SubscriptionFeatures");

            var freeTrialFeatures = await File.ReadAllTextAsync(Path.Combine(basePath, "FreeTrial.json"));
            var standardFeatures = await File.ReadAllTextAsync(Path.Combine(basePath, "Standard.json"));
            var premiumFeatures = await File.ReadAllTextAsync(Path.Combine(basePath, "Premium.json"));

            var subscriptions = new List<SubscriptionTypeModel>
    {
        new()
        {
            Id = Guid.Parse("d1f8c5b2-3e4f-4c9a-8b6d-7a0e1c2f3b4a"),
            Name = "Free Trial",
            PricePerMonth = 0,
            MaxStudents = 50,
            MaxEmployees = 10,
            FeaturesJson = freeTrialFeatures,
            IsActive = true,
            Description = "7-day free trial"
        },
        new()
        {
            Id = Guid.Parse("e2f8c5b2-3e4f-4c9a-8b6d-7a0e1c2f3b4b"),
            Name = "Standard",
            PricePerMonth = 4999.99m,
            MaxStudents = 500,
            MaxEmployees = 150,
            FeaturesJson = standardFeatures,
            IsActive = true,
            Description = "Standard subscription with essential features."
        },
        new()
        {
            Id = Guid.Parse("f3f8c5b2-3e4f-4c9a-8b6d-7a0e1c2f3b4c"),
            Name = "Premium",
            PricePerMonth = 7999.99m,
            MaxStudents = 10000,
            MaxEmployees = 1000,
            FeaturesJson = premiumFeatures,
            IsActive = true,
            Description = "Premium subscription with all features included."
        }
    };

            foreach (var subscription in subscriptions)
            {
                var existing = await _context.SubscriptionType.FindAsync(subscription.Id);
                if (existing == null)
                {
                    await _context.SubscriptionType.AddAsync(subscription);
                }
                else
                {
                    // Update only if something changed
                    bool hasChanged = existing.FeaturesJson != subscription.FeaturesJson ||
                                      existing.Name != subscription.Name ||
                                      existing.PricePerMonth != subscription.PricePerMonth ||
                                      existing.MaxStudents != subscription.MaxStudents ||
                                      existing.MaxEmployees != subscription.MaxEmployees ||
                                      existing.Description != subscription.Description ||
                                      existing.IsActive != subscription.IsActive;

                    if (hasChanged)
                    {
                        existing.Name = subscription.Name;
                        existing.PricePerMonth = subscription.PricePerMonth;
                        existing.MaxStudents = subscription.MaxStudents;
                        existing.MaxEmployees = subscription.MaxEmployees;
                        existing.FeaturesJson = subscription.FeaturesJson;
                        existing.Description = subscription.Description;
                        existing.IsActive = subscription.IsActive;
                    }
                }
            }

            await _context.SaveChangesAsync();
        }

    }
}
