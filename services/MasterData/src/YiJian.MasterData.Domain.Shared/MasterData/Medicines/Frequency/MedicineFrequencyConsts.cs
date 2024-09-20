namespace YiJian.MasterData;

/// <summary>
/// 药品频次字典 表字段长度常量
/// </summary>
public class MedicineFrequencyConsts
{  
    /// <summary>
    /// 频次编码(20)
    /// </summary>
    public static int MaxFrequencyCodeLength { get; set; } = 20;
    /// <summary>
    /// 频次名称(50)
    /// </summary>
    public static int MaxFrequencyNameLength { get; set; } = 50;
    /// <summary>
    /// 频次全称(200)
    /// </summary>
    public static int MaxFullNameLength { get; set; } = 200;
    /// <summary>
    /// 频次单位(50)
    /// </summary>
    public static int MaxUnitLength { get; set; } = 50;
    /// <summary>
    /// 执行时间 eg：8:00,10:00,12:00,14:00,16:00,18:00  一个或多个时间，多个以,隔开(200)
    /// </summary>
    public static int MaxExecDayTimesLength { get; set; } = 200;
    /// <summary>
    /// 频次周明细(200)
    /// </summary>
    public static int MaxWeeksLength { get; set; } = 200;
    /// <summary>
    /// 备注(100)
    /// </summary>
    public static int MaxRemarkLength { get; set; } = 100;
}