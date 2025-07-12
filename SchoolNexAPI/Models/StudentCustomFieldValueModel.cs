using System.ComponentModel.DataAnnotations;

namespace SchoolNexAPI.Models
{
    public class StudentCustomFieldValueModel
    {
        [Key]
        public Guid Id { get; set; }

        public Guid StudentId { get; set; }
        public StudentModel Student { get; set; }
        public Guid SchoolId { get; set; }
        public SchoolModel School { get; set; }
        public Guid CustomFieldDefinitionId { get; set; }
        public CustomFieldDefinitionModel CustomFieldDefinition { get; set; }

        public string Value { get; set; }

        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string? UpdatedBy { get; set; }
    }
}
