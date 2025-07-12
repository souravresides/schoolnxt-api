using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchoolNexAPI.Models;

namespace SchoolNexAPI.Data.Configuration
{
    public class StudentEnrollmentConfiguration : IEntityTypeConfiguration<StudentEnrollmentModel>
    {
        public void Configure(EntityTypeBuilder<StudentEnrollmentModel> builder)
        {
            builder.HasOne(x => x.Student)
                   .WithMany(y => y.Enrollments)
                   .HasForeignKey(x => x.StudentId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.AcademicYear)
                   .WithMany(y => y.Enrollments)
                   .HasForeignKey(x => x.AcademicYearId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.Property(x => x.Class).HasMaxLength(50);
            builder.Property(x => x.Section).HasMaxLength(10);
        }
    }
}
