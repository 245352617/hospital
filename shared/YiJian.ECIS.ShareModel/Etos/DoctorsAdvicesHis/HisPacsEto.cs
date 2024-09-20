using System.ComponentModel.DataAnnotations;

namespace YiJian.ECIS.ShareModel.Etos.DoctorsAdvices;

/// <summary>
/// 检查项
/// </summary>
public class HisPacsEto
{
    /// <summary>
    /// HIS医嘱号
    /// </summary> 
    [StringLength(36)]
    [Required]
    public string HisOrderNo { get; set; }

    /// <summary>
    /// Id
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// 医嘱Id
    /// </summary> 
    public Guid DoctorsAdviceId { get; set; }

    /// <summary>
    /// 检查目录编码
    /// </summary> 
    [Required, StringLength(20)]
    public string CatalogCode { get; set; }

    /// <summary>
    /// 检查目录名称
    /// </summary> 
    [Required, StringLength(100)]
    public string CatalogName { get; set; }

    /// <summary>
    /// 临床症状
    /// </summary> 
    [StringLength(2000)]
    public string ClinicalSymptom { get; set; }

    /// <summary>
    /// 病史简要
    /// </summary> 
    [StringLength(2000)]
    public string MedicalHistory { get; set; }

    /// <summary>
    /// 检查部位编码
    /// </summary> 
    [StringLength(20)]
    public string PartCode { get; set; }

    /// <summary>
    /// 检查部位名称
    /// </summary> 
    [StringLength(50)]
    public string PartName { get; set; }

    /// <summary>
    /// 目录描述名称 例如心电图申请单、超声申请单
    /// </summary> 
    [StringLength(100)]
    public string CatalogDisplayName { get; set; }

    /// <summary>
    /// 是否紧急
    /// </summary> 
    public bool IsEmergency { get; set; }

    /// <summary>
    /// 是否在床旁
    /// </summary> 
    public bool IsBedSide { get; set; }


    ///// <summary>
    ///// 出报告时间
    ///// </summary> 
    //public DateTime? ReportTime { get; set; }

    ///// <summary>
    ///// 确认报告医生编码
    ///// </summary> 
    //public string ReportDoctorCode { get; set; }

    ///// <summary>
    ///// 确认报告医生
    ///// </summary> 
    //public string ReportDoctorName { get; set; }

    ///// <summary>
    ///// 报告标识
    ///// </summary> 
    //public bool HasReport { get; set; }

}
