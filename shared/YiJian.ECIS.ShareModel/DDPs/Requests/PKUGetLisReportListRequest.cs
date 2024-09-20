using Newtonsoft.Json;

namespace YiJian.ECIS.ShareModel.DDPs.Requests;

/// <summary>
/// 检验报告列表查询
/// </summary>
public class PKUGetLisReportListRequest
{
    /// <summary>
    /// 患者ID
    /// </summary>
    [JsonProperty("patientId")]
    public string PatientId { get; set; }

    /// <summary>
    /// 患者姓名
    /// </summary>
    [JsonProperty("patientName")]
    public string PatientName { get; set; }

    /// <summary>
    /// 就诊流水号 就诊唯一号
    /// </summary>
    [JsonProperty("visitSerialNo")]
    public string VisitSerialNo { get; set; }
}
