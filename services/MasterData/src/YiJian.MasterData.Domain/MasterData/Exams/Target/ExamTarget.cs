using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Domain.Entities.Auditing;
using YiJian.ECIS;
using YiJian.ECIS.ShareModel.Enums;
using YiJian.ECIS.ShareModel.Models;

namespace YiJian.MasterData.Exams;

/// <summary>
/// 检查明细项
/// </summary>
[Comment("检查明细项")]
public class ExamTarget : FullAuditedAggregateRoot<int>, IIsActive
{
    #region Properties

    /// <summary>
    /// 一级编码 （当二级目录code和三级目录code相同时无法区分当前层级的数据是挂在哪里的）
    /// </summary>
    [Required]
    [StringLength(20)]
    [Comment("一级目录编码")]
    public string FirstNodeCode { get; set; }

    /// <summary>
    /// 一级名称
    /// </summary>
    [Required]
    [StringLength(200)]
    [Comment("一级目录名称")]
    public string FirstNodeName { get; set; }


    /// <summary>
    /// 编码
    /// </summary>
    [Required]
    [StringLength(20)]
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
    [StringLength(20)]
    [Comment("项目编码")]
    public string ProjectCode { get; set; }

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
    /// 其它价格
    /// </summary>
    [Comment("其它价格")]
    public decimal OtherPrice { get; set; }

    /// <summary>
    /// 规格
    /// </summary>
    [Required]
    [StringLength(50)]
    [Comment("规格")]
    public string Specification { get; set; }

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
    [Required]
    [StringLength(50)]
    [Comment("五笔")]
    public string WbCode { get; set; }

    /// <summary>
    /// 医保目录:0=自费,1=甲类,2=乙类,3=其它
    /// </summary>
    [Comment("医保目录:0=自费,1=甲类,2=乙类,3=其它")]
    public InsuranceCatalog InsuranceType { get; set; }

    /// <summary>
    /// 特殊标识
    /// </summary>
    [StringLength(50)]
    [Comment("特殊标识")]
    public string SpecialFlag { get; set; }

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
    #endregion

    #region Modify

    /// <summary>
    /// 修改
    /// </summary>
    /// <param name="name">名称</param>
    /// <param name="projectCode">项目编码</param>
    /// <param name="unit">单位</param>
    /// <param name="qty">数量</param>
    /// <param name="price">价格</param>
    /// <param name="otherPrice">其它价格</param>
    /// <param name="specification">规格</param>
    /// <param name="sort">排序号</param>
    /// <param name="insureType">医保类型</param>
    /// <param name="specialFlag">特殊标识</param>
    /// <param name="isActive">是否启用</param>
    public void Modify([NotNull] string name, // 名称
        [NotNull] string projectCode, // 项目编码
        [NotNull] string unit, // 单位
        int qty, // 数量
        decimal price, // 价格
        decimal otherPrice, // 其它价格
        [NotNull] string specification, // 规格
        int sort, // 排序号
        InsuranceCatalog insureType, // 医保类型
        string specialFlag, // 特殊标识
        bool isActive // 是否启用
    )
    {
        //名称
        TargetName = name;

        //项目编码
        ProjectCode = projectCode;

        //单位
        TargetUnit = unit;

        //数量
        Qty = qty;

        //价格
        Price = price;

        //其它价格
        OtherPrice = otherPrice;

        //规格
        Specification = Check.NotNull(specification, "规格", ExamTargetConsts.MaxSpecificationLength);

        //排序号
        Sort = sort;

        //拼音码
        PyCode = Check.NotNull(name.FirstLetterPY(), "拼音码", ExamTargetConsts.MaxPyCodeLength);

        //五笔
        WbCode = Check.NotNull(name.FirstLetterWB(), "五笔", ExamTargetConsts.MaxWbCodeLength);

        //医保类型
        InsuranceType = insureType;

        //特殊标识
        SpecialFlag = Check.Length(specialFlag, "特殊标识", ExamTargetConsts.MaxSpecialFlagLength);

        //是否启用
        IsActive = isActive;
    }

    #endregion
}