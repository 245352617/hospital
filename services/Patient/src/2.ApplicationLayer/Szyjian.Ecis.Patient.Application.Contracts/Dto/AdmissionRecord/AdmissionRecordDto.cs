using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Szyjian.Ecis.Patient.Domain.Shared;

namespace Szyjian.Ecis.Patient.Application.Contracts
{
    /// <summary>
    /// 病患信息Dto
    /// </summary>
    public class AdmissionRecordDto
    {
        /// <summary>
        /// 自增Id 
        /// </summary>
        public int AR_ID { get; set; }

        /// <summary>
        /// PVID 分诊库患者基本信息表主键ID
        /// </summary>
        public Guid PI_ID { get; set; }

        /// <summary>
        /// 患者唯一标识(HIS)
        /// </summary>
        public string PatientID { get; set; }

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
        /// 是否三无病人
        /// </summary>
        public bool IsNoThree { get; set; }

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
        /// 挂号流水号,冗余返回
        /// </summary>
        public string RegisterSerialNo
        {
            get
            {
                return RegisterNo;
            }
        }

        /// <summary>
        /// 挂号时间
        /// </summary>
        public DateTime? RegisterTime { get; set; }

        /// <summary>
        /// 挂号医生编码
        /// </summary>
        public string RegisterDoctorCode { get; set; }

        /// <summary>
        /// 挂号医生姓名
        /// </summary>
        public string RegisterDoctorName { get; set; }

        /// <summary>
        /// 分诊指定医生编码
        /// </summary>
        public string TriageDoctorCode { get; set; }

        /// <summary>
        /// 分诊指定医生姓名
        /// </summary>
        public string TriageDoctorName { get; set; }

        /// <summary>
        /// 患者姓名
        /// </summary>
        public string PatientName { get; set; }

        /// <summary>
        /// 患者姓名拼音首字母
        /// </summary>
        public string PatientNamePy { get; set; }

        /// <summary>
        /// 患者年龄
        /// </summary>
        public string Age { get; set; }

        /// <summary>
        /// 患者性别
        /// </summary>
        public string Sex { get; set; }

        /// <summary>
        /// 患者性别名称
        /// </summary>
        public string SexName { get; set; }

        /// <summary>
        /// 就诊流水号
        /// </summary>
        public string VisSerialNo { get; set; }

        /// <summary>
        /// 发票号
        /// </summary>
        public string InvoiceNum { get; set; }

        /// <summary>
        /// 家庭住址
        /// </summary>
        public string HomeAddress { get; set; }

        /// <summary>
        /// 患者地址address，冗余返回
        /// </summary>
        public string Address
        {
            get
            {
                return HomeAddress;
            }
        }

        /// <summary>
        /// 出生日期
        /// </summary>
        public DateTime? Birthday { get; set; }

        /// <summary>
        /// 费别
        /// </summary>
        public string ChargeType { get; set; }

        /// <summary>
        /// 费别
        /// </summary>
        public string ChargeTypeName { get; set; }

        /// <summary>
        /// 绿色通道名称
        /// </summary>
        public string GreenRoadName { get; set; }

        /// <summary>
        /// 绿色通道编码
        /// </summary>
        public string GreenRoadCode { get; set; }

        /// <summary>
        /// 就诊区域编码
        /// </summary>
        public string AreaCode { get; set; }

        /// <summary>
        /// 就诊区域
        /// </summary>
        public string AreaName { get; set; }

        /// <summary>
        /// 过号时间
        /// </summary>
        public DateTime? ExpireNumberTime { get; set; }

        /// <summary>
        /// 床号
        /// </summary>
        public string Bed { get; set; }

        /// <summary>
        /// 首诊医生编码
        /// </summary>
        public string FirstDoctorCode { get; set; }


        /// <summary>
        /// 首诊医生名称
        /// </summary>
        public string FirstDoctorName { get; set; }


        /// <summary>
        /// 责任医生编码
        /// </summary>
        public string DutyDoctorCode { get; set; }

        /// <summary>
        /// 责任医生名称
        /// </summary>
        public string DutyDoctorName { get; set; }


        /// <summary>
        /// 滞留时长
        /// </summary>
        public string RetentionTime { get; set; }


        /// <summary>
        /// 等待时长
        /// </summary>
        public string WaitingTime { get; set; }

        /// <summary>
        /// 锁定时间
        /// </summary>
        public DateTime? LockDate { get; set; }


        /// <summary>
        /// 护理等级
        /// </summary>
        public string NurseGrade { get; set; }

        /// <summary>
        /// 责任护士编码
        /// </summary>
        public string DutyNurseCode { get; set; }

        /// <summary>
        /// 责任护士名称
        /// </summary>
        public string DutyNurseName { get; set; }

        /// <summary>
        /// 接诊人编码
        /// </summary>
        public string OperatorCode { get; set; }

        /// <summary>
        /// 接诊人名称
        /// </summary>
        public string OperatorName { get; set; }

        /// <summary>
        /// 就诊状态
        /// </summary>
        public VisitStatus VisitStatus { get; set; }

        /// <summary>
        /// 非计划重返抢救室
        /// </summary>
        public bool IsPlanBackRoom { get; set; }

        /// <summary>
        /// 危重等级
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
        /// 当前用户是否对此患者关注
        /// </summary>
        public string IsAttention { get; set; }

        /// <summary>
        /// 就诊卡号
        /// </summary>
        public string CardNo { get; set; }


        /// <summary>
        /// 医保卡号
        /// </summary>
        public string MedicalCard { get; set; }

        /// <summary>
        /// 身份证号
        /// </summary>
        public string IDNo { get; set; }

        /// <summary>
        /// 分诊分级编码
        /// </summary>
        public string TriageLevel { get; set; }

        /// <summary>
        /// 分诊分级名称
        /// </summary>
        public string TriageLevelName { get; set; }

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
        public string TriageDeptCode { get; set; }

        /// <summary>
        /// 接诊科室编码（部分医院挂号科室是门诊科室的二级科室，物理上是同一个科室）
        /// </summary>
        public string DeptCode { get; set; }

        /// <summary>
        /// 接诊科室
        /// </summary>
        public string DeptName { get; set; }

        /// <summary>
        /// 分诊科室名称
        /// </summary>
        public string TriageDeptName { get; set; }

        /// <summary>
        /// 体重
        /// </summary>
        public string Weight { get; set; }

        /// <summary>
        /// 身高
        /// </summary>
        public string Height { get; set; }

        /// <summary>
        /// 既往史
        /// </summary>
        public string PastMedicalHistory { get; set; }

        /// <summary>
        /// 传染病史
        /// </summary>
        public string InfectiousHistory { get; set; }

        /// <summary>
        /// 过敏史
        /// </summary>
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
        public string PatientWhereAbout { get; set; }

        /// <summary>
        /// 分诊去向编码
        /// </summary>
        [MaxLength(50)]
        public string TriageDirectionCode { get; set; }

        /// <summary>
        /// 分诊去向名称
        /// </summary>
        [MaxLength(50)]
        public string TriageDirectionName { get; set; }


        /// <summary>
        /// 补充说明
        /// </summary>
        public string SupplementaryNotes { set; get; }

        /// <summary>
        /// 重点病种
        /// </summary>
        public string KeyDiseasesCode { set; get; }

        /// <summary>
        /// 重点病种名称
        /// </summary>
        public string KeyDiseasesName { set; get; }

        /// <summary>
        /// 死亡时间
        /// </summary>
        public DateTime? DeathTime { get; set; }

        /// <summary>
        /// 出科原因 兼容老版本
        /// </summary>
        public OutDeptReason OutDeptReason { get; set; }

        /// <summary>
        /// 出科原因编码
        /// </summary>
        public string OutDeptReasonCode { get; set; }

        /// <summary>
        /// 出科原因描述
        /// </summary>
        public string OutDeptReasonName { get; set; }

        /// <summary>
        /// 诊断编码
        /// </summary>
        public string DiagnoseCode { get; set; }

        /// <summary>
        /// 诊断名称
        /// </summary>
        public string DiagnoseName { get; set; }

        /// <summary>
        /// 序号
        /// </summary>
        public int Sort { get; set; }

        /// <summary>
        /// 是否顶置
        /// </summary>
        public bool IsTop { get; set; }

        /// <summary>
        /// 叫号排队号
        /// </summary>
        public string CallingSn { get; set; }

        /// <summary>
        /// 叫号状态
        /// </summary>
        public CallStatus CallStatus { get; set; }

        /// <summary>
        /// 叫号诊室编码
        /// </summary>
        public string CallConsultingRoomCode { get; set; }

        /// <summary>
        /// 叫号诊室编码
        /// </summary>
        public string CallConsultingRoomName { get; set; }

        /// <summary>
        /// 叫号医生 ID
        /// </summary>
        [StringLength(200)]
        public string CallingDoctorId { get; set; }

        /// <summary>
        /// 叫号医生名称
        /// </summary>
        [StringLength(200)]
        public string CallingDoctorName { get; set; }

        /// <summary>
        /// 身份类型编码
        /// </summary>
        public string IdentityCode { get; set; }

        /// <summary>
        /// 身份类型名称
        /// </summary>
        public string IdentityName { get; set; }

        /// <summary>
        /// 是否为转留观区、抢救区区患者
        /// </summary>
        public bool IsHasTransfer { get; set; }

        /// <summary>
        /// 结束就诊时间
        /// </summary>
        public DateTime? FinishVisitTime { get; set; }

        /// <summary>
        /// 意识
        /// </summary>
        public string Consciousness { get; set; }

        /// <summary>
        /// 最终去向代码
        /// </summary>
        public string LastDirectionCode { get; set; }

        /// <summary>
        /// 最终去向名称
        /// </summary>
        public string LastDirectionName { get; set; }

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
        /// 监护人/联系人电话
        /// </summary>
        public string GuardianPhone { get; set; }

        /// <summary>
        /// 修改级别编码
        /// </summary>
        public string ModifyLevelCode { get; set; }

        /// <summary>
        /// 修改级别名称
        /// </summary>
        public string ModifyLevelName { get; set; }

        /// <summary>
        /// 修改级别名称
        /// </summary>
        public bool IsFreeNumber { get; set; }

        public VitalSignInfoDto VitalSignInfo { get; set; }

        public List<ScoreInfoDto> ScoreInfo { get; set; }


        /// <summary>
        /// 代办人名称
        /// </summary>
        public string AgencyPeopleName { get; set; }

        /// <summary>
        /// 代办人证件号码
        /// </summary>
        public string AgencyPeopleCard { get; set; }

        /// <summary>
        /// 代办人联系电话
        /// </summary>
        public string AgencyPeopleMobile { get; set; }

        /// <summary>
        /// 代办人性别
        /// </summary>
        public string AgencyPeopleSex { get; set; }

        /// <summary>
        /// 代办人年龄
        /// </summary>

        public int AgencyPeopleAge { get; set; }

        /// <summary>
        /// 挂号类型 4=免费 1=普通号
        /// </summary>
        public string RegType { get; set; }

        /// <summary>
        /// 医疗证号
        /// </summary>
        public string SafetyNo { get; set; }

        /// <summary>
        /// 今年
        /// </summary>
        public int TheYear { get; set; } = DateTime.Now.Year;

        /// <summary>
        /// 今月
        /// </summary>
        public int TheMonth { get; set; } = DateTime.Now.Month;

        /// <summary>
        /// 今日
        /// </summary>
        public int TheDay { get; set; } = DateTime.Now.Day;

        /// <summary>
        /// 当前日期
        /// </summary>
        public string CurrentDate { get; set; } = DateTime.Now.ToString("yyyy-MM-dd");

        /// <summary>
        /// 当前时间
        /// </summary>
        public string CurrentDatetime { get; set; } = DateTime.Now.ToString("yyyy-MM-dd HH:mm");
        /// <summary>
        /// 当前时分
        /// </summary>
        public string CurrentHourMinute { get; set; } = DateTime.Now.ToString("HH:mm");

        /// <summary>
        /// 院前患者
        /// </summary>
        public bool IsFirstAid { get; set; }
        /// <summary>
        /// 院前患者文本
        /// </summary>
        public string IsFirstAidText { get { return IsFirstAid ? "是" : "否"; } }

        /// <summary>
        /// 护理等级
        /// </summary>  
        public string Pflegestufe { get; set; }

        /// <summary>
        /// 床头贴
        /// </summary>
        public string BedHeadSticker { get; set; }

        /// <summary>
        /// 床头贴列表
        /// </summary>
        public IList<string> BedHeadStickerList
        {
            get
            {
                if (BedHeadSticker.IsNullOrEmpty()) return new List<string>();

                return BedHeadSticker.Split(',', ';', '|');
            }
        }
        /// <summary>
        /// 是否开诊查费
        /// </summary>
        public bool IsOpenDiagnosisCost { get; set; }

        /// <summary>
        /// 是否开启绿通
        /// </summary>
        public bool IsOpenGreenChannl { get; set; }

        /// <summary>
        /// 入床时间
        /// </summary>
        public DateTime? BedTime { get; set; }

        /// <summary>
        /// 抢救入科滞留时长
        /// </summary>
        public double RescueRetentionTime { get; set; }

        /// <summary>
        /// 留观入科滞留时长
        /// </summary>
        public double ObservationRetentionTime { get; set; }

        /// <summary>
        /// 抢救入科滞留时长前端显示
        /// </summary>
        public string RescueRetentionViewTime { get; set; }

        /// <summary>
        /// 留观入科滞留时长前端显示
        /// </summary>
        public string ObservationRetentionViewTime { get; set; }

        /// <summary>
        /// 出科信息
        /// </summary>
        public string OutDeptInfo { get; set; }

        public string OutDeptCode { get; set; }

        public string OutDeptName { get; set; }

        /// <summary>
        /// 是否转诊
        /// </summary>
        public bool IsReferral { get; set; }

        #region 医保控费相关
        /// <summary>
        /// 参保人标识
        /// </summary>
        public string PatnId { get; set; }

        /// <summary>
        /// 当前就诊标识
        /// </summary>
        public string CurrMDTRTId { get; set; }

        /// <summary>
        /// 统筹区编码
        /// </summary>
        public string PoolArea { get; set; }

        /// <summary>
        /// 险种类型
        /// 310职工基本医疗保险；390城乡居民基本医疗保险；320公务员医疗补助；392城乡居民大病医疗保险；330大额医疗费用补助；510生育保险；340离休人员医疗保障；
        /// </summary>
        public string InsureType { get; set; }

        /// <summary>
        /// 异地结算标志 0否;1是
        /// </summary>
        public string OutSetlFlag { get; set; }
        #endregion

        /// <summary>
        /// 特殊病人编码
        /// </summary>
        public string SpecialCode { get; set; }

        /// <summary>
        /// 特殊病人名称
        /// </summary>
        public string SpecialName { get; set; }
    }

}