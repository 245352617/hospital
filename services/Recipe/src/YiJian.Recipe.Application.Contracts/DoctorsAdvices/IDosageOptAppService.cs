using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace YiJian.DoctorsAdvices;

/// <summary>
/// 一次剂量操作运维工具
/// </summary>
public interface IDosageOptAppService : IApplicationService
{
    /// <summary>
    /// 同步快速开嘱一次剂量配置的数据
    /// </summary>
    /// <returns></returns>
    public Task<int> SyncQuickStartAdviceDosageAsync();

}
