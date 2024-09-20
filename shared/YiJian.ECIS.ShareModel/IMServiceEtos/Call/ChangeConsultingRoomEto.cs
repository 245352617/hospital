namespace YiJian.ECIS.ShareModel.IMServiceEtos.Call;

/// <summary>
/// 客户端诊室改变
/// </summary>
[Serializable]
public class ChangeConsultingRoomEto
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="connectionId"></param>
    /// <param name="consultingRoomCode"></param>
    public ChangeConsultingRoomEto(string connectionId, string consultingRoomCode = null)
    {
        this.ConnectionId = connectionId;
        this.ConsultingRoomCode = consultingRoomCode;
    }

    /// <summary>
    /// WebSocket 客户端连接 ID
    /// </summary>
    public string ConnectionId { get; set; }

    /// <summary>
    /// 诊室代码
    /// </summary>
    public string ConsultingRoomCode { get; set; }
}
