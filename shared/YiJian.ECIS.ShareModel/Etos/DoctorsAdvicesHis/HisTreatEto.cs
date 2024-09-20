using System.ComponentModel.DataAnnotations;

namespace YiJian.ECIS.ShareModel.Etos.DoctorsAdvices;

/// <summary>
/// 诊疗项ETO
/// </summary>
public class HisTreatEto
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
    /// 数量
    /// </summary>  
    [Required]
    public int Qty { get; set; } = 1;

    /// <summary>
    /// 其它价格
    /// </summary> 
    public decimal? OtherPrice { get; set; }

    /// <summary>
    /// 规格
    /// </summary> 
    [StringLength(200)]
    public string Specification { get; set; }

    /// <summary>
    /// 默认频次码
    /// </summary> 
    [StringLength(20)]
    public string FrequencyCode { get; set; }

    /// <summary>
    /// 收费大类代码
    /// </summary> 
    public string FeeTypeMainCode { get; set; }

    /// <summary>
    /// 收费小类代码
    /// </summary> 
    public string FeeTypeSubCode { get; set; }

}
