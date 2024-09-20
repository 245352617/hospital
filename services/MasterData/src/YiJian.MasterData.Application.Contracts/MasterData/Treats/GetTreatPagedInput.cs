using System;
using YiJian.ECIS.ShareModel.Enums;
using YiJian.ECIS.ShareModel.Models;

namespace YiJian.MasterData.Treats;


/// <summary>
/// 诊疗项目字典 分页排序输入
/// </summary>
[Serializable]
public class GetTreatPagedInput : PageBase
{
    /// <summary>
    /// 分类编码
    /// </summary>
    public string CategoryCode { get; set; }

    /// <summary>
    /// 过滤条件.
    /// </summary>
    public string Filter { get; set; }

    /// <summary>
    /// 排序字段.
    /// </summary>
    public string Sorting { get; set; }

    /// <summary>
    /// 平台标识
    /// </summary>
    public PlatformType PlatformType { get; set; }
}