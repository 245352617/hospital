using Newtonsoft.Json;
using System;
using YiJian.ECIS.ShareModel.Models;

namespace YiJian.MasterData.DictionariesTypes;

/// <summary>
/// 字典类型编码 分页输入
/// </summary>
[Serializable]
public class GetDictionariesTypeInput : PageBase
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
