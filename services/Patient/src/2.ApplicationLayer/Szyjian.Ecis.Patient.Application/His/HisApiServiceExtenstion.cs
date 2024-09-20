using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Szyjian.Ecis.Patient.Application.Hospital;
using Szyjian.Ecis.Patient.Application.Hospital.Base;

namespace Szyjian.Ecis.Patient.Application
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
            // HIS Api 相关配置
            services.AddTransient<CommonApi>();

            var hisCode = config.GetValue<string>("HospitalCode") switch
            {
                "PKU" => services.AddTransient<IHospitalApi, PKUspecificApi>(),
                _ => services.AddTransient<IHospitalApi, CommonApi>(),
            };

        }
    }
}
