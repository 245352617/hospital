using System.ComponentModel.DataAnnotations;

namespace YiJian.ECIS.ShareModel.Etos.DoctorsAdvices;

/// <summary>
/// 诊疗项ETO
/// </summary>
public class TreatEto
{
    /// <summary>
    /// Id
    /// </summary>
    [Required]
    public Guid Id { get; set; }

    /// <summary>
    /// 医嘱Id
    /// </summary>  
    [Required]
    public Guid DoctorsAdviceId { get; set; }

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

    /// <summary>
    /// 项目归类
    /// </summary> 
    public string ProjectMerge { get; set; }

    /// <summary>
    /// 诊疗项Id
    /// </summary> 
    public int TreatId { get; set; }

    /// <summary>
    /// 项目类型
    /// </summary>
    public string ProjectType { get; set; }

    /// <summary>
    /// 项目类型名称
    /// </summary>
    public string ProjectName { get; set; }

    /// <summary>
    /// 附加类型
    /// </summary>
    public int AdditionalItemsType { get; set; }

    /// <summary>
    /// 处置关联处方医嘱ID
    /// </summary>
    public Guid? AdditionalItemsId { get; set; }
}
