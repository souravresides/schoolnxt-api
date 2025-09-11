using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolNexAPI.Models
{
    public class EmployeePreviousEmployment
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public Guid EmployeeId { get; set; }
        [ForeignKey("StaffId")]
        public EmployeeModel Staff { get; set; } = default!;

        public string? CompanyName { get; set; }
        public string? JobTitle { get; set; }
        public DateTime? JoiningDate { get; set; }
        public DateTime? RelievingDate { get; set; }
        public string? Location { get; set; }
        public string? ReferenceName { get; set; }
        public string? ReferenceMobile { get; set; }
    }
}
