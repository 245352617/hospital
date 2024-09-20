using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Validation;

namespace YiJian.MasterData.Labs.Position;


/// <summary>
/// 检验标本采集部位 修改输入
/// </summary>
[Serializable]
public class LabSpecimenPositionUpdate
{ 
    public int Id { get; set; }

    /// <summary>
    /// 标本编码
    /// </summary>
    [DynamicStringLength(typeof(LabSpecimenPositionConsts), nameof(LabSpecimenPositionConsts.MaxSpecimenCodeLength), ErrorMessage = "标本编码最大长度不能超过{1}!")]
    public string  SpecimenCode { get; set; }

    /// <summary>
    /// 标本名称
    /// </summary>
    [DynamicStringLength(typeof(LabSpecimenPositionConsts), nameof(LabSpecimenPositionConsts.MaxSpecimenNameLength), ErrorMessage = "标本名称最大长度不能超过{1}!")]
    public string  SpecimenName { get; set; }

    /// <summary>
    /// 采集部位编码
    /// </summary>
    [Required(ErrorMessage = "采集部位编码不能为空！")]
    [DynamicStringLength(typeof(LabSpecimenPositionConsts), nameof(LabSpecimenPositionConsts.MaxPositionCodeLength), ErrorMessage = "采集部位编码最大长度不能超过{1}!")]
    public string  SpecimenPartCode { get; set; }

    /// <summary>
    /// 采集部位名称
    /// </summary>
    [Required(ErrorMessage = "采集部位名称不能为空！")]
    [DynamicStringLength(typeof(LabSpecimenPositionConsts), nameof(LabSpecimenPositionConsts.MaxPositionNameLength), ErrorMessage = "采集部位名称最大长度不能超过{1}!")]
    public string  SpecimenPartName { get; set; }

    /// <summary>
    /// 排序号
    /// </summary>
    [Required(ErrorMessage = "排序号不能为空！")]
    public int  Sort { get; set; }
    
    public bool  IsActive { get; set; }
}