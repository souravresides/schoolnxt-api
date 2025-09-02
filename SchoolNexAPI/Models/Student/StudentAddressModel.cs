using System.ComponentModel.DataAnnotations;

namespace SchoolNexAPI.Models.Student
{
    public class StudentAddressModel
    {
        [Key]
        public Guid Id { get; set; }
        public Guid StudentId { get; set; }
        public StudentModel Student { get; set; }
        public Guid SchoolId { get; set; }
        public string? Line1 { get; set; }
        public string? Line2 { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? PinCode { get; set; }
        public string? Country { get; set; }
        public bool IsPermanentSameAsCurrent { get; set; }
    }
}
