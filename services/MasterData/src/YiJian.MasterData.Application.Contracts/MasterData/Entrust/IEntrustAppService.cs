using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using YiJian.ECIS.ShareModel.Exceptions;

namespace YiJian.MasterData.DictionariesType;


/// <summary>
/// 嘱托配置 API Interface
/// </summary>   
public interface IEntrustAppService : IApplicationService
{
    /// <summary>
    /// 保存嘱托配置
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    /// <exception cref="EcisBusinessException"></exception>
    Task<Guid> SaveEntrustAsync(EntrustCreateOrUpdateDto dto);

    /// <summary>
    /// 删除
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <exception cref="EcisBusinessException"></exception>
    Task<string> DeleteEntrustAsync(Guid id);

    /// <summary>
    /// 嘱托列表
    /// </summary>
    /// <param name="filter">编码，名称或拼音码</param>
    /// <returns></returns>
    Task<List<EntrustDto>> GetEntrustListAsync(string filter);

    /// <summary>
    /// 嘱托分页列表
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
     Task<PagedResultDto<EntrustDto>> GetEntrustPageAsync(GetEntrustPagedInput input);
}