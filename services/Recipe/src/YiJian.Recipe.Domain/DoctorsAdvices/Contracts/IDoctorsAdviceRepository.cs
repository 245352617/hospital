using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using YiJian.Recipes.DoctorsAdvices.Entities;

namespace YiJian.Recipes.DoctorsAdvices.Contracts;

/// <summary>
/// 医嘱主表
/// </summary>
public interface IDoctorsAdviceRepository : IRepository<DoctorsAdvice, Guid>
{
    /// <summary>
    /// 根据IDS获取医嘱信息集合
    /// </summary>
    /// <param name="ids"></param>
    /// <returns></returns>
    public Task<List<DoctorsAdvice>> GetDoctorsAdvicesByIdsAsync(List<Guid> ids);

}
