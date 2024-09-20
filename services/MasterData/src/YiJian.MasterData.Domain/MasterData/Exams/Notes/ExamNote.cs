using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Domain.Entities.Auditing;
using YiJian.ECIS.ShareModel.Models;

namespace YiJian.MasterData;

/// <summary>
/// 检查申请注意事项
/// </summary>
[Comment("检查申请注意事项")]
public class ExamNote : FullAuditedAggregateRoot<int>, IIsActive
{
    public void Update(string noteName, string displayName, string descTemplate)
    {
        NoteName = noteName;
        DisplayName = displayName;
        DescTemplate = descTemplate;
    }

    #region Properties

    /// <summary>
    /// 注意事项编码
    /// </summary>
    [Required]
    [StringLength(20)]
    [Comment("注意事项编码")]
    public string NoteCode { get; set; }

    /// <summary>
    /// 注意事项名称
    /// </summary>
    [Required]
    [StringLength(500)]
    [Comment("注意事项名称")]
    public string NoteName { get; set; }

    /// <summary>
    /// 执行科室编码
    /// </summary>
    [StringLength(20)]
    [Comment("执行科室编码")]
    public string ExecDeptCode { get; set; }

    /// <summary>
    /// 执行科室名称
    /// </summary>
    [StringLength(2000)]
    [Comment("执行科室名称")]
    public string ExecDeptName { get; set; }

    /// <summary>
    /// 显示名称(申请单) eg: CT检查申请单、MRI检查申请单、X线检查申请单
    /// </summary>
    [Required]
    [StringLength(2000)]
    [Comment("显示名称(申请单) eg: CT检查申请单、MRI检查申请单、X线检查申请单")]
    public string DisplayName { get; set; }

    /// <summary>
    /// 检查申请描述模板
    /// </summary>
    [Required]
    [StringLength(2000)]
    [Comment("检查申请描述模板")]
    public string DescTemplate { get; set; }

    /// <summary>
    /// 是否启用
    /// </summary>
    [Comment("是否启用")]
    public bool IsActive { get; set; } = true;

    #endregion
}