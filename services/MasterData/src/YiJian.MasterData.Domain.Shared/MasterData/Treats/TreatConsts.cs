namespace YiJian.MasterData.Treats;

/// <summary>
/// 诊疗项目字典 表字段长度常量
/// </summary>
public class TreatConsts
{  
    /// <summary>
    /// 编码(100)
    /// </summary>
    public static int MaxTreatCodeLength { get; set; } = 100;
    /// <summary>
    /// 名称(200)
    /// </summary>
    public static int MaxTreatNameLength { get; set; } = 200;
    /// <summary>
    /// 拼音码(100)
    /// </summary>
    public static int MaxPyCodeLength { get; set; } = 100;
    /// <summary>
    /// 五笔(100)
    /// </summary>
    public static int MaxWbCodeLength { get; set; } = 100;
    /// <summary>
    /// 诊疗处置类别代码(20)
    /// </summary>
    public static int MaxCategoryCodeLength { get; set; } = 20;
    /// <summary>
    /// 诊疗处置类别名称(50)
    /// </summary>
    public static int MaxCategoryNameLength { get; set; } = 50;
    /// <summary>
    /// 规格(200)
    /// </summary>
    public static int MaxSpecificationLength { get; set; } = 200;
    /// <summary>
    /// 单位(50)
    /// </summary>
    public static int MaxTreatUnitLength { get; set; } = 50;
    /// <summary>
    /// 默认频次代码(50)
    /// </summary>
    public static int MaxFrequencyCodeLength { get; set; } = 50;
    /// <summary>
    /// 执行科室代码(50)
    /// </summary>
    public static int MaxExecDeptCodeLength { get; set; } = 50;
    /// <summary>
    /// 执行科室(100)
    /// </summary>
    public static int MaxExecDeptNameLength { get; set; } = 100;
    /// <summary>
    /// 收费大类代码(50)
    /// </summary>
    public static int MaxFeeTypeMainCodeLength { get; set; } = 50;
    /// <summary>
    /// 收费小类代码(50)
    /// </summary>
    public static int MaxFeeTypeSubCodeLength { get; set; } = 50;
}