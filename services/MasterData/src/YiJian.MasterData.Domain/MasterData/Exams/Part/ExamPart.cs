using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Domain.Entities.Auditing;
using YiJian.ECIS;

namespace YiJian.MasterData;

/// <summary>
/// 检查部位
/// </summary>
[Comment("检查部位")]
public class ExamPart : FullAuditedAggregateRoot<int>
{
    #region Properties

    /// <summary>
    /// 检查部位编码
    /// </summary>
    [Required]
    [StringLength(50)]
    [Comment("检查部位编码")]
    public string PartCode { get; set; }

    /// <summary>
    /// 检查部位名称
    /// </summary>
    [Required]
    [StringLength(200)]
    [Comment("检查部位名称")]
    public string PartName { get; set; }

    /// <summary>
    /// 排序号
    /// </summary>
    [Required]
    [Comment("排序号")]
    public int Sort { get; set; }

    /// <summary>
    /// 拼音码
    /// </summary>
    [Required]
    [StringLength(20)]
    [Comment("拼音码")]
    public string PyCode { get; set; }

    #endregion

    #region Modify

    /// <summary>
    /// 修改
    /// </summary>
    /// <param name="partName">检查部位名称</param>
    /// <param name="sort">排序号</param>
    public void Modify([NotNull] string partName, // 检查部位名称
        [NotNull] int sort // 排序号
    )
    {
        //检查部位名称
        PartName = Check.NotNull(partName, "检查部位名称", ExamPartConsts.MaxPartNameLength);
        //排序号
        Sort = Check.NotNull(sort, "排序号");
        //拼音码
        PyCode = Check.NotNull(partName.FirstLetterPY(), "拼音码");
    }

    #endregion
}