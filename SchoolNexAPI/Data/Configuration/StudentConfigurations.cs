using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchoolNexAPI.Models.Student;
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

            builder.HasOne(s => s.AdditionalDetails)
                .WithOne(d => d.Student)
                .HasForeignKey<StudentAdditionalDetailsModel>(d => d.StudentId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(s => s.Address)
                .WithOne(a => a.Student)
                .HasForeignKey<StudentAddressModel>(a => a.StudentId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(s => s.BankDetails)
                .WithOne(b => b.Student)
                .HasForeignKey<StudentBankDetailsModel>(b => b.StudentId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(s => s.MedicalRecord)
                .WithOne(m => m.Student)
                .HasForeignKey<StudentMedicalRecordModel>(m => m.StudentId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(s => s.PreviousSchool)
                .WithOne(p => p.Student)
                .HasForeignKey<StudentPreviousSchoolModel>(p => p.StudentId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(s => s.Transportation)
                .WithOne(t => t.Student)
                .HasForeignKey<StudentTransportationModel>(t => t.StudentId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(s => s.Parents)
                .WithOne(p => p.Student)
                .HasForeignKey(p => p.StudentId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasIndex(s => new { s.SchoolId, s.IsActive })
            .HasDatabaseName("IX_Students_SchoolId_IsActive");

            builder.HasIndex(s => new { s.SchoolId, s.AcademicYearId, s.IsActive })
                .HasDatabaseName("IX_Students_SchoolId_Year_IsActive");

            builder.HasIndex(s => s.PhoneNumber)
                .IsUnique()
                .HasDatabaseName("IX_Students_PhoneNumber");

            builder.HasIndex(s => s.Email)
                .IsUnique()
                .HasDatabaseName("IX_Students_Email");

            builder.HasIndex(s => new { s.SchoolId, s.CreatedAt })
                .HasDatabaseName("IX_Students_SchoolId_CreatedAt");

            builder.HasIndex(s => s.FullName)
                .HasDatabaseName("IX_Students_FullName");

        }
    }
}
