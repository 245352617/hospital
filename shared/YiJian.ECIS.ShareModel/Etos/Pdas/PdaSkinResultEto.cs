namespace YiJian.ECIS.ShareModel.Etos.Pdas;

/// <summary>
/// 描述：pda皮试结果同步eto
/// 创建人： yangkai
/// 创建时间：2022/12/1 14:38:53
/// </summary>
public class PdaSkinResultEto
{
    /// <summary>
    /// 患者id
    /// </summary>
    public string PatientId { get; set; }

    /// <summary>
    /// 患者住院流水号
    /// </summary>
    public string PatientNo { get; set; }

    /// <summary>
    /// 医嘱ID
    /// </summary>
    public string OrderId { get; set; }

    /// <summary>
    /// 医嘱组号
    /// </summary>
    public string ParentGroupNumber { get; set; }

    /// <summary>
    /// 皮试结果: 1:阴性 2:阳性 3:超阳性
    /// </summary>
    public string SkinTestResultCode { get; set; }

    /// <summary>
    /// 皮试时间
    /// </summary>
    public DateTime SkinTestResultDate { get; set; }

    /// <summary>
    /// 护士编码
    /// </summary>
    public string SkinTestNurseCode { get; set; }

    /// <summary>
    /// 护士名称
    /// </summary>
    public string SkinTestNurseName { get; set; }
}
