using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using YiJian.MasterData.MasterData.Dictionaries; 

namespace YiJian.MasterData;

public interface IDictionariesAppService : IApplicationService
{
    /// <summary>
    /// 获取字典列表
    /// </summary>
    /// <param name="input"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<ListResultDto<DictionariesDto>> GetDictionariesListAsync(
        DictionariesWhereInput input, CancellationToken cancellationToken);

    /// <summary>
    /// 新增或者修改实体
    /// </summary>
    /// <param name="dto"></param>
    /// <param name="cancellationToken"></param>
    /// <param name="dataFrom"></param>
    /// <returns></returns>
    Task<DictionariesDto> SaveDictionariesAsync(CreateOrUpdateDictionariesDto dto,
        CancellationToken cancellationToken = default, int dataFrom = 0);

    /// <summary>
    /// 根据id删除字典
    /// </summary>
    /// <param name="ids"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task DeleteDictionariesAsync(List<Guid> ids,CancellationToken cancellationToken);

    /// <summary>
    /// 分组获取字典
    /// </summary>
    /// <param name="dictionariesTypeCode"></param>
    /// <returns></returns>
    Task<Dictionary<string, List<DictionariesDto>>> GetDictionariesGroupAsync(
        string dictionariesTypeCode);
     
    /// <summary>
    /// 获取电子病历水印配置信息
    /// </summary> 
    /// <returns></returns>
    public Task<EmrWatermarkDto> GetEmrWatermarkAsync();
}