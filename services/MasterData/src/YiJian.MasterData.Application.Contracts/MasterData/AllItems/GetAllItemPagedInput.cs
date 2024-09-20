using System;
using JetBrains.Annotations;
using YiJian.ECIS.ShareModel.Models;

namespace YiJian.MasterData.AllItems;

/// <summary>
/// 诊疗检查检验药品项目合集 分页排序输入
/// </summary>
[Serializable]
public class GetAllItemPagedInput : PageBase
{
    /// <summary>
    /// 过滤条件.
    /// </summary>
    public string Filter { get; set; }

    /// <summary>
    /// 排序字段.
    /// </summary>
    public string Sorting { get; set; }

    /// <summary>
    /// 类别编码
    /// </summary>
    public string CategoryCode { get; set; }
    
    /// <summary>
    /// 站点
    /// </summary>
    [CanBeNull]
    public string TypeCode { get; set; }
}
