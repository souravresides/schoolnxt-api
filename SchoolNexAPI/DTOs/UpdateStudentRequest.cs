namespace SchoolNexAPI.DTOs
{
    public class UpdateStudentRequest
    {
        public string? FullName { get; set; }
        public string? Class { get; set; }
        public string? Section { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Gender { get; set; }
        public Guid? AcademicYearId { get; set; }
        public IFormFile? Photo { get; set; }

        public AddressDto? Address { get; set; }

        public List<ParentDto>? Parents { get; set; }

        public BankDetailsDto? BankDetails { get; set; }

        public MedicalRecordDto? MedicalRecord { get; set; }

        public PreviousSchoolDto? PreviousSchool { get; set; }

        public TransportationDto? Transportation { get; set; }

        public AdditionalDetailsDto? AdditionalDetails { get; set; }
    }
}
