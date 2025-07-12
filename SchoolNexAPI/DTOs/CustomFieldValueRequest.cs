namespace SchoolNexAPI.DTOs
{
    public class CustomFieldValueRequest
    {
        public Guid CustomFieldDefinitionId { get; set; }
        public string Value { get; set; }
    }

}
