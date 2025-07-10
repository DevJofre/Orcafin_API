using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Orcafin.Domain.Entities;
using Orcafin.Domain.Enums;

namespace Orcafin.Infrastructure.Configuration
{
    public class TransactionHistoryConfiguration : IEntityTypeConfiguration<TransactionHistory>
    {
        public void Configure(EntityTypeBuilder<TransactionHistory> builder)
        {
            builder.HasKey(th => th.Id);

            builder.Property(th => th.Identifier)
                   .IsRequired()
                   .HasMaxLength(255);

            builder.Property(th => th.Description)
                   .IsRequired()
                   .HasMaxLength(500);

            builder.Property(th => th.Amount)
                   .HasColumnType("decimal(18,2)")
                   .IsRequired();

            builder.Property(th => th.Type)
                   .HasConversion(
                       v => v.ToString(),
                       v => (TransactionType)Enum.Parse(typeof(TransactionType), v))
                   .IsRequired();

            builder.Property(th => th.TransactionAt)
                   .IsRequired();

            // Configuração da chave estrangeira para User
            builder.HasOne(th => th.User)
                   .WithMany()
                   .HasForeignKey(th => th.UserId)
                   .OnDelete(DeleteBehavior.Restrict); // Ou Cascade, dependendo da regra de negócio

            // Configuração da chave estrangeira para Category
            builder.HasOne(th => th.Category)
                   .WithMany()
                   .HasForeignKey(th => th.CategoryId)
                   .OnDelete(DeleteBehavior.Restrict); // Ou Cascade, dependendo da regra de negócio
        }
    }
}