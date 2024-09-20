using System.ComponentModel.DataAnnotations;

namespace YiJian.ECIS.ShareModel.Etos.NurseExecutes;

/// <summary>
/// 退药退费来自his的审批结果，提交、取消的结果，通过或者驳回
/// </summary>
public class HisApproveRefundEto
{
    /// <summary>
    /// 医嘱号
    /// </summary>
    [Required, StringLength(20)]
    public string RecipeNo { get; set; }

    /// <summary>
    /// 病人标识
    /// </summary>
    public Guid? PIID { get; set; }

    /// <summary>
    /// 系统标识: 0=急诊，1=院前
    /// </summary>
    public int? PlatformType { get; set; }

    /// <summary>
    /// 退药还是退费，0：退药，1：退费
    /// </summary>
    [Required]
    public int RefundType { get; set; }

    /// <summary>
    /// 审批请求，0：提交，1：取消
    /// </summary>
    [Required]
    public int FromRequest { get; set; }

    /// <summary>
    /// 审批结果，0：通过，1：驳回
    /// </summary>
    [Required]
    public int ApproveResult { get; set; }

    /// <summary>
    /// 审批备注
    /// </summary>
    [StringLength(500)]
    public string ApproveComment { get; set; }

    ///// <summary>
    ///// 审批时间
    ///// </summary>
    //[Required]
    //public DateTime ApproveTime { get; set; }

    /// <summary>
    /// 审批人编码
    /// </summary>
    [Required, StringLength(20)]
    public string ApproverCode { get; set; }

    /// <summary>
    /// 审批人名称
    /// </summary>
    [Required, StringLength(50)]
    public string ApproverName { get; set; }

}
