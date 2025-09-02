using System.ComponentModel.DataAnnotations;

namespace SchoolNexAPI.Models.Student
{
    public class StudentPreviousSchoolModel
    {
        [Key]
        public Guid Id { get; set; }
        public Guid StudentId { get; set; }
        public StudentModel Student { get; set; }
        public Guid SchoolId { get; set; }
        public string? Name { get; set; }
        public string? Address { get; set; }
        public string? Board { get; set; }
        public string? MediumOfInstruction { get; set; }
        public string? TCNumber { get; set; }
        public string? LastClassPassed { get; set; }
        public string? PercentageOrGrade { get; set; }
    }
}
