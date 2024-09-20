using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;

namespace SamJan.MicroService.PreHospital.TriageService
{
    /// <summary>
    /// 院前分诊患者信息Dto
    /// </summary>
    public class PatientInfoDto
    {
        /// <summary>
        /// Id 不需要前端传值
        /// </summary>
        public Guid TriagePatientInfoId { get; set; }

        /// <summary>
        /// 任务单号
        /// </summary>
        public Guid TaskInfoId { get; set; }

        /// <summary>
        /// 任务单流水号
        /// </summary>
        public string TaskInfoNum { get; set; }

        /// <summary>
        /// 就诊号
        /// </summary>
        public string RegisterNo { get; set; }

        /// <summary>
        /// 车牌号
        /// </summary>
        public string CarNum { get; set; }

        /// <summary>
        /// 就诊号
        /// </summary>
        public string VisitNo { get; set; }

        /// <summary>
        /// 预约流水号
        /// </summary>
        public string SeqNumber { get; set; }

        /// <summary>
        /// 叫号排队号
        /// </summary>
        public string CallingSn { get; set; }

        /// <summary>
        /// 叫号号
        /// </summary>
        public string CallNo { get; set; }

        /// <summary>
        /// 叫号登记时间
        /// 等候时长从登记时间开始，当有开始就诊时间时，到开始就诊时间结束; 否则到当前时间
        /// </summary>
        public DateTime? LogTime { get; set; }

        /// <summary>
        /// 患者唯一标识(HIS)
        /// </summary>
        public string PatientId { get; set; }

        /// <summary>
        /// 患者姓名
        /// </summary>
        public string PatientName { get; set; }

        /// <summary>
        /// 患者性别
        /// </summary>
        public string Sex { get; set; }

        /// <summary>
        /// 患者性别名称
        /// </summary>
        public string SexName { get; set; }

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
        /// 联系电话
        /// </summary>
        public string ContactsPhone { get; set; }

        /// <summary>
        /// 监护人证件类型（默认居民身份证）
        /// </summary>
        public string GuardianIdTypeCode { get; set; }

        /// <summary>
        /// 监护人证件类型（默认居民身份证）
        /// </summary>
        public string GuardianIdTypeName { get; set; }

        /// <summary>
        /// 监护人身份证号码
        /// </summary>
        public string GuardianIdCardNo { get; set; }

        /// <summary>
        /// 监护人/联系人电话
        /// </summary>
        public string GuardianPhone { get; set; }

        /// <summary>
        /// 与联系人关系
        /// </summary>
        public string SocietyRelationCode { get; set; }

        /// <summary>
        /// 与联系人关系名称
        /// </summary>
        public string SocietyRelationName { get; set; }

        /// <summary>
        /// 监护人/联系人地址
        /// </summary>
        public string ContactsAddress { get; set; }

        /// <summary>
        /// 来院方式
        /// </summary>
        public string ToHospitalWay { get; set; }

        /// <summary>
        /// 来院方式名称
        /// </summary>
        public string ToHospitalWayName { get; set; }

        /// <summary>
        /// 患者身份
        /// </summary>
        public string Identity { get; set; }

        /// <summary>
        /// 患者身份名称
        /// </summary>
        public string IdentityName { get; set; }

        /// <summary>
        /// 费别
        /// </summary>
        public string ChargeType { get; set; }

        /// <summary>
        /// 费别名称
        /// </summary>
        public string ChargeTypeName { get; set; }


        /// <summary>
        /// 特约记账类型Code
        /// </summary>
        public string SpecialAccountTypeCode { get; set; }

        /// <summary>
        /// 特约记账类型名称
        /// </summary>
        public string SpecialAccountTypeName { get; set; }

        /// <summary>
        /// 身份证号
        /// </summary>
        public string IdentityNo { get; set; }

        /// <summary>
        /// 民族
        /// </summary>
        public string Nation { get; set; }

        /// <summary>
        /// 民族名称
        /// </summary>
        public string NationName { get; set; }

        /// <summary>
        /// 国家
        /// </summary>
        public string Country { get; set; }

        /// <summary>
        /// 发病时间
        /// </summary>
        public DateTime? OnsetTime { get; set; }

        /// <summary>
        /// 绿色通道
        /// </summary>
        public string GreenRoad { get; set; }

        /// <summary>
        /// 绿色通道名称
        /// </summary>
        public string GreenRoadName { get; set; }

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

        private string _age;

        /// <summary>
        /// 年龄
        /// </summary>
        public string Age
        {
            set
            {
                _age = value;
            }
            get
            {
                return Birthday.HasValue ? Birthday.Value.GetAgeString() : _age;
            }
        }

        /// <summary>
        /// 体重
        /// </summary>
        public string Weight { get; set; }

        /// <summary>
        /// 患者姓名拼音码
        /// </summary>
        public string Py { get; set; }

        /// <summary>
        /// 重点病种
        /// </summary>
        /// <returns></returns>
        public string DiseaseCode { get; set; }

        /// <summary>
        /// 重点病种名称
        /// </summary>
        public string DiseaseName { get; set; }

        /// <summary>
        /// 分诊状态，0：暂存，1：分诊
        /// </summary>
        public int TriageStatus { get; set; }

        /// <summary>
        /// 就诊状态 0：未分诊，1：待就诊，2：正在就诊，3：已就诊，4：暂停
        /// </summary>
        public VisitStatus VisitStatus { get; set; }

        /// <summary>
        /// 开始分诊时间
        /// </summary>
        public DateTime? StartTriageTime { get; set; }

        /// <summary>
        /// 分诊时间
        /// </summary>
        public DateTime? TriageTime { get; set; }

        /// <summary>
        /// 群伤事件
        /// </summary>
        public string GroupInjuryName { get; set; }

        /// <summary>
        /// 是否三无病人
        /// </summary>
        public bool IsNoThree { get; set; }

        /// <summary>
        /// 就诊类型
        /// </summary>
        public string TypeOfVisitCode { get; set; }

        /// <summary>
        /// 就诊类型名称
        /// </summary>
        public string TypeOfVisitName { get; set; }

        /// <summary>
        /// 主诉
        /// </summary>
        public string Narration { get; set; }

        /// <summary>
        /// 主诉名称
        /// </summary>
        public string NarrationName { get; set; }

        /// <summary>
        /// 主诉备注
        /// </summary>
        public string NarrationComments { get; set; }

        /// <summary>
        /// 所有主诉内容：主诉名称+主诉备注
        /// </summary>
        public string AllNarration
        {
            get
            {
                return new List<string> { NarrationName, NarrationComments }.Where(x => !string.IsNullOrWhiteSpace(x)).JoinAsString("; ");
            }
        }

        /// <summary>
        /// 意识
        /// </summary>
        public string Consciousness { get; set; }

        /// <summary>
        /// 意识名称
        /// </summary>
        public string ConsciousnessName { get; set; }

        /// <summary>
        /// 分诊人
        /// </summary>
        public string TriageUserCode { get; set; }

        /// <summary>
        /// 分诊人名称
        /// </summary>
        public string TriageUserName { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreationTime { get; set; }

        /// <summary>
        /// 分诊耗时
        /// </summary>
        public string TriageTimeConsuming => GetTimeSpan();

        /// <summary>
        /// 患者基本信息是否不可编辑
        /// 患者信息需与 HIS 同步，所以当患者信息与HIS同步后基本信息不再允许修改
        /// 即挂号之后信息便不允许再次修改（目前没有HIS修改患者的接口）
        /// by: ywlin 2021-11-29
        /// </summary>
        public bool IsBasicInfoReadOnly { get; set; }

        /// <summary>
        /// 新冠问卷是否从外部获取
        /// 当从外部获取新冠问卷时，不在ECIS中录入个人史信息
        /// </summary>
        public bool IsCovidExamFromOuterSystem { get; set; }

        /// <summary>
        /// 院前分诊生命体征信息
        /// </summary>
        public VitalSignInfoDto VitalSignInfo { get; set; }

        /// <summary>
        /// 院前分诊结果信息
        /// </summary>
        public ConsequenceInfoDto ConsequenceInfo { get; set; }

        /// <summary>
        /// 院前分诊评分信息
        /// </summary>
        public ICollection<ScoreInfoDto> ScoreInfo { get; set; }

        /// <summary>
        /// 入院情况
        /// </summary>
        public AdmissionInfoDto AdmissionInfo { get; set; }

        /// <summary>
        /// 医疗证号
        /// </summary>
        public string SafetyNo { get; set; }

        /// <summary>
        /// 获取分诊耗时
        /// </summary>
        /// <returns></returns>
        private string GetTimeSpan()
        {
            var time = TriageTime ?? CreationTime;
            if (StartTriageTime == null || time <= StartTriageTime)
            {
                return "0分";
            }

            var n1 = StartTriageTime.Value;
            var ts = time - n1;
            return ts.Minutes.ToString("f0") + "分" + ts.Seconds.ToString("f0") + "秒";
        }

        /// <summary>
        /// 首诊医生工号
        /// </summary>
        public string FirstDoctorCode { get; set; }

        /// <summary>
        /// 首诊医生名称
        /// </summary>
        public string FirstDoctorName { get; set; }

        /// <summary>
        /// 最终去向代码
        /// </summary>
        public string LastDirectionCode { get; set; }

        /// <summary>
        /// 最终去向名称
        /// </summary>
        public string LastDirectionName { get; set; }

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
        /// 人群
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
        /// 国籍
        /// </summary>
        public string CountryName { get; set; }

        /// <summary>
        /// 国籍代码
        /// </summary>
        public string CountryCode { get; set; }

        /// <summary>
        /// 挂号类型 4=免费 1=普通号
        /// </summary>
        public string RegType { get; set; }

        /// <summary>
        /// 是否是院前患者
        /// </summary>
        public bool IsFirstAid { get; set; }

        /// <summary>
        /// 就诊医生名称
        /// </summary>
        public string DoctorName { get; set; }
        /// <summary>
        /// 是否同步挂号
        /// </summary>
        public bool IsSyncRegister { get; set; }

        /// <summary>
        /// 告知单患者
        /// </summary>
        public InformPatInfo InformInfo { get; set; }

        /// <summary>
        /// 发票号
        /// </summary>
        public string InvoiceNum { get; set; }

        /// <summary>
        /// 挂号时间
        /// </summary>
        public DateTime? RegisterTime { get; set; }

        /// <summary>
        /// 开始就诊时间
        /// </summary>
        public DateTime? BeginTime { get; set; }

        /// <summary>
        /// 结束就诊时间
        /// </summary>
        public DateTime? EndTime { get; set; }

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
        /// 是否优先
        /// </summary>
        public bool IsTop { get; set; }

        /// <summary>
        /// 是否过号
        /// </summary>
        public bool IsUntreatedOver { get; set; }

        /// <summary>
        /// 患者被接诊的就诊时间
        /// </summary>
        public DateTime? VisitDate { get; set; }

        /// <summary>
        /// 被接诊后的结束就诊时间
        /// </summary>
        public DateTime? FinishVisitTime { get; set; }

        /// <summary>
        /// 诊断代码
        /// </summary>
        public string DiagnoseCode { get; set; }

        /// <summary>
        /// 诊断名称
        /// </summary>
        public string DiagnoseName { get; set; }

        /// <summary>
        /// 叫号医生编码
        /// </summary>
        public string CallDoctorCode { get; set; }

        /// <summary>
        /// 叫号医生名称
        /// </summary>
        public string CallDoctorName { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

    }
}