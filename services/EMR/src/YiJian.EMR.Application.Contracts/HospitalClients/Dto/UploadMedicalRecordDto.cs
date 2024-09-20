using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace YiJian.EMR.HospitalClients.Dto
{
    /// <summary>
    /// 回传电子病历
    /// </summary>
    public class UploadMedicalRecordDto
    {
        /// <summary>
        /// 机构ID
        /// </summary>
        [Required]
        [JsonProperty("orgCode")]
        public string OrgCode { get; set; }

        /// <summary>
        /// 书写科室
        /// </summary>
        [Required]
        [JsonProperty("deptCode")]
        public string DeptCode { get; set; }

        /// <summary>
        /// 书写医生
        /// </summary>
        [Required]
        [JsonProperty("doctorCode")]
        public string DoctorCode { get; set; }

        /// <summary>
        /// 病人ID
        /// </summary>
        [Required]
        [JsonProperty("patientId")]
        public string PatientId { get; set; }

        ///// <summary>
        ///// 就诊号
        ///// </summary>
        //[JsonProperty("visitNo")]
        //public string VisitNo { get; set; }

        /// <summary>
        /// 挂号识别号
        /// </summary>
        [Required]
        [JsonProperty("registerNo")]
        public string RegisterNo { get; set; }

        /// <summary>
        /// 主诉
        /// </summary>
        [Required]
        [JsonProperty("chiefComplaint")]
        public string ChiefComplaint { get; set; }

        /// <summary>
        /// 现病史
        /// </summary>
        [Required]
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
        [Required]
        [JsonProperty("medicalHistory")]
        public string MedicalHistory { get; set; }

        /// <summary>
        /// 体格体查
        /// </summary>
        [Required]
        [JsonProperty("bodyExam")]
        public string BodyExam { get; set; }

        /// <summary>
        /// 初步诊断
        /// </summary>
        [Required]
        [JsonProperty("preliminaryDiagnosis")]
        public string PreliminaryDiagnosis { get; set; }

        /// <summary>
        /// 处理意见
        /// </summary>
        [Required]
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
        [Required]
        [JsonProperty("auxiliaryExamination")]
        public string AuxiliaryExamination { get; set; }

        /// <summary>
        /// 病历完整信息
        /// </summary>
        [Required]
        [JsonProperty("fullContentXml")]
        public string FullContentXml { get; set; }

        /// <summary>
        /// 渠道来源
        /// </summary>
        [Required]
        [JsonProperty("channel")]
        public string Channel { get; set; }

        /// <summary>
        /// 病历名称
        /// </summary>
        [Required]
        [JsonProperty("medicalName")]
        public string MedicalName { get; set; }

        /// <summary>
        /// 创建日期
        /// </summary>
        [Required]
        [JsonProperty("createDate")]
        public string CreateDate { get; set; }

        /// <summary>
        /// 完成日期
        /// </summary>
        [Required]
        [JsonProperty("completionDate")]
        public string CompletionDate { get; set; }

    }
}
