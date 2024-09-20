using System;

namespace SamJan.MicroService.PreHospital.TriageService
{
    public class PatientOutput
    {
        /// <summary>
        /// 任务单号
        /// </summary>
        public Guid TaskInfoId { get; set; }

        /// <summary>
        /// 车牌号
        /// </summary>
        public string CarNum { get; set; }

        /// <summary>
        /// 就诊次数
        /// </summary>
        public string VisitNo { get; set; }

        /// <summary>
        /// 患者唯一标识(HIS)
        /// </summary>
        public string PatientId { get; set; }

        /// <summary>
        /// 患者姓名
        /// </summary>
        public string PatientName { get; set; }

        /// <summary>
        /// 拼音
        /// </summary>
        public string Py { get; set; }

        /// <summary>
        /// 患者性别
        /// </summary>
        public string Sex { get; set; }

        /// <summary>
        /// 体重
        /// </summary>
        public string Weight { get; set; }

        /// <summary>
        /// 患者出生日期
        /// </summary>
        public DateTime? Birthday { get; set; }

        /// <summary>
        /// 患者住址
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// 紧急联系人
        /// </summary>
        public string ContactsPerson { get; set; }

        /// <summary>
        /// 联系人电话
        /// </summary>
        public string ContactsPhone { get; set; }

        /// <summary>
        /// 来院方式
        /// </summary>
        public string ToHospitalWay { get; set; }

        /// <summary>
        /// 患者身份
        /// </summary>
        public string Identity { get; set; }

        /// <summary>
        /// 费别
        /// </summary>
        public string ChargeType { get; set; }

        /// <summary>
        /// 身份证号
        /// </summary>
        public string IdentityNo { get; set; }

        /// <summary>
        /// 民族
        /// </summary>
        public string Nation { get; set; }

        /// <summary>
        /// 国籍
        /// </summary>
        public string Country { get; set; }

        /// <summary>
        /// 国籍代码
        /// </summary>
        public string CountryCode { get; set; }

        /// <summary>
        /// 发病时间
        /// </summary>
        public DateTime? OnsetTime { get; set; }

        /// <summary>
        /// 绿色通道
        /// </summary>
        public string GreenRoad { get; set; }

        /// <summary>
        /// 就诊卡号
        /// </summary>
        public string CardNo { get; set; }

        /// <summary>
        /// RFID
        /// </summary>
        public string RFID { get; set; }

        /// <summary>
        /// 医保卡号
        /// </summary>
        public string MedicalNo { get; set; }

        /// <summary>
        /// 年龄
        /// </summary>
        public string Age { get; set; }

        /// <summary>
        /// 开始分诊时间
        /// </summary>
        public DateTime? StartTriageTime { get; set; }

        /// <summary>
        /// 分诊时间
        /// </summary>
        public DateTime CreationTime { get; set; }

        /// <summary>
        /// 是否三无病人
        /// </summary>
        public bool IsNoThree { get; set; }

        /// <summary>
        /// 证件类型编码
        /// </summary>
        public string IdTypeCode { get; set; }

        /// <summary>
        /// 证件类型
        /// </summary>
        public string IdTypeName { get; set; }

        /// <summary>
        /// 人群编码
        /// </summary>
        public string CrowdCode { get; set; }

        /// <summary>
        /// 人群编码
        /// </summary>
        public string CrowdName { get; set; }

        /// <summary>
        /// 孕周
        /// </summary>
        public int? GestationalWeeks { get; set; }

        /// <summary>
        /// 就诊原因编码
        /// </summary>
        public string VisitReasonCode { get; set; }

        /// <summary>
        /// 就诊原因
        /// </summary>
        public string VisitReasonName { get; set; }

        /// <summary>
        /// 持续时间（天）
        /// </summary>
        public int? PersistDays { get; set; }

        /// <summary>
        /// 持续时间（时）
        /// </summary>
        public int? PersistHours { get; set; }

        /// <summary>
        /// 持续时间（分）
        /// </summary>
        public int? PersistMinutes { get; set; }

        /// <summary>
        /// 监护人身份证号码
        /// </summary>
        public string GuardianIdCardNo { get; set; }

        /// <summary>
        /// 监护人/联系人电话
        /// </summary>
        public string GuardianPhone { get; set; }

        /// <summary>
        /// 监护人证件类型（默认居民身份证）
        /// </summary>
        public string GuardianIdTypeCode { get; set; } = "IdType_01";

        /// <summary>
        /// 监护人证件类型（默认居民身份证）
        /// </summary>
        public string GuardianIdTypeName { get; set; }

        /// <summary>
        /// 与联系人关系
        /// </summary>
        public string SocietyRelationCode { get; set; }

        /// <summary>
        /// 是否作废
        /// </summary>
        public bool IsCancelled { get; set; }

        /// <summary>
        /// 是否作废
        /// </summary>
        public string IsCancelledText { get { return IsCancelled ? "是" : "否"; } }

        /// <summary>
        /// 作废人
        /// </summary>
        public string CancellationUser { get; set; }

        /// <summary>
        /// 作废时间
        /// </summary>
        public DateTime? CancellationTime { get; set; }

        /// <summary>
        /// 参保地代码
        /// </summary>
        public string InsuplcAdmdvCode { get; set; }

        /// <summary>
        /// 预约流水号
        /// </summary>
        public string SeqNumber { get; set; }
    }
}