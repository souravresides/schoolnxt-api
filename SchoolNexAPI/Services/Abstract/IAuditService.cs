namespace SchoolNexAPI.Services.Abstract
{
    public interface IAuditService
    {
        Task WriteAsync(string entityName, string entityId, string action, string? dataBefore, string? dataAfter, string? performedBy);
    }
}
