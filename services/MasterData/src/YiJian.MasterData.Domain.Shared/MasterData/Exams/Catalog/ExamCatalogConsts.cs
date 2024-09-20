namespace YiJian.MasterData;

/// <summary>
/// 检查目录 表字段长度常量
/// </summary>
public class ExamCatalogConsts
{  
    /// <summary>
    /// 编码(20)
    /// </summary>
    public static int MaxCatalogCodeLength { get; set; } = 20;
    /// <summary>
    /// 名称(50)
    /// </summary>
    public static int MaxCatalogNameLength { get; set; } = 50;
    /// <summary>
    /// 显示名称(申请单) eg: CT检查申请单、MRI检查申请单、X线检查申请单(50)
    /// </summary>
    public static int MaxDisplayNameLength { get; set; } = 50;
    /// <summary>
    /// 科室编码(20)
    /// </summary>
    public static int MaxDeptCodeLength { get; set; } = 20;
    /// <summary>
    /// 科室名称(50)
    /// </summary>
    public static int MaxDeptNameLength { get; set; } = 50;
    /// <summary>
    /// 检查部位(50)
    /// </summary>
    public static int MaxExamPartCodeLength { get; set; } = 50;
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
    /// 位置(100)
    /// </summary>
    public static int MaxPositionNameLength { get; set; } = 100;
    /// <summary>
    /// 执行机房编码(20)
    /// </summary>
    public static int MaxRoomCodeLength { get; set; } = 20;
    /// <summary>
    /// 执行机房(50)
    /// </summary>
    public static int MaxRoomNameLength { get; set; } = 50;
}