using System;
using System.ComponentModel.DataAnnotations;
using Szyjian.Ecis.Patient.Domain.Shared;

namespace Szyjian.Ecis.Patient.Application.Contracts
{
    /// <summary>
    /// 新增入科记录Dto
    /// </summary>
    public class CreateAdmissionRecordDto
    {
        /// <summary>
        /// PVID 分诊库患者基本信息表主键ID
        /// </summary>
        [Required(ErrorMessage = "分诊患者基本信息ID不能为空")]
        public Guid PI_ID { get; set; }

        /// <summary>
        /// 患者唯一标识(HIS)
        /// </summary>
        [Required(ErrorMessage = "患者病历号不能为空")]
        [MaxLength(20, ErrorMessage = "患者病历号的最大长度为20")]
        public string PatientID { get; set; }

        /// <summary>
        /// 就诊号
        /// </summary>
        public string VisitNo { get; set; }

        /// <summary>
        /// 就诊时间
        /// </summary>
        public DateTime? VisitDate { get; set; }

        /// <summary>
        /// 挂号流水号
        /// </summary>
        public string RegisterNo { get; set; }

        /// <summary>
        /// 挂号时间
        /// </summary>
        public DateTime? RegisterTime { get; set; }

        /// <summary>
        /// 挂号医生编码
        /// </summary>
        [MaxLength(20, ErrorMessage = "挂号医生编码最大长度为20")]
        public string RegisterDoctorCode { get; set; }

        /// <summary>
        /// 挂号医生姓名
        /// </summary>
        [MaxLength(50, ErrorMessage = "挂号医生姓名最大长度为50")]
        public string RegisterDoctorName { get; set; }

        /// <summary>
        /// 患者姓名
        /// </summary>
        public string PatientName { get; set; }

        /// <summary>
        /// 患者年龄
        /// </summary>
        [MaxLength(20)]
        public string Age { get; set; }

        /// <summary>
        /// 患者性别编码
        /// </summary>
        public string Sex { get; set; }
        /// <summary>
        /// 患者性别名称
        /// </summary>
        public string SexName { get; set; }

        /// <summary>
        /// 家庭住址
        /// </summary>
        public string HomeAddress { get; set; }

        /// <summary>
        /// 出生日期
        /// </summary>
        public DateTime? Birthday { get; set; }

        /// <summary>
        /// 费别
        /// </summary>
        public string ChargeType { get; set; }

        /// <summary>
        /// 绿色通道名称
        /// </summary>
        public string GreenRoadName { get; set; }

        /// <summary>
        /// 绿色通道编码
        /// </summary>
        public string GreenRoadCode { get; set; }

        /// <summary>
        /// 紧急联系人
        /// </summary>
        public string ContactsPerson { get; set; }

        /// <summary>
        /// 联系人电话
        /// </summary>
        public string ContactsPhone { get; set; }

        /// <summary>
        /// 主诉编码
        /// </summary>
        public string NarrationCode { get; set; }

        /// <summary>
        /// 主诉名称
        /// </summary>
        public string NarrationName { get; set; }

        /// <summary>
        /// 分诊人
        /// </summary>
        public string TriageUserCode { get; set; }

        /// <summary>
        /// 分诊人名称
        /// </summary>
        public string TriageUserName { get; set; }

        /// <summary>
        /// 就诊区域编码
        /// </summary>
        [Required(ErrorMessage = "就诊区域编码不能为空")]
        [MaxLength(20, ErrorMessage = "就诊区域编码的最大长度为20")]
        public string AreaCode { get; set; }

        /// <summary>
        /// 就诊区域名称
        /// </summary>
        [Required(ErrorMessage = "就诊区域名称不能为空")]
        [MaxLength(50, ErrorMessage = "就诊区域名称的最大长度为50")]
        public string AreaName { get; set; }


        /// <summary>
        /// 床号
        /// </summary>
        [MaxLength(20, ErrorMessage = "就诊区域的最大长度为20")]
        public string Bed { get; set; }

        /// <summary>
        /// 首诊医生编码
        /// </summary>
        [MaxLength(20, ErrorMessage = "首诊医生编码的最大长度为20")]
        public string FirstDoctorCode { get; set; }


        /// <summary>
        /// 首诊医生名称
        /// </summary>
        [MaxLength(50, ErrorMessage = "首诊医生编码的最大长度为50")]
        public string FirstDoctorName { get; set; }


        /// <summary>
        /// 责任医生编码
        /// </summary>
        [MaxLength(20, ErrorMessage = "责任医生编码的最大长度为20")]
        public string DutyDoctorCode { get; set; }

        /// <summary>
        /// 责任医生名称
        /// </summary>
        [MaxLength(50, ErrorMessage = "责任医生名称的最大长度为50")]
        public string DutyDoctorName { get; set; }


        /// <summary>
        /// 滞留时长
        /// </summary>
        [MaxLength(20, ErrorMessage = "滞留时长的最大长度为20")]
        public string RetentionTime { get; set; }


        /// <summary>
        /// 锁定时间
        /// </summary>
        public DateTime? LockDate { get; set; }


        /// <summary>
        /// 护理等级
        /// </summary>
        [MaxLength(20, ErrorMessage = "护理等级的最大长度为20")]
        public string NurseGrade { get; set; }

        /// <summary>
        /// 责任护士编码
        /// </summary>
        [MaxLength(20, ErrorMessage = "责任护士编码的最大长度为20")]
        public string DutyNurseCode { get; set; }

        /// <summary>
        /// 责任护士名称
        /// </summary>
        [MaxLength(50, ErrorMessage = "责任护士名称的最大长度为50")]
        public string DutyNurseName { get; set; }

        /// <summary>
        /// 接诊人编码
        /// </summary>
        [MaxLength(20, ErrorMessage = "接诊人编码的最大长度为20")]
        public string OperatorCode { get; set; }

        /// <summary>
        /// 接诊人名称
        /// </summary>
        [MaxLength(50, ErrorMessage = "接诊人名称的最大长度为50")]
        public string OperatorName { get; set; }

        /// <summary>
        /// 就诊状态  未挂号 = 0, 待就诊 = 1, 过号 = 2, 已退号 = 3, 正在就诊 = 4,已就诊 = 5, 出科 = 6,
        /// </summary>
        public VisitStatus VisitStatus { get; set; } = VisitStatus.未挂号;

        /// <summary>
        /// 非计划重返抢救室
        /// </summary>
        public bool IsPlanBackRoom { get; set; }

        /// <summary>
        /// 危重等级 一般 = 0，病重 = 1， 濒危 = 2
        /// </summary>
        public EmergencyLevel EmergencyLevel { get; set; }

        /// <summary>
        /// 入科时间
        /// </summary>
        public DateTime? InDeptTime { get; set; }

        /// <summary>
        /// 出科时间
        /// </summary>
        public DateTime? OutDeptTime { get; set; }

        /// <summary>
        /// 入科方式
        /// </summary>
        public string InDeptWay { get; set; }

        /// <summary>
        /// 当前用户是否对此患者关注(默认不关注)
        /// </summary>
        public bool AttentionCode { get; set; }

        /// <summary>
        /// 就诊卡号
        /// </summary>
        [MaxLength(20, ErrorMessage = "就诊卡号的最大长度为20")]
        public string CardNo { get; set; }


        /// <summary>
        /// 医保卡号
        /// </summary>
        [MaxLength(20, ErrorMessage = "医保卡号的最大长度为20")]
        public string MedicalCard { get; set; }

        /// <summary>
        /// 身份证号
        /// </summary>
        [Required(ErrorMessage = "身份证号不能为空")]
        [MaxLength(20, ErrorMessage = "身份证号的最大长度为20")]
        public string IDNo { get; set; }

        /// <summary>
        /// 分诊分级
        /// </summary>
        [Required(ErrorMessage = "分诊分级不能为空")]
        [MaxLength(20, ErrorMessage = "分诊分级的最大长度为20")]
        public string TriageLevel { get; set; }

        /// <summary>
        /// 分诊时间
        /// </summary>
        public DateTime TriageTime { get; set; }

        /// <summary>
        /// 是否转住院
        /// </summary>
        public bool IsInHospital { get; set; }

        /// <summary>
        /// 分诊科室编码
        /// </summary>
        [Required(ErrorMessage = "分诊科室编码不能")]
        [MaxLength(20, ErrorMessage = "分诊科室编码的最大长度为20")]
        public string TriageDeptCode { get; set; }

        /// <summary>
        /// 分诊科室名称
        /// </summary>
        [MaxLength(20, ErrorMessage = "分诊科室名称的最大长度为50")]
        public string TriageDeptName { get; set; }

        /// <summary>
        /// 体重
        /// </summary>
        [MaxLength(20, ErrorMessage = "体重的最大长度为20")]
        public string Weight { get; set; }

        /// <summary>
        /// 身高
        /// </summary>
        [MaxLength(20, ErrorMessage = "身高的最大长度为20")]
        public string Height { get; set; }

        /// <summary>
        /// 既往史
        /// </summary>
        [MaxLength(20, ErrorMessage = "既往史的最大长度为200")]
        public string PastMedicalHistory { get; set; }

        /// <summary>
        /// 传染病史
        /// </summary>
        [MaxLength(20, ErrorMessage = "传染病史的最大长度为200")]
        public string InfectiousHistory { get; set; }

        /// <summary>
        /// 过敏史
        /// </summary>
        [MaxLength(20, ErrorMessage = "过敏史的最大长度为200")]
        public string AllergyHistory { get; set; }

        /// <summary>
        /// 是否流感
        /// </summary>
        public bool FluFlag { get; set; }

        /// <summary>
        /// 流感体温
        /// </summary>
        public string FluTemp { get; set; }

        /// <summary>
        /// 是否咳嗽
        /// </summary>
        public bool CoughFlag { get; set; }

        /// <summary>
        /// 是否咽痛
        /// </summary>
        public bool ChestFlag { get; set; }

        /// <summary>
        /// 就诊类型编码
        /// </summary>
        [MaxLength(50)]
        public string TypeOfVisitCode { get; set; }

        /// <summary>
        /// 就诊类型编码
        /// </summary>
        [MaxLength(100)]
        public string TypeOfVisitName { get; set; }

        /// <summary>
        /// 是否分诊错误
        /// </summary>
        public bool TriageErrorFlag { get; set; }


        /// <summary>
        /// 患者去向
        /// </summary>
        [MaxLength(50, ErrorMessage = "患者去向的最大长度为50")]
        public string PatientWhereAbout { get; set; }

        /// <summary>
        /// 序号
        /// </summary>
        public int Sort { get; set; }

        /// <summary>
        /// 是否顶置
        /// </summary>
        public bool IsTop { get; set; }

        /// <summary>
        /// 是否已叫号
        /// </summary>
        public bool IsCalled { get; set; }
        /// <summary>
        /// RFID
        /// </summary>
        public string RFID { get; set; }

        /// <summary>
        /// 监护人证件类型（默认居民身份证）
        /// </summary>
        public string GuardianIdTypeCode { get; set; }

        /// <summary>
        /// 监护人证件类型（默认居民身份证）
        /// </summary>
        public string GuardianIdTypeName { get; set; }
        /// <summary>
        /// 证件类型（默认居民身份证）
        /// </summary>
        public string IdTypeCode { get; set; }

        /// <summary>
        /// 证件类型（默认居民身份证）
        /// </summary>
        public string IdTypeName { get; set; }
        /// <summary>
        /// 来院方式Code
        /// </summary>
        public string ToHospitalWayCode { get; set; }

        /// <summary>
        /// 来院方式名称
        /// </summary>
        public string ToHospitalWayName { get; set; }
        /// <summary>
        /// 人群编码
        /// </summary>
        public string CrowdCode { get; set; }

        /// <summary>
        /// 人群
        /// </summary>
        public string CrowdName { get; set; }
        /// <summary>
        /// 就诊原因编码
        /// </summary>
        public string VisitReasonCode { get; set; }

        /// <summary>
        /// 就诊原因
        /// </summary>
        public string VisitReasonName { get; set; }
        /// <summary>
        /// 监护人身份证号码
        /// </summary>
        public string GuardianIdCardNo { get; set; }
        /// <summary>
        /// 重点病种编码
        /// </summary>
        public string KeyDiseasesCode { set; get; }

        /// <summary>
        /// 重点病种名称
        /// </summary>
        public string KeyDiseasesName { set; get; }
        /// <summary>
        /// 修改级别编码
        /// </summary>
        public string ModifyLevelCode { get; set; }

        /// <summary>
        /// 修改级别名称
        /// </summary>
        public string ModifyLevelName { get; set; }
        /// <summary>
        /// 是否开诊查费
        /// </summary>
        public bool IsOpenDiagnosisCost { get; set; }
    }
}