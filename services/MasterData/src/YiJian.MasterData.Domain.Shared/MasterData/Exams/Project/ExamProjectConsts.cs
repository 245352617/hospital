namespace YiJian.MasterData.Exams;

/// <summary>
/// 检查申请项目 表字段长度常量
/// </summary>
public class ExamProjectConsts
{  
    /// <summary>
    /// 编码(20)
    /// </summary>
    public static int MaxCodeLength { get; set; } = 20;
    /// <summary>
    /// 名称(200)
    /// </summary>
    public static int MaxNameLength { get; set; } = 200;
    /// <summary>
    /// 分类编码(20)
    /// </summary>
    public static int MaxCatalogCodeLength { get; set; } = 20;
    /// <summary>
    /// 目录名称(100)
    /// </summary>
    public static int MaxCatalogNameLength { get; set; } = 100;
    /// <summary>
    /// 拼音码(50)
    /// </summary>
    public static int MaxPyCodeLength { get; set; } = 50;
    /// <summary>
    /// 五笔(20)
    /// </summary>
    public static int MaxWbCodeLength { get; set; } = 20;
    /// <summary>
    /// 检查部位(50)
    /// </summary>
    public static int MaxExamPartCodeLength { get; set; } = 50;
    /// <summary>
    /// 检查部位(50)
    /// </summary>
    public static int MaxExamPartNameLength { get; set; } = 200;
    /// <summary>
    /// 单位(20)
    /// </summary>
    public static int MaxUnitLength { get; set; } = 20;
    /// <summary>
    /// 科室编码(20)
    /// </summary>
    public static int MaxExecDeptCodeLength { get; set; } = 20;
    /// <summary>
    /// 科室名称(50)
    /// </summary>
    public static int MaxExecDeptNameLength { get; set; } = 50;
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
    /// 执行机房名称(50)
    /// </summary>
    public static int MaxRoomNameLength { get; set; } = 50;
}