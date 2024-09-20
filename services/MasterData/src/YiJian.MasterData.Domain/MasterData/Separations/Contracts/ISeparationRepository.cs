using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using YiJian.MasterData.Separations.Entities;

namespace YiJian.MasterData.Separations.Contracts;

/// <summary>
/// 分方配置管理存储
/// </summary>
public interface ISeparationRepository : IRepository<Separation, Guid>
{
    /// <summary>
    /// 获取所有的分方配置
    /// </summary>
    /// <returns></returns> 
    public Task<List<Separation>> GetListAsync();

    /// <summary>
    /// 获取分方配置信息
    /// </summary>
    /// <returns></returns> 
    public Task<Separation> GetAsync(Guid id);
}
