using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Mapster;
using SamJan.MicroService.PreHospital.Core;

namespace SamJan.MicroService.PreHospital.TriageService
{
    /// <summary>
    /// 挂号患者信息
    /// </summary>
    public class RegisterPatientInfoDto
    {
        public RegisterPatientInfoDto()
        {
        }

        public RegisterPatientInfoDto(PatientInfo patientInfo, RegisterInfo registerInfo)
        {
            patientInfo.BuildAdapter().AdaptTo(this);
            this.RegisterNo = registerInfo.RegisterNo;
        }

        /// <summary>
        /// Id 不需要前端传值
        /// </summary>
        public Guid TriagePatientInfoId { get; set; }

        /// <summary>
        /// 任务单号
        /// </summary>
        public Guid TaskInfoId { get; set; }

        /// <summary>
        /// 优先
        /// </summary>
        public bool IsTop { get; set; }

        /// <summary>
        /// 过号
        /// </summary>
        public bool IsUntreatedOver { get; set; }

        /// <summary>
        /// 任务单流水号
        /// </summary>
        public string TaskInfoNum { get; set; }

        /// <summary>
        /// 就诊号
        /// </summary>
        public string RegisterNo { get; set; }

        /// <summary>
        /// 挂号时间
        /// </summary>
        public DateTime? RegisterTime { get; set; }

        /// <summary>
        /// 叫号系统排队号
        /// </summary>
        public string CallingSn { get; set; }

        /// <summary>
        /// 叫号登记时间
        /// </summary>
        public DateTime? LogTime { get; set; } = DateTime.Now;

        /// <summary>
        /// 车牌号
        /// </summary>
        public string CarNum { get; set; }

        /// <summary>
        /// 就诊次数
        /// </summary>
        public string VisitNo { get; set; }

        /// <summary>
        /// 就诊状态
        /// </summary>
        [Description("就诊状态")]
        public VisitStatus VisitStatus { get; set; }

        /// <summary>
        /// 就诊状态
        /// </summary>
        [Description("就诊状态")]
        public string VisitStatusName => VisitStatus.GetDescriptionByEnum();

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
        /// 联系人电话
        /// </summary>
        public string ContactsPhone { get; set; }

        /// <summary>
        /// 人群编码
        /// </summary>
        public string CrowdCode { get; set; }

        /// <summary>
        /// 人群
        /// </summary>
        public string CrowdName { get; set; }

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

        /// <summary>
        /// 年龄
        /// </summary>
        public string Age
        {
            get { return Birthday.HasValue ? Birthday.Value.GetAgeString() : ""; }
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
            get { return $"{NarrationName}；{NarrationComments}"; }
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

        #region 分诊信息

        /// <summary>
        /// 分诊科室编码
        /// </summary>
        public string TriageDeptCode { get; set; }

        /// <summary>
        /// 分诊科室名称
        /// </summary>
        public string TriageDeptName { get; set; }

        /// <summary>
        /// 分诊去向编码
        /// </summary>
        public string TriageTargetCode { get; set; }

        /// <summary>
        /// 分诊去向名称
        /// </summary>
        public string TriageTargetName { get; set; }

        /// <summary>
        /// 实际分诊级别Code
        /// </summary>
        public string ActTriageLevel { get; set; }

        /// <summary>
        /// 实际分诊级别名称
        /// </summary>
        public string ActTriageLevelName { get; set; }

        /// <summary>
        /// 分区颜色
        /// </summary>
        public string ActTriageColor
        {
            get
            {
                if (ActTriageLevel == "TriageLevel_001" || ActTriageLevel == "TriageLevel_002") return "红区";
                if (ActTriageLevel == "TriageLevel_003") return "黄区";
                return "绿区";
            }
        }

        /// <summary>
        /// 分诊耗时
        /// </summary>
        public string TriageTimeConsuming => GetTimeSpan();

        /// <summary>
        /// 挂号等待时间
        /// 计算从挂号时间开始到分诊时间结束的时间差，假如没有结束时间，则计算到当前时间的时间差
        /// </summary>
        public string RegisterWaitingConsuming => GetRegisterTimeSpan();

        /// <summary>
        /// 分诊护士编码
        /// </summary>
        public string TriageNurse { get; set; }

        /// <summary>
        /// 分诊护士名称
        /// </summary>
        public string TriageNurseName { get; set; }

        /// <summary>
        /// 就诊医生
        /// 分诊时有选择就诊医生，则显示该医生名
        /// 分诊时没有选择就诊医生，则显示his视图中的首诊医生
        /// </summary>
        public string DoctorName { get; set; }

        #endregion

        /// <summary>
        /// 等候时长
        /// 等候时长从分诊时间开始，当有开始就诊时间时，到开始就诊时间结束; 否则到当前时间
        /// </summary>
        public string WaitingDuration
        {
            get => GetWaitingDuration();
            //set => SetWaitingDuration(value);
        }

        #region 生命体征

        /// <summary>
        /// 收缩压
        /// </summary>
        public string Sbp { get; set; }

        /// <summary>
        /// 舒张压
        /// </summary>
        public string Sdp { get; set; }

        /// <summary>
        /// 血氧饱和度
        /// </summary>
        public string SpO2 { get; set; }

        /// <summary>
        /// 呼吸
        /// </summary>
        public string BreathRate { get; set; }

        /// <summary>
        /// 体温
        /// </summary>
        public string Temp { get; set; }

        /// <summary>
        /// 心率
        /// </summary>
        public string HeartRate { get; set; }

        /// <summary>
        /// BP
        /// </summary>
        public string BP { get; set; }

        #endregion

        /// <summary>
        /// 是否已填新冠问卷
        /// </summary>
        public bool HasFinishedCovid19Exam { get; set; }

        /// <summary>
        /// 前面等候人数（前面还有xx位）
        /// </summary>
        public int WaittingForNumber { get; set; }

        /// <summary>
        /// 挂号记录
        /// </summary>
        public IEnumerable<RegisterInfo> RegisterInfo { get; set; }

        /// <summary>
        /// 监护人证件类型（默认居民身份证）
        /// </summary>
        public string GuardianIdTypeCode { get; set; }
        /// <summary>
        /// 国籍
        /// </summary>
        public string CountryName { get; set; }
        /// <summary>
        /// 预约流水号
        /// </summary>
        public string SeqNumber { get; set; }
        /// <summary>
        /// 证件类型编码
        /// </summary>
        public string IdTypeCode { get; set; }

        /// <summary>
        /// 证件类型
        /// </summary>
        public string IdTypeName { get; set; }
        /// <summary>
        /// 操作员
        /// </summary>
        public string Operator { get; set; }
        /// <summary>
        /// 挂号类型 1：普通号；2专家号；3名专家号
        /// </summary>
        public string RegType { get; set; }

        /// <summary>
        /// 就诊类型 1:门诊，2:住院，3:体检
        /// </summary>
        public string PatientKind { get; set; }
        /// <summary>
        /// 午别/0：上午；1：下午；2：晚上
        /// </summary>
        public string TimeInterval { get; set; }
        /// <summary>
        /// 诊结状态  1:未诊，2：诊结 
        /// </summary>
        public string Diagnosis { get; set; }

        /// <summary>
        /// 是否退费  1:退费;0:未退费
        /// </summary>
        public string IsRefund { get; set; }

        /// <summary>
        /// 叫号医生编码
        /// </summary>
        public string CallDoctorCode { get; set; }

        /// <summary>
        /// 叫号医生名称
        /// </summary>
        public string CallDoctorName { get; set; }

        /// <summary>
        /// 获取分诊耗时
        /// </summary>
        /// <returns></returns>
        private string GetTimeSpan()
        {
            var time = TriageTime ?? CreationTime;
            if (StartTriageTime == null || time <= StartTriageTime)
            {
                return "0";
            }

            var n1 = StartTriageTime.Value;
            var ts = time - n1;
            return ts.TotalMinutes.ToString("N0");
        }

        /// <summary>
        /// 获取挂号时长
        /// </summary>
        /// <returns></returns>
        private string GetRegisterTimeSpan()
        {
            var time = TriageTime ?? DateTime.Now;
            if (RegisterTime == null || time <= RegisterTime)
            {
                return "0";
            }

            var n1 = RegisterTime.Value;
            var ts = time - n1;
            return ts.TotalMinutes.ToString("N0");
        }

        /// <summary>
        /// 等候时长
        /// 等候时长从分诊时间开始，当有开始就诊时间时，到开始就诊时间结束; 否则到当前时间
        /// </summary>
        /// <returns></returns>
        private string GetWaitingDuration()
        {
            if (!TriageTime.HasValue)
                return "0";
            var n1 = TriageTime.Value;
            var ts = DateTime.Now - n1;
            return ts.TotalMinutes.ToString("N0");
        }

        /// <summary>
        /// 设置等候时长
        /// 用于适配不同医院不同等待时常的特殊处理，如：PKU用挂号时间计算
        /// </summary>
        /// <param name="duration"></param>
        /// <returns></returns>
        public string SetWaitingDuration(string duration)
        {
            return duration;
        }

        /// <summary>
        /// 获取数据结构
        /// </summary>
        /// <returns></returns>
        public void GetColumns<T>(List<string> colums, T tt)
        {
            Type t = tt.GetType();
            var ps = t.GetProperties();
            Array.ForEach(ps, p => { colums.Add(p.Name); });
        }
    }
}