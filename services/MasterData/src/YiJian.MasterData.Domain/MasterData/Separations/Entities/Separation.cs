using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Volo.Abp;
using Volo.Abp.Domain.Entities;

namespace YiJian.MasterData.Separations.Entities;

/// <summary>
/// 分方途径分类实体
/// </summary>
[Comment("分方途径分类实体")]
public class Separation : Entity<Guid>
{
    private Separation()
    {

    }

    public Separation(Guid id, int code, [NotNull] string title, int sort, Guid? printSettingId, string printSettingName)
    {
        Id = id;
        Code = code;
        Title = Check.NotNullOrEmpty(title, nameof(title), maxLength: 50);
        Sort = sort;
        PrintSettingId = printSettingId;
        PrintSettingName = printSettingName;
    }

    /// <summary>
    /// 分方单分类编码，0=注射单，1=输液单，2=雾化单...
    /// </summary>
    [Comment("分方单分类编码，0=注射单，1=输液单，2=雾化单...")]
    public int Code { get; set; }

    /// <summary>
    /// 分方单名称
    /// </summary>
    [Comment("分方单名称")]
    [Required, StringLength(50)]
    public string Title { get; set; }

    /// <summary>
    /// 排序顺序
    /// </summary>
    [Comment("排序顺序")]
    public int Sort { get; set; }

    /// <summary>
    /// 打印模板Id
    /// </summary>
    [Comment("打印模板Id")]
    public Guid? PrintSettingId { get; set; }

    /// <summary>
    /// 打印模板名称
    /// </summary>
    [Comment("分方单名称")]
    [StringLength(100)]
    public string PrintSettingName { get; set; }

    /// <summary>
    /// 用药途经
    /// </summary>
    public virtual List<Usage> Usages { get; set; }

    public void Update(int code, [NotNull] string title, Guid? printSettingId, string printSettingName)
    {
        Code = code;
        Title = Check.NotNullOrEmpty(title, nameof(title), maxLength: 50);
        PrintSettingId = printSettingId;
        PrintSettingName = printSettingName;
    }
}
