using Newtonsoft.Json;

namespace YiJian.ECIS.ShareModel.DDPs.Requests;

/// <summary>
/// 查询检查报告列表
/// </summary>
public class PKUQueryPacsReportListRequest
{
    /// <summary>
    /// 患者ID
    /// </summary>
    [JsonProperty("patientId")]
    public string PatientId { get; set; } = string.Empty;

    /// <summary>
    /// 就诊流水号 就诊唯一号
    /// </summary>
    [JsonProperty("visitSerialNo")]
    public string VisitSerialNo { get; set; } = string.Empty;

}
