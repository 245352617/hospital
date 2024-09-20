namespace YiJian.ECIS.ShareModel.Etos.DoctorsAdvices;

/// <summary>
/// 创建院内会诊诊疗模型
/// </summary>
public class ConsultationRecordTreatEto
{
    /// <summary>
    /// 患者唯一Id
    /// </summary>
    public Guid PI_ID { get; set; }

    /// <summary>
    /// 患者编号
    /// </summary>   
    public string PatientNo { get; set; }

    /// <summary>
    /// 患者名称
    /// </summary>   
    public string PatientName { get; set; }

    /// <summary>
    /// 科室编号
    /// </summary>
    public string DeptCode { get; set; }

    /// <summary>
    /// 科室名称
    /// </summary>
    public string DeptName { get; set; }

    /// <summary>
    /// 医生编号
    /// </summary>   
    public string DoctorCode { get; set; }

    /// <summary>
    /// 医生名称
    /// </summary>   
    public string DoctorName { get; set; }

    /// <summary>
    /// 诊断信息
    /// </summary>
    public string Diagnosis { get; set; }

}
