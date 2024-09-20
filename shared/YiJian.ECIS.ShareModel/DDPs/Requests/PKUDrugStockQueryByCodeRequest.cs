using Newtonsoft.Json;

namespace YiJian.ECIS.ShareModel.DDPs.Requests;

/// <summary>
/// 药品库存信息请求参数(2:药品编码 )
/// </summary>
public class PKUDrugStockQueryByCodeRequest
{
    /// <summary>
    /// 药品库存信息请求参数
    /// </summary>
    public PKUDrugStockQueryByCodeRequest()
    {

    }

    /// <summary>
    /// 药品库存信息请求参数
    /// </summary>
    public PKUDrugStockQueryByCodeRequest(string queryCode, int storage)
    {
        QueryCode = queryCode;
        Storage = storage;
    }

    /// <summary>
    /// 查询值 2:药品编码 
    /// </summary>
    [JsonProperty("querycode")]
    public string QueryCode { get; set; }

    /// <summary>
    /// 药房编号 药房唯一编号2.1药房编码码字典（字典、写死）0.查询所有药房 
    /// </summary>
    [JsonProperty("Storage")]
    public int Storage { get; set; }
}

