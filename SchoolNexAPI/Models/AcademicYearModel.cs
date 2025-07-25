using System.ComponentModel.DataAnnotations;

namespace SchoolNexAPI.Models
{
    public class AcademicYearModel
    {
        [Key]
        public Guid Id { get; set; }
        public Guid SchoolId { get; set; }

        public string YearName { get; set; }     // e.g., "2024-2025"
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public bool IsCurrent { get; set; } = false;
        public bool IsLocked { get; set; } = false;

        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string? UpdatedBy { get; set; }

        // Navigation
        public SchoolModel School { get; set; }
        public ICollection<StudentEnrollmentModel> Enrollments { get; set; }
        
    }
}
