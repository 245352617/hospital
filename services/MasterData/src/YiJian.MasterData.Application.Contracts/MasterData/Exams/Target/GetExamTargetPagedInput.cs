using System;
using YiJian.ECIS.ShareModel.Models;

namespace YiJian.MasterData.Exams;


/// <summary>
/// 检查明细项 分页排序输入
/// </summary>
[Serializable]
public class GetExamTargetPagedInput : PageBase
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
