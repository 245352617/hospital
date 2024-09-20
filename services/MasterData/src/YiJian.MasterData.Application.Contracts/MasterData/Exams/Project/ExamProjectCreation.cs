using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Validation;
using YiJian.ECIS.ShareModel.Enums;

namespace YiJian.MasterData.Exams;


/// <summary>
/// 检查申请项目 新增输入
/// </summary>
[Serializable]
public class ExamProjectCreation
{
    /// <summary>
    /// 编码
    /// </summary>
    [Required(ErrorMessage = "编码不能为空！")]
    [DynamicStringLength(typeof(ExamProjectConsts), nameof(ExamProjectConsts.MaxCodeLength), ErrorMessage = "编码最大长度不能超过{1}!")]
    public string  ProjectCode { get; set; }

    /// <summary>
    /// 名称
    /// </summary>
    [Required(ErrorMessage = "名称不能为空！")]
    [DynamicStringLength(typeof(ExamProjectConsts), nameof(ExamProjectConsts.MaxNameLength), ErrorMessage = "名称最大长度不能超过{1}!")]
    public string  ProjectName { get; set; }

    /// <summary>
    /// 分类编码
    /// </summary>
    [Required(ErrorMessage = "分类编码不能为空！")]
    [DynamicStringLength(typeof(ExamProjectConsts), nameof(ExamProjectConsts.MaxCatalogCodeLength), ErrorMessage = "分类编码最大长度不能超过{1}!")]
    public string  CatalogCode { get; set; }

    /// <summary>
    /// 目录名称
    /// </summary>
    [Required(ErrorMessage = "目录名称不能为空！")]
    [DynamicStringLength(typeof(ExamProjectConsts), nameof(ExamProjectConsts.MaxCatalogNameLength), ErrorMessage = "目录名称最大长度不能超过{1}!")]
    public string  CatalogName { get; set; }

    /// <summary>
    /// 排序号
    /// </summary>
    public int  Sort { get; set; }

    /// <summary>
    /// 检查部位编码
    /// </summary>
    [DynamicStringLength(typeof(ExamProjectConsts), nameof(ExamProjectConsts.MaxExamPartCodeLength), ErrorMessage = "检查部位编码最大长度不能超过{1}!")]
    public string  PartCode { get; set; }
    /// <summary>
    /// 检查部位名称
    /// </summary>
    [DynamicStringLength(typeof(ExamProjectConsts), nameof(ExamProjectConsts.MaxExamPartNameLength), ErrorMessage = "检查部位名称最大长度不能超过{1}!")]
    public string  PartName { get; set; }

    /// <summary>
    /// 单位
    /// </summary>
    [DynamicStringLength(typeof(ExamProjectConsts), nameof(ExamProjectConsts.MaxUnitLength), ErrorMessage = "单位最大长度不能超过{1}!")]
    public string  Unit { get; set; }

    /// <summary>
    /// 价格
    /// </summary>
    public decimal  Price { get; set; }

    /// <summary>
    /// 科室编码
    /// </summary>
    [DynamicStringLength(typeof(ExamProjectConsts), nameof(ExamProjectConsts.MaxExecDeptCodeLength), ErrorMessage = "科室编码最大长度不能超过{1}!")]
    public string  ExecDeptCode { get; set; }

    /// <summary>
    /// 科室名称
    /// </summary>
    [DynamicStringLength(typeof(ExamProjectConsts), nameof(ExamProjectConsts.MaxExecDeptNameLength), ErrorMessage = "科室名称最大长度不能超过{1}!")]
    public string  ExecDeptName { get; set; }
    
    /// <summary>
    /// 执行机房编码
    /// </summary>
    [DynamicStringLength(typeof(ExamProjectConsts), nameof(ExamProjectConsts.MaxRoomCodeLength), ErrorMessage = "执行机房编码最大长度不能超过{1}!")]
    public string  RoomCode { get; set; }

    /// <summary>
    /// 执行机房名称
    /// </summary>
    [DynamicStringLength(typeof(ExamProjectConsts), nameof(ExamProjectConsts.MaxRoomNameLength), ErrorMessage = "执行机房名称最大长度不能超过{1}!")]
    public string  RoomName { get; set; }

    /// <summary>
    /// 是否启用
    /// </summary>
    public bool  IsActive { get; set; }

    /// <summary>
    /// 平台标识
    /// </summary>
    public PlatformType  PlatformType { get; set; }
}