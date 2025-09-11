using System.ComponentModel.DataAnnotations;

namespace SchoolNexAPI.Models
{
    public enum AcademicYearStatus { Draft = 0, Active = 1, Closed = 2, Archived = 3 }
    public class AcademicYearModel
    {
        [Key] public Guid Id { get; set; }
        public Guid SchoolId { get; set; }
        public string Name { get; set; } // "2025-26"
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public AcademicYearStatus Status { get; set; } = AcademicYearStatus.Draft;
        public bool IsCurrent { get; set; } = false;
        public DateTime? FinanceLockedAt { get; set; }

        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string? UpdatedBy { get; set; }

        // Navigation
        public SchoolModel School { get; set; }
        public ICollection<StudentEnrollmentModel> Enrollments { get; set; }
        
    }
}
