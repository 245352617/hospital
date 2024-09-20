using Newtonsoft.Json;
using YiJian.ECIS.ShareModel.DDPs.Dto;

namespace YiJian.ECIS.ShareModel.DDPs.Requests;

/// <summary>
/// 医嘱信息回传
/// </summary>
public class PKUSendMedicalInfoRequest
{
    /// <summary>
    /// 病人挂号后产生的发票号
    /// </summary>
    [JsonProperty("invoiceNo")]
    public string InvoiceNo { get; set; }

    /// <summary>
    /// 病人ID
    /// <![CDATA[ß
    /// 4.5.2 就诊记录、诊断信息回传（his提供、需对接集成平台） patientId
    /// ]]>
    /// </summary>
    [JsonProperty("patientId")]
    public string PatientId { get; set; }

    /// <summary>
    /// 病人姓名
    /// </summary>
    [JsonProperty("patientName")]
    public string PatientName { get; set; }

    /// <summary>
    /// 就诊流水号
    /// <![CDATA[
    /// 4.5.2 就诊记录、诊断信息回传（his提供、需对接集成平台）visSeriaINo
    /// ]]>
    /// </summary>
    [JsonProperty("visSerialNo")]
    public string VisSerialNo { get; set; }

    /// <summary>
    /// 挂号序号
    /// </summary>
    [JsonProperty("registSerialNo")]
    public string RegistSerialNo { get; set; }

    /// <summary>
    /// 门诊诊断
    /// </summary>
    [JsonProperty("diagnosis")]
    public string Diagnosis { get; set; }

    /// <summary>
    /// 孕周
    /// </summary>
    [JsonProperty("weekOfPregnant")]
    public int WeekOfPregnant { get; set; }

    /// <summary>
    /// 体重
    /// </summary>
    [JsonProperty("weight")]
    public string Weight { get; set; }

    /// <summary>
    /// 临床症状
    /// </summary>
    [JsonProperty("clinicalSymptom")]
    public string ClinicalSymptom { get; set; }

    /// <summary>
    /// 病史简要
    /// </summary>
    [JsonProperty("medicalHistory")]
    public string MedicalHistory { get; set; }

    /// <summary>
    /// 处方列表
    /// </summary>
    [JsonProperty("prescriptionList")]
    public List<Prescription4HisDto> PrescriptionList { get; set; }
}
