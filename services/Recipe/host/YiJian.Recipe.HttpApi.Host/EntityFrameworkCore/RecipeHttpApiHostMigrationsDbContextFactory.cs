using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace YiJian.Recipe.EntityFrameworkCore
{
    public class RecipeHttpApiHostMigrationsDbContextFactory : IDesignTimeDbContextFactory<RecipeHttpApiHostMigrationsDbContext>
    {
        public RecipeHttpApiHostMigrationsDbContext CreateDbContext(string[] args)
        {
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            var configuration = BuildConfiguration();

            var builder = new DbContextOptionsBuilder<RecipeHttpApiHostMigrationsDbContext>()
                .UseSqlServer(configuration.GetConnectionString("Recipe"), x =>
                {
                    if (!string.IsNullOrEmpty(environment) && environment.ToLower() == "production")
                    {// 生成发布版本的迁移文件
                        x.MigrationsAssembly("YiJian.Recipe.Migrations");
                    }
                });

            return new RecipeHttpApiHostMigrationsDbContext(builder.Options);
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
