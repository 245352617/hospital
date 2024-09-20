using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Validation;
using YiJian.ECIS.ShareModel.Enums;

namespace YiJian.MasterData;

/// <summary>
/// 检验项目 新增输入
/// </summary>
[Serializable]
public class LabProjectCreation
{
    /// <summary>
    /// 编码
    /// </summary>
    [Required(ErrorMessage = "编码不能为空！")]
    [DynamicStringLength(typeof(LabProjectConsts), nameof(LabProjectConsts.MaxProjectCodeLength), ErrorMessage = "编码最大长度不能超过{1}!")]
    public string ProjectCode { get; set; }

    /// <summary>
    /// 名称
    /// </summary>
    [Required(ErrorMessage = "名称不能为空！")]
    [DynamicStringLength(typeof(LabProjectConsts), nameof(LabProjectConsts.MaxProjectNameLength), ErrorMessage = "名称最大长度不能超过{1}!")]
    public string ProjectName { get; set; }

    /// <summary>
    /// 检验目录编码
    /// </summary>
    [Required(ErrorMessage = "检验目录编码不能为空！")]
    [DynamicStringLength(typeof(LabProjectConsts), nameof(LabProjectConsts.MaxCatalogCodeLength), ErrorMessage = "检验目录编码最大长度不能超过{1}!")]
    public string CatalogCode { get; set; }

    /// <summary>
    /// 目录分类名称
    /// </summary>
    [Required(ErrorMessage = "目录分类名称不能为空！")]
    [DynamicStringLength(typeof(LabProjectConsts), nameof(LabProjectConsts.MaxCatalogNameLength), ErrorMessage = "目录分类名称最大长度不能超过{1}!")]
    public string CatalogName { get; set; }

    /// <summary>
    /// 标本编码
    /// </summary>
    [Required(ErrorMessage = "标本编码不能为空！")]
    [DynamicStringLength(typeof(LabProjectConsts), nameof(LabProjectConsts.MaxSpecimenCodeLength), ErrorMessage = "标本编码最大长度不能超过{1}!")]
    public string SpecimenCode { get; set; }

    /// <summary>
    /// 标本名称
    /// </summary>
    [Required(ErrorMessage = "标本名称不能为空！")]
    [DynamicStringLength(typeof(LabProjectConsts), nameof(LabProjectConsts.MaxSpecimenNameLength), ErrorMessage = "标本名称最大长度不能超过{1}!")]
    public string SpecimenName { get; set; }

    /// <summary>
    /// 科室编码
    /// </summary>
    [DynamicStringLength(typeof(LabProjectConsts), nameof(LabProjectConsts.MaxExecDeptCodeLength), ErrorMessage = "科室编码最大长度不能超过{1}!")]
    public string ExecDeptCode { get; set; }

    /// <summary>
    /// 科室名称
    /// </summary>
    [DynamicStringLength(typeof(LabProjectConsts), nameof(LabProjectConsts.MaxExecDeptNameLength), ErrorMessage = "科室名称最大长度不能超过{1}!")]
    public string ExecDeptName { get; set; }

    /// <summary>
    /// 采集部位编码
    /// </summary>
    public string  SpecimenPartCode { get; set; }
    
    /// <summary>
    /// 采集部位名称
    /// </summary>
    public string  SpecimenPartName { get; set; }

    /// <summary>
    /// 排序号
    /// </summary>
    [Required(ErrorMessage = "排序号不能为空！")]
    public int Sort { get; set; }

    /// <summary>
    /// 单位
    /// </summary>
    [DynamicStringLength(typeof(LabProjectConsts), nameof(LabProjectConsts.MaxUnitLength), ErrorMessage = "单位最大长度不能超过{1}!")]
    public string Unit { get; set; }

    /// <summary>
    /// 价格
    /// </summary>
    [Required(ErrorMessage = "价格不能为空！")]
    public decimal Price { get; set; }

    /// <summary>
    /// 价格
    /// </summary>
    public decimal OtherPrice { get; set; }

    /// <summary>
    /// 容器编码
    /// </summary>
    [DynamicStringLength(typeof(LabProjectConsts), nameof(LabProjectConsts.MaxContainerCodeLength), ErrorMessage = "容器编码最大长度不能超过{1}!")]
    public string ContainerCode { get; set; }

    /// <summary>
    /// 容器名称
    /// </summary>
    [DynamicStringLength(typeof(LabProjectConsts), nameof(LabProjectConsts.MaxContainerNameLength), ErrorMessage = "容器名称最大长度不能超过{1}!")]
    public string ContainerName { get; set; }

    /// <summary>
    /// 是否启用
    /// </summary>
    public bool IsActive { get; set; }

    /// <summary>
    /// 平台标识
    /// </summary>
    public PlatformType PlatformType { get; set; }

    /// <summary>
    /// 分类编码和当前项目的编码组合
    /// </summary>
    public string CatalogAndProjectCode { get; set; }
}