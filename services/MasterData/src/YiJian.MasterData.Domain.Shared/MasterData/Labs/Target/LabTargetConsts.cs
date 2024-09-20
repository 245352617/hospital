namespace YiJian.MasterData.Labs;

/// <summary>
/// 检验明细项 表字段长度常量
/// </summary>
public class LabTargetConsts
{  
    /// <summary>
    /// 编码(50)
    /// </summary>
    public static int MaxTargetCodeLength { get; set; } = 50;
    /// <summary>
    /// 名称(200)
    /// </summary>
    public static int MaxTargetNameLength { get; set; } = 200;
    /// <summary>
    /// 项目编码(50)
    /// </summary>
    public static int MaxProjectCodeLength { get; set; } = 50;
    /// <summary>
    /// 拼音码(50)
    /// </summary>
    public static int MaxPyCodeLength { get; set; } = 50;
    /// <summary>
    /// 五笔(20)
    /// </summary>
    public static int MaxWbCodeLength { get; set; } = 50;
    /// <summary>
    /// 单位(20)
    /// </summary>
    public static int MaxTargetUnitLength { get; set; } = 20;
}