namespace YiJian.ECIS.ShareModel.Etos.DoctorsAdvices;

/// <summary>
/// HIS回来的状态
/// </summary>
public class DoctorsAdviceHisEto
{
    /// <summary>
    /// 系统标识：0=急诊、1=院前急救
    /// </summary>
    public int PlatformType { get; set; }

    /// <summary>
    /// 病人ID
    /// </summary>
    public Guid PIID { get; set; }

    /// <summary>
    /// HIS 回来的状态
    /// </summary>
    public List<DoctorsAdviceHisStatusEto> DoctorsAdviceHisStatusList { get; set; } = new List<DoctorsAdviceHisStatusEto>();

}
