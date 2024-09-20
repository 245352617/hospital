using Newtonsoft.Json;
using System;
using YiJian.ECIS.ShareModel.Models;
 
namespace YiJian.MasterData.DictionariesMultitypes;

/// <summary>
/// 字典多类型编码 分页输入
/// </summary>
[Serializable]
public class GetDictionariesMultitypeInput : PageBase
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
    /// 状态
    /// </summary>
    public int Status { get; set; } = -1;
    /// <summary>
    /// 开始时间
    /// </summary>
    [JsonConverter(typeof(DatetimeJsonConvert))]
    public DateTime? StartTime { get; set; }
    /// <summary>
    /// 结束
    /// </summary>
    [JsonConverter(typeof(DatetimeJsonConvert))]
    public DateTime? EndTime { get; set; }


}
