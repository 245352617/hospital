using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Domain.Entities.Auditing;
using YiJian.ECIS;
using YiJian.ECIS.ShareModel.Enums;
using YiJian.ECIS.ShareModel.Models;

namespace YiJian.MasterData.Labs;

/// <summary>
/// 检验明细项
/// </summary>
[Comment("检验明细项")]
public class LabTarget : FullAuditedAggregateRoot<int>, IIsActive
{
    #region Properties

    /// <summary>
    /// 编码
    /// </summary>
    [Required]
    [StringLength(50)]
    [Comment("编码")]
    public string TargetCode { get; set; }

    /// <summary>
    /// 名称
    /// </summary>
    [Required]
    [StringLength(200)]
    [Comment("名称")]
    public string TargetName { get; set; }

    /// <summary>
    /// 项目编码
    /// </summary>
    [Required]
    [StringLength(50)]
    [Comment("项目编码")]
    public string ProjectCode { get; set; }

    /// <summary>
    /// 排序号
    /// </summary>
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
    /// 单位
    /// </summary>
    [Required]
    [StringLength(20)]
    [Comment("单位")]
    public string TargetUnit { get; set; }

    /// <summary>
    /// 数量
    /// </summary>
    [Comment("数量")]
    public decimal Qty { get; set; }

    /// <summary>
    /// 价格
    /// </summary>
    [Comment("价格")]
    public decimal Price { get; set; }

    /// <summary>
    /// 医保目录:0=自费,1=甲类,2=乙类,3=其它
    /// </summary>
    [StringLength(20)]
    [Comment("医保目录:0=自费,1=甲类,2=乙类,3=其它")]
    public InsuranceCatalog InsuranceType { get; set; }

    /// <summary>
    /// 是否启用
    /// </summary>
    [Comment("是否启用")]
    public bool IsActive { get; set; } = true;

    /// <summary>
    /// 项目类型 --龙岗字典所需
    /// </summary>
    [Comment("项目类型--龙岗字典所需")]
    [StringLength(200)]
    public string ProjectType { get; set; }
    /// <summary>
    /// 项目归类 --龙岗字典所需
    /// </summary>
    [Comment("项目归类--龙岗字典所需")]
    [StringLength(200)]
    public string ProjectMerge { get; set; }

    /// <summary>
    /// 分类编码和当前项目的编码组合
    /// </summary>
    [Comment("分类编码和当前项目的编码组合")]
    [StringLength(50)]
    public string CatalogAndProjectCode { get; set; }
    #endregion

    #region Modify

    /// <summary>
    /// 修改
    /// </summary>
    /// <param name="name">名称</param>
    /// <param name="sort">排序号</param>
    /// <param name="unit">单位</param>
    /// <param name="qty">数量</param>
    /// <param name="price">价格</param>
    /// <param name="insureType">医保类型</param>
    /// <param name="isActive">是否启用</param>
    public void Modify([NotNull] string name, // 名称
        int sort, // 排序号
        [NotNull] string unit, // 单位
        decimal qty, // 数量
        decimal price, // 价格
        InsuranceCatalog insureType = InsuranceCatalog.Self, // 医保类型
        bool isActive = true // 是否启用
    )
    {
        //名称
        TargetName = name;

        //排序号
        Sort = sort;

        //拼音码
        PyCode = Check.NotNull(name.FirstLetterPY(), "拼音码", LabTargetConsts.MaxPyCodeLength);

        //五笔
        WbCode = Check.Length(name.FirstLetterWB(), "五笔", LabTargetConsts.MaxWbCodeLength);

        //单位
        TargetUnit = unit;

        //数量
        Qty = qty;

        //价格
        Price = price;

        //医保类型
        InsuranceType = insureType;

        //是否启用
        IsActive = isActive;
    }

    #endregion
}