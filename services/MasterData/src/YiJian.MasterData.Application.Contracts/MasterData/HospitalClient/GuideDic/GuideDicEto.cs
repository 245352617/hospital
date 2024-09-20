using Volo.Abp.EventBus;

namespace YiJian.MasterData.MasterData.HospitalClient;

/// <summary>
/// 组套指引字典
/// </summary>
[EventName("GuideDicEvents")]
public class GuideDicEto
{
    /// <summary>
    /// 指引id
    /// </summary>
    public string GuideCode { get; set; }

    /// <summary>
    /// 指引内容
    /// </summary>
    public string GuideName { get; set; }

    /// <summary>
    /// 项目名称
    /// </summary>
    public string ProjectName { get; set; }
}