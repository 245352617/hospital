using System.ComponentModel;

namespace YiJian.ECIS.ShareModel.Enums;

/// <summary>
/// 性别，M=male(男) ,F=female(女),U=Unknown(未知)
/// </summary>
public enum EGender : int
{
    /// <summary>
    /// M=male(男)
    /// </summary>
    [Description("男")]
    Male = 0,

    /// <summary>
    /// F=female(女)
    /// </summary>
    [Description("女")]
    Female = 1,

    /// <summary>
    /// U=未知
    /// </summary>
    [Description("未知")]
    Unknown = 1,
}
