namespace SchoolNexAPI.DTOs
{
    public class AuthResponseDto
    {
        public bool IsSuccess { get; set; }
        public string Token { get; set; }
        public string RefreshToken { get; set; }
        public List<string> Errors { get; set; }
        public bool Is2FARequired { get; set; }
        public string TempUserId { get; set; }
        public UserDto User { get; set; }
    }
}
