using BeetleX.Http.Clients;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using YiJian.Apis;
using YiJian.ECIS.DDP;
using YiJian.ECIS.ShareModel.DDPs;
using YiJian.ECIS.ShareModel.DDPs.Responses;
using YiJian.Health.Report.Hospitals.Dto;

namespace YiJian.Health.Report.Hospitals
{
    /// <summary>
    /// 医院系统客户端请求类
    /// </summary>
    public class HospitalClientAppService : ApplicationService , IHospitalClientAppService
    { 
        private readonly ILogger<HospitalClientAppService> _logger;
        private readonly IOptionsMonitor<RemoteServices> _remoteServices;

        private readonly IOptionsMonitor<DdpHospital> _ddpHospitalOptionsMonitor;
        private readonly DdpHospital _ddpHospital;
        private readonly DdpSwitch _ddpSwitch;
        private readonly IDdpApiService _ddpApiService;

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="remoteServices"></param>
        /// <param name="ddpHospitalOptionsMonitor"></param>
        /// <param name="ddpSwitch"></param>
        public HospitalClientAppService(ILogger<HospitalClientAppService> logger
            , IOptionsMonitor<RemoteServices> remoteServices
            , IOptionsMonitor<DdpHospital> ddpHospitalOptionsMonitor
            , DdpSwitch ddpSwitch)
        {
            _logger = logger;
            _remoteServices = remoteServices;

            _ddpHospitalOptionsMonitor = ddpHospitalOptionsMonitor;
            _ddpHospital = _ddpHospitalOptionsMonitor.CurrentValue;
            _ddpSwitch = ddpSwitch;
            _ddpApiService = _ddpSwitch.CreateService(_ddpHospital);
        }
         
        /// <summary>
        /// 查询云签
        /// </summary>
        /// <param name="relBizNo"></param>
        /// <returns></returns>   
        public async Task<string> QueryStampBaseAsync(string relBizNo)
        {
            try
            {
                relBizNo = relBizNo.Length < 4 ? relBizNo.PadLeft(4, '0') : relBizNo;
                if (_ddpHospital.DdpSwitch)
                {
                    return await QuerySignatureAsync(relBizNo);
                }

                var service = BuildHospitalService("StampBase");
                var dic = new Dictionary<string, string>
                {
                    { "relBizNo", relBizNo }
                };
                var ret = await service.QueryStampBaseAsync(dic);
                if (ret.StatusCode == 0)
                {
                    _logger.LogInformation($"云签请求参数：{relBizNo}，返回记录：{ret.EventValue?.StampBase64}");
                    return ret.EventValue?.StampBase64;
                }
                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"查询云签(QueryStampBaseAsync)异常：{ex.Message}");
                return null;
            }
        }

        /// <summary>
        /// 获取云签
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public async Task<string> QuerySignatureAsync(string userName)
        {
            var ddpResponse = await _ddpApiService.GetSignatureAsync(new DdpSignatureRequest { UserName = userName });

            if (ddpResponse.Code != 200)
            {
                _logger.LogError(
                    $"获取云签(DDP模式)异常,请求参数：{JsonConvert.SerializeObject(userName)} , 异常描述：{ddpResponse.Msg}");
                return string.Empty;
            }
            if (ddpResponse.Data.Count == 0)
            {
                return string.Empty;
            }

            string signature = ddpResponse.Data.FirstOrDefault()?.Signature;
            return signature;
        }

        /// <summary>
        /// 构建代理服务
        /// </summary>
        /// <returns></returns>
        private IHospitalProxyService BuildHospitalService(string server="")
        {
            var remoteServices = _remoteServices.CurrentValue;
            HttpCluster httpCluster = new()
            {
                TimeOut = 10 * 60 * 1000
            };
            var host = remoteServices.Hospital.BaseUrl;
            if (server=="StampBase")
            {
                host = remoteServices.StampBase.BaseUrl;
            }
            httpCluster.DefaultNode.Add(host);
            var service = httpCluster.Create<IHospitalProxyService>();
            return service;
        } 

    }
}
