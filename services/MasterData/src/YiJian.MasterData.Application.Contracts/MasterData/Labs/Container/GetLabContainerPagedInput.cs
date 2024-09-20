using System;
using YiJian.ECIS.ShareModel.Models;

namespace YiJian.MasterData.Labs.Container;

/// <summary>
/// 容器编码 分页排序输入
/// </summary>
[Serializable]
public class GetLabContainerPagedInput : PageBase
{
    /// <summary>
    /// 过滤条件.
    /// </summary>
    public string Filter { get; set; }

    /// <summary>
    /// 排序字段.
    /// </summary>
    public string Sorting { get; set; }
}
