using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace YiJian.MasterData.AllItems;

public class AllItemDataPreHospitalDto:AllItemData
{
    /// <summary>
    /// 默认用法编码
    /// </summary>
    [StringLength(20)]
    [Comment("默认用法编码")]
    public string UsageCode { get; set; }

    /// <summary>
    /// 默认用法名称
    /// </summary>
    [StringLength(50)]
    [Comment("默认用法名称")]
    public string UsageName { get; set; }

    /// <summary>
    /// 默认频次编码
    /// </summary>
    [StringLength(20)]
    [Comment("默认频次编码")]
    public string FrequencyCode { get; set; }

    /// <summary>
    /// 默认频次名称
    /// </summary>
    [StringLength(50)]
    [Comment("默认频次名称")]
    public string FrequencyName { get; set; }
    
    /// <summary>
    /// 规格
    /// </summary>
    // [Required]
    [StringLength(50)]
    [Comment("规格")]
    public string Specification { get; set; }
}