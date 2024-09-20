using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Validation;

namespace YiJian.MasterData.Labs;

/// <summary>
/// 检验目录 修改输入
/// </summary>
[Serializable]
public class LabCatalogUpdate
{ 
    public int Id { get; set; }
    
    /// <summary>
    /// 分类编码
    /// </summary>
    [Required(ErrorMessage = "分类编码不能为空！")]
    [DynamicStringLength(typeof(LabCatalogConsts), nameof(LabCatalogConsts.MaxCatalogNameLength), ErrorMessage = "分类编码最大长度不能超过{1}!")]
    public string  CatalogName { get; set; }

    /// <summary>
    /// 执行科室编码
    /// </summary>
    [DynamicStringLength(typeof(LabCatalogConsts), nameof(LabCatalogConsts.MaxExecDeptCodeLength), ErrorMessage = "执行科室编码最大长度不能超过{1}!")]
    public string  ExecDeptCode { get; set; }

    /// <summary>
    /// 执行科室名称
    /// </summary>
    [DynamicStringLength(typeof(LabCatalogConsts), nameof(LabCatalogConsts.MaxExecDeptNameLength), ErrorMessage = "执行科室名称最大长度不能超过{1}!")]
    public string  ExecDeptName { get; set; }

    /// <summary>
    /// 排序号
    /// </summary>
    [Required(ErrorMessage = "排序号不能为空！")]
    public int  Sort { get; set; }
    
    

    /// <summary>
    /// 是否启用
    /// </summary>
    public bool  IsActive { get; set; }
}