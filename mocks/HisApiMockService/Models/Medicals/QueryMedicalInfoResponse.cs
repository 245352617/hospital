using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HisApiMockService.Models.Medicals;

/// <summary>
/// 医嘱状态查询请求
/// </summary>
[Table("MedicalInfoStatus")]
public class QueryMedicalInfoResponse
{
    [Key]
    public int Id { get; set; }
    /// <summary>
    /// 就诊流水号
    /// <![CDATA[
    /// 4.5.2 就诊记录、诊断信息回传（his提供、需对接集成平台）visSeriaINo
    /// ]]>
    /// </summary>
    public string? VisSerialNo { get; set; }

    /// <summary>
    /// 渠道订单序号
    /// <![CDATA[
    /// 4.5.3医嘱信息回传（his提供、需对接集成平台） prescriptionNo, projectItemNo
    /// ]]>
    /// </summary>
    public string? ChannelBillId { get; set; }

    /// <summary>
    /// His订单序号
    /// <![CDATA[
    /// 对应his处方、医技唯一识别
    /// ]]>
    /// </summary>
    public string? HisBillId { get; set; }

    /// <summary>
    /// 就诊患者姓名
    /// </summary>
    public string? PatientName { get; set; }

    /// <summary>
    /// 就诊科室
    /// </summary>
    public string? DeptCode { get; set; }

    /// <summary>
    /// 就诊医生编号
    /// </summary>
    public string? DoctorCode { get; set; }

    /// <summary>
    /// 订单状态
    /// <![CDATA[
    /// 0.未缴费 1.已缴费  -1.已作废  2.已执行
    /// ]]>
    /// </summary>
    public int BillState { get; set; }

}
