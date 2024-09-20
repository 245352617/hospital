using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using YiJian.MasterData.MasterData.Separations.Dto;

namespace YiJian.MasterData;

/// <summary>
/// 分方配置管理
/// </summary>
public interface ISeparationAppService : IApplicationService
{
    /// <summary>
    /// 获取途径的所有分类
    /// </summary>
    /// <returns></returns> 
    public Task<List<UsageSelectDto>> GetUsagesAsync();

    /// <summary>
    /// 获取分方配置列表
    /// </summary>
    /// <returns></returns> 
    public Task<List<SeparationDto>> GetListAsync();
     
    /// <summary>
    /// 更新分方操作，无id为添加，有id为更新
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>  
    public Task<Guid> ModifyAsync(ModifySeparationDto model);
      
}