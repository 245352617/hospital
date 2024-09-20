namespace HisApiMockService.Models.Medicals;

/// <summary>
/// 限制标志
/// 默认值：1.医保用药  -1.未审批、全自费 2.地补目录属性记账 3.重疾目录属性记账
/// </summary>
public enum ERestrictedDrugs
{
    /**
-1：审批不通过、全自费
默认值：1：记账
2.地补目录属性记账
3.重疾目录属性记账
4：进口属性记账
5：国产属性记账
    */ 

    /// <summary>
    /// -1.未审批、全自费 
    /// </summary>
    QuanZifei = -1,

    /// <summary>
    /// 1.医保用药  (默认值：1：记账)
    /// </summary>
    YibaoYongyao = 1,

    /// <summary>
    /// 2.地补目录属性记账 
    /// </summary>
    DibuMuluShuxingJizhang = 2,

    /// <summary>
    /// 3.重疾目录属性记账
    /// </summary>
    ZhongjiMuluShuxingJizhang = 3,

    /// <summary>
    /// 4：进口属性记账
    /// </summary>
    JinkouShuxinJizhang = 4,

    /// <summary>
    /// 5：国产属性记账
    /// </summary>
    GuocanShuxinJizhang = 5,


}
