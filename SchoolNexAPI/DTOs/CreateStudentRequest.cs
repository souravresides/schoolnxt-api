namespace SchoolNexAPI.DTOs
{
    public class CreateStudentRequest
    {
        public string FullName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Gender { get; set; }

        public Guid AcademicYearId { get; set; }

        public List<CustomFieldInput> CustomFields { get; set; }
    }

    public class CustomFieldInput
    {
        public Guid CustomFieldDefinitionId { get; set; }
        public string Value { get; set; }
    }

}
