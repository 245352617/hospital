using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using YiJian.ECIS.ShareModel.Enums;

namespace YiJian.MasterData.Exams;

/// <summary>
/// 检查申请项目 读取输出
/// </summary>
[Serializable]
public class ExamProjectData
{
    public int Id { get; set; }

    /// <summary>
    /// 编码
    /// </summary>
    public string Code { get; set; }

    /// <summary>
    /// 名称
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// 分类编码
    /// </summary>
    public string CatalogCode { get; set; }

    /// <summary>
    /// 目录名称
    /// </summary>
    public string CatalogName { get; set; }
    /// <summary>
    /// 一级分类编码
    /// </summary>
    public string FirstCatalogCode { get; set; }

    /// <summary>
    /// 一级目录名称
    /// </summary>
    public string FirstCatalogName { get; set; }
    /// <summary>
    /// 项目编码
    /// </summary>

    public string ProjectCode { get; set; }

    /// <summary>
    /// 项目名称
    /// </summary>
    public string ProjectName { get; set; }
    /// <summary>
    /// 排序号
    /// </summary>
    public int Sort { get; set; }

    /// <summary>
    /// 拼音码
    /// </summary>
    public string PyCode { get; set; }

    /// <summary>
    /// 五笔
    /// </summary>
    public string WbCode { get; set; }

    /// <summary>
    /// 检查部位编码
    /// </summary>
    public string PartCode { get; set; }

    /// <summary>
    /// 检查部位名称
    /// </summary>
    public string PartName { get; set; }

    /// <summary>
    /// 单位
    /// </summary>
    public string Unit { get; set; }

    /// <summary>
    /// 价格
    /// </summary>
    public decimal Price { get; set; }

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
    public EDepExecutionType? DepExecutionType { get; set; } = EDepExecutionType.UntracedExec;

    /// <summary>
    /// 科室跟踪执行规则
    /// <![CDATA[
    /// depExecutionType=1：固定科室
    /// depExecutionType=9：4.4.14执行科室规则字典（his提供）ruleId字段、
    /// 默认:departmentCode
    /// ]]>
    /// </summary>
    public string DepExecutionRules { get; set; }


    /// <summary>
    /// 科室编码
    /// </summary>
    public string ExecDeptCode { get; set; }

    /// <summary>
    /// 科室名称
    /// </summary>
    public string ExecDeptName { get; set; }

    /// <summary>
    /// 执行机房编码
    /// </summary>
    public string RoomCode { get; set; }

    /// <summary>
    /// 执行机房名称
    /// </summary>
    public string RoomName { get; set; }

    /// <summary>
    /// 是否启用
    /// </summary>
    public bool IsActive { get; set; }

    /// <summary>
    /// 平台标识
    /// </summary>
    public PlatformType PlatformType { get; set; }

    /// <summary>
    /// 附加卡片类型
    ///12.TCT细胞学检查申请单
    ///11.病理检验申请单
    ///16.门诊大型设备检查治疗项目审核、报告单（需两联）、需配合医保患者使用
    /// </summary>
    public string AddCard { get; set; }

    /// <summary>
    /// 分类编码-院前使用
    /// </summary>
    public string CategoryCode { get; set; } = "Examine";

    /// <summary>
    /// 分类名称-院前使用
    /// </summary>
    public string CategoryName { get; set; } = "检查";
    /// <summary>
    /// 指引编码
    /// </summary>
    public string GuideCode { get; set; }
    /// <summary>
    /// 指引名称
    /// </summary>
    public string GuideName { get; set; }

    /// <summary>
    /// 检查单单名标题
    /// </summary>
    public string ExamTitle { get; set; }

    /// <summary>
    /// 预约地点
    /// </summary>
    public string ReservationPlace { get; set; }

    /// <summary>
    /// 预约时间
    /// </summary>
    public string ReservationTime { get; set; }

    /// <summary>
    /// 注意事项
    /// </summary>
    public string Note { get; set; }

    /// <summary>
    /// 打印模板Id
    /// </summary>
    public string TemplateId { get; set; }

    /// <summary>
    /// 附加药品编码(多个用','分隔)
    /// </summary>
    public string PrescribeCode { get; set; }

    /// <summary>
    /// 附加药品名称(多个用','分隔)
    /// </summary>
    public string PrescribeName { get; set; }

    /// <summary>
    /// 附加处置编码(多个用','分隔)
    /// </summary>
    public string TreatCode { get; set; }

    /// <summary>
    /// 附加处置名称(多个用','分隔)
    /// </summary>
    public string TreatName { get; set; }

    /// <summary>
    /// 检查小项
    /// </summary>
    public List<ExamTargetData> Items { get; set; }
}