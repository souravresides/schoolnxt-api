namespace SchoolNexAPI.DTOs
{
    public class CustomFieldDto
    {
        public Guid Id { get; set; }
        public string FieldName { get; set; }
        public string FieldValue { get; set; }
        public string FieldType { get; set; }
        public bool IsRequired { get; set; }
        public int DisplayOrder { get; set; }
        public string TargetEntity { get; set; }
        public string? FieldOptionsJson { get; set; }
        public List<string>? Options { get; set; }
    }
}
