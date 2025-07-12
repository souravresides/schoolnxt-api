using System.ComponentModel.DataAnnotations;

namespace SchoolNexAPI.Models
{
    public class StudentEnrollmentModel
    {
        [Key]
        public Guid Id { get; set; }

        public Guid StudentId { get; set; }
        public StudentModel Student { get; set; }

        public Guid AcademicYearId { get; set; }
        public AcademicYearModel AcademicYear { get; set; }

        public string Class { get; set; }
        public string Section { get; set; }
        public string RollNumber { get; set; }

        public bool IsPromoted { get; set; } = false;
        public bool IsRepeating { get; set; } = false;

        public DateTime EnrolledAt { get; set; }
        public string EnrolledBy { get; set; }
    }
}
