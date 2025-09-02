using System.ComponentModel.DataAnnotations;

namespace SchoolNexAPI.Models.Student
{
    public class StudentParentModel
    {
        [Key]
        public Guid Id { get; set; }
        public Guid StudentId { get; set; }
        public StudentModel Student { get; set; }
        public Guid SchoolId { get; set; }
        public string? Name { get; set; }
        public string? Relation { get; set; } // Father / Mother / Guardian
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
        public string? Education { get; set; }
        public string? Occupation { get; set; }
        public string? Organization { get; set; }
        public string? Designation { get; set; }
        public string? AnnualIncome { get; set; }
        public string? OfficeNumber { get; set; }
    }
}
