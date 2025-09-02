using System.ComponentModel.DataAnnotations;

namespace SchoolNexAPI.Models.Student
{
    public class StudentBankDetailsModel
    {
        [Key]
        public Guid Id { get; set; }
        public Guid StudentId { get; set; }
        public StudentModel Student { get; set; }
        public Guid SchoolId { get; set; }
        public string? BankName { get; set; }
        public string? AccountNumber { get; set; }
        public string? IFSC { get; set; }
        public string? AccountHolderName { get; set; }
    }
}
