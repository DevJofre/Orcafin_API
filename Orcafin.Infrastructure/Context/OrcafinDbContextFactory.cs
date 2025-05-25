using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Orcafin.Infrastructure.Context;

namespace Orcafin.Infrastructure.Context
{
    public class OrcafinDbContextFactory : IDesignTimeDbContextFactory<OrcafinDbContext>
    {
        public OrcafinDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<OrcafinDbContext>();

            optionsBuilder.UseSqlServer("Server=localhost;Database=OrcafinDB;User Id=sa;Password=!Admin123;TrustServerCertificate=True");

            return new OrcafinDbContext(optionsBuilder.Options);
        }
    }
}
