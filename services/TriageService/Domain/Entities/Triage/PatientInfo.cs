using SamJan.MicroService.PreHospital.Core;
using SamJan.MicroService.PreHospital.Core.BaseEntities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SamJan.MicroService.PreHospital.TriageService
{
    /// <summary>
    /// 预检分诊患者信息表   
    /// </summary>
    public class PatientInfo : BaseEntity<Guid>
    {
        private string _patientName;

        public PatientInfo SetId(Guid id)
        {
            Id = id;
            return this;
        }

        /// <summary>
        /// 任务单号
        /// </summary>
        [Description("任务单号")]
        public Guid TaskInfoId { get; set; }

        /// <summary>
        /// 车牌号
        /// </summary>
        [Description("车牌号")]
        [StringLength(20)]
        public string CarNum { get; set; }

        /// <summary>
        /// 就诊号
        /// </summary>
        [Description("就诊号")]
        [StringLength(20)]
        public string VisitNo { get; set; }

        /// <summary>
        /// 任务单流水号
        /// </summary>
        [Description("任务单流水号")]
        [StringLength(50)]
        public string TaskInfoNum { get; set; }

        /// <summary>
        /// 患者唯一标识(HIS)
        /// </summary>
        [Description("患者唯一标识(HIS)")]
        [StringLength(50)]
        [Required(ErrorMessage = "{0}不能为空")]
        public string PatientId { get; set; }

        /// <summary>
        /// 患者姓名
        /// </summary>
        [Description("患者姓名")]
        [StringLength(50)]
        public string PatientName
        {
            get => _patientName; set
            {
                _patientName = value;
                Py = PyHelper.GetFirstPy(_patientName);
            }
        }

        /// <summary>
        /// 患者性别Code
        /// </summary>
        [Description("患者性别Code")]
        [StringLength(20)]
        public string Sex { get; set; }

        /// <summary>
        /// 患者性别名称
        /// </summary>
        [Description("患者性别名称")]
        [StringLength(20)]
        public string SexName { get; set; }

        /// <summary>
        /// 患者出生日期
        /// </summary>
        [Description("患者出生日期")]
        public DateTime? Birthday { get; set; }

        /// <summary>
        /// 患者住址
        /// </summary>
        [Description("患者住址")]
        [StringLength(200)]
        public string Address { get; set; }

        /// <summary>
        /// 紧急联系人
        /// </summary>
        [Description("紧急联系人")]
        [StringLength(20)]
        public string ContactsPerson { get; set; }

        /// <summary>
        /// 与联系人关系编码
        /// </summary>
        [Description("与联系人关系编码")]
        [StringLength(20)]
        public string SocietyRelationCode { get; set; }

        /// <summary>
        /// 与联系人关系名称
        /// </summary>
        [Description("与联系人关系名称")]
        [StringLength(10)]
        public string SocietyRelationName { get; set; }

        /// <summary>
        /// 联系电话
        /// </summary>
        [Description("联系电话")]
        [StringLength(20)]
        public string ContactsPhone { get; set; }

        /// <summary>
        /// 监护人/联系人电话
        /// </summary>
        [Description("监护人/联系人电话")]
        [StringLength(20)]
        public string GuardianPhone { get; set; }

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
        /// 监护人身份证号码
        /// </summary>
        [Description("监护人身份证号码")]
        [StringLength(20)]
        public string GuardianIdCardNo { get; set; }

        /// <summary>
        /// 监护人/联系人地址
        /// </summary>
        [Description("监护人/联系人地址")]
        [StringLength(50)]
        public string ContactsAddress { get; set; }

        /// <summary>
        /// 来院方式Code
        /// </summary>
        [Description("来院方式Code")]
        [StringLength(60)]
        public string ToHospitalWayCode { get; set; }

        /// <summary>
        /// 来院方式名称
        /// </summary>
        [Description("来院方式名称")]
        [StringLength(60)]
        public string ToHospitalWayName { get; set; }

        /// <summary>
        /// 患者身份Code
        /// </summary>
        [Description("患者身份Code")]
        [StringLength(60)]
        public string Identity { get; set; }

        /// <summary>
        /// 患者身份名称
        /// </summary>
        [Description("患者身份名称")]
        [StringLength(60)]
        public string IdentityName { get; set; }

        /// <summary>
        /// 费别Code
        /// </summary>
        [Description("费别Code")]
        [StringLength(60)]
        public string ChargeType { get; set; }

        /// <summary>
        /// 费别名称
        /// </summary>
        [Description("费别名称")]
        [StringLength(60)]
        public string ChargeTypeName { get; set; }


        /// <summary>
        /// 特约记账类型Code
        /// </summary>
        [Description("特约记账类型编码")]
        [StringLength(50)]
        public string SpecialAccountTypeCode { get; set; }

        /// <summary>
        /// 特约记账类型名称
        /// </summary>
        [Description("特约记账类型名称")]
        [StringLength(50)]
        public string SpecialAccountTypeName { get; set; }

        /// <summary>
        /// 身份证号
        /// </summary>
        [Description("身份证号")]
        [StringLength(500)]
        [Encrypted]
        public string IdentityNo { get; set; }

        /// <summary>
        /// 民族Code
        /// </summary>
        [Description("民族Code")]
        [StringLength(60)]
        public string Nation { get; set; }

        /// <summary>
        /// 民族名称
        /// </summary>
        [Description("民族名称")]
        [StringLength(60)]
        public string NationName { get; set; }

        /// <summary>
        /// 国家Code
        /// </summary>
        [Description("国家Code")]
        [StringLength(60)]
        public string Country { get; set; }

        /// <summary>
        /// 体重
        /// </summary>
        [Description("体重")]
        [StringLength(20)]
        public string Weight { get; set; }

        /// <summary>
        /// 绿色通道Code
        /// </summary>
        [Description("绿色通道Code")]
        [StringLength(60)]
        public string GreenRoadCode { get; set; }

        /// <summary>
        /// 绿色通道名称
        /// </summary>
        [Description("绿色通道名称")]
        [StringLength(60)]
        public string GreenRoadName { get; set; }

        /// <summary>
        /// 发病时间
        /// </summary>
        [Description("发病时间")]
        public DateTime? OnsetTime { get; set; }

        /// <summary>
        /// 分诊人code
        /// </summary>
        [Description("分诊人code")]
        [StringLength(50)]
        public string TriageUserCode { get; set; }

        /// <summary>
        /// 分诊人名称
        /// </summary>
        [Description("分诊人名称")]
        [StringLength(50)]
        public string TriageUserName { get; set; }

        /// <summary>
        /// 分诊时间
        /// </summary>
        [Description("分诊时间")]
        public DateTime? TriageTime { get; set; }

        /// <summary>
        /// 诊疗卡号
        /// </summary>
        [Description("诊疗卡号")]
        [StringLength(20)]
        public string CardNo { get; set; }

        /// <summary>
        /// 医保卡号
        /// </summary>
        [Description("医保卡号")]
        [StringLength(20)]
        public string MedicalNo { get; set; }

        /// <summary>
        /// 电子医保凭证
        /// </summary>
        [Description("医保电子凭证")]
        [StringLength(50)]
        public string ElectronCertNo { get; set; }

        /// <summary>
        /// RFID
        /// </summary>
        [Description("RFID")]
        [StringLength(20)]
        public string RFID { get; set; }

        /// <summary>
        /// 年龄
        /// </summary>
        [Description("年龄")]
        [StringLength(20)]
        public string Age { get; set; }

        /// <summary>
        /// 患者姓名拼音首字母
        /// </summary>
        [Description("患者姓名拼音首字母")]
        [StringLength(100)]
        public string Py { get; protected set; }

        /// <summary>
        /// 重点病种Code
        /// </summary>
        [Description("重点病种Code")]
        [StringLength(50)]
        public string DiseaseCode { get; set; }

        /// <summary>
        /// 重点病种名称
        /// </summary>
        [Description("重点病种名称")]
        [StringLength(50)]
        public string DiseaseName { get; set; }

        /// <summary>
        /// 开始分诊时间
        /// </summary>
        [Description("开始分诊时间")]
        public DateTime? StartTriageTime { get; set; }

        /// <summary>
        /// 合并档案表主键Id
        /// </summary>
        [Description("合并档案表主键Id")]
        public Guid MergeRecordId { get; set; }

        /// <summary>
        /// 群伤事件Id
        /// </summary>
        [Description("群伤事件Id")]
        public Guid GroupInjuryInfoId { get; set; }

        /// <summary>
        /// 分诊来源标识 0：院前 1：急诊 2：卒中 3：胸痛 4：创伤 5：孕产妇 6：新生儿 7：中毒 8：其它
        /// </summary>
        [Description("分诊来源标识 0：院前 1：急诊 2：卒中 3：胸痛 4：创伤 5：孕产妇 6：新生儿 7：中毒 8：其它")]
        [StringLength(20)]
        public string TriageSource { get; set; }

        /// <summary>
        /// 分诊状态，0：暂存，1：分诊
        /// </summary>
        [Description("分诊状态，0：暂存，1：分诊")]
        public int TriageStatus { get; set; } = 1;

        /// <summary>
        /// 是否三无病人
        /// </summary>
        [Description("是否三无病人")]
        public bool IsNoThree { get; set; }

        /// <summary>
        /// 就诊类型Code
        /// </summary>
        [Description("就诊类型Code")]
        [StringLength(60)]
        public string TypeOfVisitCode { get; set; }

        /// <summary>
        /// 就诊类型名称
        /// </summary>
        [Description("就诊类型名称")]
        [StringLength(60)]
        public string TypeOfVisitName { get; set; }

        /// <summary>
        /// 主诉Code
        /// </summary>
        [Description("主诉Code")]
        public string Narration { get; set; }

        /// <summary>
        /// 主诉名称
        /// </summary>
        [Description("主诉名称")]
        public string NarrationName { get; set; }

        /// <summary>
        /// 主诉备注
        /// </summary>
        [Description("主诉备注")]
        public string NarrationComments { get; set; }

        /// <summary>
        /// 意识Code
        /// </summary>
        [Description("意识Code")]
        [StringLength(60)]
        public string Consciousness { get; set; }

        /// <summary>
        /// 意识名称
        /// </summary>
        [Description("意识名称")]
        [StringLength(60)]
        public string ConsciousnessName { get; set; }

        /// <summary>
        /// 叫号排队号
        /// </summary>
        [Description("叫号排队号")]
        [StringLength(50)]
        public string CallingSn { get; set; }

        /// <summary>
        /// 叫号号
        /// </summary>
        [Description("叫号号")]
        [StringLength(50)]
        public string CallNo { get; set; }

        /// <summary>
        /// 叫号登记时间
        /// 等候时长从登记时间开始，当有开始就诊时间时，到开始就诊时间结束; 否则到当前时间
        /// </summary>
        public DateTime? LogTime { get; set; }

        /// <summary>
        /// 就诊状态
        /// </summary>
        [Description("就诊状态")]
        public VisitStatus VisitStatus { get; set; }

        /// <summary>
        /// 预约流水号
        /// </summary>
        [Description("预约流水号")]
        [StringLength(50)]
        public string SeqNumber { get; set; }

        /// <summary>
        /// 患者基本信息是否不可编辑
        /// 患者信息需与 HIS 同步，所以当患者信息与HIS同步后基本信息不再允许修改
        /// 即挂号之后信息便不允许再次修改（目前没有HIS修改患者的接口）
        /// by: ywlin 2021-11-29
        /// </summary>
        [Description("患者基本信息是否不可编辑")]
        public bool IsBasicInfoReadOnly { get; set; }

        /// <summary>
        /// 新冠问卷是否从外部获取
        /// 当从外部获取新冠问卷时，不在ECIS中录入个人史信息
        /// </summary>
        [Description("新冠问卷是否从外部获取")]
        public bool IsCovidExamFromOuterSystem { get; set; }

        /// <summary>
        /// 首诊医生工号
        /// </summary>
        [Description("首诊医生工号")]
        [StringLength(50)]
        public string FirstDoctorCode { get; set; }

        /// <summary>
        /// 首诊医生名称
        /// </summary>
        [Description("首诊医生名称")]
        [StringLength(50)]
        public string FirstDoctorName { get; set; }

        /// <summary>
        /// 最终去向代码
        /// </summary>
        [Description("最终去向代码")]
        [StringLength(50)]
        public string LastDirectionCode { get; set; }

        /// <summary>
        /// 最终去向名称
        /// </summary>
        [Description("最终去向名称")]
        [StringLength(50)]
        public string LastDirectionName { get; set; }

        /// <summary>
        /// 证件类型编码
        /// </summary>
        [Description("证件类型编码")]
        [StringLength(50)]
        public string IdTypeCode { get; set; }

        /// <summary>
        /// 证件类型
        /// </summary>
        [Description("证件类型编码")]
        [StringLength(50)]
        public string IdTypeName { get; set; }

        /// <summary>
        /// 人群编码
        /// </summary>
        [Description("人群编码")]
        [StringLength(50)]
        public string CrowdCode { get; set; }

        /// <summary>
        /// 人群
        /// </summary>
        [Description("人群")]
        [StringLength(50)]
        public string CrowdName { get; set; }

        /// <summary>
        /// 孕周
        /// </summary>
        [Description("孕周")]
        public int? GestationalWeeks { get; set; }

        /// <summary>
        /// 就诊原因编码
        /// </summary>
        [Description("就诊原因编码")]
        [StringLength(4000)]
        public string VisitReasonCode { get; set; }

        /// <summary>
        /// 就诊原因
        /// </summary>
        [Description("就诊原因")]
        [StringLength(4000)]
        public string VisitReasonName { get; set; }

        /// <summary>
        /// 持续时间（天）
        /// </summary>
        [Description("持续时间（天）")]
        public int? PersistDays { get; set; }

        /// <summary>
        /// 持续时间（时）
        /// </summary>
        [Description("持续时间（时）")]
        public int? PersistHours { get; set; }

        /// <summary>
        /// 持续时间（分）
        /// </summary>
        [Description("持续时间（分）")]
        public int? PersistMinutes { get; set; }

        /// <summary>
        /// 是否作废
        /// </summary>
        [Description("是否作废")]
        public bool IsCancelled { get; set; }

        /// <summary>
        /// 作废人
        /// </summary>
        [Description("作废人")]
        public string CancellationUser { get; set; }

        /// <summary>
        /// 作废时间
        /// </summary>
        [Description("作废时间")]
        public DateTime? CancellationTime { get; set; }

        /// <summary>
        /// 参保地代码
        /// </summary>
        [Description("参保地代码")]
        [StringLength(20)]
        public string InsuplcAdmdvCode { get; set; }

        /// <summary>
        /// 国籍代码
        /// </summary>
        [Description("国籍代码")]
        [StringLength(50)]
        public string CountryCode { get; set; }

        /// <summary>
        /// 国籍名称
        /// </summary>
        [Description("国籍名称")]
        [StringLength(50)]
        public string CountryName { get; set; }
        /// <summary>
        /// 是否是院前患者
        /// </summary>
        [Description("是否是院前患者")]
        public bool IsFirstAid { get; set; }
        /// <summary>
        /// 是否转诊
        /// </summary>
        [Description("是否转诊")]
        public bool IsReferral { get; set; }

        /// <summary>
        /// 挂号类型 4=免费 1=普通号
        /// </summary>
        [Description("挂号类型")]
        [StringLength(10)]
        public string RegType { get; set; }

        /// <summary>
        /// 就诊医生名称
        /// </summary>
        [Description("就诊医生名称")]
        [StringLength(50)]
        public string DoctorName { get; set; }

        /// <summary>
        /// 开始就诊时间
        /// </summary>
        public DateTime? BeginTime { get; set; }

        /// <summary>
        /// 发票号
        /// </summary>
        public string InvoiceNum { get; set; }

        /// <summary>
        /// 扩展字段（北大诊断）
        /// </summary>
        public string ExtendField1 { get; set; }

        /// <summary>
        /// 结束就诊时间
        /// </summary>
        public DateTime? EndTime { get; set; }

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
        /// 评分信息
        /// </summary>
        [IsNeedComment(IsNeed = false)]
        public ICollection<ScoreInfo> ScoreInfo { get; set; }

        /// <summary>
        /// 生命体征信息
        /// </summary>
        [IsNeedComment(IsNeed = false)]
        public VitalSignInfo VitalSignInfo { get; set; }

        /// <summary>
        /// 分诊结果
        /// </summary>
        [IsNeedComment(IsNeed = false)]
        public ConsequenceInfo ConsequenceInfo { get; set; }

        /// <summary>
        /// 告知单患者
        /// </summary>
        [IsNeedComment(IsNeed = false)]
        public InformPatInfo Inform { get; set; }

        /// <summary>
        /// 挂号记录
        /// </summary>
        [IsNeedComment(IsNeed = false)]
        public ICollection<RegisterInfo> RegisterInfo { get; set; }

        /// <summary>
        /// 入院情况信息
        /// </summary>
        [IsNeedComment(IsNeed = false)]
        public AdmissionInfo AdmissionInfo { get; set; }

        /// <summary>
        /// 医疗证号
        /// </summary>
        [StringLength(80)]
        public string SafetyNo { get; set; }

        /// <summary>
        /// 是否同步挂号
        /// </summary>
        public bool IsSyncRegister { get; set; } = false;

        /// <summary>
        /// 流转区域
        /// </summary>
        public string TransferArea { get; set; }

        /// <summary>
        /// 获取患者拼音首字母
        /// </summary>
        /// <returns></returns>
        public PatientInfo GetNamePy()
        {
            Py = PyHelper.GetFirstPy(PatientName);
            return this;
        }

        /// <summary>
        /// 计算年龄
        /// </summary>
        /// <returns></returns>
        private PatientInfo GetAges()
        {
            DateTime? birth = Birthday, visit = DateTime.Now;
            if (birth == null || visit.Value < birth.Value) return this;

            //*<3小时         显示分钟  如：125分钟
            //3小时<*<3天     显示小时  如：48小时
            //3天<*<3个月     显示天    如：60天
            //3个月<*<3岁     显示月    如：24个月
            //*>3岁           显示岁    如：5岁，87岁等。

            try
            {
                string age;
                if (visit.Value.Year - birth.Value.Year > 3 ||
                    (visit.Value.Year - birth.Value.Year == 3 && birth.Value.Month <= visit.Value.Month)
                ) //大于等于三岁，显示岁
                {
                    if (birth.Value.Month <= visit.Value.Month) //足月
                    {
                        age = (visit.Value.Year - birth.Value.Year) + "岁";
                    }
                    else
                    {
                        age = (visit.Value.Year - birth.Value.Year - 1) + "岁";
                    }
                }
                else if (
                    ((visit.Value.Year - birth.Value.Year) * 12 + (visit.Value.Month - birth.Value.Month)) > 3
                    ||
                    ((((visit.Value.Year - birth.Value.Year) * 12 + (visit.Value.Month - birth.Value.Month)) == 3) &&
                     (birth.Value.Day <= visit.Value.Day))
                ) //大于等于三个月，显示月
                {
                    if (birth.Value.Day <= visit.Value.Day) //足日
                    {
                        age = ((visit.Value.Year - birth.Value.Year) * 12 + (visit.Value.Month - birth.Value.Month))
                              + "个月";
                    }
                    else
                    {
                        age = ((visit.Value.Year - birth.Value.Year) * 12 + (visit.Value.Month - birth.Value.Month) - 1)
                              + "个月";
                    }
                }
                else if ((visit.Value - birth.Value).TotalDays >= 3) //大于等于三天，显示天
                {
                    age = ((int)(visit.Value - birth.Value).TotalDays) + "天";
                }
                else if ((visit.Value - birth.Value).TotalHours >= 3) //大于等于三小时，显示小时
                {
                    age = ((int)(visit.Value - birth.Value).TotalHours) + "小时";
                }
                else //小于三小时，显示分钟
                {
                    age = ((int)(visit.Value - birth.Value).TotalMinutes) + "分钟";
                }

                Age = age;
                return this;
            }
            catch (Exception)
            {
                Age = "";
                return this;
            }
        }

        #region 医保控费相关
        /// <summary>
        /// 参保人标识
        /// </summary>
        [Description("参保人标识")]
        public string PatnId { get; set; }

        /// <summary>
        /// 当前就诊标识
        /// </summary>
        [Description("当前就诊标识")]
        public string CurrMDTRTId { get; set; }

        /// <summary>
        /// 统筹区编码
        /// </summary>
        [Description("统筹区编码")]
        public string PoolArea { get; set; }

        /// <summary>
        /// 险种类型
        /// 310职工基本医疗保险；390城乡居民基本医疗保险；320公务员医疗补助；392城乡居民大病医疗保险；330大额医疗费用补助；510生育保险；340离休人员医疗保障；
        /// </summary>
        [Description("险种类型")]
        public string InsureType { get; set; }

        /// <summary>
        /// 异地结算标志 0否;1是
        /// </summary>
        [Description("异地结算标志")]
        public string OutSetlFlag { get; set; }
        #endregion

        /// <summary>
        /// 是否优先
        /// </summary>
        [Description("是否优先")]
        public bool IsTop { get; set; }

        /// <summary>
        /// 是否过号
        /// </summary>
        [Description("是否过号")]
        public bool IsUntreatedOver { get; set; }

        /// <summary>
        /// 叫号医生编码
        /// </summary>
        [Description("叫号医生编码")]
        [StringLength(50)]
        public string CallDoctorCode { get; set; }

        /// <summary>
        /// 叫号医生名称
        /// </summary>
        [Description("叫号医生名称")]
        [StringLength(50)]
        public string CallDoctorName { get; set; }
    }
}