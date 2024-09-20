namespace HisApiMockService.Models.Medicals;

public class LisPatientInfoResponse
{
    /// <summary>
    /// 就诊卡号		实体卡片号
    /// </summary>
    public string IcCardNo { get; set; }

    /// <summary>
    /// 病人ID 	内部唯一号
    /// </summary>
    public string PatientId { get; set; }

    /// <summary>
    /// 就诊号
    /// </summary>
    public string VisitNo { get; set; }

    /// <summary>
    /// 就诊流水号  就诊唯一号
    /// </summary>
    public string VisitSerialNo { get; set; }

    /// <summary>
    /// 就诊类型	是	1门诊;2住院;3体检;
    /// </summary>
    public EPatientType PatientType { get; set; }

    /// <summary>
    /// 住院次数
    /// </summary>
    public string VisitNumber { get; set; }

    /// <summary>
    /// 医保号
    /// </summary>
    public string SafetyNo { get; set; }

    /// <summary>
    /// 费用类别
    /// </summary>
    public string ChargeType { get; set; }

    /// <summary>
    /// 姓名
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// 性别编码
    /// </summary>
    public string Gender { get; set; }

    /// <summary>
    /// 病人所在科室  申请手术科室
    /// </summary>
    public string DeptCode { get; set; }

    /// <summary>
    /// 科室名称
    /// </summary>
    public string DeptName { get; set; }

    /// <summary>
    /// 病人所在床号  门诊病人为空
    /// </summary>
    public string BedNo { get; set; }

    /// <summary>
    /// 病情说明	 对病人体征、病情等进一步说明
    /// </summary>
    public string PatientCondition { get; set; }

    /// <summary>
    /// 检查号
    /// </summary>
    public string LisNo { get; set; }

    /// <summary>
    /// 诊断代码	 多个用分号隔开
    /// </summary>
    public string DiagnoseCode { get; set; }

    /// <summary>
    /// 诊断名称	  多个用分号隔开
    /// </summary>
    public string DiagnoseName { get; set; }

}
