using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.EntityFrameworkCore;
using YiJian.MasterData.Departments;
using YiJian.MasterData.Domain;

namespace YiJian.MasterData.EntityFrameworkCore.Repositories;

/// <summary>
/// 执行科室规则字典
/// </summary>
public class ExecuteDepRuleDicRepository : MasterDataRepositoryBase<ExecuteDepRuleDic, int>, IExecuteDepRuleDicRepository
{
    /// <summary>
    /// 执行科室规则字典
    /// </summary>
    /// <param name="dbContextProvider"></param>
    public ExecuteDepRuleDicRepository(IDbContextProvider<MasterDataDbContext> dbContextProvider) : base(dbContextProvider)
    {
    }

    /// <summary>
    /// 根据规则Id获取执行科室信息
    /// </summary>
    /// <param name="ruleId"></param>
    /// <returns></returns>
    public async Task<List<ExecuteDepRuleDic>> GetListByRuleIdAsync(int ruleId)
    { 
        return await (await GetDbSetAsync()).Where(w=>w.RuleId == ruleId).ToListAsync();  
    }


    /// <summary>
    /// 根据规则Id获取执行科室信息
    /// </summary>
    /// <param name="keyword"></param>
    /// <param name="ruleId"></param>
    /// <returns></returns>
    public async Task<List<ExecuteDepRuleDic>> SearchListByRuleIdAsync(string keyword, int? ruleId = null)
    {
        var db = await GetDbSetAsync();
        var data = await db
            .WhereIf(
                !keyword.IsNullOrEmpty(),
                w => w.SpellCode.ToLower().Contains(keyword.ToLower()) || w.ExeDepName.Contains(keyword.Trim()) || w.ExeDepCode == keyword.Trim())
            .WhereIf(ruleId.HasValue, w => w.RuleId == ruleId.Value)
            .ToListAsync();
        return data;
    }

    /// <summary>
    /// 比对并且新增操作，删除不处理
    /// </summary>
    /// <param name="inputEntities"></param>
    /// <returns></returns>
    public async Task DiffAndUpdateAsync(List<ExecuteDepRuleDic> inputEntities)
    {
        var db = await GetDbSetAsync();
        var allEntity = await db.ToListAsync();

        //1.先判断新增的
        var tagEnitties = allEntity.Select(s => new ExecuteDepRuleDic
        {
            ExeDepCode = s.ExeDepCode,
            ExeDepName = s.ExeDepName,
            RuleId = s.RuleId,
            RuleName = s.RuleName,
            SpellCode = s.SpellCode
        }).ToList();
         
        var exceptRuleIds = inputEntities.Except(tagEnitties).ToList(); //求差集，找到传入的集合是新的记录
        if (exceptRuleIds.Any()) await db.AddRangeAsync(exceptRuleIds); 
    }

}

