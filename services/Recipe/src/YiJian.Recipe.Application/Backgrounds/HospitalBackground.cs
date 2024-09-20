using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using YiJian.DoctorsAdvices;
using YiJian.Recipe.Application.Backgrounds.Contracts;

namespace YiJian.Recipe.Application.Backgrounds
{
    /// <summary>
    /// 医嘱状态查询
    /// </summary>
    public class HospitalBackground : IHospitalBackground
    {
        private readonly IDoctorsAdviceAppService _doctorsAdviceAppService;
        private readonly ILogger<HospitalBackground> _logger;

        /// <summary>
        /// 医嘱状态查询
        /// </summary>
        /// <param name="doctorsAdviceAppService"></param>
        /// <param name="logger"></param>
        public HospitalBackground(
            IDoctorsAdviceAppService doctorsAdviceAppService,
             ILogger<HospitalBackground> logger)
        {
            _doctorsAdviceAppService = doctorsAdviceAppService;
            _logger = logger;
        }

        /// <summary>
        ///QueryMedicalInfo
        /// </summary>
        public async Task QueryMedicalInfo()
        {
            _logger.LogInformation($"【Hangfire执行时间】=> {DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}");
            await _doctorsAdviceAppService.QueryAndOptMedicalInfoAsync();
            //_doctorsAdviceAppService.QueryAndOptMedicalInfoAsync().ConfigureAwait(false);
        }

    }
}
