using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using SchoolNexAPI.Models;

namespace SchoolNexAPI.Data.Configuration
{
    public class SubscriptionTypeConfiguration : IEntityTypeConfiguration<SubscriptionTypeModel>
    {
        public void Configure(EntityTypeBuilder<SubscriptionTypeModel> builder)
        {
            builder.Property(st => st.PricePerMonth).HasColumnType("decimal(18,2)");

        }
    }
}
