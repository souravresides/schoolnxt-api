using System.ComponentModel.DataAnnotations;

namespace SchoolNexAPI.Models
{
    public class EmployeeModel
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string FullName { get; set; }

        public string Email { get; set; }

        public string ContactNumber { get; set; }

        public DateTime JoiningDate { get; set; }

        public Guid SchoolId { get; set; } // Tenant

        public bool IsActive { get; set; } = true;

        public DateTime CreatedAt { get; set; }

        public string CreatedBy { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public string? UpdatedBy { get; set; }
    }
}
