using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchoolNexAPI.Models;

namespace SchoolNexAPI.Data.Configuration
{
    public class SchoolConfiguration : IEntityTypeConfiguration<SchoolModel>
    {
        public void Configure(EntityTypeBuilder<SchoolModel> builder)
        {

            builder.HasOne(s => s.SchoolSubscription)
                   .WithOne(sub => sub.School)
                   .HasForeignKey<SchoolSubscriptionModel>(sub => sub.SchoolId);

            builder.HasOne(s => s.SchoolSettings)
                   .WithOne(setting => setting.School)
                   .HasForeignKey<SchoolSettingsModel>(setting => setting.SchoolId);
        }
    }
}
