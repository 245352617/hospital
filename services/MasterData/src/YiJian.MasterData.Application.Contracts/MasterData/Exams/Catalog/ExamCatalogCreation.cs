using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Validation;

namespace YiJian.MasterData;

/// <summary>
/// 检查目录 新增输入
/// </summary>
[Serializable]
public class ExamCatalogCreation
{
    /// <summary>
    /// 编码
    /// </summary>
    [Required(ErrorMessage = "编码不能为空！")]
    [DynamicStringLength(typeof(ExamCatalogConsts), nameof(ExamCatalogConsts.MaxCatalogCodeLength), ErrorMessage = "编码最大长度不能超过{1}!")]
    public string  CatalogCode { get; set; }

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
    /// 检查部位
    /// </summary>
    [DynamicStringLength(typeof(ExamCatalogConsts), nameof(ExamCatalogConsts.MaxExamPartCodeLength), ErrorMessage = "检查部位最大长度不能超过{1}!")]
    public string  ExamPartCode { get; set; }

    /// <summary>
    /// 排序号
    /// </summary>
    public int  IndexNo { get; set; }

    /// <summary>
    /// 拼音码
    /// </summary>
    [Required(ErrorMessage = "拼音码不能为空！")]
    [DynamicStringLength(typeof(ExamCatalogConsts), nameof(ExamCatalogConsts.MaxPyCodeLength), ErrorMessage = "拼音码最大长度不能超过{1}!")]
    public string  PyCode { get; set; }

    /// <summary>
    /// 五笔
    /// </summary>
    [DynamicStringLength(typeof(ExamCatalogConsts), nameof(ExamCatalogConsts.MaxWbCodeLength), ErrorMessage = "五笔最大长度不能超过{1}!")]
    public string  WbCode { get; set; }

    /// <summary>
    /// 位置编码
    /// </summary>
    [DynamicStringLength(typeof(ExamCatalogConsts), nameof(ExamCatalogConsts.MaxPositionCodeLength), ErrorMessage = "位置编码最大长度不能超过{1}!")]
    public string  PositionCode { get; set; }

    /// <summary>
    /// 位置
    /// </summary>
    [DynamicStringLength(typeof(ExamCatalogConsts), nameof(ExamCatalogConsts.MaxPositionNameLength), ErrorMessage = "位置最大长度不能超过{1}!")]
    public string  PositionName { get; set; }

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