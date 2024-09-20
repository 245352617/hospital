
using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Validation;

namespace YiJian.MasterData.Sequences;

/// <summary>
/// 序列 修改输入
/// </summary>
[Serializable]
public class SequenceUpdate
{ 
    public int Id { get; set; }

    /// <summary>
    /// 编码
    /// </summary>
    [Required(ErrorMessage = "编码不能为空！")]
    [DynamicStringLength(typeof(SequenceConsts), nameof(SequenceConsts.MaxCodeLength), ErrorMessage = "编码最大长度不能超过{1}!")]
    public string  Code { get; set; }

    /// <summary>
    /// 名称
    /// </summary>
    [Required(ErrorMessage = "名称不能为空！")]
    [DynamicStringLength(typeof(SequenceConsts), nameof(SequenceConsts.MaxNameLength), ErrorMessage = "名称最大长度不能超过{1}!")]
    public string  Name { get; set; }

    /// <summary>
    /// 序列值
    /// </summary>
    public int  Value { get; set; }

    /// <summary>
    /// 格式
    /// </summary>
    [DynamicStringLength(typeof(SequenceConsts), nameof(SequenceConsts.MaxFormatLength), ErrorMessage = "格式最大长度不能超过{1}!")]
    public string  Format { get; set; }

    /// <summary>
    /// 序列值长度
    /// </summary>
    public int  Length { get; set; }

    /// <summary>
    /// 日期
    /// </summary>
    public DateTime  Date { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    [DynamicStringLength(typeof(SequenceConsts), nameof(SequenceConsts.MaxMemoLength), ErrorMessage = "备注最大长度不能超过{1}!")]
    public string  Memo { get; set; }
}