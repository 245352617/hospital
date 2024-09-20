using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace YiJian.MasterData.External.LongGang.Medicines;


/// <summary>
/// 同步HIS医嘱
/// </summary>
public interface ISyncHisMedicineAppService : IApplicationService
{

    /// <summary>
    /// 同步药品信息
    /// </summary>
    /// <returns></returns>
    public Task SyncMedicineAsync();
    /// <summary>
    /// 同步药理信息
    /// </summary>
    /// <returns></returns>
    Task PushHisMedicineToxicAsync();

    /// <summary>
    /// 同步检查Project 删除Item 同时删除Project同时删除Tree 
    /// </summary>
    /// <returns></returns>
    public Task SyncExamProjectAsync();


    /// <summary>
    /// 同步检验Project 删除Item 同时删除Project同时删除Tree 
    /// </summary>
    /// <returns></returns>
    public Task SyncLabProjectAsync();
}
