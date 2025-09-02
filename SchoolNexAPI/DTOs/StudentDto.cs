namespace SchoolNexAPI.DTOs
{
    public class StudentDto
    {
        public Guid Id { get; set; }

        public string FullName { get; set; }
        public string Class { get; set; }
        public string Section { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Gender { get; set; }
        public bool IsActive { get; set; }
        public Guid AcademicYearId { get; set; }

        public string PhotoUrl { get; set; }

        public AddressDto Address { get; set; }

        public List<ParentDto> Parents { get; set; }

        public BankDetailsDto BankDetails { get; set; }

        public MedicalRecordDto MedicalRecord { get; set; }

        public PreviousSchoolDto PreviousSchool { get; set; }

        public TransportationDto Transportation { get; set; }

        public AdditionalDetailsDto AdditionalDetails { get; set; }

        // Audit
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string UpdatedBy { get; set; }


        public List<CustomFieldDto> CustomFields { get; set; }
    }

}
