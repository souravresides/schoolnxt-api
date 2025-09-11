using SchoolNexAPI.Data;
using SchoolNexAPI.Models;
using SchoolNexAPI.Services.Abstract;

namespace SchoolNexAPI.Services.Concrete
{
    public class AuditService : IAuditService
    {
        private readonly AppDbContext _db;
        public AuditService(AppDbContext db) { _db = db; }

        public async Task WriteAsync(string entityName, string entityId, string action, string? dataBefore, string? dataAfter, string? performedBy)
        {
            //var audit = new AuditLog
            //{
            //    Id = Guid.NewGuid(),
            //    EntityName = entityName,
            //    EntityId = entityId,
            //    Action = action,
            //    DataBefore = dataBefore,
            //    DataAfter = dataAfter,
            //    PerformedBy = performedBy,
            //    PerformedAt = DateTime.UtcNow
            //};
            //_db.AuditLogs.Add(audit);
            await _db.SaveChangesAsync(); // when called inside an outer tx, this will persist as part of it
        }
    }
}
