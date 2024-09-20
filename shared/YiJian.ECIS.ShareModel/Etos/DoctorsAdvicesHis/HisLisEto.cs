using System.ComponentModel.DataAnnotations;

namespace YiJian.ECIS.ShareModel.Etos.DoctorsAdvices;

/// <summary>
/// 检验项ETO
/// </summary>
public class HisLisEto
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
    /// 检验类别编码
    /// </summary> 
    [Required, StringLength(20)]
    public string CatalogCode { get; set; }

    /// <summary>
    /// 检验类别
    /// </summary> 
    [Required, StringLength(100)]
    public string CatalogName { get; set; }

    /// <summary>
    /// 临床症状
    /// </summary> 
    [Required, StringLength(2000)]
    public string ClinicalSymptom { get; set; }

    /// <summary>
    /// 是否紧急
    /// </summary> 
    public bool IsEmergency { get; set; }

    /// <summary>
    /// 是否在床旁
    /// </summary> 
    public bool IsBedSide { get; set; }



    ///// <summary>
    ///// 标本编码
    ///// </summary> 
    //public string SpecimenCode { get; set; }

    ///// <summary>
    ///// 标本名称
    ///// </summary> 
    //public string SpecimenName { get; set; }

    ///// <summary>
    ///// 标本采集部位编码
    ///// </summary> 
    //public string SpecimenPartCode { get; set; }

    ///// <summary>
    ///// 标本采集部位
    ///// </summary> 
    //public string SpecimenPartName { get; set; }

    ///// <summary>
    ///// 标本容器代码
    ///// </summary> 
    //public string ContainerCode { get; set; }

    ///// <summary>
    ///// 标本容器
    ///// </summary> 
    //public string ContainerName { get; set; }

    ///// <summary>
    ///// 标本容器颜色:0=红帽,1=蓝帽,2=紫帽
    ///// </summary> 
    //public string ContainerColor { get; set; }

    ///// <summary>
    ///// 标本说明
    ///// </summary> 
    //public string SpecimenDescription { get; set; }

    ///// <summary>
    ///// 标本采集时间
    ///// </summary> 
    //public DateTime? SpecimenCollectDatetime { get; set; }

    ///// <summary>
    ///// 标本接收时间
    ///// </summary> 
    //public DateTime? SpecimenReceivedDatetime { get; set; }

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
    //public bool HasReportName { get; set; }


}
