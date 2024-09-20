using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;

namespace YiJian.MasterData.MasterData.HospitalInfo;

/// <summary>
/// 描述：医院配置信息服务
/// 创建人： yangkai
/// 创建时间：2023/2/3 10:03:16
/// </summary>
[Authorize]
public class HospitalInfoAppService : MasterDataAppService, IHospitalInfoAppService
{
    private HospitalInfoConfigDto hospitalInfoConfigDto;

    public HospitalInfoAppService(IOptions<HospitalInfoConfigDto> optionsMonitor)
    {
        hospitalInfoConfigDto = optionsMonitor.Value;
    }

    /// <summary>
    /// 获取医院配置信息
    /// </summary>
    /// <returns></returns>
    public HospitalInfoConfigDto GetHospitalInfoConfig()
    {
        return hospitalInfoConfigDto;
    }
}
