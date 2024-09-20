using Volo.Abp.EventBus;
using YiJian.ECIS.ShareModel.Enums;

namespace YiJian.MasterData.External.LongGang.Lab;

[EventName("LabDicEvents")]
public class LabEto
{
    /// <summary>
    /// 组套id
    /// </summary>
    public string GroupId { get; set; }

    /// <summary>
    /// 组套名称
    /// </summary>
    public string GroupName { get; set; }

    /// <summary>
    /// 组套类型
    /// </summary>
    public string GroupType { get; set; }

    /// <summary>
    /// 拼音代码
    /// </summary>
    public string SpellCode { get; set; }

    /// <summary>
    /// 科室代码
    /// </summary>
    public string DepartmentCode { get; set; }

    /// <summary>
    /// 科室名称
    /// </summary>
    public string DepartmentName { get; set; }

    /// <summary>
    /// 样本类型编码
    /// </summary>
    public string SampleTypeId { get; set; }

    /// <summary>
    /// 样本类型
    /// </summary>
    public string SampleType { get; set; }

    /// <summary>
    /// 明细项目id
    /// </summary>
    public string GroupsId { get; set; }

    /// <summary>
    /// 明细项目名称
    /// </summary>
    public string GroupsName { get; set; }

    /// <summary>
    /// 总数量
    /// </summary>
    public string TotalNumber { get; set; }

    /// <summary>
    /// 单位
    /// </summary>
    public string Unit { get; set; }

    /// <summary>
    /// 单价
    /// </summary>
    public double Price { get; set; }

    /// <summary>
    /// 指引ID
    /// </summary>
    public string GuideCode { get; set; }

    /// <summary>
    /// 容器id
    /// </summary>
    public string ContainerId { get; set; }

    /// <summary>
    /// 容器类别
    /// </summary>
    public string ContainerType { get; set; }


    /// <summary>
    /// 标本编码
    /// </summary>
    public string SpecimenNo { get; set; }

    /// <summary>
    /// 标本名称
    /// </summary>
    public string SpecimenName { get; set; }

    /// <summary>
    /// 项目类型
    /// </summary>
    public string ProjectType { get; set; }

    /// <summary>
    /// 项目归类
    /// </summary>
    public string ProjectMerge { get; set; }

    /// <summary>
    ///15.孕母血清胎儿唐氏综合征筛查申请单(早、中期)
    ///14.新型冠状病毒RNA检测申请单
    ///13.基于孕妇外周血胎儿 基于孕妇外周血胎儿13 、18 、21-三体综合征基因筛查申请单
    /// </summary>
    public string AddCard { get; set; }

    /// <summary>
    /// 1.作废  0.使用中
    /// </summary>
    public string UseFlag { get; set; }


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

}