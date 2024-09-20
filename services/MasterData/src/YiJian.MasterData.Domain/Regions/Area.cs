using System;
using Volo.Abp.Domain.Entities;

namespace YiJian.MasterData.Regions;

/// <summary>
/// 描述：全国地区编码(目前狂犬报卡用到)
/// 创建人： yangkai
/// 创建时间：2023/2/10 17:36:17
/// </summary>
public class Area : Entity<Guid>
{
    /// <summary>
    /// 编码
    /// </summary>
    public string AreaCode { get; set; }

    /// <summary>
    /// 地区名称
    /// </summary>
    public string AreaName { get; set; }

    /// <summary>
    /// 地区全称
    /// </summary>
    public string AreaFullName { get; set; }

    /// <summary>
    /// 拼音代码
    /// </summary>
    public string PyCode { get; set; }
}
