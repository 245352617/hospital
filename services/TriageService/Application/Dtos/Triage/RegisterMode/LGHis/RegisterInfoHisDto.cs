using Newtonsoft.Json;

namespace SamJan.MicroService.PreHospital.TriageService.LGHis
{
    public class RegisterInfoHisDto
    {
        /// <summary>
        /// 病人ID
        /// </summary>
        [JsonProperty("patientId")]
        public string PatientId { get; set; }

        /// <summary>
        /// 证件号码
        /// </summary>
        [JsonProperty("idCard")]
        public string IdCard { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("patientNo")]
        public string PatientNo { get; set; }

        /// <summary>
        /// 证件类型
        /// </summary>
        [JsonProperty("cardType")]
        public string CardType { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("identifyNO")]
        public string IdentifyNO { get; set; }

        /// <summary>
        /// 黄军林
        /// </summary>
        [JsonProperty("patientName")]
        public string PatientName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("birthday")]
        public string Birthday { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("sex")]
        public string Sex { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("homeAddress")]
        public string HomeAddress { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("officeAddress")]
        public string OfficeAddress { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("nationaddress")]
        public string Nationaddress { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("phoneNumberHome")]
        public string PhoneNumberHome { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("phoneNumberBus")]
        public string PhoneNumberBus { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("maritalStatus")]
        public string MaritalStatus { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("ssnNum")]
        public string SsnNum { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("ethnicGroup")]
        public string EthnicGroup { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("nationality")]
        public string Nationality { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("patientClass")]
        public string PatientClass { get; set; }

        /// <summary>
        /// 医保一档
        /// </summary>
        [JsonProperty("patientType")]
        public string PatientType { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("visitNum")]
        public string VisitNum { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("visitNo")]
        public string VisitNo { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("alternateVisitId")]
        public string AlternateVisitId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("appointmentId")]
        public string AppointmentId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("job")]
        public string Job { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("weight")]
        public string Weight { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("contactName")]
        public string ContactName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("contactPhone")]
        public string ContactPhone { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("guardIdType")]
        public string GuardIdType { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("cardNo")]
        public string CardNo { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("seeDate")]
        public string SeeDate { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("registerId")]
        public string RegisterId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("registerSequence")]
        public string RegisterSequence { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("registerDate")]
        public string RegisterDate { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("shift")]
        public string Shift { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("deptId")]
        public string DeptId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("dcotorCode")]
        public string DcotorCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("isCancel")]
        public string IsCancel { get; set; }

        /// <summary>
        /// 
        /// </summary>
       [JsonProperty("operator")]
        public string Operator { get; set; }
    }
}