using FreeSql.DataAnnotations;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Szyjian.Ecis.Patient.Domain.Shared;

namespace Szyjian.Ecis.Patient.Domain
{
    /// <summary>
    /// 入科记录表
    /// </summary>
    [Table(Name = "Pat_AdmissionRecord")]
    public class AdmissionRecord
    {
        /// <summary>
        /// 乐观锁
        /// </summary>
        [Column(IsVersion = true, Position = 1)]
        public long Version { get; set; }


        #region 患者信息段

        /// <summary>
        /// 自增Id
        /// </summary>
        [Column(IsPrimary = true, IsIdentity = true, Position = 2)]
        public int AR_ID { get; set; }

        /// <summary>
        /// PVID 分诊库患者基本信息表主键ID
        /// </summary>
        [Column(IsPrimary = true, Position = 3)]
        public Guid PI_ID { get; set; }

        /// <summary>
        /// 患者唯一标识(HIS)
        /// </summary>
        [MaxLength(20)]
        [Column(IsNullable = false, IsPrimary = true, Position = 4)]
        public string PatientID { get; set; }

        /// <summary>
        /// 患者姓名
        /// </summary>
        [Column(Position = 5)]
        public string PatientName { get; set; }

        /// <summary>
        /// 患者姓名拼音首字母
        /// </summary>
        [Column(Position = 6, IsNullable = true)]
        [MaxLength(100)]
        public string PatientNamePy { get; set; }

        /// <summary>
        /// 就诊号
        /// </summary>
        [Column(IsNullable = false, IsPrimary = true, Position = 6)]
        public string VisitNo { get; set; }

        /// <summary>
        /// 就诊流水号
        /// </summary>
        [Column(Position = 7)]
        [StringLength(20)]
        public string VisSerialNo { get; set; }

        /// <summary>
        /// 挂号流水号
        /// </summary>
        [Column(Position = 7)]
        public string RegisterNo { get; set; }

        /// <summary>
        /// 患者性别编码
        /// </summary>
        [Column(Position = 8)]
        public string Sex { get; set; }

        /// <summary>
        /// 患者性别名称
        /// </summary>
        [Column(Position = 8)]
        public string SexName { get; set; }

        /// <summary>
        /// 患者年龄
        /// </summary>
        [MaxLength(20)]
        [Column(Position = 9)]
        public string Age { get; set; }

        /// <summary>
        /// 费别
        /// </summary>
        [Column(IsNullable = false, Position = 10)]
        public string ChargeType { get; set; }

        /// <summary>
        /// 费别
        /// </summary>
        [Column(IsNullable = false, Position = 11)]
        public string ChargeTypeName { get; set; }

        /// <summary>
        /// 挂号类型 4=免费 1=普通号
        /// </summary>
        public string RegType { get; set; }

        /// <summary>
        /// 医疗证号
        /// </summary>
        public string SafetyNo { get; set; }

        /// <summary>
        /// 是否三无病人
        /// </summary>
        public bool IsNoThree { get; set; }

        /// <summary>
        /// 紧急联系人
        /// </summary>
        [MaxLength(50)]
        [Column(Position = 12)]
        public string ContactsPerson { get; set; }

        /// <summary>
        /// 联系人电话
        /// </summary>
        [MaxLength(20)]
        [Column(Position = 13)]
        public string ContactsPhone { get; set; }

        /// <summary>
        /// 主诉编码
        /// </summary>
        [Column(Position = 14)]
        public string NarrationCode { get; set; }

        /// <summary>
        /// 主诉名称
        /// </summary>
        [Column(Position = 15)]
        public string NarrationName { get; set; }

        /// <summary>
        /// 分诊人
        /// </summary>
        [MaxLength(20)]
        [Column(Position = 16)]
        public string TriageUserCode { get; set; }

        /// <summary>
        /// 分诊人名称
        /// </summary>
        [MaxLength(50)]
        [Column(Position = 17)]
        public string TriageUserName { get; set; }

        /// <summary>
        /// 绿色通道名称
        /// </summary>
        [Column(Position = 18)]
        public string GreenRoadName { get; set; }

        /// <summary>
        /// 绿色通道编码
        /// </summary>
        [Column(Position = 19)]
        public string GreenRoadCode { get; set; }

        /// <summary>
        /// 就诊卡号
        /// </summary>
        [MaxLength(20)]
        [Column(Position = 20)]
        public string CardNo { get; set; }


        /// <summary>
        /// 医保卡号
        /// </summary>
        [MaxLength(20)]
        [Column(Position = 21)]
        public string MedicalCard { get; set; }

        /// <summary>
        /// 身份证号
        /// </summary>
        [MaxLength(20)]
        [Column(IsNullable = true, Position = 22)]
        public string IDNo { get; set; }

        /// <summary>
        /// 体重
        /// </summary>
        [MaxLength(20)]
        [Column(Position = 23)]
        public string Weight { get; set; }

        /// <summary>
        /// 身高
        /// </summary>
        [MaxLength(20)]
        [Column(Position = 24)]
        public string Height { get; set; }

        /// <summary>
        /// 家庭住址
        /// </summary>
        [MaxLength(200)]
        [Column(Position = 25)]
        public string HomeAddress { get; set; }


        /// <summary>
        /// 出生日期
        /// </summary>
        [Column(Position = 26)]
        public DateTime? Birthday { get; set; }

        /// <summary>
        /// 身份类型编码
        /// </summary>
        [Column(Position = 27)]
        public string IdentityCode { get; set; }

        /// <summary>
        /// 身份类型名称
        /// </summary>
        [Column(Position = 28)]
        public string IdentityName { get; set; }

        #endregion

        #region 分诊信息段

        /// <summary>
        /// 分诊分级
        /// </summary>
        [MaxLength(50)]
        [Column(IsNullable = false, Position = 29)]
        public string TriageLevel { get; set; }

        /// <summary>
        /// 分诊级别名称
        /// </summary>
        [MaxLength(100)]
        [Column(IsNullable = false, Position = 30)]
        public string TriageLevelName { get; set; }

        /// <summary>
        /// 分诊科室编码（挂号科室）
        /// </summary>
        [MaxLength(50)]
        [Column(IsNullable = false, Position = 31)]
        public string TriageDeptCode { get; set; }

        /// <summary>
        /// 分诊科室名称
        /// </summary>
        [MaxLength(50)]
        [Column(IsNullable = false, Position = 32)]
        public string TriageDeptName { get; set; }

        /// <summary>
        /// his科室编码
        /// </summary>
        [StringLength(50)]
        [Description("his科室编码")]
        public string HisDeptCode { get; set; }

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
        /// 患者去向编码
        /// </summary>
        [MaxLength(50)]
        [Column(Position = 33)]
        public string PatientWhereAboutCode { get; set; }

        /// <summary>
        /// 患者去向名称
        /// </summary>
        [MaxLength(50)]
        [Column(Position = 34)]
        public string PatientWhereAboutName { get; set; }

        /// <summary>
        /// 既往史
        /// </summary>
        [MaxLength(200)]
        [Column(Position = 35)]
        public string PastMedicalHistory { get; set; }

        /// <summary>
        /// 传染病史
        /// </summary>
        [MaxLength(200)]
        [Column(Position = 36)]
        public string InfectiousHistory { get; set; }

        /// <summary>
        /// 过敏史
        /// </summary>
        [MaxLength(200)]
        [Column(Position = 37)]
        public string AllergyHistory { get; set; }

        /// <summary>
        /// 是否流感
        /// </summary>
        [Column(Position = 38)]
        public bool FluFlag { get; set; }

        /// <summary>
        /// 流感体温
        /// </summary>
        [MaxLength(20)]
        [Column(Position = 39)]
        public string FluTemp { get; set; }

        /// <summary>
        /// 是否咳嗽
        /// </summary>
        [Column(Position = 40)]
        public bool CoughFlag { get; set; }

        /// <summary>
        /// 是否咽痛
        /// </summary>
        [Column(Position = 41)]
        public bool ChestFlag { get; set; }

        /// <summary>
        /// 就诊类型编码
        /// </summary>
        [MaxLength(50)]
        [Column(Position = 42)]
        public string TypeOfVisitCode { get; set; }

        /// <summary>
        /// 就诊类型编码
        /// </summary>
        [MaxLength(100)]
        [Column(Position = 43)]
        public string TypeOfVisitName { get; set; }

        /// <summary>
        /// 是否分诊错误
        /// </summary>
        [Column(Position = 44)]
        public bool TriageErrorFlag { get; set; }

        /// <summary>
        /// 修改级别
        /// </summary>
        [MaxLength(100)]
        [Column(Position = 43)]
        public string ChangeLevel { get; set; }

        #endregion

        #region 时间节点

        /// <summary>
        /// 就诊时间
        /// </summary>
        [Column(IsNullable = true, Position = 45)]
        public DateTime? VisitDate { get; set; }

        /// <summary>
        /// 挂号时间
        /// </summary>
        [Column(IsNullable = true, Position = 47)]
        public DateTime? RegisterTime { get; set; }

        /// <summary>
        /// 锁定时间
        /// </summary>
        [Column(IsNullable = true, Position = 48)]
        public DateTime? LockDate { get; set; }

        /// <summary>
        /// 入科时间
        /// </summary>
        [Column(IsNullable = true, Position = 49)]
        public DateTime? InDeptTime { get; set; }

        /// <summary>
        /// 出科时间
        /// </summary>
        [Column(IsNullable = true, Position = 50)]
        public DateTime? OutDeptTime { get; set; }

        /// <summary>
        /// 分诊时间
        /// </summary>
        [Column(Position = 51)]
        public DateTime TriageTime { get; set; }

        /// <summary>
        /// 死亡时间
        /// </summary>
        [Column(IsNullable = true, Position = 52)]
        public DateTime? DeathTime { get; set; }

        #endregion

        #region 患者诊疗业务段

        /// <summary>
        /// 挂号医生编码
        /// </summary>
        [MaxLength(20)]
        [Column(Position = 53)]
        public string RegisterDoctorCode { get; set; }

        /// <summary>
        /// 挂号医生姓名
        /// </summary>
        [MaxLength(50)]
        [Column(Position = 54)]
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
        /// 就诊区域编码
        /// </summary>
        [MaxLength(20)]
        [Column(IsNullable = false, Position = 55)]
        public string AreaCode { get; set; }

        /// <summary>
        /// 就诊区域名称
        /// </summary>
        [MaxLength(50)]
        [Column(IsNullable = false, Position = 56)]
        public string AreaName { get; set; }


        /// <summary>
        /// 床号
        /// </summary>
        [MaxLength(20)]
        [Column(Position = 57)]
        public string Bed { get; set; }

        /// <summary>
        /// 首诊医生编码
        /// </summary>
        [MaxLength(20)]
        [Column(Position = 58)]
        public string FirstDoctorCode { get; set; }


        /// <summary>
        /// 首诊医生名称
        /// </summary>
        [MaxLength(50)]
        [Column(Position = 59)]
        public string FirstDoctorName { get; set; }


        /// <summary>
        /// 责任医生编码
        /// </summary>
        [MaxLength(20)]
        [Column(Position = 60)]
        public string DutyDoctorCode { get; set; }

        /// <summary>
        /// 责任医生名称
        /// </summary>
        [MaxLength(50)]
        [Column(Position = 61)]
        public string DutyDoctorName { get; set; }

        /// <summary>
        /// 滞留时长
        /// </summary>
        [MaxLength(20)]
        [Column(Position = 62)]
        public string RetentionTime { get; set; }

        /// <summary>
        /// 护理等级
        /// </summary>
        [MaxLength(20)]
        [Column(Position = 63)]
        public string NurseGrade { get; set; }

        /// <summary>
        /// 责任护士编码
        /// </summary>
        [MaxLength(20)]
        [Column(Position = 64)]
        public string DutyNurseCode { get; set; }

        /// <summary>
        /// 责任护士名称
        /// </summary>
        [MaxLength(50)]
        [Column(Position = 65)]
        public string DutyNurseName { get; set; }

        /// <summary>
        /// 接诊人编码
        /// </summary>
        [MaxLength(20)]
        [Column(Position = 66)]
        public string OperatorCode { get; set; }

        /// <summary>
        /// 接诊人名称
        /// </summary>
        [MaxLength(50)]
        [Column(Position = 67)]
        public string OperatorName { get; set; }

        /// <summary>
        /// 就诊状态
        /// </summary>
        [Column(Position = 68)]
        public VisitStatus VisitStatus { get; set; }

        /// <summary>
        /// 患者状态
        /// </summary>
        public PatientStatus PatientStatus { get; set; } = PatientStatus.Default;

        /// <summary>
        /// 非计划重返抢救室
        /// </summary>
        [Column(Position = 69)]
        public bool IsPlanBackRoom { get; set; }

        /// <summary>
        /// 危重等级
        /// </summary>
        [Column(Position = 70)]
        public EmergencyLevel EmergencyLevel { get; set; }

        /// <summary>
        /// 入科方式
        /// </summary>
        [Column(Position = 71)]
        public string InDeptWay { get; set; }

        /// <summary>
        /// 关注人编码使用 | 分割
        /// </summary>
        [Column(Position = 72)]
        public string AttentionCode { get; set; }


        /// <summary>
        /// 是否转住院
        /// </summary>
        [Column(Position = 73)]
        public bool IsInHospital { get; set; }


        /// <summary>
        /// 补充说明
        /// </summary>
        [MaxLength(500)]
        [Column(Position = 74)]
        public string SupplementaryNotes { set; get; }

        /// <summary>
        /// 重点病种编码
        /// </summary>
        [MaxLength(50)]
        [Column(Position = 75)]
        public string KeyDiseasesCode { set; get; }

        /// <summary>
        /// 重点病种名称
        /// </summary>
        [MaxLength(100)]
        [Column(Position = 76)]
        public string KeyDiseasesName { set; get; }

        /// <summary>
        /// 出科原因,兼容老数据
        /// </summary>
        public OutDeptReason OutDeptReason { get; set; }

        /// <summary>
        /// 出科原因编码
        /// </summary>
        [MaxLength(60)]
        public string OutDeptReasonCode { get; set; }

        /// <summary>
        /// 出科原因名称
        /// </summary>
        [MaxLength(60)]
        public string OutDeptReasonName { get; set; }

        /// <summary>
        /// 序号
        /// </summary>
        [Column(Position = 80)]
        public int Sort { get; set; }

        /// <summary>
        /// 是否顶置
        /// </summary>
        [Column(Position = 81)]
        public bool IsTop { get; set; }

        /// <summary>
        /// 叫号状态 
        /// </summary>
        [Column(Position = 82, OldName = "IsCalled")]
        public CallStatus CallStatus { get; set; }

        /// <summary>
        /// 叫号排队号
        /// </summary>
        public string CallingSn { get; set; }

        /// <summary>
        /// 叫号登记时间
        /// 等候时长从登记时间开始，当有开始就诊时间时，到开始就诊时间结束; 否则到当前时间
        /// </summary>
        public DateTime? LogTime { get; set; }

        /// <summary>
        /// 叫号诊室编码
        /// </summary>
        public string CallConsultingRoomCode { get; set; }

        /// <summary>
        /// 叫号诊室编码
        /// </summary>
        public string CallConsultingRoomName { get; set; }

        #endregion

        /// <summary>
        /// 结束就诊时间
        /// </summary>
        public DateTime? FinishVisitTime { get; set; }

        /// <summary>
        /// 过号时间
        /// </summary>
        public DateTime? ExpireNumberTime { get; set; }

        /// <summary>
        /// 最终去向代码
        /// </summary>
        public string LastDirectionCode { get; set; }

        /// <summary>
        /// 最终去向名称
        /// </summary>
        public string LastDirectionName { get; set; }

        /// <summary>
        /// 意识
        /// </summary>
        public string Consciousness { get; set; }

        /// <summary>
        /// RFID
        /// </summary>
        public string RFID { get; set; }

        /// <summary>
        /// 监护人证件类型（默认居民身份证）
        /// </summary>
        [StringLength(20)]
        public string GuardianIdTypeCode { get; set; }

        /// <summary>
        /// 监护人证件类型（默认居民身份证）
        /// </summary>
        [StringLength(20)]
        public string GuardianIdTypeName { get; set; }
        /// <summary>
        /// 证件类型（默认居民身份证）
        /// </summary>
        [StringLength(20)]
        public string IdTypeCode { get; set; }

        /// <summary>
        /// 证件类型（默认居民身份证）
        /// </summary>
        [StringLength(20)]
        public string IdTypeName { get; set; }

        /// <summary>
        /// 来院方式Code
        /// </summary>
        [StringLength(60)]
        public string ToHospitalWayCode { get; set; }

        /// <summary>
        /// 来院方式名称
        /// </summary>
        [StringLength(60)]
        public string ToHospitalWayName { get; set; }

        /// <summary>
        /// 人群编码
        /// </summary>
        [StringLength(50)]
        public string CrowdCode { get; set; }

        /// <summary>
        /// 人群
        /// </summary>
        [StringLength(50)]
        public string CrowdName { get; set; }

        /// <summary>
        /// 就诊原因编码
        /// </summary>
        [StringLength(4000)]
        public string VisitReasonCode { get; set; }

        /// <summary>
        /// 就诊原因
        /// </summary>
        [StringLength(4000)]
        public string VisitReasonName { get; set; }

        /// <summary>
        /// 监护人身份证号码
        /// </summary>
        [StringLength(20)]
        public string GuardianIdCardNo { get; set; }

        /// <summary>
        /// 监护人/联系人电话
        /// </summary>
        [StringLength(20)]
        public string GuardianPhone { get; set; }

        /// <summary>
        /// 修改级别编码
        /// </summary>
        [StringLength(200)]
        public string ModifyLevelCode { get; set; }

        /// <summary>
        /// 修改级别名称
        /// </summary>
        [StringLength(400)]
        public string ModifyLevelName { get; set; }

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
        /// 是否是院前患者
        /// </summary>
        public bool IsFirstAid { get; set; }

        /// <summary>
        /// 接诊科室编码（部分医院挂号科室是门诊科室的二级科室，物理上是同一个科室）
        /// </summary>
        [MaxLength(50)]
        [Column(IsNullable = true, Position = 31)]
        public string DeptCode { get; set; }

        /// <summary>
        /// 接诊科室编码（部分医院挂号科室是门诊科室的二级科室，物理上是同一个科室）
        /// </summary>
        [MaxLength(50)]
        [Column(IsNullable = true, Position = 31)]
        public string DeptName { get; set; }

        /// <summary>
        /// 护理等级
        /// </summary>  
        [StringLength(50)]
        public string Pflegestufe { get; set; }

        /// <summary>
        /// 床头贴列表
        /// </summary>
        [StringLength(250)]
        public string BedHeadSticker { get; set; }

        /// <summary>
        /// 是否开诊查费
        /// </summary>
        public bool IsOpenDiagnosisCost { get; set; }

        /// <summary>
        /// 是否自动结束 默认false自动结束
        /// </summary>
        public bool NotAutoEnd { get; set; }

        /// <summary>
        /// 是否开启绿通
        /// </summary>
        public bool IsOpenGreenChannl { get; set; }

        /// <summary>
        /// 发票号
        /// </summary>
        public string InvoiceNum { get; set; }


        /// <summary>
        /// 入床时间
        /// </summary>
        public DateTime? BedTime { get; set; }

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


        [MaxLength(50)]
        public string OutDeptCode { get; set; }

        [MaxLength(50)]
        public string OutDeptName { get; set; }

        /// <summary>
        /// 是否已经推送绿通信息到his
        /// </summary>
        public bool HasPushGreenChannel { get; set; }

        /// <summary>
        /// 召回时间
        /// </summary>
        public DateTime? RecallTime { get; set; }

        /// <summary>
        /// 抢救入科滞留时长
        /// </summary>
        public double RescueRetentionTime { get; set; }

        /// <summary>
        /// 留观入科滞留时长
        /// </summary>
        public double ObservationRetentionTime { get; set; }

        /// <summary>
        /// 是否转诊
        /// </summary>
        public bool IsReferral { get; set; }

        /// <summary>
        /// 特殊病人编码
        /// </summary>
        public string SpecialCode { get; set; }

        /// <summary>
        /// 特殊病人名称
        /// </summary>
        public string SpecialName { get; set; }

        /// <summary>
        /// 获取ToString
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"{{{nameof(Version)}={Version.ToString()}, {nameof(AR_ID)}={AR_ID.ToString()}, {nameof(PI_ID)}={PI_ID.ToString()}, {nameof(PatientID)}={PatientID}, {nameof(PatientName)}={PatientName}, {nameof(PatientNamePy)}={PatientNamePy}, {nameof(VisitNo)}={VisitNo}, {nameof(VisSerialNo)}={VisSerialNo}, {nameof(RegisterNo)}={RegisterNo}, {nameof(Sex)}={Sex}, {nameof(SexName)}={SexName}, {nameof(Age)}={Age}, {nameof(ChargeType)}={ChargeType}, {nameof(ChargeTypeName)}={ChargeTypeName}, {nameof(RegType)}={RegType}, {nameof(SafetyNo)}={SafetyNo}, {nameof(IsNoThree)}={IsNoThree.ToString()}, {nameof(ContactsPerson)}={ContactsPerson}, {nameof(ContactsPhone)}={ContactsPhone}, {nameof(NarrationCode)}={NarrationCode}, {nameof(NarrationName)}={NarrationName}, {nameof(TriageUserCode)}={TriageUserCode}, {nameof(TriageUserName)}={TriageUserName}, {nameof(GreenRoadName)}={GreenRoadName}, {nameof(GreenRoadCode)}={GreenRoadCode}, {nameof(CardNo)}={CardNo}, {nameof(MedicalCard)}={MedicalCard}, {nameof(IDNo)}={IDNo}, {nameof(Weight)}={Weight}, {nameof(Height)}={Height}, {nameof(HomeAddress)}={HomeAddress}, {nameof(Birthday)}={Birthday.ToString()}, {nameof(IdentityCode)}={IdentityCode}, {nameof(IdentityName)}={IdentityName}, {nameof(TriageLevel)}={TriageLevel}, {nameof(TriageLevelName)}={TriageLevelName}, {nameof(TriageDeptCode)}={TriageDeptCode}, {nameof(TriageDeptName)}={TriageDeptName}, {nameof(HisDeptCode)}={HisDeptCode}, {nameof(TriageDirectionCode)}={TriageDirectionCode}, {nameof(TriageDirectionName)}={TriageDirectionName}, {nameof(PatientWhereAboutCode)}={PatientWhereAboutCode}, {nameof(PatientWhereAboutName)}={PatientWhereAboutName}, {nameof(PastMedicalHistory)}={PastMedicalHistory}, {nameof(InfectiousHistory)}={InfectiousHistory}, {nameof(AllergyHistory)}={AllergyHistory}, {nameof(FluFlag)}={FluFlag.ToString()}, {nameof(FluTemp)}={FluTemp}, {nameof(CoughFlag)}={CoughFlag.ToString()}, {nameof(ChestFlag)}={ChestFlag.ToString()}, {nameof(TypeOfVisitCode)}={TypeOfVisitCode}, {nameof(TypeOfVisitName)}={TypeOfVisitName}, {nameof(TriageErrorFlag)}={TriageErrorFlag.ToString()}, {nameof(ChangeLevel)}={ChangeLevel}, {nameof(VisitDate)}={VisitDate.ToString()}, {nameof(RegisterTime)}={RegisterTime.ToString()}, {nameof(LockDate)}={LockDate.ToString()}, {nameof(InDeptTime)}={InDeptTime.ToString()}, {nameof(OutDeptTime)}={OutDeptTime.ToString()}, {nameof(TriageTime)}={TriageTime.ToString()}, {nameof(DeathTime)}={DeathTime.ToString()}, {nameof(RegisterDoctorCode)}={RegisterDoctorCode}, {nameof(RegisterDoctorName)}={RegisterDoctorName}, {nameof(TriageDoctorCode)}={TriageDoctorCode}, {nameof(TriageDoctorName)}={TriageDoctorName}, {nameof(AreaCode)}={AreaCode}, {nameof(AreaName)}={AreaName}, {nameof(Bed)}={Bed}, {nameof(FirstDoctorCode)}={FirstDoctorCode}, {nameof(FirstDoctorName)}={FirstDoctorName}, {nameof(DutyDoctorCode)}={DutyDoctorCode}, {nameof(DutyDoctorName)}={DutyDoctorName}, {nameof(RetentionTime)}={RetentionTime}, {nameof(NurseGrade)}={NurseGrade}, {nameof(DutyNurseCode)}={DutyNurseCode}, {nameof(DutyNurseName)}={DutyNurseName}, {nameof(OperatorCode)}={OperatorCode}, {nameof(OperatorName)}={OperatorName}, {nameof(VisitStatus)}={VisitStatus.ToString()}, {nameof(PatientStatus)}={PatientStatus.ToString()}, {nameof(IsPlanBackRoom)}={IsPlanBackRoom.ToString()}, {nameof(EmergencyLevel)}={EmergencyLevel.ToString()}, {nameof(InDeptWay)}={InDeptWay}, {nameof(AttentionCode)}={AttentionCode}, {nameof(IsInHospital)}={IsInHospital.ToString()}, {nameof(SupplementaryNotes)}={SupplementaryNotes}, {nameof(KeyDiseasesCode)}={KeyDiseasesCode}, {nameof(KeyDiseasesName)}={KeyDiseasesName}, {nameof(OutDeptReason)}={OutDeptReason.ToString()}, {nameof(OutDeptReasonCode)}={OutDeptReasonCode}, {nameof(OutDeptReasonName)}={OutDeptReasonName}, {nameof(Sort)}={Sort.ToString()}, {nameof(IsTop)}={IsTop.ToString()}, {nameof(CallStatus)}={CallStatus.ToString()}, {nameof(CallingSn)}={CallingSn}, {nameof(LogTime)}={LogTime.ToString()}, {nameof(CallConsultingRoomCode)}={CallConsultingRoomCode}, {nameof(CallConsultingRoomName)}={CallConsultingRoomName}, {nameof(FinishVisitTime)}={FinishVisitTime.ToString()}, {nameof(ExpireNumberTime)}={ExpireNumberTime.ToString()}, {nameof(LastDirectionCode)}={LastDirectionCode}, {nameof(LastDirectionName)}={LastDirectionName}, {nameof(Consciousness)}={Consciousness}, {nameof(RFID)}={RFID}, {nameof(GuardianIdTypeCode)}={GuardianIdTypeCode}, {nameof(GuardianIdTypeName)}={GuardianIdTypeName}, {nameof(IdTypeCode)}={IdTypeCode}, {nameof(IdTypeName)}={IdTypeName}, {nameof(ToHospitalWayCode)}={ToHospitalWayCode}, {nameof(ToHospitalWayName)}={ToHospitalWayName}, {nameof(CrowdCode)}={CrowdCode}, {nameof(CrowdName)}={CrowdName}, {nameof(VisitReasonCode)}={VisitReasonCode}, {nameof(VisitReasonName)}={VisitReasonName}, {nameof(GuardianIdCardNo)}={GuardianIdCardNo}, {nameof(GuardianPhone)}={GuardianPhone}, {nameof(ModifyLevelCode)}={ModifyLevelCode}, {nameof(ModifyLevelName)}={ModifyLevelName}, {nameof(CallingDoctorId)}={CallingDoctorId}, {nameof(CallingDoctorName)}={CallingDoctorName}, {nameof(IsFirstAid)}={IsFirstAid.ToString()}, {nameof(DeptCode)}={DeptCode}, {nameof(DeptName)}={DeptName}, {nameof(Pflegestufe)}={Pflegestufe}, {nameof(BedHeadSticker)}={BedHeadSticker}, {nameof(IsOpenDiagnosisCost)}={IsOpenDiagnosisCost.ToString()}, {nameof(NotAutoEnd)}={NotAutoEnd.ToString()}, {nameof(IsOpenGreenChannl)}={IsOpenGreenChannl.ToString()}, {nameof(InvoiceNum)}={InvoiceNum}, {nameof(BedTime)}={BedTime.ToString()}, {nameof(PatnId)}={PatnId}, {nameof(CurrMDTRTId)}={CurrMDTRTId}, {nameof(PoolArea)}={PoolArea}, {nameof(InsureType)}={InsureType}, {nameof(OutSetlFlag)}={OutSetlFlag}, {nameof(OutDeptCode)}={OutDeptCode}, {nameof(OutDeptName)}={OutDeptName}, {nameof(HasPushGreenChannel)}={HasPushGreenChannel.ToString()}, {nameof(RecallTime)}={RecallTime.ToString()}, {nameof(RescueRetentionTime)}={RescueRetentionTime.ToString()}, {nameof(ObservationRetentionTime)}={ObservationRetentionTime.ToString()}, {nameof(IsReferral)}={IsReferral.ToString()}, {nameof(SpecialCode)}={SpecialCode}, {nameof(SpecialName)}={SpecialName}}}";
        }
    }
}