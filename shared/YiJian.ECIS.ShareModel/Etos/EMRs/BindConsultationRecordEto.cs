using Newtonsoft.Json;

namespace YiJian.ECIS.ShareModel.Etos.EMRs
{

    /// <summary>
    /// 绑定会诊记录
    /// </summary>
    public class BindConsultationRecordEto
    {
        /// <summary>
        /// 就诊号
        /// </summary>  
        [JsonProperty("visitNo")]
        public string VisitNo { get; set; }

        /// <summary>
        /// 流水号
        /// </summary>  
        [JsonProperty("registerSerialNo")]
        public string RegisterSerialNo { get; set; }

        /// <summary>
        /// 机构ID
        /// </summary>  
        public string OrgCode { get; set; }

        /// <summary>
        /// 患者唯一Id
        /// </summary>
        public Guid Piid { get; set; }

        /// <summary>
        /// 患者编号
        /// </summary>   
        public string PatientNo { get; set; }

        /// <summary>
        /// 患者名称
        /// </summary>   
        public string PatientName { get; set; }

        /// <summary>
        /// 科室编码
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
        /// 病历名称
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 入院时间
        /// </summary> 
        public DateTime? AdmissionTime { get; set; }

        /// <summary>
        /// 诊断
        /// </summary>
        public string Diagnosis { get; set; }

        /// <summary>
        /// 电子病历数据
        /// </summary> 
        public ConsultationRecordDataEto Data { get; set; }
    }

}
