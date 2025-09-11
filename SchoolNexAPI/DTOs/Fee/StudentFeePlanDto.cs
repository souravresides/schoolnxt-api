namespace SchoolNexAPI.DTOs.Fee
{
    public class StudentFeePlanCreateDto
    {
        public Guid? SchoolId { get; set; }
        public Guid StudentId { get; set; }
        public Guid AcademicYearId { get; set; }
        public Guid FeeStructureId { get; set; }
    }
    public class StudentFeePlanDto
    {
        public Guid Id { get; set; }
        public Guid StudentId { get; set; }
        public Guid AcademicYearId { get; set; }
        public List<StudentFeePlanLineDto> Lines { get; set; }
    }
    public class StudentFeePlanLineDto { public Guid FeeHeadId { get; set; } public decimal Amount { get; set; } public decimal Discount { get; set; } }

}
