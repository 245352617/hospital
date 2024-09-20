using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Domain.Entities.Auditing;
using YiJian.ECIS;
using YiJian.ECIS.ShareModel.Models;

namespace YiJian.MasterData;


/// <summary>
/// 检查目录
/// </summary>
[Comment("检查目录")]
public class ExamCatalog : FullAuditedAggregateRoot<int>, IIsActive
{
    #region Properties

    /// <summary>
    /// 一级目录编码
    /// </summary>
    [StringLength(20)]
    [Comment("编码")]
    public string FirstNodeCode { get; set; }

    /// <summary>
    ///一级目录 名称
    /// </summary> 
    [StringLength(50)]
    [Comment("名称")]
    public string FirstNodeName { get; set; }

    /// <summary>
    /// 二级目录编码
    /// </summary>
    [Required]
    [StringLength(20)]
    [Comment("编码")]
    public string CatalogCode { get; set; }

    /// <summary>
    /// 二级目录名称
    /// </summary>
    [Required]
    [StringLength(50)]
    [Comment("名称")]
    public string CatalogName { get; set; }



    /// <summary>
    /// 显示名称(申请单) eg: CT检查申请单、MRI检查申请单、X线检查申请单
    /// </summary>
    [Required]
    [StringLength(50)]
    [Comment("显示名称(申请单) eg: CT检查申请单、MRI检查申请单、X线检查申请单")]
    public string DisplayName { get; set; }

    /// <summary>
    /// 科室编码
    /// </summary>
    [StringLength(20)]
    [Comment("科室编码")]
    public string DeptCode { get; set; }

    /// <summary>
    /// 科室名称
    /// </summary>
    [StringLength(50)]
    [Comment("科室名称")]
    public string DeptName { get; set; }

    /// <summary>
    /// 排序号
    /// </summary>
    [Comment("排序号")]
    public int Sort { get; set; }

    /// <summary>
    /// 拼音码
    /// </summary>
    [Required]
    [StringLength(20)]
    [Comment("拼音码")]
    public string PyCode { get; set; }

    /// <summary>
    /// 五笔
    /// </summary>
    [StringLength(20)]
    [Comment("五笔")]
    public string WbCode { get; set; }

    /// <summary>
    /// 执行机房编码
    /// </summary>
    [StringLength(20)]
    [Comment("执行机房编码")]
    public string RoomCode { get; set; }

    /// <summary>
    /// 执行机房
    /// </summary>
    [StringLength(50)]
    [Comment("执行机房名称")]
    public string RoomName { get; set; }

    /// <summary>
    /// 是否启用
    /// </summary>
    [Comment("是否启用")]
    public bool IsActive { get; set; } = true;

    #endregion

    #region Modify
    /// <summary>
    /// 修改
    /// </summary>
    /// <param name="name">名称</param>
    /// <param name="displayName">显示名称(申请单) eg: CT检查申请单、MRI检查申请单、X线检查申请单</param>
    /// <param name="deptCode">科室编码</param>
    /// <param name="deptName">科室名称</param>
    /// <param name="sort">排序号</param>
    /// <param name="roomCode">执行机房编码</param>
    /// <param name="room">执行机房</param>
    /// <param name="isActive">是否启用</param>
    public void Modify([NotNull] string name,// 名称
        [NotNull] string displayName, // 显示名称(申请单) eg: CT检查申请单、MRI检查申请单、X线检查申请单
        string deptCode,              // 科室编码
        string deptName,              // 科室名称
        int sort,                  // 排序号
        string roomCode,              // 执行机房编码
        string room,                  // 执行机房
        bool isActive                 // 是否启用
        )
    {

        //名称
        CatalogName = name;

        //显示名称(申请单) eg: CT检查申请单、MRI检查申请单、X线检查申请单
        DisplayName = Check.NotNull(displayName, "显示名称(申请单)", ExamCatalogConsts.MaxDisplayNameLength);

        //科室编码
        DeptCode = Check.Length(deptCode, "科室编码", ExamCatalogConsts.MaxDeptCodeLength);

        //科室名称
        DeptName = Check.Length(deptName, "科室名称", ExamCatalogConsts.MaxDeptNameLength);

        //排序号
        Sort = sort;

        //拼音码
        PyCode = Check.NotNull(name.FirstLetterPY(), "拼音码", ExamCatalogConsts.MaxPyCodeLength);

        //五笔
        WbCode = Check.Length(name.FirstLetterWB(), "五笔", ExamCatalogConsts.MaxWbCodeLength);

        //执行机房编码
        RoomCode = Check.Length(roomCode, "执行机房编码", ExamCatalogConsts.MaxRoomCodeLength);

        //执行机房
        RoomName = room;

        //是否启用
        IsActive = isActive;

    }
    #endregion
}