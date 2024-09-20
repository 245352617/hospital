using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp;
using Volo.Abp.Domain.Entities;

namespace YiJian.MasterData.Pharmacies.Entities;

/// <summary>
/// 药房配置
/// </summary>
[Comment("药房配置")]
public class Pharmacy : Entity<Guid>
{
    private Pharmacy()
    {

    }

    /// <summary>
    /// 药房配置
    /// </summary> 
    public Pharmacy(Guid id, [NotNull]string pharmacyCode, [NotNull] string pharmacyName, bool isDefault = false)
    {
        Id = id;
        PharmacyCode = Check.NotNullOrEmpty(pharmacyCode,nameof(pharmacyCode), maxLength:32);
        PharmacyName = Check.NotNullOrEmpty(pharmacyName, nameof(pharmacyName), maxLength: 32);
        IsDefault = isDefault;
    }

    /// <summary>
    /// 药房编号
    /// </summary>
    [Required,MaxLength(32)]
    [Comment("药房编号")]
    public string PharmacyCode { get; set; }

    /// <summary>
    /// 药房名称
    /// </summary>
    [Required, MaxLength(100)]
    [Comment("药房名称")]
    public string PharmacyName { get; set; }

    /// <summary>
    /// 默认药房
    /// </summary> 
    [Comment("默认药房，1=是默认药房")]
    public bool IsDefault { get; set; }

    /// <summary>
    /// 药房配置
    /// </summary> 
    public void Update([NotNull] string pharmacyCode, [NotNull] string pharmacyName, bool isDefault = false)
    { 
        PharmacyCode = Check.NotNullOrEmpty(pharmacyCode, nameof(pharmacyCode), maxLength: 32);
        PharmacyName = Check.NotNullOrEmpty(pharmacyName, nameof(pharmacyName), maxLength: 32);
        IsDefault = isDefault;
    }

    /// <summary>
    /// 改变当前默认值
    /// </summary>
    /// <param name="isDefault"></param>
    public void ChangeIsDefaultValue(bool isDefault = false)
    {
        IsDefault = isDefault;
    }

}
