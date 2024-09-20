namespace YiJian.MasterData;

/// <summary>
/// 查询药品库存入参
/// </summary>
public class StockInput
{
    /// <summary>
    /// 机构编码:医院没有分院则返回空字符串；医院存在分院时不允许为空
    /// </summary>
    public string OrgCode { get; set; }

    /// <summary>
    /// 药品编号
    /// </summary>
    public string Drugcode { get; set; }

    /// <summary>
    /// 药房编号 药房唯一编号2.1药房编码码字典（字典、写死）
    /// </summary>
    public string Storage { get; set; } = "2.1";

    /// <summary>
    /// 开处方所需要的药品数量
    /// </summary>
    public string QuantityIn { get; set; }
}