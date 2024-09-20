namespace YiJian.MasterData.Medicines;

/// <summary>
/// 药品用法字典 表字段长度常量
/// </summary>
public class MedicineUsageConsts
{  
    /// <summary>
    /// 编码(50)
    /// </summary>
    public static int MaxUsageCodeLength { get; set; } = 50;
    /// <summary>
    /// 名称(100)
    /// </summary>
    public static int MaxUsageNameLength { get; set; } = 100;
    /// <summary>
    /// 全称(200)
    /// </summary>
    public static int MaxFullNameLength { get; set; } = 200;
    /// <summary>
    /// 备注(500)
    /// </summary>
    public static int MaxRemarkLength { get; set; } = 500;
    /// <summary>
    /// 拼音码(20)
    /// </summary>
    public static int MaxPyCodeLength { get; set; } = 20;
    /// <summary>
    /// 五笔码(20)
    /// </summary>
    public static int MaxWbCodeLength { get; set; } = 20;
    /// <summary>
    /// 诊疗项目 描述：一个或多个项目，多个以,隔开(50)
    /// </summary>
    public static int MaxTreatCodeLength { get; set; } = 50;
}