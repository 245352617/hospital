using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace YiJian.EMR.EntityFrameworkCore
{
    public class EMRHttpApiHostMigrationsDbContextFactory : IDesignTimeDbContextFactory<EMRHttpApiHostMigrationsDbContext>
    {
        public EMRHttpApiHostMigrationsDbContext CreateDbContext(string[] args)
        {
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            var configuration = BuildConfiguration();

            var builder = new DbContextOptionsBuilder<EMRHttpApiHostMigrationsDbContext>()
                .UseSqlServer(configuration.GetConnectionString("EMR"), x =>
                {
                    if (!string.IsNullOrEmpty(environment) && environment.ToLower() == "production")
                    {// 生成发布版本的迁移文件
                        x.MigrationsAssembly("YiJian.EMR.Migrations");
                    }
                });

            return new EMRHttpApiHostMigrationsDbContext(builder.Options);
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
