using System;
using YiJian.ECIS.ShareModel.Enums;
using YiJian.ECIS.ShareModel.Models;

namespace YiJian.MasterData;

/// <summary>
/// 药品字典 分页排序输入
/// </summary>
[Serializable]
public class GetMedicinePagedInput : PageBase
{
    /// <summary>
    /// 名称或拼音
    /// </summary>
    public string Filter { get; set; }
    /// <summary>
    /// 分类
    /// </summary>
    public string Category { get; set; }
    /// <summary>
    /// 是否急救药
    /// </summary>
    public bool? IsEmergency { get; set; }
    /// <summary>
    /// 药房代码
    /// </summary>
    public string PharmacyCode { get; set; }
    /// <summary>
    /// 平台标识   1：院前  0：急诊
    /// </summary>
    public PlatformType PlatformType { get; set; }
    /// <summary>
    /// 精神药  0 普通,1 毒,2 麻，3 精神
    /// </summary>
    public int? ToxicLevel { get; set; }
    
    /// <summary>
    /// 是否启用
    /// </summary>
    public bool? IsActive { get; set; }

    /// <summary>
    /// 1.急诊 0.普通
    /// </summary>
    public int EmergencySign { get;set;}

}
