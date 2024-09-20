using Newtonsoft.Json;

namespace HisApiMockService.Models;

/// <summary>
/// 请求库存药品清单请求参数
/// </summary>
public class PKUHisMedicineRequest
{
    /// <summary>
    /// 根据药品的厂商，编码，规格查询
    /// </summary>
    public DdpHisMedicineByPropRequest Param1 { get; set; }

    /// <summary>
    /// 根据库存的Id集合查询
    /// </summary>
    public DdpHisMedicineByInvIdsRequest Param2 { get; set; }

    /// <summary>
    /// 根据药品的拼音或中文名称等查询
    /// </summary>
    public DdpHisMedicineSearchRequest Param3 { get; set; }
}

/// <summary>
/// 根据药品的厂商，编码，规格查询
/// </summary>
public class DdpHisMedicineByPropRequest
{
    /// <summary>
    /// 药品厂商
    /// </summary>
    [JsonProperty("factoryCode")]
    public string FactoryCode { get; set; }

    /// <summary>
    /// 药品编码
    /// </summary>
    [JsonProperty("medicineCode")]
    public string MedicineCode { get; set; }

    /// <summary>
    /// 药品规格
    /// </summary>
    [JsonProperty("specification")]
    public string Specification { get; set; }
}

/// <summary>
/// 根据库存的Id集合查询
/// </summary>
public class DdpHisMedicineByInvIdsRequest
{
    /// <summary>
    /// 库存的Id
    /// </summary>
    [JsonProperty("invId")]
    public List<string> InvId { get; set; } = new List<string>();
}

/// <summary>
/// 根据药品的拼音或中文名称等查询
/// </summary>
public class DdpHisMedicineSearchRequest
{
    /// <summary>
    /// 每页大小，默认50
    /// </summary>
    [JsonProperty("size")]
    public int Size { get; set; } = 50;

    /// <summary>
    /// 页码索引，默认第一页
    /// </summary>
    [JsonProperty("index")]
    public int Index { get; set; } = 1;

    /// <summary>
    /// 输入拼音或中文检索
    /// </summary>
    [JsonProperty("nameOrPyCode")]
    public string NameOrPyCode { get; set; }

    /// <summary>
    /// 药房代码
    /// </summary>
    [JsonProperty("pharmacyCode")]
    public string PharmacyCode { get; set; }

    /// <summary>
    /// 1.急诊 0.普通
    /// </summary>
    [JsonProperty("emergencySign")]
    public int EmergencySign { get; set; }

}

