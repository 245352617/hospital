using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace YiJian.MasterData.DictionariesMultitypes;

/// <summary>
/// 字典多类型 API Interface
/// </summary>   
public interface IDictionariesMultitypeAppService : IApplicationService
{
    /// <summary>
    /// 新增
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    Task<Guid> SaveAsync(DictionariesMultitypeDto input);

    /// <summary>
    /// 获取
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<DictionariesMultitypeDto> GetAsync(Guid id);
    /// <summary>
    /// 删除
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task DeleteAsync(Guid id);
    /// <summary>
    /// 获取列表
    /// </summary>
    /// <returns></returns>
    Task<List<DictionariesMultitypeGroupDto>> GetListAsync(
        string filter = null,
        string sorting = null);

    /// <summary>
    /// 获取分页记录
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    Task<PagedResultDto<DictionariesMultitypeDto>> GetPagedListAsync(GetDictionariesMultitypeInput input);

}
