using System.ComponentModel.DataAnnotations;

namespace YiJian.ECIS.ShareModel.Etos.DoctorsAdvices;

/// <summary>
/// 提交医嘱的ETO
/// </summary>
public class SubmitDoctorsAdviceEto
{
    /// <summary>
    /// 医嘱
    /// </summary>
    /// <remarks>一个医嘱可能是一个开方，可能是一个检查，可能是一个检验，可能是一个诊疗项,可以通过ItemType字段区别开</remarks>
    [Required]
    public DoctorsAdviceEto DoctorsAdvice { get; set; }

    /// <summary>
    /// 药方ETO
    /// </summary>
    public PrescribeEto? Prescribe { get; set; }

    /// <summary>
    /// 检验项ETO
    /// </summary>
    public LisEto? Lis { get; set; }

    /// <summary>
    /// 检查项ETO
    /// </summary>
    public PacsEto? Pacs { get; set; }

    /// <summary>
    /// 诊疗项ETO
    /// </summary>
    public TreatEto? Treat { get; set; }

}
