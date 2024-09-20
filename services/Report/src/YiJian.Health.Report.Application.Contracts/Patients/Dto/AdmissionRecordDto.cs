using System;

namespace YiJian.Health.Report.Patients.Dto
{
    /// <summary>
    /// 描述：病患信息Dto
    /// 创建人： yangkai
    /// 创建时间：2022/11/30 14:35:48
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
        /// 就诊门诊号
        /// </summary>
        public string VisitNo { get; set; }

        /// <summary>
        /// 就诊流水号
        /// </summary>
        public string VisSerialNo { get; set; }

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
        public string RegisterDoctorCode { get; set; }

        /// <summary>
        /// 挂号医生姓名
        /// </summary>
        public string RegisterDoctorName { get; set; }

        /// <summary>
        /// 患者姓名
        /// </summary>
        public string PatientName { get; set; }

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
        public int VisitStatus { get; set; }

        /// <summary>
        /// 非计划重返抢救室
        /// </summary>
        public bool IsPlanBackRoom { get; set; }

        /// <summary>
        /// 危重等级
        /// </summary>
        public int EmergencyLevel { get; set; }

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
        public string TypeOfVisitCode { get; set; }

        /// <summary>
        /// 就诊类型编码
        /// </summary> 
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
        public string TriageDirectionCode { get; set; }

        /// <summary>
        /// 分诊去向名称
        /// </summary> 
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
        /// 死亡时间
        /// </summary>
        public DateTime? DeathTime { get; set; }

        /// <summary>
        /// 出科原因
        /// </summary>
        public int OutDeptReason { get; set; }

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
        /// 叫号状态
        /// </summary>
        public int CallStatus { get; set; }

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
        /// 当前时间字符串表示
        /// </summary>
        public string Current
        {
            get
            {
                return DateTime.Now.ToString("yyyy-MM-dd HH:mm");
            }
        }

        /// <summary>
        /// 现病史
        /// </summary>
        public string PresentMedicalHistory { get; set; }

        /// <summary>
        /// 体格检查
        /// </summary>
        public string PhysicalExamination { get; set; }

        /// <summary>
        /// 医疗证号
        /// </summary>
        public string SafetyNo { get; set; }

        /// <summary>
        /// 来院方式
        /// </summary>
        public string ToHospitalWayName { get; set; }
    }
}
