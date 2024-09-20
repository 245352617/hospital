using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Validation;
using YiJian.ECIS.ShareModel.Enums;

namespace YiJian.MasterData.Treats;

/// <summary>
/// 诊疗项目字典 新增输入
/// </summary>
[Serializable]
public class TreatCreation
{
    /// <summary>
    /// 编码
    /// </summary>
    [Required(ErrorMessage = "编码不能为空！")]
    [DynamicStringLength(typeof(TreatConsts), nameof(TreatConsts.MaxTreatCodeLength), ErrorMessage = "编码最大长度不能超过{1}!")]
    public string  TreatCode { get; set; }

    /// <summary>
    /// 名称
    /// </summary>
    [Required(ErrorMessage = "名称不能为空！")]
    [DynamicStringLength(typeof(TreatConsts), nameof(TreatConsts.MaxTreatNameLength), ErrorMessage = "名称最大长度不能超过{1}!")]
    public string  TreatName { get; set; }

    /// <summary>
    /// 拼音码
    /// </summary>
    [DynamicStringLength(typeof(TreatConsts), nameof(TreatConsts.MaxPyCodeLength), ErrorMessage = "拼音码最大长度不能超过{1}!")]
    public string  PyCode { get; set; }

    /// <summary>
    /// 五笔
    /// </summary>
    [DynamicStringLength(typeof(TreatConsts), nameof(TreatConsts.MaxWbCodeLength), ErrorMessage = "五笔最大长度不能超过{1}!")]
    public string  WbCode { get; set; }

    /// <summary>
    /// 单价
    /// </summary>
    public decimal  Price { get; set; }

    /// <summary>
    /// 其它价格
    /// </summary>
    public decimal?  OtherPrice { get; set; }
    /// <summary>
    /// 加收标志	
    /// </summary>
    public bool Additional { get; set; }
    /// <summary>
    /// 诊疗处置类别代码
    /// </summary>
    [Required(ErrorMessage = "诊疗处置类别代码不能为空！")]
    [DynamicStringLength(typeof(TreatConsts), nameof(TreatConsts.MaxCategoryCodeLength), ErrorMessage = "诊疗处置类别代码最大长度不能超过{1}!")]
    public string  CategoryCode { get; set; }

    /// <summary>
    /// 诊疗处置类别名称
    /// </summary>
    [Required(ErrorMessage = "诊疗处置类别名称不能为空！")]
    [DynamicStringLength(typeof(TreatConsts), nameof(TreatConsts.MaxCategoryNameLength), ErrorMessage = "诊疗处置类别名称最大长度不能超过{1}!")]
    public string  CategoryName { get; set; }

    /// <summary>
    /// 规格
    /// </summary>
    [DynamicStringLength(typeof(TreatConsts), nameof(TreatConsts.MaxSpecificationLength), ErrorMessage = "规格最大长度不能超过{1}!")]
    public string  Specification { get; set; }

    /// <summary>
    /// 单位
    /// </summary>
    [Required(ErrorMessage = "单位不能为空！")]
    [DynamicStringLength(typeof(TreatConsts), nameof(TreatConsts.MaxTreatUnitLength), ErrorMessage = "单位最大长度不能超过{1}!")]
    public string  Unit { get; set; }

    /// <summary>
    /// 默认频次代码
    /// </summary>
    [DynamicStringLength(typeof(TreatConsts), nameof(TreatConsts.MaxFrequencyCodeLength), ErrorMessage = "默认频次代码最大长度不能超过{1}!")]
    public string  FrequencyCode { get; set; }

    /// <summary>
    /// 执行科室代码
    /// </summary>
    [DynamicStringLength(typeof(TreatConsts), nameof(TreatConsts.MaxExecDeptCodeLength), ErrorMessage = "执行科室代码最大长度不能超过{1}!")]
    public string  ExecDeptCode { get; set; }

    /// <summary>
    /// 执行科室
    /// </summary>
    [DynamicStringLength(typeof(TreatConsts), nameof(TreatConsts.MaxExecDeptNameLength), ErrorMessage = "执行科室最大长度不能超过{1}!")]
    public string  ExecDeptName { get; set; }

    /// <summary>
    /// 收费大类代码
    /// </summary>
    [DynamicStringLength(typeof(TreatConsts), nameof(TreatConsts.MaxFeeTypeMainCodeLength), ErrorMessage = "收费大类代码最大长度不能超过{1}!")]
    public string  FeeTypeMainCode { get; set; }

    /// <summary>
    /// 收费小类代码
    /// </summary>
    [DynamicStringLength(typeof(TreatConsts), nameof(TreatConsts.MaxFeeTypeSubCodeLength), ErrorMessage = "收费小类代码最大长度不能超过{1}!")]
    public string  FeeTypeSubCode { get; set; }

    /// <summary>
    /// 平台标识
    /// </summary>
    public PlatformType  PlatformType { get; set; }
}