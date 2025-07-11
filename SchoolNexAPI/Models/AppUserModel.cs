using Microsoft.AspNetCore.Identity;

namespace SchoolNexAPI.Models
{
    public class AppUserModel : IdentityUser
    {
        public string Name { get; set; }
        public Guid? SchoolId { get; set; }  // Nullable, if the user is a SuperAdmin (no school) 

        public bool IsActive { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? UpdatedAt { get; set; }

        //public StudentModel Student { get; set; }

        //public ParentModel Parent { get; set; }
    }
}
