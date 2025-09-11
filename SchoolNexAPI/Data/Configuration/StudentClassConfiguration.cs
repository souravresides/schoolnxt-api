using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchoolNexAPI.Models;

namespace SchoolNexAPI.Data.Configuration
{
    public class StudentClassConfiguration : IEntityTypeConfiguration<StudentClassModel>
    {
        public void Configure(EntityTypeBuilder<StudentClassModel> builder)
        {

            builder.HasMany(c => c.Students)
            .WithOne(s => s.StudentClass)
            .HasForeignKey(s => s.ClassId)
            .OnDelete(DeleteBehavior.Restrict);
        }
    }
    public class ClassSectionConfiguration : IEntityTypeConfiguration<ClassSectionModel>
    {
        public void Configure(EntityTypeBuilder<ClassSectionModel> builder)
        {
            builder.HasMany(cs => cs.Students)
           .WithOne(s => s.ClassSection)
           .HasForeignKey(s => s.SectionId)
           .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
