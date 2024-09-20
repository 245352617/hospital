using Volo.Abp.EventBus;

namespace YiJian.MasterData.External.LongGang.Frequency;

[EventName("DrugFrequencyDicEvents")]
public class FrequencyEto
{
    /// <summary>
    /// 频次id
    /// </summary>
    public string DrugfrequencyId { get; set; }

    /// <summary>
    /// 频次标准编码：QD/BID等
    /// </summary>
    public string DrugfrequencyCode { get; set; }

    /// <summary>
    /// 执行次数
    /// </summary>
    public string DailyFrequency { get; set; }

    /// <summary>
    /// 执行时间点，以“-”隔开：8-9-10
    /// </summary>
    public string ExecutionTime { get; set; }

    /// <summary>
    /// 排序
    /// </summary>
    public string ArrangementOrder { get; set; }

    /// <summary>
    /// 频次名称
    /// </summary>
    public string DrugfrequencyName { get; set; }
}
