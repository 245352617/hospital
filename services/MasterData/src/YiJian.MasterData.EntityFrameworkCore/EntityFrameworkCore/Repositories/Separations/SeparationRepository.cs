using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using YiJian.MasterData.Separations.Contracts;
using YiJian.MasterData.Separations.Entities;

namespace YiJian.MasterData.EntityFrameworkCore.Repositories.Separations;

/// <summary>
/// 分方配置管理存储实现
/// </summary>
public class SeparationRepository : EfCoreRepository<MasterDataDbContext, Separation, Guid>, ISeparationRepository
{
    /// <summary>
    /// 分方配置
    /// </summary>
    /// <param name="dbContextProvider"></param>
    public SeparationRepository(IDbContextProvider<MasterDataDbContext> dbContextProvider) : base(dbContextProvider)
    {

    }

    /// <summary>
    /// 获取所有的分方配置
    /// </summary>
    /// <returns></returns> 
    public async Task<List<Separation>> GetListAsync()
    {
        var db = await GetDbSetAsync();
        return await db.Include(i=>i.Usages).ToListAsync(); 
    }

    /// <summary>
    /// 获取分方配置信息
    /// </summary>
    /// <returns></returns> 
    public async Task<Separation> GetAsync(Guid id)
    {
        var db = await GetDbSetAsync();
        return await db.Include(i => i.Usages).FirstOrDefaultAsync(w=>w.Id== id);
    }

}
