namespace HisApiMockService.Models.Medicals;

/// <summary>
///  1:RIS放射;2:US超声; 3:ES内镜;4:病理PAT; 5:心电ECG;
/// </summary>
public enum EExamType
{
    /// <summary>
    /// 1:RIS放射
    /// </summary>
    RIS = 1,

    /// <summary>
    /// 2:US超声
    /// </summary>
    US = 2,

    /// <summary>
    /// 3:ES内镜
    /// </summary>
    ES = 3,

    /// <summary>
    /// 4:病理PAT
    /// </summary>
    PAT = 4,

    /// <summary>
    /// 5:心电ECG
    /// </summary>
    ECG = 5,
}
