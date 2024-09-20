using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Validation;

namespace YiJian.MasterData;

/// <summary>
/// 检查部位 新增输入
/// </summary>
[Serializable]
public class ExamPartCreation
{
    /// <summary>
    /// 检查部位编码
    /// </summary>
    [Required(ErrorMessage = "检查部位编码不能为空！")]
    [DynamicStringLength(typeof(ExamPartConsts), nameof(ExamPartConsts.MaxPartCodeLength), ErrorMessage = "检查部位编码最大长度不能超过{1}!")]
    public string  PartCode { get; set; }

    /// <summary>
    /// 检查部位名称
    /// </summary>
    [Required(ErrorMessage = "检查部位名称不能为空！")]
    [DynamicStringLength(typeof(ExamPartConsts), nameof(ExamPartConsts.MaxPartNameLength), ErrorMessage = "检查部位名称最大长度不能超过{1}!")]
    public string  PartName { get; set; }

    /// <summary>
    /// 排序号
    /// </summary>
    [Required(ErrorMessage = "排序号不能为空！")]
    public int  Sort { get; set; }

    /// <summary>
    /// 拼音码
    /// </summary>
    [Required(ErrorMessage = "拼音码不能为空！")]
    [DynamicStringLength(typeof(ExamPartConsts), nameof(ExamPartConsts.MaxPyCodeLength), ErrorMessage = "拼音码最大长度不能超过{1}!")]
    public string  PyCode { get; set; }
}