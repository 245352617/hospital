namespace HisApiMockService.Models.Medicals;

/// <summary>
/// 患者类型代码--CV09.00.404 1.门诊 2.急诊 3.住院 9.体检（其他）
/// </summary>
public enum EVisitType : int
{
    /// <summary>
    /// 1.门诊
    /// </summary>
    Outpatient = 1,

    /// <summary>
    /// 2.急诊
    /// </summary>
    Emergency = 2,


    /// <summary>
    /// 3.住院
    /// </summary>
    Hospitalize = 3,

    /// <summary>
    /// 9.体检（其他）
    /// </summary>
    Examination = 3,

}
