using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchoolNexAPI.Models;

namespace SchoolNexAPI.Data.Configuration
{
    public class SchoolSubscriptionConfiguration : IEntityTypeConfiguration<SchoolSubscriptionModel>
    {
        public void Configure(EntityTypeBuilder<SchoolSubscriptionModel> builder)
        {
            builder.HasOne(s => s.SubscriptionType)
                    .WithMany(sub => sub.SchoolSubscriptions) // one-to-many
                    .HasForeignKey(sub => sub.SubscriptionTypeId);
        }
    }
}
