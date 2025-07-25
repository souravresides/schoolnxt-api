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
        public string FatherName { get; set; }
        public string MotherName { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string Gender { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string? UpdatedBy { get; set; }

        public Guid AcademicYearId { get; set; }

        public List<CustomFieldDto> CustomFields { get; set; }
    }

}
