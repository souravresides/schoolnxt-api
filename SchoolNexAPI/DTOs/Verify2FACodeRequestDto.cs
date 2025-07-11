namespace SchoolNexAPI.DTOs
{
    public class Verify2FACodeRequestDto
    {
        public string Email { get; set; }
        public string Code { get; set; }
    }
}
