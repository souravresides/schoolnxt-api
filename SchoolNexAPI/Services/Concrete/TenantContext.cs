using SchoolNexAPI.Services.Abstract;

namespace SchoolNexAPI.Services.Concrete
{
    public class TenantContext : ITenantContext
    {
        public Guid? CurrentSchoolId { get; set; }
        public Guid? CurrentAcademicYearId { get; set; }
        public Guid? CurrentUserId { get; set; }
        public string CurrentUserName { get; set; }
        public IList<string> CurrentUserRoles { get; set; } = new List<string>();
    }
}
