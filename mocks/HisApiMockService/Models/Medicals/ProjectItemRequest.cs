using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HisApiMockService.Models.Medicals;

/// <summary>
/// 执行科室
/// </summary>
[Table("ProjectItem")]
public class ProjectItemRequest
{
    [Key]
    public int Id { get; set; }
    /// <summary>
    /// 执行科室
    /// <![CDATA[
    /// 本科室：0
    /// ]]>
    /// </summary>
    public string ExecuteDeptCode { get; set; }

    /// <summary>
    /// 急诊标志 0.非危急   1.危急
    /// </summary>
    public int EmergencySign { get; set; }

    /// <summary>
    /// 202：检验  203：检查  0：诊疗、材料等…
    /// </summary>
    public string GroupType { get; set; }

    /// <summary>
    /// 组套ID groupType=202、203时必填
    /// </summary>
    public string GroupId { get; set; }

    /// <summary>
    /// 项目序号
    /// <![CDATA[
    /// 唯一识别医技诊疗单据的号码，用于后期状态修改
    /// ]]>
    /// </summary>
    public string ProjectItemNo { get; set; }
     
    /// <summary>
    /// 诊疗项目明细节点
    /// </summary>
    public ProjectDetailRequest ProjectDetail { get; set; }






}
