using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace SchoolNexAPI.Models
{
    public class SubscriptionTypeModel
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; } // Free, Standard, Premium
        public decimal PricePerMonth { get; set; }
        public int MaxStudents { get; set; }
        public int MaxEmployees { get; set; }
        public string FeaturesJson { get; set; }
        public bool IsActive { get; set; } = true;
        public string Description { get; set; }

        //Relationships
        public SchoolSubscriptionModel SchoolSubscription { get; set; }
    }
}
