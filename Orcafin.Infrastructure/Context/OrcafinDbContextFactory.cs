using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Orcafin.Infrastructure.Context;
using System.IO;

namespace Orcafin.Infrastructure.Context
{
    public class OrcafinDbContextFactory : IDesignTimeDbContextFactory<OrcafinDbContext>
    {
        public OrcafinDbContext CreateDbContext(string[] args)
        {
            // Build configuration
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../Orcafin"))
                .AddJsonFile("appsettings.Development.json")
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<OrcafinDbContext>();
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            optionsBuilder.UseNpgsql(connectionString);

            return new OrcafinDbContext(optionsBuilder.Options);
        }
    }
}
