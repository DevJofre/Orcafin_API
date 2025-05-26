using Microsoft.EntityFrameworkCore;
using Orcafin.Domain.Entities;
using Orcafin.Infrastructure.Configuration;

namespace Orcafin.Infrastructure.Context
{
    public class OrcafinDbContext : DbContext
    {
        public OrcafinDbContext(DbContextOptions<OrcafinDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Aplica a configuração da entidade User (e outras que você criar futuramente)
            modelBuilder.ApplyConfiguration(new UserConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}
