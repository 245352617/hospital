using Newtonsoft.Json;

namespace YiJian.ECIS.ShareModel.DDPs.Dto;

/// <summary>
/// 检验项
/// </summary>
public class Lis4HisDto : DoctorAdvices4HisDto
{
    /// <summary>
    /// 检验项目主键
    /// </summary>
    [JsonProperty("lisId")]
    public string LisId { get; set; }

    /// <summary>
    /// 检验类别编码
    /// </summary>
    [JsonProperty("catalogCode")]
    public string CatalogCode { get; set; }

    /// <summary>
    /// 检验类别
    /// </summary>
    [JsonProperty("catalogName")]
    public string CatalogName { get; set; }

    /// <summary>
    /// 标本编码
    /// </summary>
    [JsonProperty("specimenCode")]
    public string SpecimenCode { get; set; }

    /// <summary>
    /// 标本名称
    /// </summary>
    [JsonProperty("specimenName")]
    public string SpecimenName { get; set; }

    /// <summary>
    /// 标本采集部位
    /// </summary>
    [JsonProperty("specimenPartName")]
    public string SpecimenPartName { get; set; }

    /// <summary>
    /// 标本容器代码
    /// </summary>
    [JsonProperty("containerCode")]
    public string ContainerCode { get; set; }

    /// <summary>
    /// 标本容器
    /// </summary>
    [JsonProperty("containerName")]
    public string ContainerName { get; set; }

    /// <summary>
    /// 标本容器颜色:0=红帽,1=蓝帽,2=紫帽
    /// </summary>
    [JsonProperty("containerColor")]
    public string ContainerColor { get; set; }

    /// <summary>
    /// 标本说明
    /// </summary>
    [JsonProperty("specimenDescription")]
    public string SpecimenDescription { get; set; }

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
    /// 15.孕母血清胎儿唐氏综合征筛查申请单(早、中期)
    /// 14.新型冠状病毒RNA检测申请单
    /// 13.基于孕妇外周血胎儿 基于孕妇外周血胎儿13 、18 、21-三体综合征基因筛查申请单
    /// </summary>
    [JsonProperty("addCard")]
    public string AddCard { get; set; }

    /// <summary>
    /// 指引ID 关联 ExamNote表code
    /// </summary>
    [JsonProperty("guideCode")]
    public string GuideCode { get; set; }

    /// <summary>
    /// 指引名称
    /// </summary>
    [JsonProperty("guideName")]
    public string GuideName { get; set; }

    /// <summary>
    /// 检验小项集合
    /// </summary>
    [JsonProperty("lisItems")]
    public List<LisItem4HisDto> LisItems { get; set; } = new List<LisItem4HisDto>();

    /// <summary>
    /// 项目Id
    /// </summary>
    [JsonProperty("projectCode")]
    public string ProjectCode { get; set; }
}
