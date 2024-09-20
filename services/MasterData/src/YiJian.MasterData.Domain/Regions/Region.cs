using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Domain.Entities;
using YiJian.ECIS.ShareModel.Enums;

namespace YiJian.MasterData.Regions;

/// <summary>
/// 地区字典
/// </summary>
[Comment("地区字典")]
public class Region : Entity<Guid>
{
    public Region(string parentCode, string regionCode, string regionName, ERegionType regionType, string pyCode)
    {
        ParentCode = parentCode;
        RegionCode = regionCode;
        RegionName = regionName;
        RegionType = regionType;
        PyCode = pyCode;
    }

    /// <summary>
    /// 区域编码
    /// </summary>
    [Comment("区域编码")]
    [StringLength(10)]
    [Required]
    public string RegionCode { get; private set; }

    /// <summary>
    /// 区域名称
    /// </summary>
    [Comment("区域名称")]
    [StringLength(30)]
    [Required]
    public string RegionName { get; private set; }

    /// <summary>
    /// 区域类型
    /// </summary>
    [Comment("区域类型")]
    [Required]
    public ERegionType RegionType { get; private set; }

    /// <summary>
    /// 父级编码
    /// </summary>
    [Comment("父级编码")]
    [Required]
    [StringLength(10)]
    public string ParentCode { get; private set; }

    /// <summary>
    /// 拼音码
    /// </summary>
    [Comment("拼音码")]
    [Required]
    [StringLength(20)]
    public string PyCode { get; private set; }

}