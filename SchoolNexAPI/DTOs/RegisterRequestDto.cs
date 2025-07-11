namespace SchoolNexAPI.DTOs
{
    public class RegisterRequestDto
    {
        public Guid? SchoolId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
    }
}
