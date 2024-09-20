using Volo.Abp.EventBus;

namespace YiJian.MasterData.External.LongGang.Lab;

/// <summary>
/// 查询His药品信息出参
/// </summary>
[EventName("SpecimenDicEvents")]
public class SpecimenEto
{
    /// <summary>
    /// 标本编号
    /// </summary>
    public string SpecimenNo { get; set; }

    /// <summary>
    /// 标本名称
    /// </summary>
    public string SpecimenName { get; set; }

    /// <summary>
    /// 拼音
    /// </summary>
    public string SpellCode { get; set; }
}
