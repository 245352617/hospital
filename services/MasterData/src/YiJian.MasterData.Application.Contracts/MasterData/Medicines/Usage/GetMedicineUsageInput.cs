using System;
using YiJian.ECIS.ShareModel.Models;

namespace YiJian.MasterData.Medicines;


/// <summary>
/// 药品用法字典 分页输入
/// </summary>
[Serializable]
public class GetMedicineUsageInput : PageBase
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
