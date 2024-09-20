namespace HisApiMockService.Models.Medicals;

/// <summary>
/// 就诊类型 1门急诊,2住院, 3体检
/// </summary>
public enum  EPatientType:int
{
    /// <summary>
    /// 1 门急诊
    /// </summary>
    EmergencyDepartment = 1,

    /// <summary>
    /// 2 住院
    /// </summary>
    Hospitalize = 2,

    /// <summary>
    /// 3 体检
    /// </summary>
    Examination = 3,
}
