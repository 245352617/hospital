using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Application.Dtos;

namespace YiJian.MasterData.MasterData.Pharmacies.Dto;

/// <summary>
/// 修改药方配置
/// </summary>
public class ModifyPharmacyDto:EntityDto<Guid?>
{
    /// <summary>
    /// 药房编号
    /// </summary>
    [Required, MaxLength(32)] 
    public string PharmacyCode { get; set; }

    /// <summary>
    /// 药房名称
    /// </summary>
    [Required, MaxLength(100)] 
    public string PharmacyName { get; set; }

    /// <summary>
    /// 默认药房
    /// </summary>  
    public bool IsDefault { get; set; } = false;
}
