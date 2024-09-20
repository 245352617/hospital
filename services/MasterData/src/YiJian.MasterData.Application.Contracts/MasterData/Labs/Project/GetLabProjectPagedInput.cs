using System;
using YiJian.ECIS.ShareModel.Enums;
using YiJian.ECIS.ShareModel.Models;

namespace YiJian.MasterData;

/// <summary>
/// 检验项目 分页排序输入
/// </summary>
[Serializable]
public class GetLabProjectPagedInput : PageBase
{
    /// <summary>
    /// 过滤条件.
    /// </summary>
    public string Filter { get; set; }

    /// <summary>
    /// 排序字段.
    /// </summary>
    public string Sorting { get; set; }

    /// <summary>
    /// 平台标识   1：院前  0：急诊
    /// </summary>
    public PlatformType PlatformType { get; set; }
}