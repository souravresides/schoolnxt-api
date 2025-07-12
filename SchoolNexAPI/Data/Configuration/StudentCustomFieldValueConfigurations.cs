using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchoolNexAPI.Models;
using System.Reflection.Emit;

namespace SchoolNexAPI.Data.Configuration
{
    public class StudentCustomFieldValueConfigurations : IEntityTypeConfiguration<StudentCustomFieldValueModel>
    {
        public void Configure(EntityTypeBuilder<StudentCustomFieldValueModel> builder)
        {

            builder.HasOne(x => x.Student)
                 .WithMany(y => y.CustomFieldValuesList)
                 .HasForeignKey(x => x.StudentId)
                 .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.CustomFieldDefinition)
                 .WithMany()
                 .HasForeignKey(x => x.CustomFieldDefinitionId)
                 .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
