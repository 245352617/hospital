using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Domain.Entities.Auditing;
using YiJian.ECIS;
using YiJian.ECIS.ShareModel.Models;

namespace YiJian.MasterData.Labs;


/// <summary>
/// 检验标本
/// </summary>
[Comment("检验标本")]
public class LabSpecimen : FullAuditedAggregateRoot<int>, IIsActive
{
    /// <summary>
    /// 更新
    /// </summary>
    /// <param name="specimenName"></param>
    public void Update(string specimenName)
    {
        //标本名称
        SpecimenName = specimenName;
        //拼音码
        PyCode = Check.NotNull(specimenName.FirstLetterPY(), "拼音码", LabSpecimenConsts.MaxPyCodeLength);
        //五笔
        WbCode = Check.Length(specimenName.FirstLetterWB(), "五笔", LabSpecimenConsts.MaxWbCodeLength);
    }

    #region Properties

    /// <summary>
    /// 标本编码
    /// </summary>
    [Required]
    [StringLength(50)]
    [Comment("标本编码")]
    public string SpecimenCode { get; set; }

    /// <summary>
    /// 标本名称
    /// </summary>
    [Required]
    [StringLength(200)]
    [Comment("标本名称")]
    public string SpecimenName { get; set; }

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
    [StringLength(50)]
    [Comment("拼音码")]
    public string PyCode { get; set; }

    /// <summary>
    /// 五笔
    /// </summary>
    [StringLength(50)]
    [Comment("五笔")]
    public string WbCode { get; set; }

    /// <summary>
    /// 是否启用
    /// </summary>
    [Comment("是否启用")]
    public bool IsActive { get; set; } = true;

    #endregion

    #region Modify

    /// <summary>
    /// 修改
    /// </summary>
    /// <param name="name">标本名称</param>
    /// <param name="sort">排序号</param>
    /// <param name="isActive">是否启用</param>
    public void Modify([NotNull] string name, // 标本名称
        int sort, // 排序号
        bool isActive // 是否启用
    )
    {
        //标本名称
        SpecimenName = name;

        //排序号
        Sort = sort;

        //拼音码
        PyCode = Check.NotNull(name.FirstLetterPY(), "拼音码", LabSpecimenConsts.MaxPyCodeLength);

        //五笔
        WbCode = Check.Length(name.FirstLetterWB(), "五笔", LabSpecimenConsts.MaxWbCodeLength);

        //是否启用
        IsActive = isActive;
    }

    #endregion
}