using FreeSql;
using FreeSql.Internal;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Data.Common;

namespace Szyjian.BaseFreeSql
{
    /// <summary>
    /// freesql链接扩展
    /// </summary>
    public static class SzyjianBaseFreeSqlServiceCollectionExtensions
    {
        /// <summary>
        /// 添加SQLserver的freesql
        /// </summary>
        /// <param name="services"></param>
        /// <param name="connectionStrings"></param>
        public static void AddFreeSqlForSqlServer(this IServiceCollection services, string connectionStrings)
        {
            string env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            IFreeSql implementationInstance = new FreeSqlBuilder()
                .UseConnectionString(DataType.SqlServer, connectionStrings)
                .UseNameConvert(NameConvertType.None)
                .UseAutoSyncStructure(false)
                .UseMonitorCommand(delegate (DbCommand cmd)
                {
                    if (string.Equals(env, Environments.Development, StringComparison.OrdinalIgnoreCase))
                    {
                        Console.WriteLine("【 SQL：" + cmd.CommandText + " 】");
                    }
                })
                .Build();
            services.AddSingleton(implementationInstance);
            services.AddScoped<UnitOfWorkManager>();
        }
    }
}
