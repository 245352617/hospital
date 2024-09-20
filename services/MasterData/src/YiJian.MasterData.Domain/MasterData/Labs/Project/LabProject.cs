using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Domain.Entities.Auditing;
using YiJian.ECIS.ShareModel.Enums;
using YiJian.ECIS.ShareModel.Models;

namespace YiJian.MasterData;

/// <summary>
/// 检验项目
/// </summary>
[Comment("检验项目")]
public class LabProject : FullAuditedAggregateRoot<int>, IIsActive
{
    #region Properties

    /// <summary>
    /// 编码
    /// </summary>
    [Required]
    [StringLength(50)]
    [Comment("编码")]
    public string ProjectCode { get; set; }

    /// <summary>
    /// 名称
    /// </summary>
    [Required]
    [StringLength(200)]
    [Comment("名称")]
    public string ProjectName { get; set; }

    /// <summary>
    /// 检验目录编码
    /// </summary>
    [Required]
    [StringLength(20)]
    [Comment("检验目录编码")]
    public string CatalogCode { get; set; }

    /// <summary>
    /// 检验目录名称
    /// </summary>
    [Required]
    [StringLength(200)]
    [Comment("检验目录名称")]
    public string CatalogName { get; set; }

    /// <summary>
    /// 标本编码
    /// </summary>
    [StringLength(20)]
    [Comment("标本编码")]
    public string SpecimenCode { get; set; }

    /// <summary>
    /// 标本名称
    /// </summary>
    [StringLength(200)]
    [Comment("标本名称")]
    public string SpecimenName { get; set; }

    /// <summary>
    /// 科室编码
    /// </summary>
    [StringLength(20)]
    [Comment("科室编码")]
    public string ExecDeptCode { get; set; }

    /// <summary>
    /// 科室名称
    /// </summary>
    [StringLength(50)]
    [Comment("科室名称")]
    public string ExecDeptName { get; set; }

    /// <summary>
    /// 位置编码
    /// </summary>
    [StringLength(20)]
    [Comment("检验位置编码")]
    public string SpecimenPartCode { get; set; }

    /// <summary>
    /// 位置名称
    /// </summary>
    [StringLength(100)]
    [Comment("检验位置名称")]

    public string SpecimenPartName { get; set; }

    /// <summary>
    /// 排序号
    /// </summary>
    [Required]
    [Comment("排序号")]
    public int Sort { get; set; }

    /// <summary>
    /// 拼音码
    /// </summary>
    [StringLength(200)]
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
    [StringLength(50)]
    [Comment("单位")]
    public string Unit { get; set; }

    /// <summary>
    /// 价格
    /// </summary>
    [Required]
    [Comment("价格")]
    public decimal Price { get; set; }

    /// <summary>
    /// 其他价格
    /// </summary>
    [Comment("其他价格")]
    public decimal OtherPrice { get; set; }

    /// <summary>
    /// 容器编码
    /// </summary>
    [StringLength(100)]
    [Comment("容器编码")]
    public string ContainerCode { get; set; }

    /// <summary>
    /// 容器名称
    /// </summary>
    [StringLength(200)]
    [Comment("容器名称")]
    public string ContainerName { get; set; }

    /// <summary>
    /// 是否启用
    /// </summary>
    [Comment("是否启用")]
    public bool IsActive { get; set; } = true;

    /// <summary>
    /// 平台标识
    /// </summary>
    [Comment("平台标识")]
    public PlatformType PlatformType { get; set; }

    /// <summary>
    /// 附加卡片类型	15.孕母血清胎儿唐氏综合征筛查申请单(早、中期)
    ///14.新型冠状病毒RNA检测申请单
    ///13.基于孕妇外周血胎儿 基于孕妇外周血胎儿13 、18 、21-三体综合征基因筛查申请单
    /// </summary>
    [StringLength(50)]
    [Comment("附加卡片类型 15.孕母血清胎儿唐氏综合征筛查申请单(早、中期)14.新型冠状病毒RNA检测申请单13.基于孕妇外周血胎儿 基于孕妇外周血胎儿13 、18 、21-三体综合征基因筛查申请单")]
    public string AddCard { get; set; }


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
    /// 指引ID 关联 ExamNote表code
    /// </summary>
    [StringLength(50)]
    [Comment("指引ID 关联 ExamNote表code")]
    public string GuideCode { get; set; }

    /// <summary>
    /// 分类编码和当前项目的编码组合
    /// </summary>
    [Comment("分类编码和当前项目的编码组合")]
    [StringLength(50)]
    public string CatalogAndProjectCode { get; set; }

    /// <summary>
    /// 附加药品编码(多个用','分隔)
    /// </summary>
    [Comment("附加药品编码(多个用','分隔)")]
    [StringLength(50)]
    public string PrescribeCode { get; set; }

    /// <summary>
    /// 附加药品名称(多个用','分隔)
    /// </summary>
    [Comment("附加药品名称(多个用','分隔)")]
    [StringLength(500)]
    public string PrescribeName { get; set; }

    /// <summary>
    /// 附加处置编码(多个用','分隔)
    /// </summary>
    [Comment("附加处置编码(多个用','分隔)")]
    [StringLength(50)]
    public string TreatCode { get; set; }

    /// <summary>
    /// 附加处置名称(多个用','分隔)
    /// </summary>
    [Comment("附加处置名称(多个用','分隔)")]
    [StringLength(500)]
    public string TreatName { get; set; }
    #endregion

    #region Modify

    /// <summary>
    /// 修改
    /// </summary>
    /// <param name="name">名称</param>
    /// <param name="catalogCode">检验分类编码</param>
    /// <param name="catalog">检验分类</param>
    /// <param name="specimenCode">标本编码</param>
    /// <param name="specimen">标本</param>
    /// <param name="deptCode">科室编码</param>
    /// <param name="dept">科室</param>
    /// <param name="positionCode">位置编码</param>
    /// <param name="position">位置</param>
    /// <param name="indexNo">排序号</param>
    /// <param name="unit">单位</param>
    /// <param name="price">价格</param>
    /// <param name="otherPrice">价格</param>
    /// <param name="isActive">是否启用</param>
    /// <param name="containerCode"></param>
    /// <param name="containerName"></param>
    /// <param name="platformType"></param>
    /// <param name="depExecutionType"></param>
    /// <param name="depExecutionRules"></param>
    public void Modify([NotNull] string name, // 名称
        [NotNull] string catalogCode, // 检验分类编码
        [NotNull] string catalog, // 检验分类
        [NotNull] string specimenCode, // 标本编码
        [NotNull] string specimen, // 标本
        string deptCode, // 科室编码
        string dept, // 科室
        string positionCode, // 位置编码
        string position, // 位置
        [NotNull] int indexNo, // 排序号
        [NotNull] string unit, // 单位
                               //[NotNull] decimal price, // 价格
        decimal otherPrice, // 价格
        bool isActive, // 是否启用
        string containerCode,
        string containerName,
        PlatformType platformType = PlatformType.EmergencyTreatment,
        EDepExecutionType? depExecutionType = null,
        string depExecutionRules = null
    )
    {
        //名称
        ProjectName = name;
        //检验分类编码
        CatalogCode = catalogCode;
        //检验分类
        CatalogName = catalog;
        //标本编码
        SpecimenCode = specimenCode;
        //标本
        SpecimenName = specimen;
        //科室编码
        ExecDeptCode = deptCode;
        //科室
        ExecDeptName = dept;
        //位置编码
        SpecimenPartCode = positionCode;
        //位置
        SpecimenPartName = position;
        //排序号
        Sort = indexNo;
        //拼音码
        PyCode = name.FirstLetterPY();
        //五笔
        WbCode = name.FirstLetterWB();
        //单位
        Unit = unit;
        //价格
        //Price = price; 
        //价格
        OtherPrice = otherPrice;
        //是否启用
        IsActive = isActive;
        ContainerCode = containerCode;
        ContainerName = containerName;
        PlatformType = platformType;
        DepExecutionType = depExecutionType;
        DepExecutionRules = depExecutionRules;
    }

    #endregion
}