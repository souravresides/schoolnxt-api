namespace SchoolNexAPI.DTOs
{
    public class StudentDto
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Gender { get; set; }

        public Guid AcademicYearId { get; set; }

        public List<CustomFieldDto> CustomFields { get; set; }
    }

}
