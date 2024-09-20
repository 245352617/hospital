using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HisApiMockService.Models.Advices;

/// <summary>
/// 诊断、就诊记录、医嘱状态变更
/// </summary>
[Table("RecordStatusRequest")]
public class UpdateRecordStatusRequest
{
    [Key]
    public int Id { get; set; }
    /// <summary>
    /// 记录类型
    /// </summary>
    [Required]
    public ERecordType RecordType { get;set;}

    /// <summary>
    /// 就诊流水号
    /// </summary>
    [Required]
    public string VisSerialNo { get; set; }

    /// <summary>
    /// 病人ID
    /// </summary>
    [Required]
    public string PatientId { get; set; }

    /// <summary>
    /// 操作人编码
    /// </summary>
    [Required] 
    public string OperatorCode { get; set; }

    /// <summary>
    /// 科室编码
    /// </summary>
    [Required] 
    public string DeptCode { get; set; }

    /// <summary>
    /// 记录信息
    /// </summary>
    public List<Recordinfo> Recordinfo { get; set; }

}
