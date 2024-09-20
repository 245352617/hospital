namespace HisApiMockService.Models.Medicals;

/// <summary>
/// 1	口服 每日一剂分两次服
/// 3	口服 每日一剂分三次服
/// 4	睡前服
/// 5	含服
/// 6	餐前服
/// 7	餐后服
/// 8	餐中服
/// 11	研碎
/// 12	-
/// 13	空腹口服
/// 19	外用
/// 20	灌肠
/// 21	分两剂外洗患处
/// </summary>
public enum EDrugAdministration
{
    /// <summary>
    /// 1	口服 每日一剂分两次服
    /// </summary>
    Koufu_1_1_2 = 1,

    /// <summary>
    /// 3	口服 每日一剂分三次服
    /// </summary>
    Koufu_1_1_3 = 3,

    /// <summary>
    /// 4	睡前服
    /// </summary>
    ShuiqianFu = 4,

    /// <summary>
    /// 5	含服
    /// </summary>
    HanFu = 5,

    /// <summary>
    /// 6	餐前服
    /// </summary>
    CanqianFu = 6,

    /// <summary>
    /// 7	餐后服
    /// </summary>
    CanhouFu = 7,

    /// <summary>
    /// 8	餐中服
    /// </summary>
    CanzhongFu = 8,

    /// <summary>
    /// 11	研碎
    /// </summary>
    Yansui = 11,
    /// <summary>
    /// 未设置
    /// </summary>
    NotSet = 12,

    /// <summary>
    /// 13	空腹口服
    /// </summary>
    KongfuKoufu = 13,

    /// <summary>
    /// 19	外用
    /// </summary>
    Waiyong = 19,

    /// <summary>
    /// 20	灌肠
    /// </summary>
    Guanchang = 20,

    /// <summary>
    /// 21	分两剂外洗患处
    /// </summary>
    FenLiangciWaixiHuanchu = 21,
}
