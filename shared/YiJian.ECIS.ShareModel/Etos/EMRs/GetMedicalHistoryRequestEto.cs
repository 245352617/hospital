namespace YiJian.ECIS.ShareModel.Etos.EMRs;

/// <summary>
/// 获取
/// </summary>
public class GetMedicalHistoryRequestEto
{
    /// <summary>
    /// 患者电子病历Id
    /// </summary>
    public Guid PatientEmrId { get; set; }

    /// <summary>
    /// 流水号
    /// </summary>  
    public string RegisterSerialNo { get; set; }

}
