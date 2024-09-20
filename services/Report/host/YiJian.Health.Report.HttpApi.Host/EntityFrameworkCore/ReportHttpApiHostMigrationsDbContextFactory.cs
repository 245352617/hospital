using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace YiJian.Health.Report.EntityFrameworkCore
{
    public class ReportHttpApiHostMigrationsDbContextFactory : IDesignTimeDbContextFactory<ReportHttpApiHostMigrationsDbContext>
    {
        public ReportHttpApiHostMigrationsDbContext CreateDbContext(string[] args)
        {
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            var configuration = BuildConfiguration();

            var builder = new DbContextOptionsBuilder<ReportHttpApiHostMigrationsDbContext>()
                .UseSqlServer(configuration.GetConnectionString("Report"), x =>
                {
                    if (!string.IsNullOrEmpty(environment) && environment.ToLower() == "production")
                    {// 生成发布版本的迁移文件
                        x.MigrationsAssembly("YiJian.Health.Report.Migrations");
                    }
                });

            return new ReportHttpApiHostMigrationsDbContext(builder.Options);
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
