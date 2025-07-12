namespace SchoolNexAPI.DTOs
{
    public class UpdateCustomFieldRequest
    {
        public Guid Id { get; set; }
        public string FieldName { get; set; }
        public string FieldType { get; set; }
        public bool IsRequired { get; set; }
        public int DisplayOrder { get; set; }
        public string TargetEntity { get; set; }
    }
}
