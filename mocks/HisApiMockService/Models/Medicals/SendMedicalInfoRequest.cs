using System.ComponentModel.DataAnnotations.Schema;

namespace HisApiMockService.Models.Medicals;

/// <summary>
/// 用于医嘱新增提交时回传给his进行存储
/// </summary>
[Table("SendMedicalInfo")]
public class SendMedicalInfoRequest
{
    public int Id { get; set; }
    /// <summary>
    /// 就诊流水号
    /// <![CDATA[
    /// 4.5.2 就诊记录、诊断信息回传（his提供、需对接集成平台）visSeriaINo
    /// ]]>
    /// </summary>
    public string? VisSerialNo { get; set; }

    /// <summary>
    /// 病人ID
    /// </summary>
    public string? PatientId { get; set; }
     
    /// <summary>
    /// 医生编码
    /// </summary>
    public string? DoctorCode { get; set; }

    /// <summary>
    /// 科室编码
    /// </summary>
    public string? DeptCode { get; set; }

    /// <summary>
    /// 医生姓名
    /// </summary>
    public string? DoctorName { get; set; }

    /// <summary>
    /// 处方外层节点
    /// </summary>
    public List<DrugItemRequest> DrugItem { get; set; }

    /// <summary>
    /// 诊疗项目外层节点
    /// </summary>
    public List<ProjectItemRequest> ProjectItem { get;set;}
     
}
