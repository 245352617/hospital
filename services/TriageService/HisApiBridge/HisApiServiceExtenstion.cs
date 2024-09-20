using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace SamJan.MicroService.PreHospital.TriageService
{
    public static class HisApiServiceExtenstion
    {
        /// <summary>
        /// 根据医院不同HIS注入不同的API，以适配多个医院
        /// </summary>
        /// <param name="services"></param>
        /// <param name="config"></param>
        public static void ConfigureHisApi(this IServiceCollection services, IConfiguration config)
        {
            services.AddHttpClient("HisApi", httpClient =>
            {
                httpClient.BaseAddress = new Uri(config["HisApiSettings:BaseUrl"]);
            });

            // HIS Api 相关配置
            services.AddTransient<CommonHisApi>();
            services.AddTransient<IPekingUniversityHisApi, PekingUniversityHisApi>();
            var hisCode = config.GetValue<string>("HospitalCode");
            Console.WriteLine($"读取到的 HospitalCode 为 {hisCode}");
            if (hisCode == "PekingUniversity")
            {
                // 配置文件
                var dbBuilder = new ConfigurationBuilder()
                    .AddJsonFile("bdConfigs.json", true); // 北大特殊配置（科室队列配置）
                var queueConfiguration = dbBuilder.Build();
                services.Configure<BdQueueConfigs>(queueConfiguration.GetSection("QueueConfig"));
                services.AddTransient<IHisApi, PekingUniversityHisApi>();
                services.AddTransient<PekingUniversityHisApi>();
                services.AddTransient<ICallApi, PekingUniversityCallApi>();
                // 后台任务，同步 HIS 挂号列表
                services.AddHostedService<BdSyncHisRegisterPatientBackgroundService>();
            }
            else if (hisCode == "Longgang")
            {
                services.AddTransient<IHisApi, LonggangHisApi>();
                services.AddTransient<LonggangHisApi>();
            }
            else if (hisCode == "Jinwan")
            {
                services.AddTransient<IHisApi, JinwanHisApi>();
                services.AddTransient<JinwanHisApi>();
            }
            else if (hisCode == "Qianxinan")
            {
                services.AddTransient<IHisApi, QianxinanHisApi>();
                services.AddTransient<QianxinanHisApi>();
            }
            else if (hisCode == "Mock")
            {
                services.AddTransient<IHisApi, MockHisApi>();
                services.AddTransient<MockHisApi>();
            }
            else if (hisCode == "Shanda")
            {
                services.AddTransient<IHisApi, ShandaHisApi>();
                services.AddTransient<ShandaHisApi>();
            }
            else
            {
                services.AddTransient<IHisApi, CommonHisApi>();
                services.AddTransient<CommonHisApi>();
            }
        }
    }
}
