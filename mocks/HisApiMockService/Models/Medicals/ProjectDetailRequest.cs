using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HisApiMockService.Models.Medicals;

/// <summary>
/// 诊疗项目明细节点
/// </summary>
[Table("ProjectDetail")]
public class ProjectDetailRequest
{
    [Key]
    public int Id { get; set; }
    /// <summary>
    /// 项目明细序号
    /// <![CDATA[
    /// 唯一识别医技诊疗明细单据的号码，用于后期状态修改
    /// ]]>
    /// </summary>
    public string? ProjectDetailNo { get; set; }

    /// <summary>
    /// 项目明细id
    /// <![CDATA[
    /// 检验：4.4.5检验组套（his提供）
    /// ]]>
    /// </summary>
    public string? GroupsId { get; set; }

    /// <summary>
    /// 项目类型
    /// <![CDATA[
    /// 检验：4.4.5检验组套（his提供） projectType
    /// 检查：4.4.6检查组套（his提供） projectType
    /// 诊疗材料:4.4.8诊疗材料字典（his提供）  projectType 
    /// ]]>
    /// </summary>
    public string? ProjectType { get; set; }

    /// <summary>
    /// 检验：4.4.5检验组套（his提供） projectMerge
    /// 检查：4.4.6检查组套（his提供） projectMerge
    /// 诊疗材料:4.4.8诊疗材料字典（his提供）  projectMerge
    /// </summary>
    public string? ProjectMerge { get; set; }

    /// <summary>
    /// 项目主项
    /// <![CDATA[
    /// 主项:1  次项:0 
    /// ]]>
    /// </summary>
    public string? ProjectMain { get; set; }

    /// <summary>
    /// 项目单价 格式：0.00 
    /// </summary>
    public decimal ProjectPrice { get; set; }

    /// <summary>
    /// 项目数量 格式：0.00 
    /// </summary>
    public decimal ProjectQuantity { get; set; }

    /// <summary>
    /// 项目总价
    /// <![CDATA[
    /// 项目数量*项目单价 格式：0.00
    /// ]]>
    /// </summary>
    public decimal ProjectTotamount { get; set; }

    /// <summary>
    /// 组套名称
    /// </summary>
    public string? ProjectName { get; set; }

    /// <summary>
    /// 限制标志
    /// </summary>
    public ERestrictedDrugs RestrictedDrugs { get; set; }

    /// <summary>
    /// 备注信息
    /// </summary>
    public string? Remarks { get; set; }


}
