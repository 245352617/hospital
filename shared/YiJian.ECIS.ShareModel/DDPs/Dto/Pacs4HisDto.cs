using Newtonsoft.Json;

namespace YiJian.ECIS.ShareModel.DDPs.Dto;

/// <summary>
/// 检查项
/// </summary>
public class Pacs4HisDto : DoctorAdvices4HisDto
{
    /// <summary>
    /// 检查目录编码
    /// </summary>
    [JsonProperty("catalogCode")]
    public string CatalogCode { get; set; }

    /// <summary>
    /// 检查目录名称
    /// </summary>
    [JsonProperty("catalogName")]
    public string CatalogName { get; set; }

    /// <summary>
    /// 一级检查目录编码
    /// </summary>
    [JsonProperty("firstCatalogCode")]
    public string FirstCatalogCode { get; set; }

    /// <summary>
    /// 一级检查目录名称
    /// </summary>
    [JsonProperty("firstCatalogName")]
    public string FirstCatalogName { get; set; }

    /// <summary>
    /// 检查部位编码
    /// </summary>
    [JsonProperty("fartCode")]
    public string PartCode { get; set; }

    /// <summary>
    /// 检查部位名称
    /// </summary>
    [JsonProperty("partName")]
    public string PartName { get; set; }

    /// <summary>
    /// 目录描述名称 例如心电图申请单、超声申请单
    /// </summary>
    [JsonProperty("catalogDisplayName")]
    public string CatalogDisplayName { get; set; }

    /// <summary>
    /// 是否紧急
    /// </summary>
    [JsonProperty("isEmergency")]
    public bool IsEmergency { get; set; }

    /// <summary>
    /// 是否在床旁
    /// </summary>
    [JsonProperty("isBedSide")]
    public bool IsBedSide { get; set; }

    /// <summary>
    /// 附加卡片类型  
    /// 12.TCT细胞学检查申请单 
    /// 11.病理检验申请单 
    /// 16.门诊大型设备检查治疗项目审核、报告单（需两联）、需配合医保患者使用 
    /// </summary>
    [JsonProperty("addCard")]
    public string AddCard { get; set; }

    /// <summary>
    /// 指引ID 关联 ExamNote表code
    /// </summary>
    [JsonProperty("guideCode")]
    public string GuideCode { get; set; }

    /// <summary>
    /// 指引名称 关联 ExamNote表code
    /// </summary>
    [JsonProperty("guideName")]
    public string GuideName { get; set; }

    /// <summary>
    /// 检查单单名标题
    /// </summary>
    [JsonProperty("examTitle")]
    public string ExamTitle { get; set; }

    /// <summary>
    /// 检查小项集合
    /// </summary>
    [JsonProperty("pacsItems")]
    public virtual List<PacsItem4HisDto> PacsItems { get; set; } = new List<PacsItem4HisDto>();

    /// <summary>
    /// 项目code
    /// </summary>
    [JsonProperty("projectCode")]
    public string ProjectCode { get; set; }

    /// <summary>
    /// 厂商的主键值（北大）
    /// </summary>
    [JsonProperty("manufacturer")]
    public string Manufacturer { get; set; }
}
