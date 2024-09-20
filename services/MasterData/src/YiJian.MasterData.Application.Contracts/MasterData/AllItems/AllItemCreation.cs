using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Validation;

namespace YiJian.MasterData.AllItems;

/// <summary>
/// 诊疗检查检验药品项目合集 新增输入
/// </summary>
[Serializable]
public class AllItemCreation
{
    /// <summary>
    /// 分类编码
    /// </summary>
    [DynamicStringLength(typeof(AllItemConsts), nameof(AllItemConsts.MaxCategoryCodeLength),
        ErrorMessage = "分类编码最大长度不能超过{1}!")]
    [Required(ErrorMessage = "分类编码不能为空！")]
    public string CategoryCode { get; set; }

    /// <summary>
    /// 分类名称
    /// </summary>
    [DynamicStringLength(typeof(AllItemConsts), nameof(AllItemConsts.MaxCategoryNameLength),
        ErrorMessage = "分类名称最大长度不能超过{1}!")]
    [Required(ErrorMessage = "分类名称不能为空！")]
    public string CategoryName { get; set; }

    /// <summary>
    /// 编码
    /// </summary>
    [DynamicStringLength(typeof(AllItemConsts), nameof(AllItemConsts.MaxCodeLength),
        ErrorMessage = "编码最大长度不能超过{1}!")]
    [Required(ErrorMessage = "编码不能为空！")]
    public string Code { get; set; }

    /// <summary>
    /// 名称
    /// </summary>
    [DynamicStringLength(typeof(AllItemConsts), nameof(AllItemConsts.MaxNameLength),
        ErrorMessage = "名称最大长度不能超过{1}!")]
    [Required(ErrorMessage = "名称不能为空！")]
    public string Name { get; set; }

    /// <summary>
    /// 单位
    /// </summary>
    [DynamicStringLength(typeof(AllItemConsts), nameof(AllItemConsts.MaxUnitLength),
        ErrorMessage = "单位最大长度不能超过{1}!")]
    [Required(ErrorMessage = "单位不能为空！")]
    public string Unit { get; set; }

    /// <summary>
    /// 价格
    /// </summary>
    public decimal Price { get; set; }

    /// <summary>
    /// 排序
    /// </summary>
    public int IndexNo { get; set; }

    /// <summary>
    /// 类型编码
    /// </summary>
    [DynamicStringLength(typeof(AllItemConsts), nameof(AllItemConsts.MaxTypeCodeLength),
        ErrorMessage = "类型编码最大长度不能超过{1}!")]
    public string TypeCode { get; set; }

    /// <summary>
    /// 类型名称
    /// </summary>
    [DynamicStringLength(typeof(AllItemConsts), nameof(AllItemConsts.MaxTypeNameLength),
        ErrorMessage = "类型名称最大长度不能超过{1}!")]
    public string TypeName { get; set; }

    /// <summary>
    /// 收费分类编码
    /// </summary>
    [DynamicStringLength(typeof(AllItemConsts), nameof(AllItemConsts.MaxChargeCodeLength),
        ErrorMessage = "收费分类编码最大长度不能超过{1}!")]
    public string ChargeCode { get; set; }

    /// <summary>
    /// 收费分类名称
    /// </summary>
    [DynamicStringLength(typeof(AllItemConsts), nameof(AllItemConsts.MaxChargeNameLength),
        ErrorMessage = "收费分类名称最大长度不能超过{1}!")]
    public string ChargeName { get; set; }
}