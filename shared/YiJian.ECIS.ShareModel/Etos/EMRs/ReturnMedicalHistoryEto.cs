using Newtonsoft.Json;

namespace YiJian.ECIS.ShareModel.Etos.EMRs
{

    /// <summary>
    /// 互联网医院病历信息回写平台
    /// </summary>
    public class ReturnMedicalHistoryEto
    {
        /// <summary>
        /// 机构ID,固定：H7110
        /// </summary> 
        [JsonRequired]
        [JsonProperty("orgCode")]
        public string OrgCode { get; set; } = "H7110";

        /// <summary>
        /// 书写科室,一级科室代码
        /// </summary> 
        [JsonRequired]
        [JsonProperty("deptCode")]
        public string DeptCode { get; set; }

        /// <summary>
        /// 书写医生,书写医生工号
        /// </summary> 
        [JsonRequired]
        [JsonProperty("doctorCode")]
        public string DoctorCode { get; set; }

        /// <summary>
        /// 病人ID,His内部识别id
        /// </summary> 
        [JsonRequired]
        [JsonProperty("patientId")]
        public string PatientId { get; set; }

        /// <summary>
        /// 就诊号
        /// </summary> 
        [JsonProperty("visitNo")]
        public string VisitNo { get; set; }

        /// <summary>
        /// 挂号识别号
        /// <![CDATA[
        /// 8.1 挂号信息回传（正式、hisweb）registerNo
        /// ]]>
        /// </summary> 
        [JsonRequired]
        [JsonProperty("registerNo")]
        public string RegisterNo { get; set; }

        /// <summary>
        /// 主诉
        /// </summary> 
        [JsonRequired]
        [JsonProperty("chiefComplaint")]
        public string ChiefComplaint { get; set; }

        /// <summary>
        /// 现病史
        /// </summary> 
        [JsonRequired]
        [JsonProperty("historyPresentIllness")]
        public string HistoryPresentIllness { get; set; }

        /// <summary>
        /// 药物过敏史
        /// </summary> 
        [JsonProperty("allergySign")]
        public string AllergySign { get; set; }

        /// <summary>
        /// 既往史
        /// </summary> 
        [JsonRequired]
        [JsonProperty("medicalHistory")]
        public string MedicalHistory { get; set; }

        /// <summary>
        /// 体格体查
        /// </summary> 
        [JsonRequired]
        [JsonProperty("bodyExam")]
        public string BodyExam { get; set; }

        /// <summary>
        /// 初步诊断
        /// </summary>  
        [JsonRequired]
        [JsonProperty("preliminaryDiagnosis")]
        public string PreliminaryDiagnosis { get; set; }

        /// <summary>
        /// 处理意见
        /// </summary>  
        [JsonRequired]
        [JsonProperty("handlingOpinions")]
        public string HandlingOpinions { get; set; }

        /// <summary>
        /// 门诊手术
        /// </summary> 
        [JsonProperty("outpatientSurgery")]
        public string OutpatientSurgery { get; set; }

        /// <summary>
        /// 扩展字段一
        /// </summary>
        [JsonProperty("reserveNum1")]
        public string ReserveNum1 { get; set; }

        /// <summary>
        /// 辅助检查
        /// </summary> 
        [JsonRequired]
        [JsonProperty("auxiliaryExamination")]
        public string AuxiliaryExamination { get; set; }

        /// <summary>
        /// 病历完整信息
        /// </summary>
        [JsonRequired]
        [JsonProperty("fullContentXml")]
        public string FullContentXml { get; set; }

        /// <summary>
        /// 渠道来源,尚哲：SZKJ
        /// </summary> 
        [JsonRequired]
        [JsonProperty("channel")]
        public string Channel { get; set; } = "SZKJ";

        /// <summary>
        /// 病历名称
        /// </summary>
        [JsonRequired]
        [JsonProperty("medicalName")]
        public string MedicalName { get; set; }

        /// <summary>
        /// 创建日期
        /// </summary>
        [JsonRequired]
        [JsonProperty("createDate")]
        public DateTime CreateDate { get; set; }

        /// <summary>
        /// 完成日期
        /// </summary>
        [JsonRequired]
        [JsonProperty("completionDate")]
        public DateTime CompletionDate { get; set; }

    }
}