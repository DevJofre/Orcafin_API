using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Orcafin.Domain.Entities;
using Orcafin.Domain.Enums;

namespace Orcafin.Infrastructure.Configuration
{
    public class SubscriptionPlanConfiguration : IEntityTypeConfiguration<SubscriptionPlan>
    {
        public void Configure(EntityTypeBuilder<SubscriptionPlan> builder)
        {
            builder.HasKey(sp => sp.Id);

            builder.Property(sp => sp.Type)
                   .HasConversion(
                       v => v.ToString(),
                       v => (SubscriptionType)Enum.Parse(typeof(SubscriptionType), v));

            builder.Property(sp => sp.GatewayId)
                   .IsRequired()
                   .HasMaxLength(255);
        }
    }
}