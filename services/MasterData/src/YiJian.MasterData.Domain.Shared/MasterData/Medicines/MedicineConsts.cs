namespace YiJian.MasterData.Medicines;

/// <summary>
/// 药品字典 表字段长度常量
/// </summary>
public class MedicineConsts
{  
    /// <summary>
    /// 药品编码(50)
    /// </summary>
    public static int MaxMedicineCodeLength { get; set; } = 50;
    /// <summary>
    /// 药品名称(200)
    /// </summary>
    public static int MaxMedicineNameLength { get; set; } = 200;
    /// <summary>
    /// 学名(200)
    /// </summary>
    public static int MaxScientificNameLength { get; set; } = 200;
    /// <summary>
    /// 别名(200)
    /// </summary>
    public static int MaxAliasLength { get; set; } = 200;
    /// <summary>
    /// 别名拼音(50)
    /// </summary>
    public static int MaxAliasPyCodeLength { get; set; } = 50;
    /// <summary>
    /// 别名五笔码(50)
    /// </summary>
    public static int MaxAliasWbCodeLength { get; set; } = 50;
    /// <summary>
    /// 拼音码(50)
    /// </summary>
    public static int MaxPyCodeLength { get; set; } = 50;
    /// <summary>
    /// 五笔(20)
    /// </summary>
    public static int MaxWbCodeLength { get; set; } = 20;
    /// <summary>
    /// 药物属性：西药、中药、西药制剂、中药制剂(50)
    /// </summary>
    public static int MaxMedicinePropertyLength { get; set; } = 50;
    /// <summary>
    /// 剂量单位(20)
    /// </summary>
    public static int MaxDosageUnitLength { get; set; } = 20;
    /// <summary>
    /// 基本单位(20)
    /// </summary>
    public static int MaxUnitLength { get; set; } = 20;
    /// <summary>
    /// 包装单位(20)
    /// </summary>
    public static int MaxBigPackUnitLength { get; set; } = 20;
    /// <summary>
    /// 小包装单位(20)
    /// </summary>
    public static int MaxSmallPackUnitLength { get; set; } = 20;
    /// <summary>
    /// 厂家名称(100)
    /// </summary>
    public static int MaxFactoryNameLength { get; set; } = 100;
    /// <summary>
    /// 厂家代码(50)
    /// </summary>
    public static int MaxFactoryCodeLength { get; set; } = 50;
    /// <summary>
    /// 批次号(20)
    /// </summary>
    public static int MaxBatchNoLength { get; set; } = 20;
    /// <summary>
    /// 重量单位(20)
    /// </summary>
    public static int MaxWeightUnitLength { get; set; } = 20;
    /// <summary>
    /// 体积单位(20)
    /// </summary>
    public static int MaxVolumeUnitLength { get; set; } = 20;
    /// <summary>
    /// 备注(20)
    /// </summary>
    public static int MaxRemarkLength { get; set; } = 20;
    /// <summary>
    /// 限制性用药描述(200)
    /// </summary>
    public static int MaxLimitedNoteLength { get; set; } = 200;
    /// <summary>
    /// 规格(50)
    /// </summary>
    public static int MaxSpecificationLength { get; set; } = 50;
    /// <summary>
    /// 剂型(20)
    /// </summary>
    public static int MaxDosageFormLength { get; set; } = 20;
    /// <summary>
    /// 执行科室编码(20)
    /// </summary>
    public static int MaxExecDeptCodeLength { get; set; } = 20;
    /// <summary>
    /// 执行科室(20)
    /// </summary>
    public static int MaxExecDeptNameLength { get; set; } = 20;
    /// <summary>
    /// 默认用法编码(20)
    /// </summary>
    public static int MaxUsageCodeLength { get; set; } = 20;
    /// <summary>
    /// 默认用法名称(50)
    /// </summary>
    public static int MaxUsageNameLength { get; set; } = 50;
    /// <summary>
    /// 默认频次编码(20)
    /// </summary>
    public static int MaxFrequencyCodeLength { get; set; } = 20;
    /// <summary>
    /// 默认频次名称(50)
    /// </summary>
    public static int MaxFrequencyNameLength { get; set; } = 50;
    /// <summary>
    /// 药房代码(20)
    /// </summary>
    public static int MaxPharmacyCodeLength { get; set; } = 20;
    /// <summary>
    /// 药房(20)
    /// </summary>
    public static int MaxPharmacyNameLength { get; set; } = 20;
    /// <summary>
    /// 基药标准  N普通,Y国基,P省基,C市基(20)
    /// </summary>
    public static int MaxBaseFlagLength { get; set; } = 20;
}