using System.ComponentModel;

namespace YiJian.ECIS.ShareModel.Enums;

/// <summary>
/// 医保目录
/// </summary>
public enum InsuranceCatalog
{
    /// <summary>
    /// 自费
    /// </summary>
    [Description("自费")]
    Self = 0,

    /// <summary>
    /// 甲类
    /// </summary>
    [Description("甲类")]
    ClassA = 1,

    /// <summary>
    /// 乙类
    /// </summary>
    [Description("乙类")]
    ClassB = 2,
    /// <summary>
    /// 丙类
    /// </summary>
    [Description("丙类")]
    ClassC = 4,
    /// <summary>
    /// 其它
    /// </summary>
    [Description("其它")]
    Other = 3,
    /// <summary>
    /// 少儿
    /// </summary>
    [Description("少儿")]
    Children = 5
}