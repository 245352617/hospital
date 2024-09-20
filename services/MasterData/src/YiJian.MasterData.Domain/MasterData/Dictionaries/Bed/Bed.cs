using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Domain.Entities;

namespace YiJian.MasterData;

/// <summary>
/// Defines the <see cref="Bed" />.
/// </summary>
[Comment("床")]
public class Bed : Entity<int>
{
    /// <summary>
    /// 床号
    /// </summary>
    [StringLength(16)]
    [Comment("床号")]
    public string BedNo { get; set; }

    /// <summary>
    /// 区域标识,R红区，Y黄区，L留观、G绿区
    /// </summary>
    [StringLength(16)]
    [Comment("区域标识,R红区，Y黄区，L留观、G绿区")]
    public string DistrictNo { get; set; }

    /// <summary>
    /// 床名
    /// </summary>
    [StringLength(32)]
    [Comment("床名")]
    public string BedName { get; set; }

    /// <summary>
    /// 状态 0 未用，1占用
    /// </summary>
    [Comment("状态 0 未用，1占用")]
    public int AvailabilityStatus { get; set; }

    /// <summary>
    /// 工作站编号
    /// </summary>
    [StringLength(32)]
    [Comment("工作站编号")]
    public string WorkstationNo { get; set; }

    /// <summary>
    /// 房间号
    /// </summary>
    [StringLength(32)]
    [Comment("房间号")]
    public string RoomNo { get; set; }

    /// <summary>
    /// Prevents a default instance of the <see cref="Bed"/> class from being created.
    /// </summary>
    private Bed()
    {
    }
}
