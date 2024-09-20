using System;
using YiJian.ECIS.ShareModel.Enums;
using YiJian.ECIS.ShareModel.Models;

namespace YiJian.MasterData.Medicines;

[Serializable]
public class GetMedicineInput : PageBase
{
    /// <summary>
    /// 名称或拼音
    /// </summary>
    public string NameOrPyCode { get; set; }
    /// <summary>
    /// 分类
    /// </summary>
    public string Category { get; set; }
    /// <summary>
    /// 是否急救药
    /// </summary>
    public bool? IsEmergency { get; set; }
    
    /// <summary>
    /// 平台标识   1：院前  0：急诊
    /// </summary>
    public PlatformType PlatformType { get; set; }
}
