using Volo.Abp.EventBus;
using YiJian.ECIS.ShareModel.Enums;

namespace YiJian.MasterData.External.LongGang.Teat;

[EventName("TreatmentGroupDicEvents")]
public class TreatsEto
{
    /// <summary>
    /// 项目id
    /// </summary>
    public string ProjectId { get; set; }

    /// <summary>
    /// 项目名称
    /// </summary>
    public string ProjectName { get; set; }

    /// <summary>
    /// 项目单位
    /// </summary>
    public string Unit { get; set; }

    /// <summary>
    /// 项目类型
    /// </summary>
    public string ProjectType { get; set; }

    /// <summary>
    /// 项目类型名称
    /// </summary>
    public string ProjectTypeName { get; set; }

    /// <summary>
    /// 项目单价
    /// </summary>
    public double? Price { get; set; }

    /// <summary>
    /// 拼英代码
    /// </summary>
    public string SpellCode { get; set; }

    /// <summary>
    /// 加收标志
    /// </summary>
    public string Additional { get; set; }

    /// <summary>
    /// 加收后金额
    /// </summary>
    public double ChargeAmount { get; set; }

    /// <summary>
    /// 项目归类
    /// </summary>
    public string ProjectMerge { get; set; }

    /// <summary>
    /// 1.作废  0.使用中
    /// </summary>
    public string UseFlag { get; set; }

    /// <summary>
    /// 医保编码
    /// </summary>
    public string MeducalInsuranceCode { get; set; }

    /// <summary>
    /// 医保二级编码
    /// </summary>
    public string YBInneCode { get; set; }
     
    /// <summary>
    /// 科室跟踪执行类别
    /// </summary>
    public EDepExecutionType? DepExecutionType { get; set; }

    /// <summary>
    /// 科室跟踪执行规则
    /// <![CDATA[
    /// depExecutionType=1：固定科室
    /// depExecutionType=9：4.4.14执行科室规则字典（his提供）ruleId字段、
    /// 默认:departmentCode
    /// ]]>
    /// </summary>
    public string DepExecutionRules { get; set; }

}