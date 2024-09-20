using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Validation;

namespace YiJian.MasterData.Labs.Container;

/// <summary>
/// 容器编码 修改输入
/// </summary>
[Serializable]
public class LabContainerUpdate
{ 
    public int Id { get; set; }

    /// <summary>
    /// 容器编码
    /// </summary>
    [Required(ErrorMessage = "容器编码不能为空！")]
    [DynamicStringLength(typeof(LabContainerConsts), nameof(LabContainerConsts.MaxContainerCodeLength), ErrorMessage = "容器编码最大长度不能超过{1}!")]
    public string  ContainerCode { get; set; }

    /// <summary>
    /// 容器名称
    /// </summary>
    [Required(ErrorMessage = "容器名称不能为空！")]
    [DynamicStringLength(typeof(LabContainerConsts), nameof(LabContainerConsts.MaxContainerNameLength), ErrorMessage = "容器名称最大长度不能超过{1}!")]
    public string  ContainerName { get; set; }

    /// <summary>
    /// 容器颜色
    /// </summary>
    [Required(ErrorMessage = "容器颜色不能为空！")]
    [DynamicStringLength(typeof(LabContainerConsts), nameof(LabContainerConsts.MaxContainerColorLength), ErrorMessage = "容器颜色最大长度不能超过{1}!")]
    public string  ContainerColor { get; set; }

    public bool  IsActive { get; set; }
}