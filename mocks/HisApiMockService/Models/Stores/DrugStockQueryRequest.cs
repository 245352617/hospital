namespace HisApiMockService.Models.Stores;

/// <summary>
/// 药品库存信息查询请求参数
/// </summary>
public class DrugStockQueryRequest
{
    /// <summary>
    /// 查询类别
    /// <![CDATA[
    /// 0:查询所有药品 
    /// 1:药品名称(支持模糊检索)
    /// 2:药品编码 
    /// ]]>
    /// </summary>
    public int QueryType { get; set; }

    /// <summary>
    /// 查询值
    /// <![CDATA[
    /// queryType非0时需要结合storage使用
    /// 1:药品名称
    /// 2:药品编码
    /// ]]>
    /// </summary>
    public string QueryCode { get; set; }

    /// <summary>
    /// 药房编号 药房唯一编号2.1药房编码码字典（字典、写死）0.查询所有药房 
    /// </summary>
    public int Storage { get; set; }

}

