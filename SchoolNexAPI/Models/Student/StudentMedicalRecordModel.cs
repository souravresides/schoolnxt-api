using System.ComponentModel.DataAnnotations;

namespace SchoolNexAPI.Models.Student
{
    public class StudentMedicalRecordModel
    {
        [Key]
        public Guid Id { get; set; }
        public Guid StudentId { get; set; }
        public StudentModel Student { get; set; }
        public Guid SchoolId { get; set; }
        public string? Weight { get; set; }
        public string? Height { get; set; }
        public string? BMI { get; set; }
        public string? PulseRate { get; set; }
        public string? Haemoglobin { get; set; }
        public string? Allergies { get; set; }
        public bool COVIDVaccination { get; set; }
        public bool ChildImmunisation { get; set; }
        public string? ImmunisationRemarks { get; set; }
    }
}
