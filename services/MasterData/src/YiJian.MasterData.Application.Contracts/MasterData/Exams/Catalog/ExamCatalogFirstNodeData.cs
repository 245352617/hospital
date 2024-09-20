using System;
using System.Collections.Generic;

namespace YiJian.MasterData;

/// <summary>
/// 检查目录 读取输出
/// </summary>
[Serializable]
public class ExamCatalogFirstNodeData
{
    /// <summary>
    /// 编码 (FirstNodeCode 一级目录编码)
    /// </summary>
    public string CatalogCode { get; set; }

    /// <summary>
    /// 名称
    /// </summary>
    public string CatalogName { get; set; }

    /// <summary>
    /// 显示名称(申请单) eg: CT检查申请单、MRI检查申请单、X线检查申请单
    /// </summary>
    public string DisplayName { get; set; }

    public List<ExamCatalogDataV2> Children { get; set; }
}