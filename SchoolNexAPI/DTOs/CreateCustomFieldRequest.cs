using System.ComponentModel.DataAnnotations;

namespace SchoolNexAPI.DTOs
{
    public class CreateCustomFieldRequest
    {
        [Required]
        public string FieldName { get; set; }
        [Required]
        public string FieldType { get; set; }
        public bool IsRequired { get; set; }
        public int DisplayOrder { get; set; }
        [Required]
        public string TargetEntity { get; set; } // Student, Employee, etc.
        public List<string>? Options { get; set; } // e.g., ["Aadhar", "Pan", "DL"]
    }
}
