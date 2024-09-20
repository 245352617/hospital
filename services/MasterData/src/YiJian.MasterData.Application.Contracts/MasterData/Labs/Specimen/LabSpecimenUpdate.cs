using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Validation;

namespace YiJian.MasterData.Labs;

/// <summary>
/// 检验标本 修改输入
/// </summary>
[Serializable]
public class LabSpecimenUpdate
{ 
    public int Id { get; set; }

    /// <summary>
    /// 标本编码
    /// </summary>
    [Required(ErrorMessage = "标本编码不能为空！")]
    [DynamicStringLength(typeof(LabSpecimenConsts), nameof(LabSpecimenConsts.MaxSpecimenCodeLength), ErrorMessage = "标本编码最大长度不能超过{1}!")]
    public string  SpecimenCode { get; set; }

    /// <summary>
    /// 标本名称
    /// </summary>
    [Required(ErrorMessage = "标本名称不能为空！")]
    [DynamicStringLength(typeof(LabSpecimenConsts), nameof(LabSpecimenConsts.MaxSpecimenNameLength), ErrorMessage = "标本名称最大长度不能超过{1}!")]
    public string  SpecimenName { get; set; }

    /// <summary>
    /// 排序号
    /// </summary>
    [Required(ErrorMessage = "排序号不能为空！")]
    public int  Sort { get; set; }

    /// <summary>
    /// 拼音码
    /// </summary>
    [Required(ErrorMessage = "拼音码不能为空！")]
    [DynamicStringLength(typeof(LabSpecimenConsts), nameof(LabSpecimenConsts.MaxPyCodeLength), ErrorMessage = "拼音码最大长度不能超过{1}!")]
    public string  PyCode { get; set; }

    /// <summary>
    /// 五笔
    /// </summary>
    [DynamicStringLength(typeof(LabSpecimenConsts), nameof(LabSpecimenConsts.MaxWbCodeLength), ErrorMessage = "五笔最大长度不能超过{1}!")]
    public string  WbCode { get; set; }

    /// <summary>
    /// 是否启用
    /// </summary>
    public bool  IsActive { get; set; }
}