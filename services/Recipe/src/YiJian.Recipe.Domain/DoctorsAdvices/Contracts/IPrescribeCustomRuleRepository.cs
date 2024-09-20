using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using YiJian.DoctorsAdvices.Entities;

namespace YiJian.Recipes.DoctorsAdvices.Contracts;

/// <summary>
/// 自定义规则药品一次剂量名单 (自己维护)
/// </summary>
public interface IPrescribeCustomRuleRepository : IRepository<PrescribeCustomRule, int>
{
    /// <summary>
    /// 根据药品code获取一次剂量实体
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public Task<PrescribeCustomRule> GetEntityByIdAsync(int id);

    /// <summary>
    /// 从数据库中获取所有的自己配置的一次剂量规则数据
    /// </summary>
    /// <returns></returns>
    public Task<List<PrescribeCustomRule>> GetAllListAsync();

}
