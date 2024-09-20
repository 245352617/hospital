using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace YiJian.ECIS.IMService.EntityFrameworkCore
{
    public class IMServiceHttpApiHostMigrationsDbContextFactory : IDesignTimeDbContextFactory<IMServiceHttpApiHostMigrationsDbContext>
    {
        public IMServiceHttpApiHostMigrationsDbContext CreateDbContext(string[] args)
        {
            var configuration = BuildConfiguration();

            var builder = new DbContextOptionsBuilder<IMServiceHttpApiHostMigrationsDbContext>()
                .UseSqlServer(configuration.GetConnectionString("IMService"));

            return new IMServiceHttpApiHostMigrationsDbContext(builder.Options);
        }

        private static IConfigurationRoot BuildConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false);

            return builder.Build();
        }
    }
}
