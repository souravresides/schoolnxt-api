namespace SchoolNexAPI.DTOs
{
    public class EmailSenderDto
    {
        public string RecipientName { get; set; }
        public string Subject { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public DateTime Date { get; set; }
        public string Body { get; set; }
        public Stream EmailAttachment { get; set; }

    }
}
