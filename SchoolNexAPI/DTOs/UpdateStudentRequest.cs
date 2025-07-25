namespace SchoolNexAPI.DTOs
{
    public class UpdateStudentRequest
    {
        public string FullName { get; set; }
        public string Class { get; set; }
        public string Section { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Gender { get; set; }
        public string FatherName { get; set; }
        public string MotherName { get; set; }
        public string Address { get; set; }
        public Guid AcademicYearId { get; set; }
        public List<CustomFieldValueRequest> CustomFields { get; set; }
    }

}
