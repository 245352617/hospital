namespace HisApiMockService.Models.Advices;

/// <summary>
/// 1.作废（recordType=1，3时使用）
/// 2.结束就诊 （ recordType=2）
/// 3.暂挂（ recordType=2）
/// </summary>
public enum ErecordState
{
    /// <summary>
    /// 1.作废（recordType=1，3时使用）
    /// </summary>
    Invalid = 1,

    /// <summary>
    /// 2.结束就诊 （ recordType=2）
    /// </summary>
    End = 2,

    /// <summary>
    /// 3.暂挂（ recordType=2）
    /// </summary>
    Pause = 3,

}
