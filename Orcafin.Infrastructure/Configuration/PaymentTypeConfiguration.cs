using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Orcafin.Domain.Entities;
using Orcafin.Domain.Enums;

namespace Orcafin.Infrastructure.Configuration
{
    public class PaymentTypeConfiguration : IEntityTypeConfiguration<PaymentType>
    {
        public void Configure(EntityTypeBuilder<PaymentType> builder)
        {
            builder.HasKey(pt => pt.Id);

            builder.Property(pt => pt.Name)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(pt => pt.Status)
                   .HasConversion(
                       v => v.ToString(),
                       v => (PaymentStatus)Enum.Parse(typeof(PaymentStatus), v));
        }
    }
}