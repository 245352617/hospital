using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Domain.Entities.Auditing;

namespace YiJian.MasterData;

/// <summary>
/// 字典
/// </summary>
[Comment("字典")]
public class Dictionaries : FullAuditedAggregateRoot<Guid>
{
    public Dictionaries SetId(Guid id)
    {
        this.Id = id;
        return this;
    }

    /// <summary>
    /// 字典编码
    /// </summary>
    [Required]
    [StringLength(50)]
    [Comment("字典编码")]
    public string DictionariesCode { get; set; }

    /// <summary>
    /// 字典名称
    /// </summary>
    [StringLength(100)]
    [Required]
    [Comment("字典名称")]
    public string DictionariesName { get; set; }

    /// <summary>
    /// 字典类型编码
    /// </summary>
    [Required]
    [StringLength(50)]
    [Comment("字典类型编码")]
    public string DictionariesTypeCode { get; set; }

    /// <summary>
    /// 字典类型名称
    /// </summary>
    [StringLength(100)]
    [Comment("字典类型名称")]
    public string DictionariesTypeName { get; set; }

    /// <summary>
    /// 使用状态
    /// </summary>
    [Required]
    [DefaultValue(0)]
    [Comment("使用状态")]
    public bool Status { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    [Comment("备注")]
    public string Remark { get; set; }

    /// <summary>
    /// 拼音码
    /// </summary>
    [Comment("拼音码")]
    public string Py { get; set; }

    /// <summary>
    /// 排序
    /// </summary>
    [Comment("排序")]
    public int Sort { get; set; }

    /// <summary>
    /// 默认选中
    /// </summary>
    [Comment("默认选中")]
    public bool IsDefaltChecked { get; set; }

}