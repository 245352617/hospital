using Newtonsoft.Json;

namespace YiJian.ECIS.ShareModel.DDPs.Requests;

/// <summary>
/// 查询检查报告信息
/// </summary>
public class PKUQueryPacsReportRequest
{
    /// <summary>
    /// 患者ID
    /// </summary>
    [JsonProperty("patientId")]
    public string PatientId { get; set; }

    /// <summary>
    /// 就诊流水号 就诊唯一号
    /// </summary>
    [JsonProperty("visitSerialNo")]
    public string VisitSerialNo { get; set; }

    /// <summary>
    /// 申请单号
    /// </summary>
    [JsonProperty("applyNo")]
    public string ApplyNo { get; set; }
}
