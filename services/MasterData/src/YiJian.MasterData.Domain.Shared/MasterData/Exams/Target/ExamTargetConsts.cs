namespace YiJian.MasterData.Exams;

/// <summary>
/// 检查明细项 表字段长度常量
/// </summary>
public class ExamTargetConsts
{  
    /// <summary>
    /// 编码(20)
    /// </summary>
    public static int MaxTargetCodeLength { get; set; } = 20;
    /// <summary>
    /// 名称(200)
    /// </summary>
    public static int MaxTargetNameLength { get; set; } = 200;
    /// <summary>
    /// 项目编码(20)
    /// </summary>
    public static int MaxProjectCodeLength { get; set; } = 20;
    /// <summary>
    /// 单位(20)
    /// </summary>
    public static int MaxTargetUnitLength { get; set; } = 20;
    /// <summary>
    /// 规格(50)
    /// </summary>
    public static int MaxSpecificationLength { get; set; } = 50;
    /// <summary>
    /// 拼音码(50)
    /// </summary>
    public static int MaxPyCodeLength { get; set; } = 50;
    /// <summary>
    /// 五笔(20)
    /// </summary>
    public static int MaxWbCodeLength { get; set; } = 20;
    /// <summary>
    /// 特殊标识(50)
    /// </summary>
    public static int MaxSpecialFlagLength { get; set; } = 50;
}