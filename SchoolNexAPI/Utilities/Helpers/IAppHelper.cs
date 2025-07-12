namespace SchoolNexAPI.Utilities.Helpers
{
    public interface IAppHelper
    {
        Guid GetUserId();
        List<string> GetUserRoles();
    }
}
