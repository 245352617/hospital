using System;
using Volo.Abp.Application.Dtos;

namespace YiJian.MasterData;

/// <summary>
/// 诊室
/// </summary>
[Serializable]
public class ConsultingRoomData : EntityDto<Guid>
{
    /// <summary>
    /// 名称
    /// </summary>
    public string Name { get; set; }
    
    /// <summary>
    /// 编码
    /// </summary>
    public string Code { get; set; }
    
    /// <summary>
    /// 诊室 IP
    /// </summary>
    public string IP { get; set; }

    /// <summary>
    /// 叫号屏 IP
    /// </summary>
    public string CallScreenIp { get; set; }
    
    /// <summary>
    /// 是否启用
    /// </summary>
    public bool IsActived { get; set; }
}
