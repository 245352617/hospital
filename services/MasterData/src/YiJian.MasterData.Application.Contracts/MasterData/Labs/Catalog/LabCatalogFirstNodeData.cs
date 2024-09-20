using System;
using System.Collections.Generic;

namespace YiJian.MasterData.Labs;


/// <summary>
/// 检验目录 读取输出
/// </summary>
[Serializable]
public class LabCatalogFirstNodeData
{
    /// <summary>
    /// 编码
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

    public List<LabCatalogData> Children { get; set; }
}
 