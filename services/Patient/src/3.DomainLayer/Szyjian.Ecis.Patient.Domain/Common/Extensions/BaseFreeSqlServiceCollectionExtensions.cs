using FreeSql;
using FreeSql.Internal;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Data.Common;

namespace Szyjian.Ecis.Patient.Domain.Common.Extensions
{
    /// <summary>
    /// freesql扩展
    /// </summary>
    public static class BaseFreeSqlServiceCollectionExtensions
    {
        /// <summary>
        /// 添加freesql
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="services"></param>
        /// <param name="connectionStrings"></param>
        /// <param name="dataType"></param>
        /// <param name="isAutoSyncStructure"></param>
        public static void AddFreeSql<T>(
            this IServiceCollection services,
            string connectionStrings,
            DataType dataType = DataType.SqlServer,
            bool isAutoSyncStructure = false)
        {
            string env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            var implementationInstance = new FreeSqlBuilder().UseConnectionString(dataType, connectionStrings).UseNameConvert(NameConvertType.None).UseAutoSyncStructure(isAutoSyncStructure).UseMonitorCommand((Action<DbCommand>)(cmd =>
            {
                if (!string.Equals(env, Environments.Development, StringComparison.OrdinalIgnoreCase))
                    return;
                Console.WriteLine("【 SQL：" + cmd.CommandText + " 】");
            })).Build<T>();
            services.AddSingleton(implementationInstance);
            services.AddScoped<UnitOfWorkManager>();
        }
    }
}
