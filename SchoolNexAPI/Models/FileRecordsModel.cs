using SchoolNexAPI.Enums;
using SchoolNexAPI.Migrations;
using System.ComponentModel.DataAnnotations;

namespace SchoolNexAPI.Models
{
    public class FileRecordsModel
    {
        [Key]
        public Guid Id { get; set; }

        public Guid? SchoolId { get; set; }

        [Required]
        public string Entity { get; set; }

        [Required]
        public Guid EntityId { get; set; }

        [Required]
        public string FileName { get; set; }

        [Required]
        public string FilePath { get; set; }
        [Required]
        public string FileCategory { get; set; }

        public string DocumentType { get; set; }
        public string ContentType { get; set; }

        public long FileSize { get; set; }

        public Guid? UploadedByUserId { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? DeletedAt { get; set; }
        public Guid? DeletedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public SchoolModel? School { get; set; }
    }
}
