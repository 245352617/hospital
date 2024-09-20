using Newtonsoft.Json;

namespace YiJian.ECIS.ShareModel.DDPs.Dto;

/// <summary>
/// 诊疗项
/// </summary>
public class Treat4HisDto : DoctorAdvices4HisDto
{
    /// <summary>
    /// 加收标志	
    /// </summary>
    [JsonProperty("additional")]
    public bool Additional { get; set; }

    /// <summary>
    /// 其它价格 (诊疗项用作儿童加收费用)
    /// </summary>
    [JsonProperty("otherPrice")]
    public decimal? OtherPrice { get; set; }

    /// <summary>
    /// 默认频次代码
    /// </summary>
    [JsonProperty("frequencyCode")]
    public string FrequencyCode { get; set; }

    /// <summary>
    /// 频次
    /// </summary>
    [JsonProperty("frequencyName")]
    public string FrequencyName { get; set; }

    /// <summary>
    /// 包装规格
    /// </summary>
    [JsonProperty("specification")]
    public string Specification { get; set; }

    /// <summary>
    /// 用法编码
    /// </summary>
    [JsonProperty("usageCode")]
    public string UsageCode { get; set; }

    /// <summary>
    /// 用法名称
    /// </summary>
    [JsonProperty("usageName")]
    public string UsageName { get; set; }

    /// <summary>
    /// 开药天数
    /// </summary>
    [JsonProperty("longDays")]
    public int LongDays { get; set; } = 1;

    /// <summary>
    /// 项目类型
    /// </summary>
    [JsonProperty("projectType")]
    public string ProjectType { get; set; }

    /// <summary>
    /// 项目类型名称
    /// </summary>
    [JsonProperty("projectName")]
    public string ProjectName { get; set; }

    /// <summary>
    /// 项目归类
    /// </summary>
    [JsonProperty("projectMerge")]
    public string ProjectMerge { get; set; }

    /// <summary>
    /// 诊疗项Id
    /// </summary>
    [JsonProperty("treatId")]
    public int TreatId { get; set; }

    /// <summary>
    /// 附加类型
    /// </summary>
    [JsonProperty("additionalItemsType")]
    public int AdditionalItemsType { get; set; }

    /// <summary>
    /// 厂商的主键值（北大）
    /// </summary>
    [JsonProperty("manufacturer")]
    public string Manufacturer { get; set; }

    /// <summary>
    /// 数量
    /// </summary>
    [JsonProperty("qty")]
    public decimal Qty { get; set; }
}