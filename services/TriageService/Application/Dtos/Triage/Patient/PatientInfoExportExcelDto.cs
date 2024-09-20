using System;
using Newtonsoft.Json;

namespace SamJan.MicroService.PreHospital.TriageService
{
    public class PatientInfoExportExcelDto
    {
        /// <summary>
        /// id
        /// </summary>
        [JsonProperty("id")]
        public Guid Id { get; set; }

        /// <summary>
        /// 就诊号
        /// </summary>
        [JsonProperty("registerNo")]
        public string RegisterNo { get; set; }

        /// <summary>
        /// 群伤
        /// </summary>
        [JsonProperty("groupInjury")]
        public string GroupInjury { get; set; }

        /// <summary>
        /// 年龄
        /// </summary>
        [JsonProperty("age")]

        public string Age { get; set; }

        /// <summary>
        /// 分诊去向
        /// </summary>
        [JsonProperty("triageTarget")]
        public string TriageTarget { get; set; }

        /// <summary>
        /// 绿通
        /// </summary>
        [JsonProperty("greenRoad")]
        public string GreenRoad { get; set; }

        /// <summary>
        /// 费别
        /// </summary>
        [JsonProperty("chargeType")]
        public string ChargeType { get; set; }

        /// <summary>
        /// 来院方式
        /// </summary>
        [JsonProperty("toHospitalWay")]
        public string ToHospitalWay { get; set; }

        /// <summary>
        /// 患者姓名
        /// </summary>
        [JsonProperty("patientName")]
        public string PatientName { get; set; }

        /// <summary>
        /// 上报
        /// </summary>
        [JsonProperty("report")]
        public string Report { get; set; }

        /// <summary>
        /// 其他去向
        /// </summary>
        [JsonProperty("otherTarget")]
        public string OtherTarget { get; set; }

        /// <summary>
        /// 实际分诊级别
        /// </summary>
        [JsonProperty("actTriageLevel")]
        public string ActTriageLevel { get; set; }

        /// <summary>
        /// 生日
        /// </summary>
        [JsonProperty("birthday")]
        public string Birthday { get; set; }

        /// <summary>
        /// 分诊时间
        /// </summary>
        [JsonProperty("triageTime")]
        public string TriageTime { get; set; }

        /// <summary>
        /// 分诊耗时
        /// </summary>
        [JsonProperty("triageTimeConsuming")]
        public string TriageTimeConsuming { get; set; }

        /// <summary>
        /// 患者id
        /// </summary>
        [JsonProperty("patientId")]
        public string PatientId { get; set; }

        /// <summary>
        /// 就诊号
        /// </summary>
        [JsonProperty("visitNo")]
        public string VisitNo { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        [JsonProperty("sex")]
        public string Sex { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        [JsonProperty("sexName")]
        public string SexName { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [JsonProperty("remark")]
        public string Remark { get; set; }

        /// <summary>
        /// 分诊科室
        /// </summary>
        [JsonProperty("triageDeptCode")]
        public string TriageDeptCode { get; set; }

        /// <summary>
        /// 分诊科室名称
        /// </summary>
        [JsonProperty("triageDeptName")]
        public string TriageDeptName { get; set; }
        /// <summary>
        /// SPO2
        /// </summary>
        [JsonProperty("spO2")]
        public string SpO2 { get; set; }

        /// <summary>
        /// 体温
        /// </summary>
        [JsonProperty("temp")]
        public string Temp { get; set; }

        /// <summary>
        /// 心率
        /// </summary>
        [JsonProperty("heartRate")]
        public string HeartRate { get; set; }

        /// <summary>
        /// 呼吸
        /// </summary>
        [JsonProperty("breathRate")]
        public string BreathRate { get; set; }

        /// <summary>
        /// 收缩压
        /// </summary>
        [JsonProperty("sbp")]
        public string Sbp { get; set; }

        /// <summary>
        /// 舒张压
        /// </summary>
        [JsonProperty("sdp")]
        public string Sdp { get; set; }

        /// <summary>
        /// 血糖
        /// </summary>
        [JsonProperty("bloodGlucose")]
        public string BloodGlucose { get; set; }

        /// <summary>
        /// 主诉
        /// </summary>
        [JsonProperty("narration")]
        public string Narration { get; set; }

        /// <summary>
        /// 重点病种
        /// </summary>
        [JsonProperty("diseaseCode")]
        public string DiseaseCode { get; set; }

        /// <summary>
        /// RFID
        /// </summary>
        [JsonProperty("rFID")]
        public string RFID { get; set; }

        /// <summary>
        /// 分诊人
        /// </summary>
        [JsonProperty("triageUserCode")]
        public string TriageUserCode { get; set; }

        /// <summary>
        /// 分诊人名称
        /// </summary>
        [JsonProperty("triageUserName")]
        public string TriageUserName { get; set; }

        /// <summary>
        /// 民族
        /// </summary>
        [JsonProperty("nation")]
        public string Nation { get; set; }

        /// <summary>
        /// 国家
        /// </summary>
        [JsonProperty("country")]
        public string Country { get; set; }

        /// <summary>
        /// 任务单流水号
        /// </summary>
        [JsonProperty("taskInfoNum")]
        public string TaskInfoNum { get; set; }

        /// <summary>
        /// 救护车车牌号
        /// </summary>
        [JsonProperty("carNum")]
        public string CarNum { get; set; }

        /// <summary>
        /// 患者身份
        /// </summary>
        [JsonProperty("identity")]
        public string Identity { get; set; }


        /// <summary>
        /// 就诊类型
        /// </summary>
        [JsonProperty("typeOfVisit")]
        public string TypeOfVisit { get; set; }

        /// <summary>
        /// 意识
        /// </summary>
        [JsonProperty("consciousness")]
        public string Consciousness { get; set; }

        /// <summary>
        /// 是否
        /// </summary>
        [JsonProperty("isTriaged")]
        public int IsTriaged { get; set; }

        /// <summary>
        /// 首诊医生工号
        /// </summary>
        [JsonProperty("firstDoctorCode")]
        public string FirstDoctorCode { get; set; }

        /// <summary>
        /// 首诊医生名称
        /// </summary>
        [JsonProperty("firstDoctorName")]
        public string FirstDoctorName { get; set; }

        /// <summary>
        /// 最终去向代码
        /// </summary>
        [JsonProperty("lastDirectionCode")]
        public string LastDirectionCode { get; set; }

        /// <summary>
        /// 最终去向名称
        /// </summary>
        [JsonProperty("lastDirectionName")]
        public string LastDirectionName { get; set; }

        /// <summary>
        /// 证件类型编码
        /// </summary>
        [JsonProperty("idTypeCode")]
        public string IdTypeCode { get; set; }

        /// <summary>
        /// 证件类型
        /// </summary>
        [JsonProperty("idTypeName")]
        public string IdTypeName { get; set; }

        /// <summary>
        /// 监护人证件类型（默认居民身份证）
        /// </summary>
        [JsonProperty("guardianIdTypeCode")]
        public string GuardianIdTypeCode { get; set; }

        /// <summary>
        /// 监护人证件类型（默认居民身份证）
        /// </summary>
        [JsonProperty("guardianIdTypeName")]
        public string GuardianIdTypeName { get; set; }

        /// <summary>
        /// 人群编码
        /// </summary>
        [JsonProperty("crowdCode")]
        public string CrowdCode { get; set; }

        /// <summary>
        /// 人群编码
        /// </summary>
        [JsonProperty("crowdName")]
        public string CrowdName { get; set; }

        /// <summary>
        /// 孕周
        /// </summary>
        [JsonProperty("gestationalWeeks")]
        public int? GestationalWeeks { get; set; }

        /// <summary>
        /// 就诊原因编码
        /// </summary>
        [JsonProperty("visitReasonCode")]
        public string VisitReasonCode { get; set; }

        /// <summary>
        /// 就诊原因
        /// </summary>
        [JsonProperty("visitReasonName")]
        public string VisitReasonName { get; set; }

        /// <summary>
        /// 持续时间（天）
        /// </summary>
        [JsonProperty("persistDays")]
        public int? PersistDays { get; set; }

        /// <summary>
        /// 持续时间（时）
        /// </summary>
        [JsonProperty("persistHours")]
        public int? PersistHours { get; set; }

        /// <summary>
        /// 持续时间（分）
        /// </summary>
        [JsonProperty("persistMinutes")]
        public int? PersistMinutes { get; set; }

        /// <summary>
        /// 是否作废
        /// </summary>
        [JsonProperty("isCancelled")]
        public bool IsCancelled { get; set; }
        /// <summary>
        /// 是否作废
        /// </summary>
        [JsonProperty("isCancelledText")]
        public string IsCancelledText { get; set; }

        /// <summary>
        /// 作废人
        /// </summary>
        [JsonProperty("cancellationUser")]
        public string CancellationUser { get; set; }

        /// <summary>
        /// 作废时间
        /// </summary>
        [JsonProperty("cancellationTime")]
        public DateTime? CancellationTime { get; set; }

        /// <summary>
        /// 身份证号
        /// </summary>
        [JsonProperty("identityNo")]
        public string IdentityNo { get; set; }

        /// <summary>
        /// 联系电话
        /// </summary>
        [JsonProperty("contactsPhone")]
        public string ContactsPhone { get; set; }

        /// <summary>
        /// 患者住址
        /// </summary>
        [JsonProperty("address")]
        public string Address { get; set; }

        /// <summary>
        /// 紧急联系人/监护人
        /// </summary>
        [JsonProperty("contactsPerson")]
        public string ContactsPerson { get; set; }

        /// <summary>
        /// 监护人身份证号码
        /// </summary>
        [JsonProperty("guardianIdCardNo")]
        public string GuardianIdCardNo { get; set; }

        /// <summary>
        /// 监护人/联系人电话
        /// </summary>
        [JsonProperty("guardianPhone")]
        public string GuardianPhone { get; set; }

        /// <summary>
        /// 既往史
        /// </summary>
        [JsonProperty("pastMedicalHistory")]
        public string PastMedicalHistory { get; set; }

        /// <summary>
        /// 过敏史
        /// </summary>
        [JsonProperty("allergyHistory")]
        public string AllergyHistory { get; set; }

        /// <summary>
        /// 排队号
        /// </summary>
        [JsonProperty("callingSn")]
        public string CallingSn { get; set; }

        /// <summary>
        /// 国籍
        /// </summary>
        [JsonProperty("countryName")]
        public string CountryName { get; set; }

        /// <summary>
        /// 国籍代码
        /// </summary>
        [JsonProperty("countryCode")]
        public string CountryCode { get; set; }

        /// <summary>
        /// 是否是院前患者
        /// </summary>
        [JsonProperty("isFirstAid")]
        public string IsFirstAid { get; set; }

        /// <summary>
        /// 就诊医生名称
        /// </summary>
        [JsonProperty("doctorName")]
        public string DoctorName { get; set; }

        /// <summary>
        /// 挂号时间
        /// </summary>
        [JsonProperty("registerTime")]
        public string RegisterTime { get; set; }

        /// <summary>
        /// 开始就诊时间
        /// </summary>
        [JsonProperty("beginTime")]
        public string BeginTime { get; set; }

        /// <summary>
        /// 结束就诊时间
        /// </summary>
        [JsonProperty("endTime")]
        public string EndTime { get; set; }

        /// <summary>
        /// 收缩压/舒张压
        /// </summary>
        [JsonProperty("sbp_sdp")]
        public string Sbp_Sdp { get; set; }

        /// <summary>
        /// 是否过号
        /// </summary>
        [JsonProperty("isUntreatedOver")]
        public bool IsUntreatedOver { get; set; }

        /// <summary>
        /// 患者被接诊的就诊时间
        /// </summary>
        [JsonProperty("visitDate")]
        public DateTime? VisitDate { get; set; }

        /// <summary>
        /// 被接诊后的结束就诊时间
        /// </summary>
        [JsonProperty("finishVisitTime")]
        public DateTime? FinishVisitTime { get; set; }

        /// <summary>
        /// 诊断代码
        /// </summary>
        [JsonProperty("diagnoseCode")]
        public string DiagnoseCode { get; set; }

        /// <summary>
        /// 诊断名称
        /// </summary>
        [JsonProperty("diagnoseName")]
        public string DiagnoseName { get; set; }

        /// <summary>
        /// 叫号医生编码
        /// </summary>
        [JsonProperty("callDoctorCode")]
        public string CallDoctorCode { get; set; }

        /// <summary>
        /// 叫号医生名称
        /// </summary>
        [JsonProperty("callDoctorName")]
        public string CallDoctorName { get; set; }
    }
}