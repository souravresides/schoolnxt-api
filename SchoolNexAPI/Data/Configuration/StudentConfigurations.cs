using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchoolNexAPI.Models;
using System.Reflection.Emit;

namespace SchoolNexAPI.Data.Configuration
{
    public class StudentConfigurations : IEntityTypeConfiguration<StudentModel>
    {
        public void Configure(EntityTypeBuilder<StudentModel> builder)
        {

            builder.HasOne(x => x.School)
                 .WithMany(y => y.Students)
                 .HasForeignKey(x => x.SchoolId)
                 .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
