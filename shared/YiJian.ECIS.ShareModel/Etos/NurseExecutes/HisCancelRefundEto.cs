using System.ComponentModel.DataAnnotations;
using YiJian.ECIS.ShareModel.Enums;

namespace YiJian.ECIS.ShareModel.Etos.NurseExecutes;

/// <summary>
/// 退药退费向his提交 取消退药退费 请求
/// </summary>
public class HisCancelRefundEto
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
    /// 申请护士编码
    /// </summary>
    [StringLength(20)]
    public string NurseCode { get; set; }

    /// <summary>
    /// 申请护士名称
    /// </summary>
    [StringLength(50)]
    public string NurseName { get; set; }
}
