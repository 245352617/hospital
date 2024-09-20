using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace SamJan.MicroService.PreHospital.TriageService
{
    /// <summary>
    /// 外部 API 注入扩展
    /// </summary>
    public static class ExternalApiServiceCollectionExtensions
    {
        /// <summary>
        /// 注入外部 API
        /// </summary>
        /// <param name="service"></param>
        public static void ConfigureExternalApi(this IServiceCollection service)
        {
            var config = service.GetConfiguration();
            service.ConfigureRfidApi(config);
        }

        /// <summary>
        /// 注入 RFID API
        /// </summary>
        /// <param name="service"></param>
        /// <param name="configuration"></param>
        private static void ConfigureRfidApi(this IServiceCollection service,IConfiguration configuration)
        {
            if (!configuration.GetValue<bool>("Settings:RFID:IsEnabled")) return;
            if (configuration["HospitalCode"] == "Longgang")
            {
                service.AddTransient<IRfidApi, LonggangRfidApi>();
            }
        }
    }
}