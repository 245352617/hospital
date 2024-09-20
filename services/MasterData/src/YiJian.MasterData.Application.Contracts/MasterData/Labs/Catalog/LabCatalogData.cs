using System;
using System.Collections.Generic;

namespace YiJian.MasterData.Labs;

/// <summary>
/// 检验目录 读取输出
/// </summary>
[Serializable]
public class LabCatalogData
{
    public int Id { get; set; }

    /// <summary>
    /// 分类编码
    /// </summary>
    public string CatalogCode { get; set; }

    /// <summary>
    /// 分类编码
    /// </summary>
    public string CatalogName { get; set; }

    /// <summary>
    /// 执行科室编码
    /// </summary>
    public string ExecDeptCode { get; set; }

    /// <summary>
    /// 执行科室名称
    /// </summary>
    public string ExecDeptName { get; set; }

    /// <summary>
    /// 排序号
    /// </summary>
    public int Sort { get; set; }

    /// <summary>
    /// 拼音码
    /// </summary>
    public string PyCode { get; set; }

    /// <summary>
    /// 五笔
    /// </summary>
    public string WbCode { get; set; }

    /// <summary>
    /// 是否启用
    /// </summary>
    public bool IsActive { get; set; }

    /// <summary>
    /// 检验项目
    /// </summary>
    public List<LabProjectData> LabProjects { get; set; }
}
 