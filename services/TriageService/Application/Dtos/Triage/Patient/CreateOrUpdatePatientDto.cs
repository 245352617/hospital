using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SamJan.MicroService.PreHospital.TriageService
{
    /// <summary>
    /// 更新或创建分诊Dto
    /// </summary>
    public class CreateOrUpdatePatientDto
    {
        /// <summary>
        /// PatientInfo表Id（注：修改时传入）
        /// </summary>
        public Guid TriagePatientInfoId { get; set; }

        /// <summary>
        /// EmrService中的Emr_PatientInfo Id
        /// </summary>
        public Guid EmrPatientInfoId { get; set; }


        /// <summary>
        /// 群伤事件Id
        /// </summary>
        public string GroupInjuryName { get; set; }

        /// <summary>
        /// 任务单号
        /// </summary>
        public Guid TaskInfoId { get; set; }

        /// <summary>
        /// 车牌号
        /// </summary>
        [MaxLength(20, ErrorMessage = "车牌号的最大长度为{1}")]
        public string CarNum { get; set; }

        /// <summary>
        /// 就诊号
        /// </summary>
        public string VisitNo { get; set; }

        /// <summary>
        /// 电子医保凭证
        /// </summary>
        public string ElectronCertNo { get; set; }

        /// <summary>
        /// 任务单流水号
        /// </summary>
        [MaxLength(50, ErrorMessage = "任务单流水号的最大长度为{1}")]
        public string TaskInfoNum { get; set; }

        /// <summary>
        /// 患者唯一标识(HIS)
        /// </summary>
        [Required(ErrorMessage = "患者病历号不能为空")]
        [MaxLength(50, ErrorMessage = "患者病历号的最大长度为{1}")]
        public string PatientId { get; set; }

        /// <summary>
        /// 患者姓名
        /// </summary>
        [MaxLength(50, ErrorMessage = "患者姓名的最大长度为{1}")]
        public string PatientName { get; set; }

        /// <summary>
        /// 性别Code
        /// </summary>
        [MaxLength(20, ErrorMessage = "性别Code的最大长度为{1}")]
        public string Sex { get; set; }

        /// <summary>
        /// 性别名称
        /// </summary>
        [MaxLength(20, ErrorMessage = "性别名称的最大长度为{1}")]
        public string SexName { get; set; }

        private string _birthday;
        /// <summary>
        /// 患者出生日期
        /// </summary>
        public string Birthday
        {
            get
            {
                if (DateTime.TryParse(_birthday, out DateTime birthday))
                {
                    return birthday.ToString("yyyy-MM-dd");
                }
                return null;
            }
            set { _birthday = value; }
        }

        /// <summary>
        /// 患者出生日期（由于院前分诊、急诊分诊前端传入的字段名不同所以设置两个）
        /// </summary>
        public DateTime? BirthDate { get; set; }

        /// <summary>
        /// 患者住址
        /// </summary>
        [MaxLength(200, ErrorMessage = "患者住址的最大长度为{1}")]
        public string Address { get; set; }

        /// <summary>
        /// 紧急联系人
        /// </summary>
        [MaxLength(20, ErrorMessage = "紧急联系人的最大长度为{1}")]
        public string ContactsPerson { get; set; }

        /// <summary>
        /// 联系/患者自己电话
        /// </summary>
        [MaxLength(20, ErrorMessage = "联系/患者自己电话的最大长度为{1}")]
        public string ContactsPhone { get; set; }

        /// <summary>
        /// 监护人证件类型（默认居民身份证）
        /// </summary>
        [MaxLength(20, ErrorMessage = "监护人证件类型的最大长度为{1}")]
        public string GuardianIdTypeCode { get; set; } = "IdType_01";

        /// <summary>
        /// 监护人身份证号码
        /// </summary>
        [MaxLength(20, ErrorMessage = "监护人身份证号码的最大长度为{1}")]
        public string GuardianIdCardNo { get; set; }

        /// <summary>
        /// 监护人/联系人电话
        /// </summary>
        [MaxLength(20, ErrorMessage = "监护人/联系人电话的最大长度为{1}")]
        public string GuardianPhone { get; set; }

        /// <summary>
        /// 监护人/联系人地址
        /// </summary>
        [MaxLength(50, ErrorMessage = "监护人/联系人地址的最大长度为{1}")]
        public string ContactsAddress { get; set; }

        /// <summary>
        /// 与联系人关系
        /// </summary>
        [MaxLength(20, ErrorMessage = "与联系人关系的最大长度为{1}")]
        public string SocietyRelationCode { get; set; }

        /// <summary>
        /// 来院方式
        /// </summary>
        [MaxLength(60, ErrorMessage = "来院方式的最大长度为{1}")]
        public string ToHospitalWay { get; set; }

        /// <summary>
        /// 来院方式名称
        /// </summary>
        [MaxLength(60, ErrorMessage = "来院方式名称的最大长度为{1}")]
        public string ToHospitalWayName { get; set; }

        /// <summary>
        /// 患者身份Code
        /// </summary>
        [MaxLength(60, ErrorMessage = "患者身份Code的最大长度为{1}")]
        public string Identity { get; set; }

        /// <summary>
        /// 患者身份Name
        /// </summary>
        [MaxLength(60, ErrorMessage = "患者身份Name的最大长度为{1}")]
        public string IdentityName { get; set; }

        /// <summary>
        /// 费别Code
        /// </summary>
        [MaxLength(60, ErrorMessage = "费别Code的最大长度为{1}")]
        public string ChargeType { get; set; }

        /// <summary>
        /// 费别Name
        /// </summary>
        [MaxLength(60, ErrorMessage = "费别Name的最大长度为{1}")]
        public string ChargeTypeName { get; set; }


        /// <summary>
        /// 特约记账类型编码
        /// </summary>
        [MaxLength(50, ErrorMessage = "特约记账类型编码最大长度为{1}")]
        public string SpecialAccountTypeCode { get; set; }

        /// <summary>
        /// 特约记账类型名称
        /// </summary>
        [MaxLength(50, ErrorMessage = "特约记账类型名称的最大长度为{1}")]
        public string SpecialAccountTypeName { get; set; }


        /// <summary>
        /// 身份证号
        /// </summary>
        [MaxLength(500, ErrorMessage = "身份证号的最大长度为{1}")]
        public string IdentityNo { get; set; }

        /// <summary>
        /// 民族Code
        /// </summary>
        [MaxLength(60, ErrorMessage = "民族Code的最大长度为{1}")]
        public string Nation { get; set; }

        /// <summary>
        /// 民族Name
        /// </summary>
        [MaxLength(60, ErrorMessage = "民族名称的最大长度为{1}")]
        public string NationName { get; set; }

        /// <summary>
        /// 发病时间
        /// </summary>
        public DateTime? OnsetTime { get; set; }

        /// <summary>
        /// 绿色通道Code
        /// </summary>
        [MaxLength(60, ErrorMessage = "{0}的最大长度为{1}")]
        public string GreenRoad { get; set; }

        /// <summary>
        /// 绿色通道Name
        /// </summary>
        [MaxLength(60, ErrorMessage = "{0}的最大长度为{1}")]
        public string GreenRoadName { get; set; }

        /// <summary>
        /// 就诊卡号
        /// </summary>
        [MaxLength(20, ErrorMessage = "{0}的最大长度为{1}")]
        public string CardNo { get; set; }

        /// <summary>
        /// RFID
        /// </summary>
        [MaxLength(20, ErrorMessage = "{0}的最大长度为{1}")]
        public string RFID { get; set; }

        /// <summary>
        /// 医保卡号
        /// </summary>
        [MaxLength(50, ErrorMessage = "{0}的最大长度为{1}")]
        public string MedicalNo { get; set; }

        /// <summary>
        /// 年龄
        /// </summary>
        [MaxLength(20, ErrorMessage = "{0}的最大长度为{1}")]
        public string Age { get; set; }

        /// <summary>
        /// 体重
        /// </summary>
        [MaxLength(20, ErrorMessage = "{0}的最大长度为{1}")]
        public string Weight { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreationTime { get; set; }

        /// <summary>
        /// 重点病种Code
        /// </summary>
        [MaxLength(50, ErrorMessage = "{0}的最大长度为{1}")]
        public string DiseaseCode { get; set; }

        /// <summary>
        /// 重点病种Name
        /// </summary>
        [MaxLength(50, ErrorMessage = "{0}的最大长度为{1}")]
        public string DiseaseName { get; set; }

        /// <summary>
        /// 分诊状态，0：暂存，1：分诊
        /// </summary>
        public int TriageStatus { get; set; } = 1;

        /// <summary>
        /// 就诊状态
        /// </summary>
        public int VisitStatus { get; set; }

        /// <summary>
        /// 开始分诊时间
        /// </summary>
        public DateTime? StartTriageTime { get; set; }

        /// <summary>
        /// 是否三无病人
        /// </summary>
        public bool IsNoThree { get; set; }

        /// <summary>
        /// 就诊类型Code
        /// </summary>
        [MaxLength(60, ErrorMessage = "{0}的最大长度为{1}")]
        public string TypeOfVisitCode { get; set; }

        /// <summary>
        /// 就诊类型Name
        /// </summary>
        [MaxLength(60, ErrorMessage = "{0}的最大长度为{1}")]
        public string TypeOfVisitName { get; set; }

        /// <summary>
        /// 主诉Code
        /// </summary>
        public string Narration { get; set; }

        /// <summary>
        /// 主诉Name
        /// </summary>
        public string NarrationName { get; set; }

        /// <summary>
        /// 主诉备注
        /// </summary>
        public string NarrationComments { get; set; }

        /// <summary>
        /// 意识Code
        /// </summary>
        [MaxLength(60, ErrorMessage = "{0}的最大长度为{1}")]
        public string Consciousness { get; set; }

        /// <summary>
        /// 意识Name
        /// </summary>
        [MaxLength(60, ErrorMessage = "{0}的最大长度为{1}")]
        public string ConsciousnessName { get; set; }

        /// <summary>
        /// 分诊人
        /// </summary>
        [MaxLength(50, ErrorMessage = "{0}的最大长度为{1}")]
        public string TriageUserCode { get; set; }

        /// <summary>
        /// 分诊人名称
        /// </summary>
        [MaxLength(50, ErrorMessage = "{0}的最大长度为{1}")]
        public string TriageUserName { get; set; }

        /// <summary>
        /// 预约流水号
        /// </summary>
        public string SeqNumber { get; set; }

        /// <summary>
        /// 证件类型编码
        /// </summary>
        public string IdTypeCode { get; set; }

        /// <summary>
        /// 人群编码
        /// </summary>
        public string CrowdCode { get; set; }

        /// <summary>
        /// 孕周
        /// </summary>
        public int? GestationalWeeks { get; set; }

        /// <summary>
        /// 就诊原因编码
        /// </summary>
        public string VisitReasonCode { get; set; }

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
        /// 参保地代码
        /// </summary>
        public string InsuplcAdmdvCode { get; set; }

        /// <summary>
        /// 国籍代码
        /// </summary>
        public string CountryCode { get; set; }
        /// <summary>
        /// 是否是院前患者
        /// </summary>
        public bool IsFirstAid { get; set; }
        /// <summary>
        /// 挂号类型 4=免费
        /// </summary>
        public string RegType { get; set; }

        /// <summary>
        /// 医疗证号
        /// </summary>
        public string SafetyNo { get; set; }

        /// <summary>
        /// 是否跳过挂号
        /// </summary>
        public bool SkipRegister { get; set; } = false;

        /// <summary>
        /// 发票号
        /// </summary>
        public string InvoiceNum { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 院前分诊生命体征信息
        /// </summary>
        public CreateOrUpdateVitalSignDto VitalSignInfo { get; set; }

        /// <summary>
        /// 院前分诊结果信息
        /// </summary>
        public CreateOrUpdateConsequenceDto ConsequenceInfo { get; set; }

        /// <summary>
        /// 院前分诊评分信息
        /// </summary>
        public ICollection<CreateOrUpdateScoreDto> ScoreInfo { get; set; }

        /// <summary>
        /// 入院情况
        /// </summary>
        public CreateOrUpdateAdmissionInfoDto AdmissionInfo { get; set; }
    }
}