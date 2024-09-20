using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Domain.Entities;

namespace YiJian.MasterData;

/// <summary>
/// 诊疗分组
/// </summary>
[Comment("诊疗分组")]
public class TreatGroup : Entity<Guid>
{
    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="id"></param>
    /// <param name="catalogCode">目录编码</param>
    /// <param name="catalogName">目录名称</param>
    /// <param name="dictionaryCode">字典编码</param>
    /// <param name="dictionaryName">字典名称</param>
    public TreatGroup(Guid id, [NotNull] string catalogCode, string catalogName, [NotNull] string dictionaryCode,
        string dictionaryName)
    {
        Id = id;
        CatalogCode = catalogCode;
        CatalogName = catalogName;
        DictionaryCode = dictionaryCode;
        DictionaryName = dictionaryName;
    }

    /// <summary>
    /// 目录编码
    /// </summary>
    [Required, StringLength(50)]
    [Comment("目录编码")]
    public string CatalogCode { get; private set; }

    /// <summary>
    /// 目录名称
    /// </summary>
    [StringLength(200)]
    [Comment("目录名称")]
    public string CatalogName { get; private set; }

    /// <summary>
    /// 字典编码
    /// </summary>
    [Required, StringLength(50)]
    [Comment("字典编码")]
    public string DictionaryCode { get; private set; }

    /// <summary>
    /// 字典名称
    /// </summary>
    [StringLength(200)]
    [Comment("字典名称")]
    public string DictionaryName { get; private set; }
}