namespace SchoolNexAPI.DTOs
{
    public class EmployeeDto
    {
        public string EmployeeId { get; set; } = default!;
        public string? MobileNumber { get; set; }
        public string? Email { get; set; }
        public string FirstName { get; set; } = default!;
        public string? MiddleName { get; set; }
        public string? LastName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? Gender { get; set; }
        public string? BloodGroup { get; set; }
        public string UserRole { get; set; } = default!;
        public string? Department { get; set; }

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

        public string? AadharNumber { get; set; }
        public string? PANNumber { get; set; }
        public string? Religion { get; set; }
        public string? Category { get; set; }
        public string? FatherName { get; set; }
        public string? MotherName { get; set; }
        public string? MaritalStatus { get; set; }
        public string? SpouseName { get; set; }
        public string? EmergencyContactNumber { get; set; }

        public string? BankName { get; set; }
        public string? BankAccountNumber { get; set; }
        public string? IFSCCode { get; set; }
        public string? AccountHolderName { get; set; }

        public string? ProfilePhotoUrl { get; set; }

        public List<PreviousEmploymentDto>? PreviousEmployments { get; set; }
    }

    public class PreviousEmploymentDto
    {
        public string? CompanyName { get; set; }
        public string? JobTitle { get; set; }
        public DateTime? JoiningDate { get; set; }
        public DateTime? RelievingDate { get; set; }
        public string? Location { get; set; }
        public string? ReferenceName { get; set; }
        public string? ReferenceMobile { get; set; }
    }
}
