using System.ComponentModel.DataAnnotations;

namespace SchoolNexAPI.Models.Fees
{
    public class InvoiceSequence
    {
        [Key] public Guid Id { get; set; }
        public Guid SchoolId { get; set; }
        public Guid AcademicYearId { get; set; }
        public long NextNumber { get; set; } = 1;
    }

}
