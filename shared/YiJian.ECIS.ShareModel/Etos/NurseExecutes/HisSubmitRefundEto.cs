using System.ComponentModel.DataAnnotations;
using YiJian.ECIS.ShareModel.Enums;

namespace YiJian.ECIS.ShareModel.Etos.NurseExecutes;

/// <summary>
/// 退药退费向his提交 申请退药退费 请求
/// </summary>
public class HisSubmitRefundEto
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
    public EPlatformType PlatformType { get; set; }

    /// <summary>
    /// 退药还是退费，0：退药，1：退费
    /// </summary>
    [Required]
    public int RefundType { get; set; }

    /// <summary>
    /// 申请时间
    /// </summary>
    [Required]
    public DateTime RequestTime { get; set; }

    /// <summary>
    /// 规格
    /// </summary>
    [StringLength(200)]
    public string Specification { get; set; }

    /// <summary>
    /// 数量
    /// </summary>
    public int RefundQty { get; set; }

    /// <summary>
    /// 申请护士编码
    /// </summary>
    [StringLength(20)]
    public string NurseCode { get; set; }

    /// <summary>
    /// 申请护士名称
    /// </summary>
    [StringLength(50)]
    public string NurseName { get; set; }

    /// <summary>
    /// 原因
    /// </summary>
    [StringLength(500)]
    public string Reason { get; set; }
}
