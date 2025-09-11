using System.ComponentModel.DataAnnotations;

namespace SchoolNexAPI.Models
{
    public class AuditLog
    {
        public Guid Id { get; set; }
        public Guid SchoolId { get; set; }
        public string ResourceType { get; set; } = null!;
        public Guid? ResourceId { get; set; }
        public string Action { get; set; } = null!;
        public Guid PerformedBy { get; set; }
        public DateTimeOffset PerformedAt { get; set; }
        public string? DataJson { get; set; }
    }
}
