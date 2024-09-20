using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Domain.Entities.Auditing;
using YiJian.ECIS;
using YiJian.ECIS.ShareModel.Models;

namespace YiJian.MasterData.Labs.Position;

/// <summary>
/// 检验标本采集部位
/// </summary>
[Comment("检验标本采集部位")]
public class LabSpecimenPosition : FullAuditedAggregateRoot<int>, IIsActive
{
    /// <summary>
    /// 标本编码
    /// </summary>
    [StringLength(50)]
    [Comment("标本编码")]
    public string SpecimenCode { get; private set; }

    /// <summary>
    /// 标本名称
    /// </summary>
    [StringLength(100)]
    [Comment("标本名称")]
    public string SpecimenName { get; private set; }

    /// <summary>
    /// 采集部位编码
    /// </summary>
    [Required]
    [StringLength(50)]
    [Comment("采集部位编码")]
    public string SpecimenPartCode { get; private set; }

    /// <summary>
    /// 采集部位名称
    /// </summary>
    [Required]
    [StringLength(100)]
    [Comment("采集部位名称")]
    public string SpecimenPartName { get; private set; }

    /// <summary>
    /// 排序号
    /// </summary>
    [Required]
    [Comment("排序号")]
    public int Sort { get; private set; }

    /// <summary>
    /// 拼音码
    /// </summary>
    [Required]
    [StringLength(50)]
    [Comment("拼音码")]
    public string PyCode { get; private set; }

    /// <summary>
    /// 是否启用
    /// </summary>
    [Comment("是否启用")]
    public bool IsActive { get; private set; } = true;

    #region constructor
    /// <summary>
    /// 检验标本采集部位构造器
    /// </summary>
    /// <param name="specimenCode">标本编码</param>
    /// <param name="specimenName">标本名称</param>
    /// <param name="positionCode">采集部位编码</param>
    /// <param name="positionName">采集部位名称</param>
    /// <param name="sort">排序号</param>
    /// <param name="isActive"></param>
    public LabSpecimenPosition([NotNull] string specimenCode,// 标本编码
        [NotNull] string specimenName,// 标本名称
        [NotNull] string positionCode,// 采集部位编码
        [NotNull] string positionName,// 采集部位名称
        [NotNull] int sort,        // 排序号
        bool isActive
        )
    {
        //标本编码
        SpecimenCode = Check.NotNull(specimenCode, "标本编码", LabSpecimenPositionConsts.MaxSpecimenCodeLength);

        Modify(specimenName,// 标本名称
            positionCode,   // 采集部位编码
            positionName,   // 采集部位名称
            sort,        // 排序号
            isActive
            );
    }
    #endregion

    #region Modify
    /// <summary>
    /// 修改
    /// </summary>
    /// <param name="specimenName">标本名称</param>
    /// <param name="positionCode">采集部位编码</param>
    /// <param name="positionName">采集部位名称</param>
    /// <param name="sort">排序号</param>
    /// <param name="isActive"></param>
    public void Modify([NotNull] string specimenName,// 标本名称
        [NotNull] string positionCode,// 采集部位编码
        [NotNull] string positionName,// 采集部位名称
        [NotNull] int sort,       // 排序号
        bool isActive
        )
    {

        //标本名称
        SpecimenName = Check.NotNull(specimenName, "标本名称", LabSpecimenPositionConsts.MaxSpecimenNameLength);

        //采集部位编码
        SpecimenPartCode = Check.NotNull(positionCode, "采集部位编码", LabSpecimenPositionConsts.MaxPositionCodeLength);

        //采集部位名称
        SpecimenPartName = Check.NotNull(positionName, "采集部位名称", LabSpecimenPositionConsts.MaxPositionNameLength);

        //排序号
        Sort = Check.NotNull(sort, "排序号");

        //拼音码
        PyCode = Check.NotNull(positionName.FirstLetterPY(), "拼音码", LabSpecimenPositionConsts.MaxPyCodeLength);

        //
        IsActive = isActive;

    }
    #endregion

    #region constructor
    private LabSpecimenPosition()
    {
        // for EFCore
    }
    #endregion
}
