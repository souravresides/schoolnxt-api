using Microsoft.EntityFrameworkCore;
using SchoolNexAPI.Data;
using SchoolNexAPI.DTOs;
using SchoolNexAPI.Models;
using SchoolNexAPI.Services.Abstract;
using SchoolNexAPI.Utilities.Helpers;
using System.Text.Json;

namespace SchoolNexAPI.Services.Concrete
{
    public class SubscriptionTypeService : ISubscriptionTypeService
    {
        private readonly AppDbContext _context;
        private readonly IAppHelper _appHelper;

        public SubscriptionTypeService(AppDbContext context, IAppHelper appHelper)
        {
            _context = context;
            this._appHelper = appHelper;
        }

        public async Task<List<SubscriptionTypeModel>> GetAllAsync()
        {
            return await _context.SubscriptionType.ToListAsync();
        }

        public async Task<SubscriptionTypeModel> GetByIdAsync(Guid id)
        {
            return await _context.SubscriptionType.FindAsync(id);
        }

        private async Task<bool> IsSubscriptionActiveAsync(Guid schoolId)
        {
            var subscription = await _context.SchoolSubscription
                .FirstOrDefaultAsync(s => s.SchoolId == schoolId);

            if (subscription == null || subscription.SubscriptionExpiresOn == null)
                return false;

            return subscription.SubscriptionExpiresOn > DateTime.UtcNow;
        }

        public async Task EnsureSubscriptionIsActiveAsync(Guid schoolId)
        {
            if (!_appHelper.GetUserRoles().Contains("SuperAdmin"))
            {
                if (!await IsSubscriptionActiveAsync(schoolId))
                    throw new UnauthorizedAccessException("Your subscription has expired. Please renew to continue.");
            }
            
        }

        
    }
}
