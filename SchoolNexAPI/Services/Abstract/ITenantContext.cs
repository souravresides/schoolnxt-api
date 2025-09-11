namespace SchoolNexAPI.Services.Abstract
{
    public interface ITenantContext
    {
        Guid? CurrentSchoolId { get; set; }    // null for SuperAdmin operations
        Guid? CurrentAcademicYearId { get; set; }
        Guid? CurrentUserId { get; set; }
        string CurrentUserName { get; set; }
        IList<string> CurrentUserRoles { get; set; }
    }
}
