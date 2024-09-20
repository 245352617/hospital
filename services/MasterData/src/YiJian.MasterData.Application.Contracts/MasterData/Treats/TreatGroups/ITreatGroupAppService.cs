using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace YiJian.MasterData;

public interface ITreatGroupAppService : IApplicationService
{
    /// <summary>
    /// 保存
    /// </summary>
    /// <param name="dictionariesCode"></param>
    /// <param name="dto"></param>
    /// <returns></returns>
    Task<bool> SaveAsync(string dictionariesCode, List<TreatGroupCreate> dto);

    /// <summary>
    /// 获取诊疗分类
    /// </summary>
    /// <param name="dictionariesCode"></param>
    /// <returns></returns>
    Task<List<TreatCatalogDto>> GetTreatCatalogAsync(string dictionariesCode);

    /// <summary>
    /// 获取处置列表
    /// </summary>
    /// <returns></returns>
    Task<List<DictDto>> GetListAsync();
}