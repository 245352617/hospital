namespace YiJian.MasterData.MasterData.HospitalClient;

public class UsagesEto
{
    /// <summary>
    /// 药品用法ID
    /// </summary>
    public string DrugUsageId { get; set; }

    /// <summary>
    /// 拼音码
    /// </summary>
    public string SpellCode { get; set; }

    /// <summary>
    /// 用法名称
    /// </summary>
    public string DrugUsageName { get; set; }

    /// <summary>
    /// 附加卡片类型	10.注射单,皮试单  08.雾化申请单  09.输液卡
    /// </summary>
    public string AddCard { get; set; }

    /// <summary>
    /// 附加项目
    /// </summary>
    public string Project { get; set; }
}