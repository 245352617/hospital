using Newtonsoft.Json;

namespace YiJian.ECIS.ShareModel.DDPs.Requests;

/// <summary>
/// 检验报告详情查询
/// </summary>
public class PKUGetLisReportRequest
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

    /// <summary>
    /// 报告单号
    /// </summary>
    [JsonProperty("reportNo")]
    public string ReportNo { get; set; }
}
