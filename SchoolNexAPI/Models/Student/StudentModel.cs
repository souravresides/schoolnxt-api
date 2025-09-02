using System.ComponentModel.DataAnnotations;

namespace SchoolNexAPI.Models.Student
{
    public class StudentModel
    {
        [Key]
        public Guid Id { get; set; }

        [Required] public string FullName { get; set; }
        [Required] public string Class { get; set; }
        public string? Section { get; set; }
        [Required] public DateTime DateOfBirth { get; set; }
        public string? Email { get; set; }
        [Required] public string PhoneNumber { get; set; }
        public string Gender { get; set; }
        public string? PhotoPath { get; set; }

        // Foreign Keys
        public Guid SchoolId { get; set; }
        public SchoolModel School { get; set; }
        public Guid AcademicYearId { get; set; }

        // Metadata
        public bool IsActive { get; set; } = true;
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string? UpdatedBy { get; set; }

        // Navigation Properties
        public StudentAddressModel Address { get; set; }
        public ICollection<StudentParentModel> Parents { get; set; }
        public StudentBankDetailsModel BankDetails { get; set; }
        public StudentMedicalRecordModel MedicalRecord { get; set; }
        public StudentPreviousSchoolModel PreviousSchool { get; set; }
        public StudentTransportationModel Transportation { get; set; }
        public StudentAdditionalDetailsModel AdditionalDetails { get; set; }


        // Relationships
        public List<StudentCustomFieldValueModel> CustomFieldValuesList { get; set; }
        public ICollection<StudentEnrollmentModel> Enrollments { get; set; }
    }
}
