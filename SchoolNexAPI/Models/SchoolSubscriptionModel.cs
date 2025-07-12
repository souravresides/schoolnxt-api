using SchoolNexAPI.Migrations;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace SchoolNexAPI.Models
{
    public class SchoolSubscriptionModel
    {
        [Key]
        public Guid Id { get; set; }
        public Guid SchoolId { get; set; }
        
        public SchoolModel School { get; set; }
        public Guid SubscriptionTypeId { get; set; }
        
        public SubscriptionTypeModel SubscriptionType { get; set; }
        public SubscriptionTerm SubscriptionTerm { get; set; }
        public DateTime? SubscriptionExpiresOn { get; set; }
        public string Remarks { get; set; }
    }

}
