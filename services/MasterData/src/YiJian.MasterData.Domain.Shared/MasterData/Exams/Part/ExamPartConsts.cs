namespace YiJian.MasterData;

/// <summary>
/// 检查部位 表字段长度常量
/// </summary>
public class ExamPartConsts
{  
    /// <summary>
    /// 检查部位编码(20)
    /// </summary>
    public static int MaxPartCodeLength { get; set; } = 50;
    /// <summary>
    /// 检查部位名称(200)
    /// </summary>
    public static int MaxPartNameLength { get; set; } = 200;
    /// <summary>
    /// 拼音码(20)
    /// </summary>
    public static int MaxPyCodeLength { get; set; } = 20;
}