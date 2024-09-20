using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using YiJian.MasterData.Departments;

namespace YiJian.MasterData.Domain;

/// <summary>
///执行科室规则字典
/// </summary>
public interface IExecuteDepRuleDicRepository : IRepository<ExecuteDepRuleDic, int>
{
    /// <summary>
    /// 根据规则Id获取执行科室信息
    /// </summary>
    /// <param name="ruleId"></param>
    /// <returns></returns>
    public Task<List<ExecuteDepRuleDic>> GetListByRuleIdAsync(int ruleId);

    /// <summary>
    /// 根据规则Id获取执行科室信息
    /// </summary>
    /// <param name="keyword"></param>
    /// <param name="ruleId"></param>
    /// <returns></returns>
    public Task<List<ExecuteDepRuleDic>> SearchListByRuleIdAsync(string keyword,int? ruleId=null);

    /// <summary>
    /// 比对并且新增或更新操作，删除不处理
    /// </summary>
    /// <param name="list"></param>
    /// <returns></returns>
    public Task DiffAndUpdateAsync(List<ExecuteDepRuleDic> list);
}
