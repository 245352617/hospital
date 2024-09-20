using YiJian.ECIS.ShareModel.Enums;

namespace YiJian.ECIS.ShareModel.IMServiceEtos.Call;

/// <summary>
/// 同步流转消息
/// </summary>
public class SyncTransferEto
{
    /// <summary>
    /// Triage_PatientInfo表ID
    /// </summary>
    public string RegisterNo { get; set; }

    /// <summary>
    /// 流转类型编码
    /// </summary>
    public TransferType TransferTypeCode { get; set; }

    /// <summary>
    /// 就诊区域编码
    /// </summary>
    public string AreaCode { get; set; }

    /// <summary>
    /// 就诊区域名称
    /// </summary>
    public string AreaName { get; set; }

    /// <summary>
    /// 流转科室代码
    /// </summary>
    public string ToDeptCode { get; set; }

    /// <summary>
    /// 流转科室名称
    /// </summary>
    public string ToDeptName { get; set; }
}
