using System.ComponentModel;

namespace YiJian.ECIS.ShareModel.Enums;

/// <summary>
/// 区域类型枚举
/// </summary>
public enum ERegionType
{
    /// <summary>
    /// 省
    /// </summary>
    [Description("省")] Province,
    /// <summary>
    /// 市
    /// </summary>
    [Description("市")] City,
    /// <summary>
    /// 区/县
    /// </summary>
    [Description("区/县")] County,
    /// <summary>
    /// 镇
    /// </summary>
    [Description("镇")] Town,
}