using System.ComponentModel.DataAnnotations;

namespace SchoolNexAPI.Models
{
    public class CustomFieldDefinitionModel
    {
        [Key]
        public Guid Id { get; set; }

        public Guid SchoolId { get; set; }

        public string EntityType { get; set; }  // e.g., "Student"
        public string FieldName { get; set; }   // e.g., "FatherName"
        public string DisplayName { get; set; } // e.g., "Father's Full Name"
        public string FieldType { get; set; }   // e.g., "Text", "Dropdown"
        public bool IsVisible { get; set; } = true;
        public bool IsRequired { get; set; } = false;
        public int DisplayOrder { get; set; }
        public string? FieldOptionsJson { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string? UpdatedBy { get; set; }
    }
}
