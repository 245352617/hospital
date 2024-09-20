namespace YiJian.ECIS.ShareModel.Etos.DoctorsAdvices;

/// <summary>
///  电子病例的病例信息
/// </summary>
public class PushEmrDataEto
{
    /// <summary>
    /// 电子病例的病例信息
    /// </summary> 
    public PushEmrDataEto(
        string pastmedicalhistory,
        string presentmedicalhistory,
        string physicalexamination,
        string narrationname,
        string allergySign,
        string diagnosename,
        string treatopinion,
        string outpatientSurgery,
        string aidpacs)
    {
        Pastmedicalhistory = pastmedicalhistory;
        Presentmedicalhistory = presentmedicalhistory;
        Physicalexamination = physicalexamination;
        Narrationname = narrationname;
        AllergySign = allergySign;
        Diagnosename = diagnosename;
        Treatopinion = treatopinion;
        OutpatientSurgery = outpatientSurgery;
        Aidpacs = aidpacs;
    }

    /// <summary>
    /// 设置患者信息
    /// </summary> 
    public void SetPatientInfo(
        Guid piid,
        string patientId,
        string patientName,
        string visitNo,
        string registerNo)
    {
        Piid = piid;
        PatientId = patientId;
        PatientName = patientName;
        VisitNo = visitNo;
        RegisterNo = registerNo;
    }

    /// <summary>
    /// 科室，医生 信息设置
    /// </summary> 
    public void SetBaseInfo(
        string deptCode,
        string doctorCode
        )
    {
        DeptCode = deptCode;
        DoctorCode = doctorCode;
    }

    #region 患者，医院，科室等基本信息

    /// <summary>
    /// 患者唯一uuid
    /// </summary>
    public Guid Piid { get; set; }

    /// <summary>
    /// 患者Id
    /// </summary> 
    public string PatientId { get; set; }

    /// <summary>
    /// 患者名称
    /// </summary> 
    public string PatientName { get; set; }

    /// <summary>
    /// 书写科室,一级科室代码
    /// </summary>  
    public string DeptCode { get; set; }

    /// <summary>
    /// 书写医生,书写医生工号
    /// </summary>  
    public string DoctorCode { get; set; }

    /// <summary>
    /// 就诊号
    /// </summary>  
    public string VisitNo { get; set; }

    /// <summary>
    /// 挂号识别号
    /// <![CDATA[
    /// 8.1 挂号信息回传（正式、hisweb）registerNo
    /// ]]>
    /// </summary>  
    public string RegisterNo { get; set; }

    #endregion


    #region 病例信息

    /// <summary>
    /// 既往史 MedicalHistory √
    /// </summary>
    public string Pastmedicalhistory { get; set; }

    /// <summary>
    /// 现病史 HistoryPresentIllness √
    /// </summary>
    public string Presentmedicalhistory { get; set; }

    /// <summary>
    /// 体格检查 BodyExam √
    /// </summary>
    public string Physicalexamination { get; set; }

    /// <summary>
    /// 主诉 ChiefComplaint √
    /// </summary>
    public string Narrationname { get; set; }

    /// <summary>
    /// 药物过敏史 AllergySign
    /// </summary>  
    public string AllergySign { get; set; }

    /// <summary>
    /// 初步诊断 PreliminaryDiagnosis √
    /// </summary>  
    public string Diagnosename { get; set; }

    /// <summary>
    /// 处理意见 HandlingOpinions √
    /// </summary>  
    public string Treatopinion { get; set; }

    /// <summary>
    /// 门诊手术 OutpatientSurgery
    /// </summary>  
    public string OutpatientSurgery { get; set; }

    /// <summary>
    /// 辅助检查 AuxiliaryExamination √
    /// </summary>  
    public string Aidpacs { get; set; }

    #endregion


}
