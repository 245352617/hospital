using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Application.Dtos;

namespace YiJian.MasterData.MasterData.Separations.Dto;

/// <summary>
/// 用药途经
/// </summary>
public class UsageDto : EntityDto<Guid?>
{
    /// <summary>
    /// 用法编码
    /// </summary> 
    [Required, StringLength(20)]
    public string UsageCode { get; set; }

    /// <summary>
    /// 用法名称
    /// </summary> 
    [Required, StringLength(20)]
    public string UsageName { get; set; }
     
}
