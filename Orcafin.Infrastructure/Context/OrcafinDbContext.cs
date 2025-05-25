using Microsoft.EntityFrameworkCore;
using Orcafin.Domain.Entities;

namespace Orcafin.Infrastructure.Context
{
    public class OrcafinDbContext : DbContext
    {
        public OrcafinDbContext(DbContextOptions<OrcafinDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
    }
}
 