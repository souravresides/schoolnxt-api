using SchoolNexAPI.Models;

namespace SchoolNexAPI.Services.Abstract
{
    public interface ISubscriptionTypeService
    {
        Task<List<SubscriptionTypeModel>> GetAllAsync();
        Task<SubscriptionTypeModel> GetByIdAsync(Guid id);
        Task EnsureSubscriptionIsActiveAsync(Guid schoolId);
        //Task<Guid> CreateAsync(SubscriptionTypeModel model);
    }
}
