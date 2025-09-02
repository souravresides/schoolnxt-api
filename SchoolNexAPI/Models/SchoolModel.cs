using SchoolNexAPI.Models.Student;
using System.ComponentModel.DataAnnotations;

namespace SchoolNexAPI.Models
{
    public class SchoolModel
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string ShortName { get; set; }
        public string Board { get; set; }
        public string Type { get; set; } // Public, Private, etc.
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string WebsiteUrl { get; set; }

        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
        public string LogoUrl { get; set; }

        public bool IsActive { get; set; } = true;
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string? UpdatedBy { get; set; }

        // Relationships
        public SchoolSubscriptionModel SchoolSubscription { get; set; }
        public SchoolSettingsModel SchoolSettings { get; set; }
        public List<StudentModel> Students { get; set; }
        public List<FileRecordsModel> FileRecords { get; set; }
        public ICollection<AcademicYearModel> AcademicYears { get; set; }
    }
}
