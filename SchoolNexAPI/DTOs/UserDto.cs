namespace SchoolNexAPI.DTOs
{
    public class UserDto
    {
        public string UserId { get; set; }
        public string Email { get; set; }
        public string? Name { get; set; }
        public string? ProfilePicture { get; set; }
        public string? PhoneNumber { get; set; }
        public List<string> Roles { get; set; }
        public string SchoolName { get; set; }
    }
}
