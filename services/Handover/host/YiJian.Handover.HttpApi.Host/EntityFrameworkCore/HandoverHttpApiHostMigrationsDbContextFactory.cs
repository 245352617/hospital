using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace YiJian.Handover.EntityFrameworkCore
{
    public class HandoverHttpApiHostMigrationsDbContextFactory : IDesignTimeDbContextFactory<HandoverHttpApiHostMigrationsDbContext>
    {
        public HandoverHttpApiHostMigrationsDbContext CreateDbContext(string[] args)
        {
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            var configuration = BuildConfiguration();

            var builder = new DbContextOptionsBuilder<HandoverHttpApiHostMigrationsDbContext>()
                .UseSqlServer(configuration.GetConnectionString("Handover"), x =>
                {
                    if (!string.IsNullOrEmpty(environment) && environment.ToLower() == "production")
                    {// 生成发布版本的迁移文件
                        x.MigrationsAssembly("YiJian.Handover.Migrations");
                    }
                });

            return new HandoverHttpApiHostMigrationsDbContext(builder.Options);
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
