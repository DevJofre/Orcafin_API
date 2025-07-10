using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Orcafin.Domain.Entities;
using Orcafin.Domain.Enums;

namespace Orcafin.Infrastructure.Configuration
{
    public class PaymentHistoryConfiguration : IEntityTypeConfiguration<PaymentHistory>
    {
        public void Configure(EntityTypeBuilder<PaymentHistory> builder)
        {
            builder.HasKey(ph => ph.Id);

            builder.Property(ph => ph.PaidAt)
                   .IsRequired();

            builder.Property(ph => ph.Status)
                   .HasConversion(
                       v => v.ToString(),
                       v => (PaymentStatusEnum)Enum.Parse(typeof(PaymentStatusEnum), v));

            // Configuração da chave estrangeira para User
            builder.HasOne(ph => ph.User)
                   .WithMany()
                   .HasForeignKey(ph => ph.UserId)
                   .OnDelete(DeleteBehavior.Restrict); // Ou Cascade, dependendo da regra de negócio

            // Configuração da chave estrangeira para PaymentType
            builder.HasOne(ph => ph.PaymentType)
                   .WithMany()
                   .HasForeignKey(ph => ph.PaymentTypeId)
                   .OnDelete(DeleteBehavior.Restrict); // Ou Cascade, dependendo da regra de negócio
        }
    }
}