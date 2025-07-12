namespace SchoolNexAPI.DTOs
{
    public class UpdateStudentRequest
    {
        public string FullName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Gender { get; set; }
        public Guid AcademicYearId { get; set; }
        public List<CustomFieldValueRequest> CustomFields { get; set; }
    }

}
