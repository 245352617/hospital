namespace YiJian.MasterData.Labs;

/// <summary>
/// 检验标本 表字段长度常量
/// </summary>
public class LabSpecimenConsts
{  
    /// <summary>
    /// 标本编码(50)
    /// </summary>
    public static int MaxSpecimenCodeLength { get; set; } = 50;
    /// <summary>
    /// 标本名称(200)
    /// </summary>
    public static int MaxSpecimenNameLength { get; set; } = 200;
    /// <summary>
    /// 拼音码(50)
    /// </summary>
    public static int MaxPyCodeLength { get; set; } = 50;
    /// <summary>
    /// 五笔(20)
    /// </summary>
    public static int MaxWbCodeLength { get; set; } = 20;
}