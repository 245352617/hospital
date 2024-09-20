using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace YiJian.MasterData.EntityFrameworkCore
{
    public class MasterDataHttpApiHostMigrationsDbContextFactory : IDesignTimeDbContextFactory<MasterDataHttpApiHostMigrationsDbContext>
    {
        public MasterDataHttpApiHostMigrationsDbContext CreateDbContext(string[] args)
        {
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            var configuration = BuildConfiguration();

            var builder = new DbContextOptionsBuilder<MasterDataHttpApiHostMigrationsDbContext>()
                .UseSqlServer(configuration.GetConnectionString("Default"), x =>
                {
                    if (!string.IsNullOrEmpty(environment) && environment.ToLower() == "production")
                    {// 生成发布版本的迁移文件
                        x.MigrationsAssembly("YiJian.MasterData.Migrations" );
                    }
                });

            // var builder = new DbContextOptionsBuilder<MasterDataHttpApiHostMigrationsDbContext>()
            //     .UseSqlServer(configuration.GetConnectionString("Default"));

            return new MasterDataHttpApiHostMigrationsDbContext(builder.Options);
        }

        private static IConfigurationRoot BuildConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false)
                .AddJsonFile("appsettings.development.json", optional: true);

            return builder.Build();
        }
    }
}
