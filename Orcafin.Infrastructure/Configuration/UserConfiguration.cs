using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Orcafin.Domain.Entities;

namespace Orcafin.Infrastructure.Configuration
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");

            builder.HasKey(u => u.Id);

            builder.Property(u => u.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(u => u.Email)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(u => u.PasswordHash)
                .IsRequired()
                .HasMaxLength(255);

            // Seed de dados
            builder.HasData(
                new User { Id = 1, Name = "Jofre Tomas", Email = "jofre.tomas@email.com", PasswordHash = "3c4f9a1e..." },
                new User { Id = 2, Name = "Maria Silva", Email = "maria.silva@email.com", PasswordHash = "6b2f1c7e..." },
                new User { Id = 3, Name = "João Pedro", Email = "joao.pedro@email.com", PasswordHash = "e5d1a2f3..." },
                new User { Id = 4, Name = "Ana Paula", Email = "ana.paula@email.com", PasswordHash = "9f8e7b6a..." },
                new User { Id = 5, Name = "Lucas Oliveira", Email = "lucas.oliveira@email.com", PasswordHash = "d4a3b1c9..." },
                new User { Id = 6, Name = "Fernanda Costa", Email = "fernanda.costa@email.com", PasswordHash = "8a2b3c4d..." },
                new User { Id = 7, Name = "Carlos Mendes", Email = "carlos.mendes@email.com", PasswordHash = "1c2d3e4f..." },
                new User { Id = 8, Name = "Juliana Rocha", Email = "juliana.rocha@email.com", PasswordHash = "5a6b7c8d..." },
                new User { Id = 9, Name = "Rafael Lima", Email = "rafael.lima@email.com", PasswordHash = "7d6c5b4a..." },
                new User { Id = 10, Name = "Beatriz Almeida", Email = "beatriz.almeida@email.com", PasswordHash = "2e3f4a5b..." }
            );

        }
    }
}
