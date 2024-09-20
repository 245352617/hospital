using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Validation;
using YiJian.ECIS.ShareModel.Enums;

namespace YiJian.MasterData.Labs;

/// <summary>
/// 检验明细项 修改输入
/// </summary>
[Serializable]
public class LabTargetUpdate
{ 
    public int Id { get; set; }

    /// <summary>
    /// 编码
    /// </summary>
    [Required(ErrorMessage = "编码不能为空！")]
    [DynamicStringLength(typeof(LabTargetConsts), nameof(LabTargetConsts.MaxTargetCodeLength), ErrorMessage = "编码最大长度不能超过{1}!")]
    public string  TargetCode { get; set; }

    /// <summary>
    /// 名称
    /// </summary>
    [Required(ErrorMessage = "名称不能为空！")]
    [DynamicStringLength(typeof(LabTargetConsts), nameof(LabTargetConsts.MaxTargetNameLength), ErrorMessage = "名称最大长度不能超过{1}!")]
    public string  TargetName { get; set; }

    /// <summary>
    /// 项目编码
    /// </summary>
    public string  ProjectCode { get; set; }

    /// <summary>
    /// 排序号
    /// </summary>
    public int  Sort { get; set; }

    /// <summary>
    /// 拼音码
    /// </summary>
    [Required(ErrorMessage = "拼音码不能为空！")]
    [DynamicStringLength(typeof(LabTargetConsts), nameof(LabTargetConsts.MaxPyCodeLength), ErrorMessage = "拼音码最大长度不能超过{1}!")]
    public string  PyCode { get; set; }

    /// <summary>
    /// 五笔
    /// </summary>
    [DynamicStringLength(typeof(LabTargetConsts), nameof(LabTargetConsts.MaxWbCodeLength), ErrorMessage = "五笔最大长度不能超过{1}!")]
    public string  WbCode { get; set; }

    /// <summary>
    /// 单位
    /// </summary>
    [Required(ErrorMessage = "单位不能为空！")]
    [DynamicStringLength(typeof(LabTargetConsts), nameof(LabTargetConsts.MaxTargetUnitLength), ErrorMessage = "单位最大长度不能超过{1}!")]
    public string  TargetUnit { get; set; }

    /// <summary>
    /// 数量
    /// </summary>
    public decimal  Qty { get; set; }

    /// <summary>
    /// 价格
    /// </summary>
    public decimal  Price { get; set; }

    /// <summary>
    /// 医保目录:0=自费,1=甲类,2=乙类,3=其它
    /// </summary>
    public InsuranceCatalog InsuranceType { get; set; }

    /// <summary>
    /// 是否启用
    /// </summary>
    public bool  IsActive { get; set; }
    /// <summary>
    /// 分类编码和当前项目的编码组合
    /// </summary>
    public string CatalogAndProjectCode { get; set; }
}