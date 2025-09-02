namespace SchoolNexAPI.DTOs
{
    public class CreateStudentRequest
    {
        public string FullName { get; set; }
        public string Class { get; set; }
        public string? Section { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Gender { get; set; }
        public Guid AcademicYearId { get; set; }
        public IFormFile? Photo { get; set; }

        public AddressDto Address { get; set; }

        public List<ParentDto> Parents { get; set; }

        public BankDetailsDto BankDetails { get; set; }

        public MedicalRecordDto MedicalRecord { get; set; }

        public PreviousSchoolDto PreviousSchool { get; set; }

        public TransportationDto Transportation { get; set; }

        public AdditionalDetailsDto AdditionalDetails { get; set; }
    }



    public class AddressDto
    {
        public string Line1 { get; set; }
        public string Line2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PinCode { get; set; }
        public string Country { get; set; }
        public bool? IsPermanentSameAsCurrent { get; set; }
    }

    public class ParentDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Relation { get; set; } // Father / Mother / Guardian
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Education { get; set; }
        public string Occupation { get; set; }
        public string Organization { get; set; }
        public string Designation { get; set; }
        public string AnnualIncome { get; set; }
        public string OfficeNumber { get; set; }
    }

    public class BankDetailsDto
    {
        public string BankName { get; set; }
        public string AccountNumber { get; set; }
        public string IFSC { get; set; }
        public string AccountHolderName { get; set; }
    }

    public class MedicalRecordDto
    {
        public string Weight { get; set; }
        public string Height { get; set; }
        public string BMI { get; set; }
        public string PulseRate { get; set; }
        public string Haemoglobin { get; set; }
        public string Allergies { get; set; }
        public bool COVIDVaccination { get; set; }
        public bool ChildImmunisation { get; set; }
        public string ImmunisationRemarks { get; set; }
    }

    public class PreviousSchoolDto
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string Board { get; set; }
        public string MediumOfInstruction { get; set; }
        public string TCNumber { get; set; }
        public string LastClassPassed { get; set; }
        public string PercentageOrGrade { get; set; }
    }

    public class TransportationDto
    {
        public bool NeedsTransportation { get; set; }
        public string PickupPoint { get; set; }
    }

    public class AdditionalDetailsDto
    {
        public string Religion { get; set; }
        public string Category { get; set; }
        public bool RightToEducation { get; set; }
        public string Nationality { get; set; }
        public bool BPLStudent { get; set; }
        public string BPLCardNo { get; set; }
        public bool PwD { get; set; }
        public string TypeOfDisability { get; set; }
        public string IdentificationMark { get; set; }
        public string MotherTongue { get; set; }
        public string SecondLanguage { get; set; }
        public string EmergencyContactNumber { get; set; }
        public bool SingleParentChild { get; set; }
        public string SingleParent { get; set; }
        public bool SponsoredStudent { get; set; }
        public string SponsorName { get; set; }
    }
}

