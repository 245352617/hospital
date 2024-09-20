using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using YiJian.Recipe.EntityFrameworkCore;
using YiJian.Recipes.DoctorsAdvices.Entities;

namespace YiJian.Recipes.DoctorsAdvices.Contracts;

/// <summary>
/// 医嘱主表
/// </summary>
public class DoctorsAdviceRepository : EfCoreRepository<RecipeDbContext, DoctorsAdvice, Guid>, IDoctorsAdviceRepository
{
    /// <summary>
    /// 医嘱主表
    /// </summary>
    /// <param name="dbContextProvider"></param>
    public DoctorsAdviceRepository(IDbContextProvider<RecipeDbContext> dbContextProvider)
      : base(dbContextProvider)
    {

    }

    /// <summary>
    /// 根据IDS获取医嘱信息集合
    /// </summary>
    /// <param name="ids"></param>
    /// <returns></returns>
    public async Task<List<DoctorsAdvice>> GetDoctorsAdvicesByIdsAsync(List<Guid> ids)
    {
        if (!ids.Any()) return await Task.FromResult(new List<DoctorsAdvice>());
        var db = await GetDbSetAsync();
        return await db.Where(w => ids.Contains(w.Id)).ToListAsync();
    }

}
