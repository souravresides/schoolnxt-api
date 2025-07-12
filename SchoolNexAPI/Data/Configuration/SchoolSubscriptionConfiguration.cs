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
                   .WithOne(sub => sub.SchoolSubscription)
                   .HasForeignKey<SchoolSubscriptionModel>(sub => sub.SubscriptionTypeId);
        }
    }
}
