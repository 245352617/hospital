using Newtonsoft.Json;

namespace YiJian.ECIS.ShareModel.DDPs.Dto;

/// <summary>
/// 检查小项
/// </summary>
public class PacsItem4HisDto
{
    /// <summary>
    /// 小项编码
    /// </summary>
    [JsonProperty("targetCode")]
    public string TargetCode { get; set; }

    /// <summary>
    /// 小项名称
    /// </summary>
    [JsonProperty("targetName")]
    public string TargetName { get; set; }

    /// <summary>
    /// 单价
    /// </summary>
    [JsonProperty("price")]
    public decimal Price { get; set; }

    /// <summary>
    /// 数量
    /// </summary>
    [JsonProperty("qty")]
    public decimal Qty { get; set; }

    /// <summary>
    /// 单位
    /// </summary>
    [JsonProperty("targetUnit")]
    public string TargetUnit { get; set; }

    /// <summary>
    /// 医保目录编码
    /// </summary>
    [JsonProperty("insuranceCode")]
    public string InsuranceCode { get; set; }

    /// <summary>
    /// 医保目录:0=自费,1=甲类,2=乙类,3=其它
    /// </summary>
    [JsonProperty("insuranceType")]
    public int InsuranceType { get; set; }

    /// <summary>
    /// 项目编码
    /// </summary>
    [JsonProperty("projectCode")]
    public string ProjectCode { get; set; }

    /// <summary>
    /// 其它价格
    /// </summary>
    [JsonProperty("otherPrice")]
    public decimal OtherPrice { get; set; }

    /// <summary>
    /// 规格
    /// </summary>
    [JsonProperty("specification")]
    public string Specification { get; set; }

    /// <summary>
    /// 排序号
    /// </summary>
    [JsonProperty("sort")]
    public int Sort { get; set; }

    /// <summary>
    /// 拼音码
    /// </summary>
    [JsonProperty("pyCode")]
    public string PyCode { get; set; }

    /// <summary>
    /// 五笔编码
    /// </summary>
    [JsonProperty("wbCode")]
    public string WbCode { get; set; }

    /// <summary>
    /// 特殊标识
    /// </summary>
    [JsonProperty("specialFlag")]
    public string SpecialFlag { get; set; }

    /// <summary>
    /// 是否启用
    /// </summary>
    [JsonProperty("isActive")]
    public bool IsActive { get; set; }

    /// <summary>
    /// 项目类型
    /// </summary>
    [JsonProperty("projectType")]
    public string ProjectType { get; set; }

    /// <summary>
    /// 项目归类
    /// </summary>
    [JsonProperty("projectMerge")]
    public string ProjectMerge { get; set; }
}
