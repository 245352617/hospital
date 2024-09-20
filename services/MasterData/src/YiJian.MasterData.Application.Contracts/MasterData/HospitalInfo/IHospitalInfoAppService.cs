using Volo.Abp.Application.Services;

namespace YiJian.MasterData.MasterData.HospitalInfo;

/// <summary>
/// 描述：医院配置信息服务接口
/// 创建人： yangkai
/// 创建时间：2023/2/3 10:10:56
/// </summary>
public interface IHospitalInfoAppService : IApplicationService
{
    /// <summary>
    /// 获取医院配置信息
    /// </summary>
    /// <returns></returns>
    HospitalInfoConfigDto GetHospitalInfoConfig();
}
