using Microsoft.EntityFrameworkCore;
using System;
using YiJian.ECIS.ShareModel.Enums;

namespace YiJian.MasterData.Treats;

/// <summary>
/// 诊疗项目字典 读取输出
/// </summary>
[Serializable]
public class TreatData
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
    /// 拼音码
    /// </summary>
    public string PyCode { get; set; }

    /// <summary>
    /// 五笔
    /// </summary>
    public string WbCode { get; set; }

    /// <summary>
    /// 单价
    /// </summary>
    public decimal Price { get; set; }
    /// <summary>
    /// 单价
    /// </summary>
    public decimal RetPrice { get; set; }
    /// <summary>
    /// 其它价格
    /// </summary>
    public decimal? OtherPrice { get; set; }
    
    /// <summary>
    /// 加收标志	
    /// </summary>
    public bool Additional { get; set; }

    /// <summary>
    /// 诊疗处置类别代码
    /// </summary>
    public string CategoryCode { get; set; }

    /// <summary>
    /// 诊疗处置类别名称
    /// </summary>
    public string CategoryName { get; set; }

    /// <summary>
    /// 规格
    /// </summary>
    public string Specification { get; set; }

    /// <summary>
    /// 单位
    /// </summary>
    public string Unit { get; set; }

    /// <summary>
    /// 默认频次代码
    /// </summary>
    public string FrequencyCode { get; set; }

    /// <summary>
    /// 收费大类代码
    /// </summary>
    public string FeeTypeMainCode { get; set; }

    /// <summary>
    /// 收费小类代码
    /// </summary>
    public string FeeTypeSubCode { get; set; }

    /// <summary>
    /// 平台标识
    /// </summary>
    public PlatformType PlatformType { get; set; }

    /// <summary>
    /// 项目归类
    /// </summary>
    public string ProjectMerge { get; set; }
    
    /// <summary>
    /// 项目类型
    /// </summary>
    public string ProjectType { get; set; }

    /// <summary>
    /// 项目类型名称
    /// </summary>
    public string ProjectTypeName { get; set; }
     
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
    /// 项目类型名称
    /// </summary>
    public string ProjectName {
        get
        {
            return ProjectTypeName;
        }
    }


    /// <summary>
    /// 医保编码
    /// </summary>
    public string MeducalInsuranceCode { get; set; }

    /// <summary>
    /// 医保二级编码
    /// </summary>
    public string YBInneCode { get; set; }

    // /// <summary>
    // /// 【汕大需求】开立检验检查诊疗要有医嘱限制性提醒，而且默认是否
    // /// 医保限制支持条件
    // /// </summary>
    // public string AdditionalRemark => "医保限制支持条件：当前药品限工伤保险";
}