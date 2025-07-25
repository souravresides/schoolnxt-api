using System.ComponentModel.DataAnnotations;

namespace SchoolNexAPI.Models
{
    public class StudentModel
    {
        [Key]
        public Guid Id { get; set; }

        public Guid SchoolId { get; set; }
        public SchoolModel School { get; set; }
        public Guid AcademicYearId { get; set; }
        public AcademicYearModel AcademicYear { get; set; }
        public string FullName { get; set; }
        public string Class { get; set; }
        public string Section { get; set; }
        public DateTime DateOfBirth { get; set; }

        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Gender { get; set; }

        public string FatherName { get; set; }
        public string MotherName { get; set; }
        public string Address { get; set; }

        public bool IsActive { get; set; } = true;
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string? UpdatedBy { get; set; }

        // Relationships
        public List<StudentCustomFieldValueModel> CustomFieldValuesList { get; set; }
        public ICollection<StudentEnrollmentModel> Enrollments { get; set; }
    }
}
