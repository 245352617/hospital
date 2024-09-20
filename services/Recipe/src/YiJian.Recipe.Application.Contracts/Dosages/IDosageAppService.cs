using System.Collections.Generic;
using System.Threading.Tasks;
using YiJian.Dosages.Dto;

namespace YiJian.Dosages;

/// <summary>
/// 剂量配置表，处理HIS药品剂量换算配置错误的问题
/// </summary>
public interface IDosageAppService
{

    /// <summary>
    /// 获取所有的自定义一次剂量表数据
    /// </summary>
    /// <returns></returns>
    public Task<List<CustomDosageDto>> GetAllCustomDosagesAsync();

    /// <summary>
    /// 更新或插入新记录
    /// </summary>
    /// <param name="param"></param>
    /// <returns></returns>
    public Task<bool> UpsertAsync(CustomDosageDto param);

    /// <summary>
    /// 如果数据库插入请执行这个操作，清空一次剂量的缓存
    /// </summary>
    /// <returns></returns>
    public bool CleanCache();


}