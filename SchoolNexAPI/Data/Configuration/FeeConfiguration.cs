using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchoolNexAPI.Models.Fees;
using SchoolNexAPI.Services.Abstract;

namespace SchoolNexAPI.Data.Configuration
{
    public class InvoiceConfiguration : IEntityTypeConfiguration<Invoice>
    {
        private readonly ITenantContext _tenant;

        public InvoiceConfiguration(ITenantContext tenant)
        {
            this._tenant = tenant;
        }
        public void Configure(EntityTypeBuilder<Invoice> builder)
        {
            builder.HasIndex(i => new { i.SchoolId, i.AcademicYearId, i.InvoiceNumber }).IsUnique();
            builder.HasQueryFilter(i => !_tenant.CurrentSchoolId.HasValue || i.SchoolId == _tenant.CurrentSchoolId.Value);
        }
    }
    public class PaymentConfiguration : IEntityTypeConfiguration<Payment>
    {
        private readonly ITenantContext _tenant;

        public PaymentConfiguration(ITenantContext tenant)
        {
            this._tenant = tenant;
        }
        public void Configure(EntityTypeBuilder<Payment> builder)
        {
            builder.HasIndex(p => new { p.SchoolId, p.IdempotencyKey }).IsUnique();
            builder.HasQueryFilter(p => !_tenant.CurrentSchoolId.HasValue || p.SchoolId == _tenant.CurrentSchoolId.Value);
        }
    }
    public class StudentFeeConfiguration : IEntityTypeConfiguration<StudentFeePlan>
    {
        private readonly ITenantContext _tenant;

        public StudentFeeConfiguration(ITenantContext tenant)
        {
            this._tenant = tenant;
        }
        public void Configure(EntityTypeBuilder<StudentFeePlan> builder)
        {
            builder.HasIndex(p => new { p.SchoolId, p.StudentId, p.AcademicYearId }).IsUnique();
            builder.HasQueryFilter(s => !_tenant.CurrentSchoolId.HasValue || s.SchoolId == _tenant.CurrentSchoolId.Value);
        }
    }
    public class AdjustmentConfiguration : IEntityTypeConfiguration<Adjustment>
    {
        private readonly ITenantContext _tenant;

        public AdjustmentConfiguration(ITenantContext tenant)
        {
            this._tenant = tenant;
        }
        public void Configure(EntityTypeBuilder<Adjustment> builder)
        {
            builder.HasQueryFilter(a => !_tenant.CurrentSchoolId.HasValue || a.SchoolId == _tenant.CurrentSchoolId.Value);
        }
    }
    public class FeeStructureConfiguration : IEntityTypeConfiguration<FeeStructure>
    {
        private readonly ITenantContext _tenant;

        public FeeStructureConfiguration(ITenantContext tenant)
        {
            this._tenant = tenant;
        }
        public void Configure(EntityTypeBuilder<FeeStructure> builder)
        {
            builder.HasQueryFilter(f => !_tenant.CurrentSchoolId.HasValue || f.SchoolId == _tenant.CurrentSchoolId.Value);
        }
    }
}
