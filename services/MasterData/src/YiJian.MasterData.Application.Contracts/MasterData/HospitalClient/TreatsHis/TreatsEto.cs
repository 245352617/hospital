using Volo.Abp.EventBus;

namespace YiJian.MasterData.MasterData.HospitalClient;

[EventName("TreatDicEvents")]
public class TreatsEto
{
    /// <summary>
    /// 项目id
    /// </summary>
    public string ProjectId { get; set; }

    /// <summary>
    /// 项目名称
    /// </summary>
    public string ProjectName { get; set; }

    /// <summary>
    /// 项目单位
    /// </summary>
    public string Unit { get; set; }

    /// <summary>
    /// 项目类型
    /// </summary>
    public string ProjectType { get; set; }

    /// <summary>
    /// 项目类型名称
    /// </summary>
    public string ProjectTypeName { get; set; }

    /// <summary>
    /// 项目单价
    /// </summary>
    public double Price { get; set; }

    /// <summary>
    /// 拼英代码
    /// </summary>
    public string SpellCode { get; set; }

    /// <summary>
    /// 加收标志
    /// </summary>
    public string Additional { get; set; }

    /// <summary>
    /// 加收后金额
    /// </summary>
    public double ChargeAmount { get; set; }
    /// <summary>
    /// 项目归类
    /// </summary>
    public string ProjectMerge { get; set; }

}