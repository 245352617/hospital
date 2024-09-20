using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using YiJian.Apis;
using YiJian.ECIS.DDP.Apis;
using YiJian.ECIS.ShareModel.DDPs;

namespace YiJian.ECIS.DDP;

/// <summary>
/// Ddp配置实例化开关
/// </summary>
public class DdpSwitch
{
    private readonly IConfiguration _configuration;

    public ILogger<PKUApiService> _logger { get; set; }
    public DdpSwitch(ILogger<PKUApiService> logger, IConfiguration configuration)
    {
        _logger = logger;
        _configuration = configuration;
    }
    /// <summary>
    /// 创建DDP服务
    /// </summary>
    /// <param name="ddpHospital"></param>
    /// <returns></returns>
    public IDdpApiService CreateService(DdpHospital ddpHospital)
    {
        switch (ddpHospital.UseHospital)
        {
            case "PKU":
                return new PKUApiService(ddpHospital, _logger, _configuration);

            //龙岗中心 举个例子 (龙岗中心用老的一套，返回null的情况，其他医院可以在这里配置)
            //case "LDC":
            //    return new LDCApiService(ddpHospital);
            default:
                break;
        }
        return null;
    }
}
