using Volo.Abp.EventBus;
using YiJian.ECIS.ShareModel.Exceptions;

namespace YiJian.ECIS.ShareModel.Etos.Pdas;

/// <summary>
/// 描述：接收pda数据的Eto
/// 创建人： yangkai
/// 创建时间：2022/12/1 15:38:44
/// </summary>
[EventName("PdaToEcis")]
public class ReceivePdaDataEto
{
    /// <summary>
    /// 事件代码 
    /// SkinTestResult:皮试结果反馈
    /// ExecuteResult:医嘱执行结果
    /// </summary>
    public string Eventcode { get; set; }

    /// <summary>
    /// 时间戳
    /// </summary>
    [System.Text.Json.Serialization.JsonConverter(typeof(DateTimeJsonConverter))]
    public DateTime? @DateTime { get; set; }

    /// <summary>
    /// 参数体
    /// </summary>
    public object Body { get; set; }
}
