using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Orcafin.Domain.Entities;
using Orcafin.Domain.Enums;

namespace Orcafin.Infrastructure.Configuration
{
    public class UserAssignmentConfiguration : IEntityTypeConfiguration<UserAssignment>
    {
        public void Configure(EntityTypeBuilder<UserAssignment> builder)
        {
            builder.HasKey(ua => ua.Id);

            builder.Property(ua => ua.PaymentMethod)
                   .HasConversion(
                       v => v.ToString(),
                       v => (PaymentMethodType)Enum.Parse(typeof(PaymentMethodType), v));

            builder.Property(ua => ua.LastPaymentDate)
                   .IsRequired();

            builder.Property(ua => ua.NextPaymentDate)
                   .IsRequired();

            // Configuração da chave estrangeira para User
            builder.HasOne(ua => ua.User)
                   .WithMany()
                   .HasForeignKey(ua => ua.UserId)
                   .OnDelete(DeleteBehavior.Restrict); // Ou Cascade, dependendo da regra de negócio

            // Configuração da chave estrangeira para SubscriptionPlan
            builder.HasOne(ua => ua.SubscriptionPlan)
                   .WithMany()
                   .HasForeignKey(ua => ua.PlanId)
                   .OnDelete(DeleteBehavior.Restrict); // Ou Cascade, dependendo da regra de negócio
        }
    }
}