using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace YiJian.ECIS.Call.EntityFrameworkCore
{
    public class CallHttpApiHostMigrationsDbContextFactory : IDesignTimeDbContextFactory<CallHttpApiHostMigrationsDbContext>
    {
        public CallHttpApiHostMigrationsDbContext CreateDbContext(string[] args)
        {
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            var configuration = BuildConfiguration();

            var builder = new DbContextOptionsBuilder<CallHttpApiHostMigrationsDbContext>()
                .UseSqlServer(configuration.GetConnectionString("Call"), x =>
                    {
                        if (!string.IsNullOrEmpty(environment) && environment.ToLower() == "production")
                        {// 生成发布版本的迁移文件
                            x.MigrationsAssembly("YiJian.Call.Migrations");
                        }
                    });            return new CallHttpApiHostMigrationsDbContext(builder.Options);
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
