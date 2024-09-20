using System;
using Newtonsoft.Json;

namespace SamJan.MicroService.PreHospital.TriageService
{
    public class PatientInformExportExcelDto
    {
        /// <summary>
        /// id
        /// </summary>
        [JsonProperty("id")]
        public Guid Id { get; set; }

        /// <summary>
        /// 急救任务流水
        /// </summary>
        [JsonProperty("taskInfoNum")]
        public string TaskInfoNum { get; set; }

        /// <summary>
        /// 车牌号
        /// </summary>
        [JsonProperty("carNum")]
        public string CarNum { get; set; }

        /// <summary>
        /// 患者编号
        /// </summary>
        [JsonProperty("patientId")]
        public string PatientId { get; set; }

        /// <summary>
        /// 患者姓名
        /// </summary>
        [JsonProperty("patientName")]
        public string PatientName { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        [JsonProperty("gender")]
        public string Gender { get; set; }

        /// <summary>
        /// 年龄
        /// </summary>
        [JsonProperty("age")]
        public string Age { get; set; }

        /// <summary>
        /// 证件类型
        /// </summary>
        [JsonProperty("idTypeName")]
        public string IdTypeName { get; set; }

        /// <summary>
        /// 身份证号
        /// </summary>
        [JsonProperty("identityNo")]
        public string IdentityNo { get; set; }

        /// <summary>
        /// 预警级别
        /// </summary>
        [JsonProperty("warningLv")]
        public string WarningLv { get; set; }

        /// <summary>
        /// 病种判断
        /// </summary>
        [JsonProperty("diseaseIdentification")]
        public string DiseaseIdentification { get; set; }

        /// <summary>
        /// 告知时间
        /// </summary>
        [JsonProperty("informTime")]
        public DateTime? InformTime { get; set; }

        /// <summary>
        /// 告知患者来源
        /// </summary>
        [JsonProperty("source")]
        public string Source { get; set; }
    }
}