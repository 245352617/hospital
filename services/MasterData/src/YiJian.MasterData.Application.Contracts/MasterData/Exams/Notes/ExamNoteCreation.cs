using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Validation;

namespace YiJian.MasterData;


/// <summary>
/// 检查申请注意事项 新增输入
/// </summary>
[Serializable]
public class ExamNoteCreation
{
    /// <summary>
    /// 注意事项编码
    /// </summary>
    [Required(ErrorMessage = "注意事项编码不能为空！")]
    [DynamicStringLength(typeof(ExamNoteConsts), nameof(ExamNoteConsts.MaxNoteCodeLength), ErrorMessage = "注意事项编码最大长度不能超过{1}!")]
    public string  NoteCode { get; set; }

    /// <summary>
    /// 注意事项名称
    /// </summary>
    [Required(ErrorMessage = "注意事项名称不能为空！")]
    [DynamicStringLength(typeof(ExamNoteConsts), nameof(ExamNoteConsts.MaxNoteNameLength), ErrorMessage = "注意事项名称最大长度不能超过{1}!")]
    public string  NoteName { get; set; }

    /// <summary>
    /// 执行科室编码
    /// </summary>
    [DynamicStringLength(typeof(ExamNoteConsts), nameof(ExamNoteConsts.MaxExecDeptCodeLength), ErrorMessage = "执行科室编码最大长度不能超过{1}!")]
    public string  ExecDeptCode { get; set; }

    /// <summary>
    /// 执行科室名称
    /// </summary>
    [DynamicStringLength(typeof(ExamNoteConsts), nameof(ExamNoteConsts.MaxExecDeptNameLength), ErrorMessage = "执行科室名称最大长度不能超过{1}!")]
    public string  ExecDeptName { get; set; }

    /// <summary>
    /// 显示名称(申请单) eg: CT检查申请单、MRI检查申请单、X线检查申请单
    /// </summary>
    [Required(ErrorMessage = "显示名称(申请单)不能为空！")]
    [DynamicStringLength(typeof(ExamNoteConsts), nameof(ExamNoteConsts.MaxDisplayNameLength), ErrorMessage = "显示名称(申请单)最大长度不能超过{1}!")]
    public string  DisplayName { get; set; }

    /// <summary>
    /// 检查申请描述模板
    /// </summary>
    [Required(ErrorMessage = "检查申请描述模板不能为空！")]
    [DynamicStringLength(typeof(ExamNoteConsts), nameof(ExamNoteConsts.MaxDescTemplateLength), ErrorMessage = "检查申请描述模板最大长度不能超过{1}!")]
    public string  DescTemplate { get; set; }

    /// <summary>
    /// 是否启用
    /// </summary>
    public bool  IsActive { get; set; }
}