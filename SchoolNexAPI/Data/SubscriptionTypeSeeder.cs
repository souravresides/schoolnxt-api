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
            if (await _context.SubscriptionType.AnyAsync())
                return; // Prevent duplicate seeding

            var basePath = Path.Combine(_env.ContentRootPath, "Data/SubscriptionFeatures");

            var freeTrialFeatures = await File.ReadAllTextAsync(Path.Combine(basePath, "FreeTrial.json"));
            var standardFeatures = await File.ReadAllTextAsync(Path.Combine(basePath, "Standard.json"));
            var premiumFeatures = await File.ReadAllTextAsync(Path.Combine(basePath, "Premium.json"));

            await _context.SubscriptionType.AddRangeAsync(new[]
         {
                new SubscriptionTypeModel
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
                new SubscriptionTypeModel
                {
                    Id = Guid.Parse("e2f8c5b2-3e4f-4c9a-8b6d-7a0e1c2f3b4b"),
                    Name = "Standard",
                    PricePerMonth = 4999.99m,
                    MaxStudents = 200,
                    MaxEmployees = 50,
                    FeaturesJson = standardFeatures,
                    IsActive = true,
                    Description = "Standard subscription with essential  features."
                }, new SubscriptionTypeModel
                {
                    Id = Guid.Parse("f3f8c5b2-3e4f-4c9a-8b6d-7a0e1c2f3b4c"),
                    Name = "Premium",
                    PricePerMonth = 7999.99m,
                    MaxStudents = 500,
                    MaxEmployees = 100,
                    FeaturesJson = premiumFeatures,
                    IsActive = true,
                    Description = "Premium subscription with all features included."
                }
            }
                );
            await _context.SaveChangesAsync();
        }
    }
}
