namespace YiJian.MasterData.Labs;

/// <summary>
/// 检验目录 表字段长度常量
/// </summary>
public class LabCatalogConsts
{  
    /// <summary>
    /// 分类编码(20)
    /// </summary>
    public static int MaxCatalogCodeLength { get; set; } = 20;
    /// <summary>
    /// 分类编码(200)
    /// </summary>
    public static int MaxCatalogNameLength { get; set; } = 200;
    /// <summary>
    /// 执行科室编码(20)
    /// </summary>
    public static int MaxExecDeptCodeLength { get; set; } = 20;
    /// <summary>
    /// 执行科室名称(50)
    /// </summary>
    public static int MaxExecDeptNameLength { get; set; } = 50;
    /// <summary>
    /// 拼音码(20)
    /// </summary>
    public static int MaxPyCodeLength { get; set; } = 20;
    /// <summary>
    /// 五笔(20)
    /// </summary>
    public static int MaxWbCodeLength { get; set; } = 20;
    /// <summary>
    /// 位置编码(20)
    /// </summary>
    public static int MaxPositionCodeLength { get; set; } = 20;
    /// <summary>
    /// 位置描述(100)
    /// </summary>
    public static int MaxPositionNameLength { get; set; } = 100;
}