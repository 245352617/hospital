using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace SamJan.MicroService.PreHospital.TriageService
{
    public class PreHospitalTriageMigrationsDbContextFactory : IDesignTimeDbContextFactory<PreHospitalTriageDbContext>
    {
        public PreHospitalTriageDbContext CreateDbContext(string[] args)
        {
            var configuration = BuildConfiguration();

            var builder = new DbContextOptionsBuilder<PreHospitalTriageDbContext>()
                .UseSqlServer(configuration.GetConnectionString("DefaultConnection"));

            return new PreHospitalTriageDbContext(builder.Options, configuration);
        }

        private static IConfigurationRoot BuildConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../TriageService/"))
                .AddJsonFile("appsettings.json", optional: false)
                .AddJsonFile("appsettings.Development.json",false);

            return builder.Build();
        }
    }
}
