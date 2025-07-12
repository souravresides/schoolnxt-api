using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchoolNexAPI.Models;

namespace SchoolNexAPI.Data.Configuration
{
    public class AcademicYearConfiguration : IEntityTypeConfiguration<AcademicYearModel>
    {
        public void Configure(EntityTypeBuilder<AcademicYearModel> builder)
        {
            builder.Property(x => x.YearName).IsRequired().HasMaxLength(20);

            builder.HasOne(x => x.School)
                   .WithMany(y => y.AcademicYears)
                   .HasForeignKey(x => x.SchoolId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
