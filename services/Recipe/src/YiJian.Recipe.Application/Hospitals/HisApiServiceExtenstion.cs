using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace YiJian.Hospitals
{
    public static class HospitalApiServiceExtenstion
    {
        /// <summary>
        /// 根据医院不同HIS注入不同的API，以适配多个医院
        /// </summary>
        /// <param name="services"></param>
        /// <param name="config"></param>
        public static void ConfigureHospitalApi(this IServiceCollection services, IConfiguration config)
        {
            var hisCode = config.GetValue<string>("HospitalCode");
            Console.WriteLine($"读取到的 HospitalCode 为 {hisCode}");
            if (hisCode == "PKU")
            {
                services.AddTransient<IHospitalAPI, PKUHospitalAPI>();
            }
            else
            {
                services.AddTransient<IHospitalAPI, NullHospitalAPI>();
            }
        }
    }
}
