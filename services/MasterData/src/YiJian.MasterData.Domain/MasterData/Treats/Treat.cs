using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Domain.Entities.Auditing;
using YiJian.ECIS;
using YiJian.ECIS.ShareModel.Enums;

namespace YiJian.MasterData.Treats;

/// <summary>
/// 诊疗项目字典
/// </summary>
[Comment("诊疗项目字典")]
public class Treat : FullAuditedAggregateRoot<int>
{
    public Treat SetId(int id)
    {
        this.Id = id;
        return this;
    }

    /// <summary>
    /// 主键
    /// </summary>
    [Comment("主键")]
    public new int Id { get; set; }

    /// <summary>
    /// 编码
    /// </summary>
    [Required]
    [StringLength(100)]
    [Comment("编码")]
    public string TreatCode { get; set; }

    /// <summary>
    /// 名称
    /// </summary>
    [Required]
    [StringLength(200)]
    [Comment("名称")]
    public string TreatName { get; set; }

    /// <summary>
    /// 拼音码
    /// </summary>
    [Required]
    [StringLength(100)]
    [Comment("拼音码")]
    public string PyCode { get; set; }

    /// <summary>
    /// 五笔
    /// </summary>
    [Required]
    [StringLength(100)]
    [Comment("五笔")]
    public string WbCode { get; set; }

    /// <summary>
    /// 单价
    /// </summary>
    [Comment("单价")]
    public decimal Price { get; set; }

    /// <summary>
    /// 其它价格 加收金额
    /// </summary>
    [Comment("其它价格")]
    public decimal? OtherPrice { get; set; }

    /// <summary>
    /// 加收标志	
    /// </summary>
    [Comment("加收标志")]
    public bool Additional { get; set; }

    /// <summary>
    /// 诊疗处置类别代码
    /// </summary>
    [Required]
    [StringLength(20)]
    [Comment("诊疗处置类别代码")]
    public string CategoryCode { get; set; }

    /// <summary>
    /// 诊疗处置类别名称
    /// </summary>
    [Required]
    [StringLength(50)]
    [Comment("诊疗处置类别名称")]
    public string CategoryName { get; set; }

    /// <summary>
    /// 规格
    /// </summary>
    [StringLength(200)]
    [Comment("规格")]
    public string Specification { get; set; }

    /// <summary>
    /// 单位
    /// </summary>
    [StringLength(50)]
    [Comment("单位")]
    public string Unit { get; set; }

    /// <summary>
    /// 默认频次代码
    /// </summary>
    [StringLength(50)]
    [Comment("默认频次代码")]
    public string FrequencyCode { get; set; }

    /// <summary>
    /// 执行科室代码
    /// </summary>
    [StringLength(50)]
    [Comment("执行科室代码")]
    public string ExecDeptCode { get; set; }

    /// <summary>
    /// 执行科室名称
    /// </summary>
    [StringLength(100)]
    [Comment("执行科室名称")]
    public string ExecDeptName { get; set; }

    /// <summary>
    /// 收费大类代码
    /// </summary>
    [StringLength(50)]
    [Comment("收费大类代码")]
    public string FeeTypeMainCode { get; set; }

    /// <summary>
    /// 收费小类代码
    /// </summary>
    [StringLength(50)]
    [Comment("收费小类代码")]
    public string FeeTypeSubCode { get; set; }

    /// <summary>
    /// 平台标识
    /// </summary>
    [Comment("平台标识")]
    public PlatformType PlatformType { get; set; }

    /// <summary>
    /// 项目归类 --龙岗字典所需
    /// </summary>
    [Comment("项目归类--龙岗字典所需")]
    [StringLength(200)]
    public string ProjectMerge { get; set; }
     

    /// <summary>
    /// 科室跟踪执行类别
    ///<![CDATA[
    /// 0.不跟踪执行(默认开单科室)              
    /// 1.按固定科室执行(取depExecutionRules字段)
    /// 2.按病人科室执行(默认开单科室)
    /// 3.按病人病区执行（默认开单科室）         
    /// 9.按规则执行（医生选择开单科室、默认为开单科室）
    /// ]]>
    /// </summary>
    [Comment("科室跟踪执行类别 0.不跟踪执行(默认开单科室),1.按固定科室执行(取depExecutionRules字段),2.按病人科室执行(默认开单科室),3.按病人病区执行（默认开单科室）,9.按规则执行（医生选择开单科室、默认为开单科室）")]
    public EDepExecutionType? DepExecutionType { get; set; } = EDepExecutionType.UntracedExec;

    /// <summary>
    /// 科室跟踪执行规则
    /// <![CDATA[
    /// depExecutionType=1：固定科室
    /// depExecutionType=9：4.4.14执行科室规则字典（his提供）ruleId字段、
    /// 默认:departmentCode
    /// ]]>
    /// </summary>
    [Comment("科室跟踪执行规则 depExecutionType=1：固定科室,depExecutionType=9：4.4.14执行科室规则字典（his提供）ruleId字段、默认:departmentCode")]
    public string DepExecutionRules { get; set; }

    /// <summary>
    /// 医保编码
    /// </summary>
    [Comment("医保编码")]
    [StringLength(50)]
    public string MeducalInsuranceCode { get; set; }

    /// <summary>
    /// 医保二级编码
    /// </summary>
    [Comment("医保二级编码")]
    [StringLength(50)]
    public string YBInneCode { get; set; }


    #region Modify

    /// <summary>
    /// 修改
    /// </summary>
    /// <param name="name">名称</param>
    /// <param name="price">单价</param>
    /// <param name="otherPrice">其它价格</param>
    /// <param name="categoryCode">诊疗处置类别代码</param>
    /// <param name="category">诊疗处置类别</param>
    /// <param name="specification">规格</param>
    /// <param name="unit">单位</param>
    /// <param name="frequencyCode">默认频次代码</param>
    /// <param name="execDeptCode">执行科室代码</param>
    /// <param name="execDept">执行科室</param>
    /// <param name="feeTypeMain">收费大类代码</param>
    /// <param name="feeTypeSub">收费小类代码</param>
    /// <param name="platformType"></param>
    public void Modify([NotNull] string name, // 名称
        decimal price, // 单价
        decimal? otherPrice, // 其它价格
        [NotNull] string categoryCode, // 诊疗处置类别代码
        [NotNull] string category, // 诊疗处置类别
        string specification, // 规格
        [NotNull] string unit, // 单位
        string frequencyCode, // 默认频次代码
        string execDeptCode, // 执行科室代码
        string execDept, // 执行科室
        string feeTypeMain, // 收费大类代码
        string feeTypeSub, // 收费小类代码
        PlatformType platformType
    )
    {
        //名称
        TreatName = name;


        //拼音码
        PyCode = Check.NotNull(name.FirstLetterPY(), "拼音码", TreatConsts.MaxPyCodeLength);


        //五笔
        WbCode = Check.NotNull(name.FirstLetterWB(), "五笔", TreatConsts.MaxWbCodeLength);


        //单价
        Price = price;


        //其它价格
        OtherPrice = otherPrice;


        //诊疗处置类别代码
        CategoryCode = categoryCode;


        //诊疗处置类别
        CategoryName = category;


        //规格
        Specification = specification;


        //单位
        Unit = Check.NotNull(unit, "单位");


        //默认频次代码
        FrequencyCode = frequencyCode;


        //执行科室代码
        ExecDeptCode = execDeptCode;


        //执行科室
        ExecDeptName = execDept;


        //收费大类代码
        FeeTypeMainCode = feeTypeMain;


        //收费小类代码
        FeeTypeSubCode = feeTypeSub;
        PlatformType = platformType;
    }

    #endregion

    /// <summary>
    /// 同步修改
    /// </summary>
    /// <param name="treatName"></param>
    /// <param name="price"></param>
    /// <param name="categoryCode"></param>
    /// <param name="categoryName"></param>
    /// <param name="unit"></param>
    /// <param name="otherPrice"></param>
    /// <param name="projectMerge"></param>
    /// <param name="isDeleted"></param>
    /// <param name="additional"></param>
    public void Update(string treatName,
        decimal price,
        string categoryCode,
        string categoryName,
        string unit,
        decimal? otherPrice,
        string projectMerge,
        bool isDeleted,
        bool additional)
    {
        TreatName = treatName;
        Price = price;
        CategoryCode = categoryCode;
        CategoryName = categoryName;
        Unit = unit;
        OtherPrice = otherPrice;
        ProjectMerge = projectMerge;
        IsDeleted = isDeleted;
        Additional = additional;
        PyCode = treatName.FirstLetterPY();
        WbCode = treatName.FirstLetterWB();
    }
}