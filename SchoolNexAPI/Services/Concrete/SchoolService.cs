using Microsoft.EntityFrameworkCore;
using SchoolNexAPI.Data;
using SchoolNexAPI.DTOs.School;
using SchoolNexAPI.Migrations;
using SchoolNexAPI.Models;
using SchoolNexAPI.Services.Abstract;
using System.Text.Json;

namespace SchoolNexAPI.Services.Concrete
{
    public class SchoolService : ISchoolService
    {
        private readonly AppDbContext _context;

        public SchoolService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Guid> CreateSchoolAsync(SchoolRequestDto dto)
        {
            var freeTrialType = await _context.SubscriptionType.FirstOrDefaultAsync(s => s.Name == "Free Trial");
            var school = new SchoolModel
            {
                Id = Guid.NewGuid(),
                Name = dto.Name,
                ShortName = dto.ShortName,
                Board = dto.Board,
                Type = dto.Type,
                Email = dto.Email,
                PhoneNumber = dto.PhoneNumber,
                WebsiteUrl = dto.WebsiteUrl,
                AddressLine1 = dto.AddressLine1,
                AddressLine2 = dto.AddressLine2,
                City = dto.City,
                State = dto.State,
                PostalCode = dto.PostalCode,
                Country = dto.Country,
                LogoUrl = dto.LogoUrl,
                CreatedAt = DateTime.UtcNow,
                CreatedBy = dto.CreatedBy,
                IsActive = true,
                SchoolSubscription = new SchoolSubscriptionModel
                {
                    Id = Guid.NewGuid(),
                    SubscriptionTypeId = freeTrialType.Id,
                    SubscriptionExpiresOn = DateTime.UtcNow.AddDays(7),
                    Remarks = "Initial Subscription",
                    SubscriptionTerm = SubscriptionTerm.FreeTrial
                },
                SchoolSettings = new SchoolSettingsModel
                {
                    Id = Guid.NewGuid(),
                    Timezone = dto.Timezone,
                    Locale = "en-IN",
                    Currency = dto.Currency
                }
            };

            await _context.Schools.AddAsync(school);
            await _context.SaveChangesAsync();

            return school.Id;
        }

        public async Task UpdateSchoolAsync(Guid id, SchoolRequestDto dto)
        {
            var school = await _context.Schools
                .Include(s => s.SchoolSettings)
                .Include(s => s.SchoolSubscription)
                .FirstOrDefaultAsync(s => s.Id == id);

            if (school == null)
                throw new Exception("School not found.");

            school.Name = dto.Name;
            school.ShortName = dto.ShortName;
            school.Board = dto.Board;
            school.Type = dto.Type;
            school.Email = dto.Email;
            school.PhoneNumber = dto.PhoneNumber;
            school.WebsiteUrl = dto.WebsiteUrl;
            school.AddressLine1 = dto.AddressLine1;
            school.AddressLine2 = dto.AddressLine2;
            school.City = dto.City;
            school.State = dto.State;
            school.PostalCode = dto.PostalCode;
            school.Country = dto.Country;
            school.LogoUrl = dto.LogoUrl;
            school.UpdatedAt = DateTime.UtcNow;
            school.UpdatedBy = dto.CreatedBy;

            await _context.SaveChangesAsync();
        }

        public async Task UpdateSchoolSettingsAsync(Guid schoolId, string timezone, string locale, string currency)
        {
            var settings = await _context.SchoolSettings
                .FirstOrDefaultAsync(s => s.SchoolId == schoolId);

            if (settings == null)
                throw new Exception("Settings not found.");

            settings.Timezone = timezone;
            settings.Locale = locale;
            settings.Currency = currency;

            await _context.SaveChangesAsync();
        }

        public async Task UpgradeSchoolSubscriptionAsync(Guid schoolId, Guid subscriptionTypeId, SubscriptionTerm term)
        {
            var subscription = await _context.SchoolSubscription
                .FirstOrDefaultAsync(s => s.SchoolId == schoolId);

            if (subscription == null)
                throw new Exception("Subscription not found.");

            subscription.SubscriptionTypeId = subscriptionTypeId;
            subscription.SubscriptionTerm = term;

            subscription.SubscriptionExpiresOn = term switch
            {
                SubscriptionTerm.Monthly => DateTime.UtcNow.AddMonths(1),
                SubscriptionTerm.Yearly => DateTime.UtcNow.AddYears(1),
                _ => throw new ArgumentOutOfRangeException()
            };

            subscription.Remarks = $"Upgraded to {term} plan on {DateTime.UtcNow:yyyy-MM-dd}";

            await _context.SaveChangesAsync();
        }

        public async Task<List<SchoolModel>> GetAllSchoolsAsync()
        {
            return await _context.Schools
                .Include(s => s.SchoolSubscription).ThenInclude(s => s.SubscriptionType)
                .Include(s => s.SchoolSettings)
                .ToListAsync();
        }

        public async Task<SchoolModel> GetSchoolByIdAsync(Guid id)
        {
            return await _context.Schools
                .Include(s => s.SchoolSubscription).ThenInclude(x => x.SubscriptionType)
                .Include(s => s.SchoolSettings)
                .FirstOrDefaultAsync(s => s.Id == id);
        }


    }
}
