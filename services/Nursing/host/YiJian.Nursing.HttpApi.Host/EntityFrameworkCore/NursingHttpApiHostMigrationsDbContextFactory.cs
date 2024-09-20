using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace YiJian.Nursing.EntityFrameworkCore
{
    /// <summary>
    /// 数据库上下文工厂
    /// </summary>
    public class NursingHttpApiHostMigrationsDbContextFactory : IDesignTimeDbContextFactory<NursingHttpApiHostMigrationsDbContext>
    {
        /// <summary>
        /// 创建数据库上下文
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public NursingHttpApiHostMigrationsDbContext CreateDbContext(string[] args)
        {
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            var configuration = BuildConfiguration();

            var builder = new DbContextOptionsBuilder<NursingHttpApiHostMigrationsDbContext>()
                .UseSqlServer(configuration.GetConnectionString("Nursing"), x =>
                {
                    if (!string.IsNullOrEmpty(environment) && environment.ToLower() == "production")
                    {   // 生成发布版本的迁移文件
                        x.MigrationsAssembly("YiJian.Nursing.Migrations");
                    }
                });

            return new NursingHttpApiHostMigrationsDbContext(builder.Options);
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
