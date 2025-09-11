namespace SchoolNexAPI.DTOs.Fee
{
    public class GenerateInvoiceDto
    {
        public Guid? SchoolId { get; set; }
        public Guid StudentId { get; set; }
        public Guid AcademicYearId { get; set; }
        public DateTime DueDate { get; set; }
    }
    public class InvoiceDto
    {
        public Guid Id { get; set; }
        public string InvoiceNumber { get; set; }
        public Guid StudentId { get; set; }
        public DateTime IssueDate { get; set; }
        public DateTime DueDate { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal AmountDue { get; set; }
        public List<InvoiceLineDto> Lines { get; set; }
    }
    public class InvoiceLineDto { public Guid FeeHeadId { get; set; } public decimal Amount { get; set; } public decimal Tax { get; set; } }


}
