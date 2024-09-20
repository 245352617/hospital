using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HisApiMockService.Models.Advices;

/// <summary>
/// 记录信息
/// </summary>
[Table("RecordInfo")]
public class Recordinfo
{
    [Key]
    public int Id { get; set; }
    /// <summary>
    /// 操作日期/结束就诊日期/暂挂时间 
    /// <![CDATA[
    /// 格式：yyyy-mm-dd hh24:mi:ss recordType = 2，则为结束就诊时间
    /// ]]>
    /// </summary>
    [Required]
    public string? OperatorDate { get; set; }

    [Required]
    public ErecordState RecordState { get; set; }

    /// <summary>
    /// 记录id
    /// <![CDATA[
    /// recordType = 1: 就诊记录、诊断信息回传（his提供、需对接集成平台） diagNo [4.5.2 ]
    /// recordType = 2：visSerialNo
    /// recordType = 3：医嘱信息回传（his提供、需对接集成平台） prescriptionNo、 projectItemNo [4.5.3]
    /// ]]>
    /// </summary>
    [Required]
    public string? RecordNo { get; set; }

    /// <summary>
    /// 记录明细id
    /// <![CDATA[
    /// recordType=1:null
    /// recordType=2：null
    /// recordType=3：医嘱信息回传（his提供、需对接集成平台）  chargeDetailNo 、  projectDetailNo [4.5.3]
    /// ]]>
    /// </summary>
    public string? RecordDetailNo { get; set; }


}
