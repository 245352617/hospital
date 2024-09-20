namespace YiJian.MasterData;

public class OperationDto
{
    /// <summary>
    /// 手术编码
    /// </summary>
    public string OperationCode { get; set; }
    /// <summary>
    /// 手术名称
    /// </summary>
    public string OperationName { get; set; }

    /// <summary>
    /// 拼音编码
    /// </summary>
    public string PyCode { get; set; }
    /// <summary>
    /// 时长
    /// </summary>
    public int Duration { get; set; }
     
    /// <summary>
    /// 价格
    /// </summary>
    public decimal Price { get; set; }
}