using System;
using YiJian.ECIS.ShareModel.Models;

namespace YiJian.MasterData.Labs;

/// <summary>
/// 检验明细项 分页输入
/// </summary>
[Serializable]
public class GetLabTargetInput : PageBase
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
