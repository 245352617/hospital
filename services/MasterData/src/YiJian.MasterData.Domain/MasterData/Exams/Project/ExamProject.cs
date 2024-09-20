using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Domain.Entities.Auditing;
using YiJian.ECIS.ShareModel.Enums;
using YiJian.ECIS.ShareModel.Models;

namespace YiJian.MasterData.Exams;

/// <summary>
/// 检查申请项目
/// </summary>
[Comment("检查申请项目")]
public class ExamProject : FullAuditedAggregateRoot<int>, IIsActive
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
    public string ProjectCode { get; set; }

    /// <summary>
    /// 名称
    /// </summary>
    [Required]
    [StringLength(200)]
    [Comment("名称")]
    public string ProjectName { get; set; }

    /// <summary>
    /// 目录编码
    /// </summary>
    [Required]
    [StringLength(20)]
    [Comment("目录编码")]
    public string CatalogCode { get; set; }

    /// <summary>
    /// 目录名称
    /// </summary>
    [Required]
    [StringLength(100)]
    [Comment("目录名称")]
    public string CatalogName { get; set; }

    /// <summary>
    /// 排序号
    /// </summary>
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
    /// 检查部位
    /// </summary>
    [StringLength(50)]
    [Comment("检查部位编码")]
    public string PartCode { get; set; }

    /// <summary>
    /// 检查部位
    /// </summary>
    [StringLength(50)]
    [Comment("检查部位名称")]
    public string PartName { get; set; }

    /// <summary>
    /// 单位
    /// </summary>
    [StringLength(20)]
    [Comment("单位")]
    public string Unit { get; set; }

    /// <summary>
    /// 价格
    /// </summary>
    [Comment("价格")]
    public decimal Price { get; set; }

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
    /// 执行机房编码
    /// </summary>
    [StringLength(20)]
    [Comment("执行机房编码")]
    public string RoomCode { get; set; }

    /// <summary>
    /// 执行机房名称
    /// </summary>
    [StringLength(50)]
    [Comment("执行机房名称")]
    public string RoomName { get; set; }


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
    /// 附加卡片类型
    ///12.TCT细胞学检查申请单
    ///11.病理检验申请单
    ///16.门诊大型设备检查治疗项目审核、报告单（需两联）、需配合医保患者使用
    /// </summary>
    [StringLength(50)]
    [Comment("附加卡片类型 12.TCT细胞学检查申请单 11.病理检验申请单 16.门诊大型设备检查治疗项目审核、报告单（需两联）、需配合医保患者使用")]
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
    /// 检查单单名标题
    /// </summary>
    [StringLength(64)]
    [Comment("检查单单名标题")]
    public string ExamTitle { get; set; }

    /// <summary>
    /// 预约地点
    /// </summary>
    [Comment("预约地点")]
    public string ReservationPlace { get; set; }

    /// <summary>
    /// 预约时间
    /// </summary>
    [Comment("预约时间")]
    public string ReservationTime { get; set; }

    /// <summary>
    /// 注意事项
    /// </summary>
    [Comment("注意事项")]
    public string Note { get; set; }

    /// <summary>
    /// 打印模板Id
    /// </summary>
    [Comment("打印模板Id")]
    public string TemplateId { get; set; }

    /// <summary>
    /// 明天医网检查部位编码 （北大的字段）
    /// </summary>
    [Comment("明天医网检查编码")]
    [StringLength(50)]
    public string CheckPartCode { get; set; }

    /// <summary>
    /// 明天医网检查类型 （北大的字段）
    /// </summary>
    [Comment("明天医网检查类型")]
    [StringLength(50)]
    public string CheckTypeCode { get; set; }

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
    /// <param name="catalogCode">分类编码</param>
    /// <param name="catalog">分类名称</param>
    /// <param name="indexNo">排序号</param>
    /// <param name="examPartName"></param>
    /// <param name="unit">单位</param>
    /// <param name="price">价格</param>
    /// <param name="deptCode">科室编码</param>
    /// <param name="deptName"></param>
    /// <param name="roomCode">执行机房编码</param>
    /// <param name="room">执行机房描述</param>
    /// <param name="isActive">是否启用</param>
    /// <param name="examPartCode"></param>
    /// <param name="platformType"></param>
    public void Modify(string name, // 名称
        string catalogCode, // 分类编码
        string catalog, // 分类名称
        int indexNo, // 排序号
        string examPartCode, // 检查部位编码
        string examPartName, // 检查部位名称
        string unit, // 单位
        decimal price, // 价格
        string deptCode, // 科室编码
        string deptName, // 科室名称
        string roomCode, // 执行机房编码
        string room, // 执行机房描述
        bool isActive, // 是否启用
        PlatformType platformType
    )
    {
        //名称
        ProjectName = name;

        //分类编码
        CatalogCode = catalogCode;

        //分类名称
        CatalogName = catalog;

        //排序号
        Sort = indexNo;

        //拼音码
        PyCode = name.FirstLetterPY();

        //五笔
        WbCode = name.FirstLetterWB();

        //检查部位
        PartCode = examPartCode;
        PartName = examPartName;

        //单位
        Unit = unit;

        //价格
        Price = price;

        //科室编码
        ExecDeptCode = deptCode;
        ExecDeptName = deptName;

        //执行机房编码
        RoomCode = roomCode;

        //执行机房描述
        RoomName = room;
        IsActive = isActive;
        PlatformType = platformType;
    }

    #endregion
}