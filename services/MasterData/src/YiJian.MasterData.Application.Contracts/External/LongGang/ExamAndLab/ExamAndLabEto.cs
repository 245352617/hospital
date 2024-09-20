using Volo.Abp.EventBus;
using YiJian.ECIS.ShareModel.Enums;

namespace YiJian.MasterData.External.LongGang.ExamAndLab;

/// <summary>
/// 1：检验；2：检查
/// </summary>
[EventName("InspectDicEvents")]
public class ExamAndLabEto
{
    /// <summary>
    /// 检查检验类型  1：检验；2：检查
    /// </summary>
    public int InspectType { get; set; }

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
    /// 拼英代码
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
    /// 12.TCT细胞学检查申请单
    ///11.病理检验申请单
    ///16.门诊大型设备检查治疗项目审核、报告单（需两联）、需配合医保患者使用
    /// </summary>
    public string AddCard { get; set; }


    /// <summary>
    /// 上级目录编码 
    /// </summary>
    public string SuperiorCode { get; set; }

    /// <summary>
    /// 上级目录名称
    /// </summary>
    public string SuperiorName { get; set; }

    /// <summary>
    /// 1.作废  0.使用中
    /// </summary>
    public string UseFlag { get; set; }

    /// <summary>
    /// 科室跟踪执行类别
    /// </summary>
    public EDepExecutionType DepExecutionType { get; set; }

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
    /// 明天医网检查部位编码 对于 ztbm
    /// </summary>
    public string CheckPartCode { get; set; }

}

