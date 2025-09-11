using System.ComponentModel.DataAnnotations;

namespace SchoolNexAPI.Models.Fees
{
    public class StudentFeePlan
    {
        [Key] public Guid Id { get; set; }
        public Guid SchoolId { get; set; }
        public Guid AcademicYearId { get; set; }
        public Guid StudentId { get; set; }
        public Guid FeeStructureId { get; set; }
        public ICollection<StudentFeePlanLine> Lines { get; set; } = new List<StudentFeePlanLine>();
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public string CreatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string? UpdatedBy { get; set; }

        [Timestamp] public byte[] RowVersion { get; set; }
    }
    public class StudentFeePlanLine
    {
        [Key] public Guid Id { get; set; }
        public Guid StudentFeePlanId { get; set; }
        public Guid FeeHeadId { get; set; }
        public decimal Amount { get; set; }
        public decimal Discount { get; set; } = 0m;
    }


}
