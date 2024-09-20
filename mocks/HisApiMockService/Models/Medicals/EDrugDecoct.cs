namespace HisApiMockService.Models.Medicals;

/// <summary>
/// 2.6 草药煎法
/// 
/// 2   水煎
/// 3   开水冲服
/// 4	文武火
/// 5  	大火
/// 6  	文火
/// 7  	猛火
/// 11  喷伤口
/// 12	冲化
/// 13	焗
/// 15	三碗水煲成一碗
/// 16	四碗水煲成一碗
/// 18	代煎药 每袋 ML 日 袋 温服
/// 22	包服
/// 23	包煎
/// 24	水冲外洗
/// 25	免煎
/// 27	轻煎
/// 28	平煎
/// 29	浓煎
/// 30	炒热外敷
/// 32	打粉
/// 33	加开水1000毫升
/// 34	加开水1500毫升
/// 35	加开水2000毫升
/// 36	加开水3000毫升
/// </summary>
public enum EDrugDecoct
{
    /// <summary>
    /// 水煎 = 2,
    /// </summary>
    Shuijian = 2,
    /// <summary>
    /// 开水冲服 = 3,
    /// </summary>
    KaishuiChongfu = 3,
    /// <summary>
    /// 文武火 = 4,
    /// </summary>
    WenwuHuo = 4,

    /// <summary>
    /// 大火 = 5,
    /// </summary>
    DaHuo = 5,

    /// <summary>
    /// 文火 = 6,
    /// </summary>
    WenHuo = 6,

    /// <summary>
    /// 猛火 = 7,
    /// </summary>
    MengHuo = 7,

    /// <summary>
    /// 喷伤口 = 11,
    /// </summary>
    PenShangkou = 11,

    /// <summary>
    /// 冲化 = 12,
    /// </summary>
    ChongHua = 12,

    /// <summary>
    /// 焗 = 13,
    /// </summary>
    ju = 13,

    /// <summary>
    /// 三碗水煲成一碗 = 15,
    /// </summary>
    SanwanShuiBaoChengYiwan = 15,

    /// <summary>
    /// 四碗水煲成一碗 = 16,
    /// </summary>
    SiwanShuiBaoChengYiwan = 16,

    /// <summary>
    /// 代煎药每袋ML日袋温服 = 18,
    /// </summary>
    DaiJianyao = 18,

    /// <summary>
    /// 包服 = 22,
    /// </summary>
    BaoFu = 22,

    /// <summary>
    /// 包煎 = 23,
    /// </summary>
    BaoJian = 23,

    /// <summary>
    /// 水冲外洗 = 24,
    /// </summary>
    ShuichongWaixi = 24,

    /// <summary>
    /// 免煎 = 25,
    /// </summary>
    MianJian = 25,

    /// <summary>
    /// 轻煎 = 27,
    /// </summary>
    QingJian = 27,

    /// <summary>
    /// 平煎 = 28,
    /// </summary>
    PingJian = 28,

    /// <summary>
    /// 浓煎 = 29,
    /// </summary>
    NongJian = 29,

    /// <summary>
    /// 炒热外敷 = 30,
    /// </summary>
    JianreWaifu = 30,

    /// <summary>
    /// 打粉 = 32,
    /// </summary>
    DaFen = 32,

    /// <summary>
    /// 加开水1000毫升 = 33,
    /// </summary>
    JiaKaishui1000ml = 33,

    /// <summary>
    /// 加开水1500毫升 = 34,
    /// </summary>
    JiaKaishui1500ml = 34,

    /// <summary>
    /// 加开水2000毫升 = 35,
    /// </summary>
    JiaKaishui2000ml = 35,

    /// <summary>
    /// 加开水3000毫升 = 36,
    /// </summary>
    JiaKaishui3000ml = 36, 
}
