namespace YiJian.MasterData.Sequences;

/// <summary>
/// 序列 表字段长度常量
/// </summary>
public class SequenceConsts
{  
    /// <summary>
    /// 编码(20)
    /// </summary>
    public static int MaxCodeLength { get; set; } = 20;
    /// <summary>
    /// 名称(50)
    /// </summary>
    public static int MaxNameLength { get; set; } = 50;
    /// <summary>
    /// 格式(20)
    /// </summary>
    public static int MaxFormatLength { get; set; } = 20;
    /// <summary>
    /// 备注(200)
    /// </summary>
    public static int MaxMemoLength { get; set; } = 200;
}