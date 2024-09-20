using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Application.Dtos;

namespace YiJian.MasterData;

/// <summary>
/// 更新诊室
/// </summary>
[Serializable]
public class ConsultingRoomUpdate : EntityDto<Guid>
{
    /// <summary>
    /// 名称
    /// </summary>
    [Required]
    [StringLength(50)]
    public string Name { get; set; }
    
    /// <summary>
    /// 编码
    /// </summary>
    public string Code { get; set; }
    
    /// <summary>
    /// 诊室电脑 IP
    /// </summary>
    public string IP { get; set; }

    /// <summary>
    /// 叫号屏 IP
    /// </summary>
    public string CallScreenIp { get; set; }
    
    public bool IsActived { get; set; }
}
