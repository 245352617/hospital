using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using YiJian.ECIS.ShareModel.Enums;
using YiJian.MasterData.Labs;

namespace YiJian.MasterData;

/// <summary>
/// 检验项目 读取输出
/// </summary>
[Serializable]
public class LabProjectData
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
    /// 检验目录编码
    /// </summary>
    public string CatalogCode { get; set; }

    /// <summary>
    /// 目录分类名称
    /// </summary>
    public string CatalogName { get; set; }

    /// <summary>
    /// 标本编码
    /// </summary>
    public string SpecimenCode { get; set; }

    /// <summary>
    /// 标本名称
    /// </summary>
    public string SpecimenName { get; set; }

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
    /// 采集部位编码
    /// </summary>
    public string SpecimenPartCode { get; set; }

    /// <summary>
    /// 采集部位名称
    /// </summary>
    public string SpecimenPartName { get; set; }

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
    /// 单位
    /// </summary>
    public string Unit { get; set; }

    /// <summary>
    /// 价格
    /// </summary>
    public decimal Price { get; set; }

    /// <summary>
    /// 价格
    /// </summary>
    public decimal OtherPrice { get; set; }

    /// <summary>
    /// 容器编码
    /// </summary>
    public string ContainerCode { get; set; }

    /// <summary>
    /// 容器名称
    /// </summary>
    public string ContainerName { get; set; }

    /// <summary>
    /// 是否启用
    /// </summary>
    public bool IsActive { get; set; }

    /// <summary>
    /// 平台标识
    /// </summary>
    public PlatformType PlatformType { get; set; }
    /// <summary>
    /// 附加卡片类型	15.孕母血清胎儿唐氏综合征筛查申请单(早、中期)
    ///14.新型冠状病毒RNA检测申请单
    ///13.基于孕妇外周血胎儿 基于孕妇外周血胎儿13 、18 、21-三体综合征基因筛查申请单
    /// </summary>
    public string AddCard { get; set; }

    /// <summary>
    /// 分类编码-院前使用
    /// </summary>
    public string CategoryCode { get; set; } = "Lab";

    /// <summary>
    /// 分类名称-院前使用
    /// </summary>
    public string CategoryName { get; set; } = "检验";
    /// <summary>
    /// 指引编码
    /// </summary>
    public string GuideCode { get; set; }

    /// <summary>
    /// 指引名称
    /// </summary>
    public string GuideName { get; set; }

    /// <summary>
    /// 指引大类
    /// </summary>
    public string GuideCatelogName { get; set; }

    /// <summary>
    /// 检验小项
    /// </summary>
    public List<LabTargetData> Items { get; set; }

    /// <summary>
    /// 分类编码和当前项目的编码组合
    /// </summary>
    public string CatalogAndProjectCode { get; set; }

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
}