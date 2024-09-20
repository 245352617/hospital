using BeetleX.Http.Clients;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Szyjian.Ecis.Patient.Application.Contracts;
using Szyjian.Ecis.Patient.Domain.Shared;

namespace Szyjian.Ecis.Patient.Application.Triages
{
    /// <summary>
    /// 分诊服务
    /// </summary>
    public class TriageAppService : EcisPatientAppService, ITriageAppService
    {
        private readonly IOptionsMonitor<RemoteServices> _remoteServices;
        private readonly ILogger<TriageAppService> _log;

        /// <summary>
        /// 分诊服务
        /// </summary>
        /// <param name="remoteServices"></param>
        /// <param name="log"></param>
        public TriageAppService(IOptionsMonitor<RemoteServices> remoteServices, ILogger<TriageAppService> log)
        {
            _remoteServices = remoteServices;
            _log = log;
        }


        /// <summary>
        /// 获取分诊科室配置信息
        /// </summary>
        /// <param name="triageConfigCodes">科室代码</param>
        /// <param name="authorization"></param>
        /// <returns>院内的HIS科室</returns>
        public async Task<List<DeptInfoDto>> GetTriageConfigPageListAsync(List<string> triageConfigCodes, string authorization)
        {
            try
            {
                _log.LogInformation($"获取分诊科室配置信息请求参数:{authorization}");
                var service = BuildHospitalService();
                var result = await service.GetTriageConfigPageListAsync(authorization);
                var retDepts = result.Data.Items.ToList().Where(w => triageConfigCodes.Contains(w.TriageConfigCode.Trim()));
                if (retDepts.Any()) return retDepts.ToList();
                return new List<DeptInfoDto>();
            }
            catch (Exception ex)
            {
                _log.LogError(ex, $"获取分诊科室配置信息异常：{ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// 构建代理服务
        /// </summary>
        /// <returns></returns>
        private ITriageProxyService BuildHospitalService()
        {
            var remoteServices = _remoteServices.CurrentValue;
            HttpCluster httpCluster = new HttpCluster();
            httpCluster.TimeOut = 10 * 60 * 1000;
            var host = remoteServices.Triage.BaseUrl;
            httpCluster.DefaultNode.Add(host);
            var service = httpCluster.Create<ITriageProxyService>();
            return service;
        }
    }
}
