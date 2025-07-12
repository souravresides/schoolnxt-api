namespace SchoolNexAPI.Services.Abstract
{
    public interface ISchoolSubscriptionService
    {
        Task<bool> IsSubscriptionValidAsync(Guid schoolId);
        Task<bool> HasFeatureAccessAsync(Guid schoolId, string featureName);
    }
}
