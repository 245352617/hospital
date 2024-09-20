namespace HisApiMockService.Models.Medicals;

/// <summary>
/// 0 西药 1 中成药 2 中草药
/// 不同药品类型不能在同一张处方
/// </summary>
public enum EDrugType
{
    /// <summary>
    /// 0 西药
    /// </summary>
    XiYao = 0,

    /// <summary>
    /// 1 中成药
    /// </summary>
    ZhongChengYao = 1,

    /// <summary>
    /// 2 中草药
    /// </summary>
    ZhongCaoYao = 2, 
}