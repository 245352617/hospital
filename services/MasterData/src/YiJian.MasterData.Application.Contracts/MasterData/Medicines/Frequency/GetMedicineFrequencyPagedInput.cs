using System;
using YiJian.ECIS.ShareModel.Models;


namespace YiJian.MasterData;


/// <summary>
/// 药品频次字典 分页排序输入
/// </summary>
[Serializable]
public class GetMedicineFrequencyPagedInput : PageBase
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
