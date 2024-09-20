using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Domain.Entities.Auditing;
using YiJian.ECIS.ShareModel.Models;

namespace YiJian.MasterData.Labs;


/// <summary>
/// 检验目录
/// </summary>
[Comment("检验目录")]
public class LabCatalog : FullAuditedAggregateRoot<int>, IIsActive
{
    #region Properties

    /// <summary>
    /// 分类编码
    /// </summary>
    [Required]
    [StringLength(20)]
    [Comment("分类编码")]
    public string CatalogCode { get; set; }

    /// <summary>
    /// 分类名称
    /// </summary>
    [Required]
    [StringLength(200)]
    [Comment("分类名称")]
    public string CatalogName { get; set; }

    /// <summary>
    /// 执行科室编码
    /// </summary>
    [StringLength(20)]
    [Comment("执行科室编码")]
    public string ExecDeptCode { get; set; }

    /// <summary>
    /// 执行科室名称
    /// </summary>
    [StringLength(50)]
    [Comment("执行科室名称")]
    public string ExecDeptName { get; set; }

    /// <summary>
    /// 排序号
    /// </summary>
    [Required]
    [Comment("排序号")]
    public int Sort { get; set; }

    /// <summary>
    /// 拼音码
    /// </summary>
    [Required]
    [StringLength(50)]
    [Comment("拼音码")]
    public string PyCode { get; set; }

    /// <summary>
    /// 五笔
    /// </summary>
    [StringLength(50)]
    [Comment("五笔")]
    public string WbCode { get; set; }

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
    /// <param name="name">分类编码</param>
    /// <param name="deptCode">科室编码</param>
    /// <param name="dept">科室</param>
    /// <param name="sort">排序号</param>
    /// <param name="isActive">是否启用</param>
    public void Modify([NotNull] string name, // 分类编码
        string deptCode, // 科室编码
        string dept, // 科室
        int sort, // 排序号
        bool isActive // 是否启用
    )
    {
        //分类编码
        CatalogName = name;

        //科室编码
        ExecDeptCode = deptCode;

        //科室
        ExecDeptName = dept;

        //排序号
        Sort = sort;

        //拼音码
        PyCode = name.FirstLetterPY();

        //五笔
        WbCode = name.FirstLetterWB();

        //是否启用
        IsActive = isActive;
    }

    #endregion
}