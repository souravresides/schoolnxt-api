using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchoolNexAPI.Models;

namespace SchoolNexAPI.Data.Configuration
{
    public class PaymentTransactionConfiguration : IEntityTypeConfiguration<PaymentTransactionModel>
    {
        public void Configure(EntityTypeBuilder<PaymentTransactionModel> builder)
        {
            builder.Property(st => st.AmountPaid).HasColumnType("decimal(18,2)");
        }
    }
}
