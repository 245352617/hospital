using Newtonsoft.Json;

namespace YiJian.ECIS.ShareModel.DDPs.Requests;

/// <summary>
/// 作废医嘱
/// </summary>
public class PKUObsAdviceRequest
{
    /// <summary>
    /// his识别号
    /// </summary>
    [JsonProperty("hisNumber")]
    public string HisNumber { get; set; } = string.Empty;

    /// <summary>
    /// 类型 医技 YJ  处方 CF
    /// </summary>
    [JsonProperty("type")]
    public string Type { get; set; } = string.Empty;

    /// <summary>
    /// 作废来源
    /// </summary>
    [JsonProperty("obsOrgin")]
    public string ObsOrgin { get; set; } = "尚哲";

    /// <summary>
    /// 来源主键
    /// </summary>
    [JsonProperty("manufacturer")]
    public string Manufacturer { get; set; } = string.Empty;
}

