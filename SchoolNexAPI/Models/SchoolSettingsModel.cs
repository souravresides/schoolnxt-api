using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace SchoolNexAPI.Models
{
    public class SchoolSettingsModel
    {
        [Key]
        public Guid Id { get; set; }
        public Guid SchoolId { get; set; }
        
        public SchoolModel School { get; set; }
        public string Timezone { get; set; } // e.g., Asia/Kolkata
        public string Locale { get; set; }   // e.g., en-IN
        public string Currency { get; set; } // e.g., INR
    }
}
