namespace YiJian.Handover
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using Volo.Abp.Validation;

    /// <summary>
    /// 护士交班 新增输入
    /// </summary>
    [Serializable]
    public class NurseHandoverCreation
    {
        /// <summary>
        /// triage分诊患者id
        /// </summary>
        public Guid  PI_ID { get; set; }

        /// <summary>
        /// 患者id
        /// </summary>
        [DynamicStringLength(typeof(NurseHandoverConsts), nameof(NurseHandoverConsts.MaxPatientIdLength), ErrorMessage = "患者id最大长度不能超过{1}!")]
        public string  PatientId { get; set; }

        /// <summary>
        /// 就诊号
        /// </summary>
        public int?  VisitNo { get; set; }

        /// <summary>
        /// 患者姓名
        /// </summary>
        [DynamicStringLength(typeof(NurseHandoverConsts), nameof(NurseHandoverConsts.MaxPatientNameLength), ErrorMessage = "患者姓名最大长度不能超过{1}!")]
        public string  PatientName { get; set; }

        /// <summary>
        /// 性别编码
        /// </summary>
        [DynamicStringLength(typeof(NurseHandoverConsts), nameof(NurseHandoverConsts.MaxSexLength), ErrorMessage = "性别编码最大长度不能超过{1}!")]
        public string  Sex { get; set; }

        /// <summary>
        /// 性别名称
        /// </summary>
        [DynamicStringLength(typeof(NurseHandoverConsts), nameof(NurseHandoverConsts.MaxSexNameLength), ErrorMessage = "性别名称最大长度不能超过{1}!")]
        public string  SexName { get; set; }

        /// <summary>
        /// 年龄
        /// </summary>
        [DynamicStringLength(typeof(NurseHandoverConsts), nameof(NurseHandoverConsts.MaxAgeLength), ErrorMessage = "年龄最大长度不能超过{1}!")]
        public string  Age { get; set; }

        /// <summary>
        /// 分诊级别
        /// </summary>
        [DynamicStringLength(typeof(NurseHandoverConsts), nameof(NurseHandoverConsts.MaxTriageLevelLength), ErrorMessage = "分诊级别最大长度不能超过{1}!")]
        public string  TriageLevel { get; set; }

        /// <summary>
        /// 分诊级别名称
        /// </summary>
        [DynamicStringLength(typeof(NurseHandoverConsts), nameof(NurseHandoverConsts.MaxTriageLevelNameLength), ErrorMessage = "分诊级别名称最大长度不能超过{1}!")]
        public string  TriageLevelName { get; set; }

        /// <summary>
        /// 区域编码
        /// </summary>
        [DynamicStringLength(typeof(NurseHandoverConsts), nameof(NurseHandoverConsts.MaxAreaCodeLength), ErrorMessage = "区域编码最大长度不能超过{1}!")]
        public string  AreaCode { get; set; }

        /// <summary>
        /// 区域名称
        /// </summary>
        [DynamicStringLength(typeof(NurseHandoverConsts), nameof(NurseHandoverConsts.MaxAreaNameLength), ErrorMessage = "区域名称最大长度不能超过{1}!")]
        public string  AreaName { get; set; }

        /// <summary>
        /// 入科时间
        /// </summary>
        public DateTime  InDeptTime { get; set; }

        /// <summary>
        /// 诊断
        /// </summary>
        [DynamicStringLength(typeof(NurseHandoverConsts), nameof(NurseHandoverConsts.MaxDiagnoseNameLength), ErrorMessage = "诊断最大长度不能超过{1}!")]
        public string  DiagnoseName { get; set; }

        /// <summary>
        /// 床号
        /// </summary>
        [DynamicStringLength(typeof(NurseHandoverConsts), nameof(NurseHandoverConsts.MaxBedLength), ErrorMessage = "床号最大长度不能超过{1}!")]
        public string  Bed { get; set; }

        /// <summary>
        /// 是否三无
        /// </summary>
        public bool  IsNoThree { get; set; }

        /// <summary>
        /// 是否病危
        /// </summary>
        public bool  CriticallyIll { get; set; }

        /// <summary>
        /// 交班内容
        /// </summary>
        [DynamicStringLength(typeof(NurseHandoverConsts), nameof(NurseHandoverConsts.MaxContentLength), ErrorMessage = "交班内容最大长度不能超过{1}!")]
        public string  Content { get; set; }

        /// <summary>
        /// 检验
        /// </summary>
        [DynamicStringLength(typeof(NurseHandoverConsts), nameof(NurseHandoverConsts.MaxTestLength), ErrorMessage = "检验最大长度不能超过{1}!")]
        public string  Test { get; set; }

        /// <summary>
        /// 检查
        /// </summary>
        [DynamicStringLength(typeof(NurseHandoverConsts), nameof(NurseHandoverConsts.MaxInspectLength), ErrorMessage = "检查最大长度不能超过{1}!")]
        public string  Inspect { get; set; }

        /// <summary>
        /// 电子病历
        /// </summary>
        [DynamicStringLength(typeof(NurseHandoverConsts), nameof(NurseHandoverConsts.MaxEmrLength), ErrorMessage = "电子病历最大长度不能超过{1}!")]
        public string  Emr { get; set; }

        /// <summary>
        /// 出入量
        /// </summary>
        [DynamicStringLength(typeof(NurseHandoverConsts), nameof(NurseHandoverConsts.MaxInOutVolumeLength), ErrorMessage = "出入量最大长度不能超过{1}!")]
        public string  InOutVolume { get; set; }

        /// <summary>
        /// 生命体征
        /// </summary>
        [DynamicStringLength(typeof(NurseHandoverConsts), nameof(NurseHandoverConsts.MaxVitalSignsLength), ErrorMessage = "生命体征最大长度不能超过{1}!")]
        public string  VitalSigns { get; set; }

        /// <summary>
        /// 药物
        /// </summary>
        [DynamicStringLength(typeof(NurseHandoverConsts), nameof(NurseHandoverConsts.MaxMedicineLength), ErrorMessage = "药物最大长度不能超过{1}!")]
        public string  Medicine { get; set; }

        /// <summary>
        /// 最新现状
        /// </summary>
        public string  LatestStatus { get; set; }

        /// <summary>
        /// 背景
        /// </summary>
        [DynamicStringLength(typeof(NurseHandoverConsts), nameof(NurseHandoverConsts.MaxBackgroundLength), ErrorMessage = "背景最大长度不能超过{1}!")]
        public string  Background { get; set; }

        /// <summary>
        /// 评估
        /// </summary>
        [DynamicStringLength(typeof(NurseHandoverConsts), nameof(NurseHandoverConsts.MaxAssessmentLength), ErrorMessage = "评估最大长度不能超过{1}!")]
        public string  Assessment { get; set; }

        /// <summary>
        /// 建议
        /// </summary>
        [DynamicStringLength(typeof(NurseHandoverConsts), nameof(NurseHandoverConsts.MaxProposalLength), ErrorMessage = "建议最大长度不能超过{1}!")]
        public string  Proposal { get; set; }

        /// <summary>
        /// 设备
        /// </summary>
        [DynamicStringLength(typeof(NurseHandoverConsts), nameof(NurseHandoverConsts.MaxDevicesLength), ErrorMessage = "设备最大长度不能超过{1}!")]
        public string  Devices { get; set; }

        /// <summary>
        /// 交班时间
        /// </summary>
        public string  HandoverTime { get; set; }

        /// <summary>
        /// 交班护士编码
        /// </summary>
        [Required(ErrorMessage = "交班护士编码不能为空！")]
        [DynamicStringLength(typeof(NurseHandoverConsts), nameof(NurseHandoverConsts.MaxHandoverNurseCodeLength), ErrorMessage = "交班护士编码最大长度不能超过{1}!")]
        public string  HandoverNurseCode { get; set; }

        /// <summary>
        /// 交班护士名称
        /// </summary>
        [Required(ErrorMessage = "交班护士名称不能为空！")]
        [DynamicStringLength(typeof(NurseHandoverConsts), nameof(NurseHandoverConsts.MaxHandoverNurseNameLength), ErrorMessage = "交班护士名称最大长度不能超过{1}!")]
        public string  HandoverNurseName { get; set; }

        /// <summary>
        /// 接班护士编码
        /// </summary>
        [Required(ErrorMessage = "接班护士编码不能为空！")]
        [DynamicStringLength(typeof(NurseHandoverConsts), nameof(NurseHandoverConsts.MaxSuccessionNurseCodeLength), ErrorMessage = "接班护士编码最大长度不能超过{1}!")]
        public string  SuccessionNurseCode { get; set; }

        /// <summary>
        /// 接班护士名称
        /// </summary>
        [Required(ErrorMessage = "接班护士名称不能为空！")]
        [DynamicStringLength(typeof(NurseHandoverConsts), nameof(NurseHandoverConsts.MaxSuccessionNurseNameLength), ErrorMessage = "接班护士名称最大长度不能超过{1}!")]
        public string  SuccessionNurseName { get; set; }

        /// <summary>
        /// 交班日期
        /// </summary>
        [Required(ErrorMessage = "交班日期不能为空！")]
        public string  HandoverDate { get; set; }

        /// <summary>
        /// 班次id
        /// </summary>
        [Required(ErrorMessage = "班次id不能为空！")]
        public Guid  ShiftSettingId { get; set; }

        /// <summary>
        /// 班次名称
        /// </summary>
        [Required(ErrorMessage = "班次名称不能为空！")]
        [DynamicStringLength(typeof(NurseHandoverConsts), nameof(NurseHandoverConsts.MaxShiftSettingNameLength), ErrorMessage = "班次名称最大长度不能超过{1}!")]
        public string  ShiftSettingName { get; set; }

        /// <summary>
        /// 交班状态，0：未提交，1：提交交班
        /// </summary>
        public int  Status { get; set; }

        /// <summary>
        /// 创建人编码
        /// </summary>
        [Required(ErrorMessage = "创建人编码不能为空！")]
        [DynamicStringLength(typeof(NurseHandoverConsts), nameof(NurseHandoverConsts.MaxCreationCodeLength), ErrorMessage = "创建人编码最大长度不能超过{1}!")]
        public string  CreationCode { get; set; }

        /// <summary>
        /// 创建人名称
        /// </summary>
        [Required(ErrorMessage = "创建人名称不能为空！")]
        [DynamicStringLength(typeof(NurseHandoverConsts), nameof(NurseHandoverConsts.MaxCreationNameLength), ErrorMessage = "创建人名称最大长度不能超过{1}!")]
        public string  CreationName { get; set; }

        /// <summary>
        /// 查询的全部患者
        /// </summary>
        public int  TotalPatient { get; set; }
        /// <summary>
        /// 责任护士
        /// </summary>
        public string DutyNurseName { get;  set; }
    }
}