using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Razorpay.Api;
using SchoolNexAPI.Models;

namespace SchoolNexAPI.Data.Configuration
{
    public class FileRecordsConfiguration : IEntityTypeConfiguration<FileRecordsModel>
    {
        public void Configure(EntityTypeBuilder<FileRecordsModel> builder)
        {
            builder.HasKey(f => f.Id);

            builder.HasQueryFilter(f => f.DeletedAt == null);

            // Indexes for tenant isolation and query performance
            builder.HasIndex(f => new { f.SchoolId, f.Entity, f.EntityId })
                  .HasDatabaseName("IX_Files_School_Entity");

            builder.HasIndex(f => new { f.Entity, f.EntityId })
                  .HasDatabaseName("IX_Files_Entity");

            builder.HasIndex(f => f.CreatedAt)
                  .HasDatabaseName("IX_Files_CreatedAt");

            builder.HasIndex(f => f.UploadedByUserId)
                  .HasDatabaseName("IX_Files_UploadedBy");

            builder.HasOne(x => x.School)
                 .WithMany(y => y.FileRecords)
                 .HasForeignKey(x => x.SchoolId)
                 .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
