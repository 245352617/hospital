namespace YiJian.ECIS.ShareModel.DDPs;

/// <summary>
/// ddp数组返回
/// </summary>
public class DdpDataResponse<T>
{
    /// <summary>
    /// 返回的集合
    /// </summary>
    public T? DicDatas { get; set; }

    /// <summary>
    /// 类型
    /// </summary>
    public int DicType { get; set; }

}
