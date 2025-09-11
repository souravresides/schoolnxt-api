using System.ComponentModel.DataAnnotations;

namespace SchoolNexAPI.Models.Fees
{
    public class FeeHead
    {
        [Key] public Guid Id { get; set; }
        public Guid SchoolId { get; set; }
        [Required] public string Name { get; set; } // Tuition, Transport
        public string? Description { get; set; }
    }

    public enum BillingCycle { OneTime = 0, Term = 1, Monthly = 2 }

    public class FeeStructure
    {
        [Key] public Guid Id { get; set; }
        public Guid SchoolId { get; set; }
        public Guid AcademicYearId { get; set; }
        [Required] public string ClassName { get; set; }
        public string? Section { get; set; }
        public BillingCycle BillingCycle { get; set; } = BillingCycle.Monthly;
        public ICollection<FeeStructureItem> Items { get; set; } = new List<FeeStructureItem>();
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public string CreatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string? UpdatedBy { get; set; }
    }
    public class FeeStructureItem
    {
        [Key] public Guid Id { get; set; }
        public Guid FeeStructureId { get; set; }
        public Guid FeeHeadId { get; set; }
        public decimal Amount { get; set; }  // decimal(18,2)
    }

}
