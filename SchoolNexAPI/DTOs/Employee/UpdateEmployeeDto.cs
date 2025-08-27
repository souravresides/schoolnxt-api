namespace SchoolNexAPI.DTOs.Employee
{
    public class UpdateEmployeeDto
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string ContactNumber { get; set; }
        public string Role { get; set; }
        public DateTime JoiningDate { get; set; }
    }

}
