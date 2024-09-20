using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Validation;

namespace YiJian.MasterData;

/// <summary>
/// 检查目录 修改输入
/// </summary>
[Serializable]
public class ExamCatalogUpdate
{ 
    public int Id { get; set; }
    
    /// <summary>
    /// 名称
    /// </summary>
    [Required(ErrorMessage = "名称不能为空！")]
    [DynamicStringLength(typeof(ExamCatalogConsts), nameof(ExamCatalogConsts.MaxCatalogNameLength), ErrorMessage = "名称最大长度不能超过{1}!")]
    public string  CatalogName { get; set; }

    /// <summary>
    /// 显示名称(申请单) eg: CT检查申请单、MRI检查申请单、X线检查申请单
    /// </summary>
    [Required(ErrorMessage = "显示名称(申请单)不能为空！")]
    [DynamicStringLength(typeof(ExamCatalogConsts), nameof(ExamCatalogConsts.MaxDisplayNameLength), ErrorMessage = "显示名称(申请单)最大长度不能超过{1}!")]
    public string  DisplayName { get; set; }

    /// <summary>
    /// 科室编码
    /// </summary>
    [DynamicStringLength(typeof(ExamCatalogConsts), nameof(ExamCatalogConsts.MaxDeptCodeLength), ErrorMessage = "科室编码最大长度不能超过{1}!")]
    public string  DeptCode { get; set; }

    /// <summary>
    /// 科室名称
    /// </summary>
    [DynamicStringLength(typeof(ExamCatalogConsts), nameof(ExamCatalogConsts.MaxDeptNameLength), ErrorMessage = "科室名称最大长度不能超过{1}!")]
    public string  DeptName { get; set; }
    

    /// <summary>
    /// 排序号
    /// </summary>
    public int  Sort { get; set; }

    /// <summary>
    /// 执行机房编码
    /// </summary>
    [DynamicStringLength(typeof(ExamCatalogConsts), nameof(ExamCatalogConsts.MaxRoomCodeLength), ErrorMessage = "执行机房编码最大长度不能超过{1}!")]
    public string  RoomCode { get; set; }

    /// <summary>
    /// 执行机房
    /// </summary>
    [DynamicStringLength(typeof(ExamCatalogConsts), nameof(ExamCatalogConsts.MaxRoomNameLength), ErrorMessage = "执行机房最大长度不能超过{1}!")]
    public string  RoomName { get; set; }

    /// <summary>
    /// 是否启用
    /// </summary>
    public bool  IsActive { get; set; }
}