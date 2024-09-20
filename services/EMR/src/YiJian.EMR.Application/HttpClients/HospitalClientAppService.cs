using BeetleX.Http.Clients;
using DotNetCore.CAP;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using YiJian.ECIS.ShareModel.Models.Responses;
using YiJian.ECIS.ShareModel.Responses;
using YiJian.EMR.HospitalClients;
using YiJian.EMR.HospitalClients.Dto;
using YiJian.EMR.HttpClients.Dto;
using YiJian.EMR.Templates.Dto;
using YiJian.EMR.Writes.Dto;

namespace YiJian.EMR.HttpClients;

/// <summary>
/// 
/// </summary>
public class HospitalClientAppService : ApplicationService, IHospitalClientAppService
{
    private readonly ILogger<HospitalClientAppService> _logger;
    private readonly IOptionsMonitor<RemoteServices> _remoteServices;

    /// <summary>
    /// 
    /// </summary> 
    public HospitalClientAppService(
        ILogger<HospitalClientAppService> logger,
        IOptionsMonitor<RemoteServices> remoteServices)
    {
        _logger = logger;
        _remoteServices = remoteServices;
    }

}


/// <summary>
/// 
/// </summary>
public class MasterDataAppService : ApplicationService, IMasterDataAppService
{
    private readonly ILogger<MasterDataAppService> _logger;
    private readonly IOptionsMonitor<RemoteServices> _remoteServices;

    /// <summary>
    /// 
    /// </summary> 
    public MasterDataAppService(
        ILogger<MasterDataAppService> logger,
        IOptionsMonitor<RemoteServices> remoteServices)
    {
        _logger = logger;
        _remoteServices = remoteServices;
    }

    /// <summary>
    /// 获取电子病历水印配置信息
    /// </summary>
    /// <returns></returns>
    public async Task<EmrWatermarkDto> GetEmrWatermarkingAsync()
    {
        var remoteServices = _remoteServices.CurrentValue;
        HttpCluster httpCluster = new HttpCluster();
        httpCluster.TimeOut = 10 * 60 * 1000;
        httpCluster.DefaultNode.Add(remoteServices.Default.BaseUrl);
        var service = httpCluster.Create<MasterRemoteService>();
        var ret = await service.GetEmrmarkingAsync();
        return ret.Data;
    }

}
