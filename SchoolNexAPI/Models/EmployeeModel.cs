using System.ComponentModel.DataAnnotations;

namespace SchoolNexAPI.Models
{
    public class EmployeeModel
    {
        [Key]
        public Guid Id { get; set; }

        // Basic Details
        [Required, MaxLength(50)]
        public string EmployeeId { get; set; } = default!;
        [Required]
        public Guid SchoolId { get; set; }
        public SchoolModel School { get; set; }
        [MaxLength(15)]
        public string? MobileNumber { get; set; }
        [MaxLength(100)]
        public string? Email { get; set; }
        [Required, MaxLength(50)]
        public string FirstName { get; set; } = default!;
        [MaxLength(50)]
        public string? MiddleName { get; set; }
        [MaxLength(50)]
        public string? LastName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? Gender { get; set; }
        public string? BloodGroup { get; set; }

        // Roles & Department
        [Required]
        public string UserRole { get; set; } = default!;
        public string? Department { get; set; }

        // Address Details
        public string? CurrentAddressLine1 { get; set; }
        public string? CurrentAddressLine2 { get; set; }
        public string? CurrentCity { get; set; }
        public string? CurrentState { get; set; }
        public string? CurrentPinCode { get; set; }
        public string? CurrentCountry { get; set; }

        public string? PermanentAddressLine1 { get; set; }
        public string? PermanentAddressLine2 { get; set; }
        public string? PermanentCity { get; set; }
        public string? PermanentState { get; set; }
        public string? PermanentPinCode { get; set; }
        public string? PermanentCountry { get; set; }

        // Employment Details
        public string? JobTitle { get; set; }
        public string? Designation { get; set; }
        public string? EmploymentType { get; set; }
        public DateTime? AppointmentDate { get; set; }
        public int? ExperienceYears { get; set; }
        public string? HighestQualification { get; set; }
        public string? UAN { get; set; }
        public string? PFAccountNumber { get; set; }
        public string? ESICodeNumber { get; set; }
        public string? ReportingManager { get; set; }
        public string? Reportee { get; set; }

        // Additional Details
        public string? AadharNumber { get; set; }
        public string? PANNumber { get; set; }
        public string? Religion { get; set; }
        public string? Category { get; set; }
        public string? FatherName { get; set; }
        public string? MotherName { get; set; }
        public string? MaritalStatus { get; set; }
        public string? SpouseName { get; set; }
        public string? EmergencyContactNumber { get; set; }

        // Bank Details
        public string? BankName { get; set; }
        public string? BankAccountNumber { get; set; }
        public string? IFSCCode { get; set; }
        public string? AccountHolderName { get; set; }

        // File Upload
        public string? ProfilePhotoUrl { get; set; }

        // Navigation Property
        public ICollection<EmployeePreviousEmployment> PreviousEmployments { get; set; } = new List<EmployeePreviousEmployment>();
    }
}
