using SchoolNexAPI.Models.Fees;

namespace SchoolNexAPI.DTOs.Fee
{
    public class FeeStructureCreateDto
    {
        public Guid? SchoolId { get; set; } // optional for SuperAdmin
        public Guid AcademicYearId { get; set; }
        public string ClassName { get; set; }
        public string? Section { get; set; }
        public BillingCycle BillingCycle { get; set; }
        public List<FeeStructureItemDto> Items { get; set; } = new();
    }
    public class FeeStructureItemDto { public Guid FeeHeadId { get; set; } public decimal Amount { get; set; } }
    public class FeeStructureDto
    {
        public Guid Id { get; set; }
        public string ClassName { get; set; }
        public List<FeeStructureItemDto> Items { get; set; }
    }

}
