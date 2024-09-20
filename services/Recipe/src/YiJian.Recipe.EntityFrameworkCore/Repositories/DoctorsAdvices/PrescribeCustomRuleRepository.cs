using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using YiJian.DoctorsAdvices.Entities;
using YiJian.Recipe.EntityFrameworkCore;

namespace YiJian.Recipes.DoctorsAdvices.Contracts;

/// <summary>
/// 自定义规则药品一次剂量名单 (自己维护)
/// </summary>
public class PrescribeCustomRuleRepository : EfCoreRepository<RecipeDbContext, PrescribeCustomRule, int>, IPrescribeCustomRuleRepository
{
    /// <summary>
    /// 自定义规则药品一次剂量名单 (自己维护)
    /// </summary>
    /// <param name="dbContextProvider"></param>
    public PrescribeCustomRuleRepository(IDbContextProvider<RecipeDbContext> dbContextProvider)
      : base(dbContextProvider)
    {

    }

    /// <summary>
    /// 根据药品code获取一次剂量实体
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<PrescribeCustomRule> GetEntityByIdAsync(int id)
    {
        var db = await GetDbSetAsync();
        return await db.FirstOrDefaultAsync(w => w.Id == id);
    }

    /// <summary>
    /// 从数据库中获取所有的自己配置的一次剂量规则数据
    /// </summary>
    /// <returns></returns>
    public async Task<List<PrescribeCustomRule>> GetAllListAsync()
    {
        var db = await GetDbSetAsync();
        return await db.ToListAsync();
    }

}
