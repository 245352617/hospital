using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Volo.Abp.Domain.Entities;

namespace YiJian.MasterData.Separations.Entities;

/// <summary>
/// 用药途经（打印途径配置）
/// </summary>
[Comment("用药途经")]
public class Usage : Entity<Guid>
{
    private Usage()
    {

    }

    public Usage(Guid id, [NotNull] string usageCode, [NotNull] string usageName, Guid separationId)
    {
        Id = id;
        UsageCode = usageCode;
        UsageName = usageName;
        SeparationId = separationId;
    }

    /// <summary>
    /// 用法编码
    /// </summary>
    [Comment("用法编码")]
    [Required, StringLength(20)]
    public string UsageCode { get; set; }

    /// <summary>
    /// 用法名称
    /// </summary>
    [Comment("用法名称")]
    [Required, StringLength(20)]
    public string UsageName { get; set; }

    /// <summary>
    /// 分方配置
    /// </summary>
    [Comment("分方配置Id")]
    public Guid SeparationId { get; set; }

    /// <summary>
    /// 分方配置
    /// </summary>
    [NotMapped]
    public virtual Separation Separation { get; set; }

    /// <summary>
    /// 更新
    /// </summary>
    /// <param name="usageCode"></param>
    /// <param name="usageName"></param>
    public void Update(string usageCode, string usageName)
    {
        UsageCode = usageCode;
        UsageName = usageName;
    }
}
