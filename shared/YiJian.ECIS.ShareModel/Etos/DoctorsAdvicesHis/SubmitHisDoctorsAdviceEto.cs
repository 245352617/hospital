using System.ComponentModel.DataAnnotations;

namespace YiJian.ECIS.ShareModel.Etos.DoctorsAdvices;

/// <summary>
/// 提交医嘱的ETO
/// </summary>
public class SubmitHisDoctorsAdviceEto
{
    /// <summary>
    /// 医嘱
    /// </summary>
    /// <remarks>一个医嘱可能是一个开方，可能是一个检查，可能是一个检验，可能是一个诊疗项,可以通过ItemType字段区别开</remarks>
    [Required]
    public HisDoctorsAdviceEto DoctorsAdvice { get; set; }

    /// <summary>
    /// 药方ETO
    /// </summary>
    public HisPrescribeEto Prescribe { get; set; }

    /// <summary>
    /// 检验项ETO
    /// </summary>
    public HisLisEto Lis { get; set; }

    /// <summary>
    /// 检查项ETO
    /// </summary>
    public HisPacsEto Pacs { get; set; }

    /// <summary>
    /// 诊疗项ETO
    /// </summary>
    public HisTreatEto Treat { get; set; }

}
