using System.ComponentModel.DataAnnotations;

namespace SchoolNexAPI.Models
{
    public class RefreshTokenModel
    {
        [Key]
        public int Id { get; set; }
        public string Token { get; set; }
        public DateTime Expires { get; set; }
        public bool IsExpired => DateTime.UtcNow >= Expires;
        public DateTime Created { get; set; }
        public string UserId { get; set; }
        public AppUserModel User { get; set; }
    }
}
