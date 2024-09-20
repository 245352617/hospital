using System;
using YiJian.ECIS.ShareModel.Models;

namespace YiJian.MasterData.Labs.Position;

/// <summary>
/// 检验标本采集部位 分页排序输入
/// </summary>
[Serializable]
public class GetLabSpecimenPositionPagedInput : PageBase
{
    /// <summary>
    /// 部位编码
    /// </summary>
    public string PositionCode { get; set; }

    /// <summary>
    /// 过滤条件.
    /// </summary>
    public string Filter { get; set; }

    /// <summary>
    /// 排序字段.
    /// </summary>
    public string Sorting { get; set; }
}
