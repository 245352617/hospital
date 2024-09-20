using Newtonsoft.Json;

namespace YiJian.ECIS.ShareModel.DDPs.Requests;

/// <summary>
/// 药品库存信息请求参数 (0:查询所有药品 )
/// </summary>
public class PKUDrugStockQueryAllRequest
{
    /// <summary>
    /// 药品库存信息请求参数
    /// </summary>
    public PKUDrugStockQueryAllRequest()
    {

    }

    /// <summary>
    /// 药品库存信息请求参数
    /// </summary>
    public PKUDrugStockQueryAllRequest(int storage)
    {
        Storage = storage;
    }

    /// <summary>
    /// 药房编号 药房唯一编号2.1药房编码码字典（字典、写死）0.查询所有药房 
    /// </summary>
    [JsonProperty("Storage")]
    public int Storage { get; set; }
}

