namespace YiJian.MasterData.Labs.Position;

/// <summary>
/// 检验标本采集部位 表字段长度常量
/// </summary>
public class LabSpecimenPositionConsts
{  
    /// <summary>
    /// 标本编码(50)
    /// </summary>
    public static int MaxSpecimenCodeLength { get; set; } = 50;
    /// <summary>
    /// 标本名称(100)
    /// </summary>
    public static int MaxSpecimenNameLength { get; set; } = 100;
    /// <summary>
    /// 采集部位编码(50)
    /// </summary>
    public static int MaxPositionCodeLength { get; set; } = 50;
    /// <summary>
    /// 采集部位名称(100)
    /// </summary>
    public static int MaxPositionNameLength { get; set; } = 100;
    /// <summary>
    /// 拼音码(50)
    /// </summary>
    public static int MaxPyCodeLength { get; set; } = 50;
}