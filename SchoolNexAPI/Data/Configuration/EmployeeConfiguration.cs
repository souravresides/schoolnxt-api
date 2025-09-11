using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Razorpay.Api;
using SchoolNexAPI.Models;

namespace SchoolNexAPI.Data.Configuration
{
    public class EmployeeConfiguration : IEntityTypeConfiguration<EmployeeModel>
    {
        public void Configure(EntityTypeBuilder<EmployeeModel> builder)
        {
            builder.HasOne(x => x.School)
                 .WithMany(y => y.Employees)
                 .HasForeignKey(x => x.SchoolId)
                 .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(e => e.EmployeeId).IsUnique();
            builder.HasIndex(e => e.SchoolId);
            builder.HasIndex(e => e.Email).IsUnique();
            builder.HasIndex(e => e.MobileNumber).IsUnique(false);

            builder.HasIndex(e => new { e.FirstName, e.LastName }); // composite index
            builder.HasIndex(e => e.UserRole);
            builder.HasIndex(e => e.Department);
        }
    }
}
