using Microsoft.EntityFrameworkCore;
using System;
using Volo.Abp.Domain.Entities.Auditing;

namespace YiJian.Recipes.Preferences.Entities;

/// <summary>
/// 快速开嘱信息
/// </summary>
[Comment("快速开嘱信息")]
public class QuickStartAdvice : FullAuditedAggregateRoot<Guid>
{
    /// <summary>
    /// 快速开嘱信息
    /// </summary>
    private QuickStartAdvice()
    {

    }

    /// <summary>
    /// 快速开嘱信息
    /// </summary> 
    public QuickStartAdvice(
        Guid id,
        int sort,
        Guid quickStartCatalogueId)
    {
        Id = id;
        Sort = sort;
        QuickStartCatalogueId = quickStartCatalogueId;
    }

    /// <summary>
    /// 统计使用过的次数（个人统计）
    /// </summary>
    [Comment(" 统计使用过的次数（个人统计）")]
    public int UsageCount { get; set; }

    /// <summary>
    /// 排序序号
    /// </summary>
    [Comment("排序序号")]
    public int Sort { get; set; }

    /// <summary>
    /// 快速开嘱的目录Id
    /// </summary>
    [Comment("快速开嘱的目录Id")]
    public Guid QuickStartCatalogueId { get; set; }

    /// <summary>
    /// 药品信息
    /// </summary>
    public virtual QuickStartMedicine Medicine { get; set; }

    /// <summary>
    /// 累加使用
    /// </summary>
    public void INCR()
    {
        UsageCount += 1;
    }

    /// <summary>
    /// 重置初始化使用次数
    /// </summary>
    public void ResetUsageCount()
    {
        UsageCount = 0;
    }

}
