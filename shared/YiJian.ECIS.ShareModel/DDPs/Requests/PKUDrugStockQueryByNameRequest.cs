using Newtonsoft.Json;

namespace YiJian.ECIS.ShareModel.DDPs.Requests;

/// <summary>
/// 药品库存信息请求参数(1:药品名称(支持模糊检索))
/// </summary>
public class PKUDrugStockQueryByNameRequest
{
    /// <summary>
    /// 药品库存信息请求参数
    /// </summary>
    public PKUDrugStockQueryByNameRequest()
    {

    }

    /// <summary>
    /// 药品库存信息请求参数
    /// </summary>
    public PKUDrugStockQueryByNameRequest(string queryName, int storage)
    {
        QueryName = queryName;
        Storage = storage;
    }

    /// <summary>
    /// 查询值 1:药品名称  
    /// </summary>
    [JsonProperty("queryName")]
    public string QueryName { get; set; }

    /// <summary>
    /// 药房编号 药房唯一编号2.1药房编码码字典（字典、写死）0.查询所有药房 
    /// </summary>
    [JsonProperty("Storage")]
    public int Storage { get; set; }
}

