using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using YiJian.MasterData.MasterData.Pharmacies.Dto;

namespace YiJian.MasterData.MasterData.Pharmacies;

/// <summary>
/// 药房配置
/// </summary>
public interface IPharmacyAppService : IApplicationService
{

    /// <summary>
    /// 获取药房配置信息
    /// </summary>
    /// <returns></returns> 
    public Task<List<ModifyPharmacyDto>> GetAsync();

    /// <summary>
    /// 删除操作，POST是兼容操作,DELETE是Restfull接口
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>  
    public Task<Guid> DeleteAsync(Guid id);
     
    /// <summary>
    /// 添加，更改药房配置信息
    /// </summary>
    /// <returns></returns> 
    public Task<Guid> ModifyAsync(ModifyPharmacyDto model);
     
}
