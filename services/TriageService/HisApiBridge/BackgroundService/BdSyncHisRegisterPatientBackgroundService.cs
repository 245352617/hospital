using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SamJan.MicroService.PreHospital.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SamJan.MicroService.PreHospital.TriageService
{
    /// <summary>
    /// 北大医院同步挂号列表
    /// TODO: 系统使用微服务架构，后续后台任务应单独使用一个服务托管
    /// </summary>
    public class BdSyncHisRegisterPatientBackgroundService : BackgroundService
    {
        private readonly IConfiguration _configuration;
        private readonly IHisApi _hisApi;
        private readonly ILogger<BdSyncHisRegisterPatientBackgroundService> _log;

        public BdSyncHisRegisterPatientBackgroundService(IConfiguration configuration, IHisApi hisApi, ILogger<BdSyncHisRegisterPatientBackgroundService> log)
        {
            this._configuration = configuration;
            this._hisApi = hisApi;
            _log = log;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                if (!int.TryParse(this._configuration["PekingUniversity:RegisterPatientSyncDelay"], out int delay) || delay <= 0)
                {
                    return;
                }
                _log.LogInformation($"Sync Register Patient Info From HIS");
                try
                {
                    if (_configuration["PekingUniversity:SyncRegisterPatientVersion"].ParseToInt() == 2)
                        await this._hisApi.SyncRegisterPatientFromHisV2();
                    else
                        await _hisApi.SyncRegisterPatientFromHis();
                }
                catch (Exception ex)
                {
                    _log.LogError($"同步挂号信息出错：{ex.Message}");
                }

                await Task.Delay(delay * 1000);
            }
        }
    }
}
