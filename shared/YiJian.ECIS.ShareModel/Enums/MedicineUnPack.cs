using System.ComponentModel;


namespace YiJian.ECIS.ShareModel.Enums;

/// <summary>
/// 门诊拆分属性 0最小单位总量取整 1包装单位总量取整 2最小单位每次取整 3包装单位每次取整 4最小单位可拆分
/// </summary>
public enum MedicineUnPack
{
    /// <summary>
    /// 未知
    /// </summary>
    [Description("未知")]
    UnKnown = -1,
    /// <summary>
    /// 最小单位总量取整
    /// </summary>
    [Description("最小单位总量取整")]
    RoundByMinUnitAmount = 0,
    /// <summary>
    /// 包装单位总量取整
    /// </summary>
    [Description("包装单位总量取整")]
    RoundByPackUnitAmount = 1,
    /// <summary>
    /// 最小单位每次取整
    /// </summary>
    [Description("最小单位每次取整")]
    RoundByMinUnitTime = 2,
    /// <summary>
    /// 包装单位每次取整
    /// </summary>
    [Description("包装单位每次取整")]
    RoundByPackUnitTime = 3,
    /// <summary>
    /// 最小单位可拆分
    /// </summary>
    [Description("最小单位可拆分")]
    RoundByMinUnit = 4

}
