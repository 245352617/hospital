namespace YiJian.ECIS.ShareModel.IMServiceEtos.Call;

/// <summary>
/// 叫号信息，语音播报，呼叫指定患者到指定诊室
/// </summary>
public class CallingEto
{
    /// <summary>
    /// 医生 ID
    /// </summary>
    public string DoctorId { get; set; }

    /// <summary>
    /// 医生名称
    /// </summary>
    public string DoctorName { get; set; }

    /// <summary>
    /// 诊室代码
    /// </summary>
    public string ConsultingRoomCode { get; set; }

    /// <summary>
    /// 诊室名称
    /// </summary>
    public string ConsultingRoomName { get; set; }

    /// <summary>
    /// 患者 ID
    /// </summary>
    public string PatientId { get; set; }

    /// <summary>
    /// 患者名称
    /// </summary>
    public string PatientName { get; set; }

    /// <summary>
    /// 科室代码
    /// </summary>
    public string DepartmentCode { get; set; }

    /// <summary>
    /// 科室名称
    /// </summary>
    public string DepartmentName { get; set; }

    /// <summary>
    /// 分诊科室编码
    /// </summary>
    public string TriageDept { get; set; }

    /// <summary>
    /// 分诊科室名称
    /// </summary>
    public string TriageDeptName { get; set; }

    /// <summary>
    /// 排队号
    /// </summary>
    public string CallingSn { get; set; }

    /// <summary>
    /// 就诊号
    /// 挂号之后生成就诊号
    /// </summary>
    public string RegisterNo { get; set; }

    /// <summary>
    /// 叫号时间
    /// </summary>
    public DateTime? LastCalledTime { get; set; }

    /// <summary>
    /// 实际分诊级别
    /// </summary>
    public string ActTriageLevel { get; set; }

    /// <summary>
    /// 实际分诊级别名称
    /// </summary>
    public string ActTriageLevelName { get; set; }
}
