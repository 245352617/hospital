using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Volo.Abp.Domain.Entities.Auditing;
using YiJian.ECIS.ShareModel.Extensions;

namespace YiJian.Handover
{
    using JetBrains.Annotations;
    using YiJian.ECIS;

    /// <summary>
    /// 护士交班
    /// </summary>
    public class NurseHandover : FullAuditedAggregateRoot<Guid>
    {
        /// <summary>
        /// triage分诊患者id
        /// </summary>
        [Description("triage分诊患者id")]
        public Guid PI_ID { get; private set; }

        /// <summary>
        /// 患者id
        /// </summary>
        [Description("患者id")]
        [StringLength(50)]
        public string PatientId { get; private set; }

        /// <summary>
        /// 就诊号
        /// </summary>
        [Description("就诊号")]
        public int? VisitNo { get; private set; }

        /// <summary>
        /// 患者姓名
        /// </summary>
        [StringLength(100)]
        [Description("患者姓名")]
        public string PatientName { get; private set; }

        /// <summary>
        /// 性别编码
        /// </summary>
        [StringLength(20)]
        [Description("性别编码")]
        public string Sex { get; private set; }

        /// <summary>
        /// 性别名称
        /// </summary>
        [StringLength(20)]
        [Description("性别名称")]
        public string SexName { get; private set; }

        /// <summary>
        /// 年龄
        /// </summary>
        [StringLength(20)]
        [Description("年龄")]
        public string Age { get; private set; }

        /// <summary>
        /// 分诊级别
        /// </summary>
        [StringLength(50)]
        [Description("分诊级别")]
        public string TriageLevel { get; private set; }

        /// <summary>
        /// 分诊级别名称
        /// </summary>
        [StringLength(100)]
        [Description("分诊级别名称")]
        public string TriageLevelName { get; private set; }

        /// <summary>
        /// 区域编码
        /// </summary>
        [Description("区域编码")]
        [StringLength(50)]
        public string AreaCode { get; private set; }

        /// <summary>
        /// 区域名称
        /// </summary>
        [Description("区域名称")]
        [StringLength(100)]
        public string AreaName { get; private set; }

        /// <summary>
        /// 入科时间
        /// </summary>
        public DateTime InDeptTime { get; private set; }

        /// <summary>
        /// 诊断
        /// </summary>
        [StringLength(4000)]
        [Description("诊断")]
        public string DiagnoseName { get; private set; }

        /// <summary>
        /// 床号
        /// </summary>
        [StringLength(10)]
        [Description("床号")]
        public string Bed { get; private set; }

        /// <summary>
        /// 是否三无
        /// </summary>
        public bool IsNoThree { get; private set; }

        /// <summary>
        /// 是否病危
        /// </summary>
        public bool CriticallyIll { get; private set; }

        /// <summary>
        /// 交班内容
        /// </summary>
        [StringLength(4000)]
        [Description("交班内容")]
        public string Content { get; private set; }

        /// <summary>
        /// 检验
        /// </summary>
        [StringLength(4000)]
        [Description("检验")]
        public string Test { get; private set; }

        /// <summary>
        /// 检查
        /// </summary>
        [StringLength(4000)]
        [Description("检查")]
        public string Inspect { get; private set; }

        /// <summary>
        /// 电子病历
        /// </summary>
        [StringLength(4000)]
        [Description("电子病历")]
        public string Emr { get; private set; }

        /// <summary>
        /// 出入量
        /// </summary>
        [StringLength(4000)]
        [Description("出入量")]
        public string InOutVolume { get; private set; }

        /// <summary>
        /// 生命体征
        /// </summary>
        [StringLength(4000)]
        [Description("生命体征")]
        public string VitalSigns { get; private set; }

        /// <summary>
        /// 药物
        /// </summary>
        [StringLength(4000)]
        [Description("药物")]
        public string Medicine { get; private set; }

        /// <summary>
        /// 最新现状
        /// </summary>
        [Column(TypeName = "ntext")]
        [Description("最新现状")]
        public string LatestStatus { get; private set; }

        /// <summary>
        /// 背景
        /// </summary>
        [StringLength(500)]
        [Description("背景")]
        public string Background { get; private set; }

        /// <summary>
        /// 评估
        /// </summary>
        [StringLength(500)]
        [Description("评估")]
        public string Assessment { get; private set; }

        /// <summary>
        /// 建议
        /// </summary>
        [StringLength(500)]
        [Description("建议")]
        public string Proposal { get; private set; }

        /// <summary>
        /// 设备
        /// </summary>
        [StringLength(200)]
        [Description("设备")]
        public string Devices { get; private set; }

        /// <summary>
        /// 交班时间
        /// </summary>
        [Description("交班时间")]
        public DateTime HandoverTime { get; private set; }

        /// <summary>
        /// 交班护士编码
        /// </summary>
        [Required]
        [StringLength(50)]
        [Description("交班护士编码")]
        public string HandoverNurseCode { get; private set; }

        /// <summary>
        /// 交班护士名称
        /// </summary>
        [Required]
        [StringLength(100)]
        [Description("交班护士名称")]
        public string HandoverNurseName { get; private set; }

        /// <summary>
        /// 接班护士编码
        /// </summary>
        [Required]
        [StringLength(50)]
        [Description("接班护士编码")]
        public string SuccessionNurseCode { get; private set; }

        /// <summary>
        /// 接班护士名称
        /// </summary>
        [Required]
        [StringLength(100)]
        [Description("接班护士名称")]
        public string SuccessionNurseName { get; private set; }

        /// <summary>
        /// 交班日期
        /// </summary>
        [Required]
        public DateTime HandoverDate { get; private set; }


        /// <summary>
        /// 班次id
        /// </summary>
        [Required]
        [Description("班次id")]
        public Guid ShiftSettingId { get; private set; }

        /// <summary>
        /// 班次名称
        /// </summary>
        [Required]
        [StringLength(100)]
        [Description("班次名称")]
        public string ShiftSettingName { get; private set; }

        /// <summary>
        /// 交班状态，0：未提交，1：提交交班
        /// </summary>
        [Description("交班状态")]
        public int Status { get; private set; } = 0;

        /// <summary>
        /// 创建人编码
        /// </summary>
        [Required]
        [StringLength(50)]
        public string CreationCode { get; set; }

        /// <summary>
        /// 创建人名称
        /// </summary>
        [Required]
        [StringLength(100)]
        public string CreationName { get; set; }

        /// <summary>
        /// 查询的全部患者
        /// </summary>
        public int TotalPatient { get; set; }
        /// <summary>
        /// 责任护士
        /// </summary>
        [Description("责任护士")]
        [StringLength(100)]
        public string DutyNurseName { get; private set; }
        #region constructor

        /// <summary>
        /// 护士交班构造器
        /// </summary>
        /// <param name="id"></param>
        /// <param name="pIID">triage分诊患者id</param>
        /// <param name="patientId">患者id</param>
        /// <param name="visitNo">就诊号</param>
        /// <param name="patientName">患者姓名</param>
        /// <param name="sex">性别编码</param>
        /// <param name="sexName">性别名称</param>
        /// <param name="age">年龄</param>
        /// <param name="triageLevel">分诊级别</param>
        /// <param name="triageLevelName">分诊级别名称</param>
        /// <param name="areaCode">区域编码</param>
        /// <param name="areaName">区域名称</param>
        /// <param name="inDeptTime">入科时间</param>
        /// <param name="diagnoseName">诊断</param>
        /// <param name="bed">床号</param>
        /// <param name="isNoThree">是否三无</param>
        /// <param name="criticallyIll">是否病危</param>
        /// <param name="content">交班内容</param>
        /// <param name="test">检验</param>
        /// <param name="inspect">检查</param>
        /// <param name="emr">电子病历</param>
        /// <param name="inOutVolume">出入量</param>
        /// <param name="vitalSigns">生命体征</param>
        /// <param name="medicine">药物</param>
        /// <param name="latestStatus">最新现状</param>
        /// <param name="background">背景</param>
        /// <param name="assessment">评估</param>
        /// <param name="proposal">建议</param>
        /// <param name="devices">设备</param>
        /// <param name="handoverTime">交班时间</param>
        /// <param name="handoverNurseCode">交班护士编码</param>
        /// <param name="handoverNurseName">交班护士名称</param>
        /// <param name="successionNurseCode">接班护士编码</param>
        /// <param name="successionNurseName">接班护士名称</param>
        /// <param name="handoverDate">交班日期</param>
        /// <param name="shiftSettingId">班次id</param>
        /// <param name="shiftSettingName">班次名称</param>
        /// <param name="status">交班状态，0：未提交，1：提交交班</param>
        /// <param name="creationCode">创建人编码</param>
        /// <param name="creationName">创建人名称</param>
        /// <param name="totalPatient">查询的全部患者</param>
        public NurseHandover(Guid id,
            Guid pIID, // triage分诊患者id
            string patientId, // 患者id
            int? visitNo, // 就诊号
            string patientName, // 患者姓名
            string sex, // 性别编码
            string sexName, // 性别名称
            string age, // 年龄
            string triageLevel, // 分诊级别
            string triageLevelName, // 分诊级别名称
            string areaCode, // 区域编码
            string areaName, // 区域名称
            DateTime inDeptTime, // 入科时间
            string diagnoseName, // 诊断
            string bed, // 床号
            bool isNoThree, // 是否三无
            bool criticallyIll, // 是否病危
            string content, // 交班内容
            string test, // 检验
            string inspect, // 检查
            string emr, // 电子病历
            string inOutVolume, // 出入量
            string vitalSigns, // 生命体征
            string medicine, // 药物
            string latestStatus, // 最新现状
            string background, // 背景
            string assessment, // 评估
            string proposal, // 建议
            string devices, // 设备
            DateTime handoverTime, // 交班时间
            [NotNull] string handoverNurseCode, // 交班护士编码
            [NotNull] string handoverNurseName, // 交班护士名称
            [NotNull] string successionNurseCode, // 接班护士编码
            [NotNull] string successionNurseName, // 接班护士名称
            [NotNull] DateTime handoverDate, // 交班日期
            [NotNull] Guid shiftSettingId, // 班次id
            [NotNull] string shiftSettingName, // 班次名称
            int status, // 交班状态，0：未提交，1：提交交班
            [NotNull] string creationCode, // 创建人编码
            [NotNull] string creationName, // 创建人名称
            int totalPatient, string dutyNurseName // 查询的全部患者
        ) : base(id)
        {
            //triage分诊患者id
            PI_ID = pIID;

            Modify(patientId, // 患者id
                visitNo, // 就诊号
                patientName, // 患者姓名
                sex, // 性别编码
                sexName, // 性别名称
                age, // 年龄
                triageLevel, // 分诊级别
                triageLevelName, // 分诊级别名称
                areaCode, // 区域编码
                areaName, // 区域名称
                inDeptTime, // 入科时间
                diagnoseName, // 诊断
                bed, // 床号
                isNoThree, // 是否三无
                criticallyIll, // 是否病危
                content, // 交班内容
                test, // 检验
                inspect, // 检查
                emr, // 电子病历
                inOutVolume, // 出入量
                vitalSigns, // 生命体征
                medicine, // 药物
                latestStatus, // 最新现状
                background, // 背景
                assessment, // 评估
                proposal, // 建议
                devices, // 设备
                handoverTime, // 交班时间
                handoverNurseCode, // 交班护士编码
                handoverNurseName, // 交班护士名称
                successionNurseCode, // 接班护士编码
                successionNurseName, // 接班护士名称
                handoverDate, // 交班日期
                shiftSettingId, // 班次id
                shiftSettingName, // 班次名称
                status, // 交班状态，0：未提交，1：提交交班
                creationCode, // 创建人编码
                creationName, // 创建人名称
                totalPatient,dutyNurseName // 查询的全部患者
            );
        }

        #endregion

        #region Modify

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="patientId">患者id</param>
        /// <param name="visitNo">就诊号</param>
        /// <param name="patientName">患者姓名</param>
        /// <param name="sex">性别编码</param>
        /// <param name="sexName">性别名称</param>
        /// <param name="age">年龄</param>
        /// <param name="triageLevel">分诊级别</param>
        /// <param name="triageLevelName">分诊级别名称</param>
        /// <param name="areaCode">区域编码</param>
        /// <param name="areaName">区域名称</param>
        /// <param name="inDeptTime">入科时间</param>
        /// <param name="diagnoseName">诊断</param>
        /// <param name="bed">床号</param>
        /// <param name="isNoThree">是否三无</param>
        /// <param name="criticallyIll">是否病危</param>
        /// <param name="content">交班内容</param>
        /// <param name="test">检验</param>
        /// <param name="inspect">检查</param>
        /// <param name="emr">电子病历</param>
        /// <param name="inOutVolume">出入量</param>
        /// <param name="vitalSigns">生命体征</param>
        /// <param name="medicine">药物</param>
        /// <param name="latestStatus">最新现状</param>
        /// <param name="background">背景</param>
        /// <param name="assessment">评估</param>
        /// <param name="proposal">建议</param>
        /// <param name="devices">设备</param>
        /// <param name="handoverTime">交班时间</param>
        /// <param name="handoverNurseCode">交班护士编码</param>
        /// <param name="handoverNurseName">交班护士名称</param>
        /// <param name="successionNurseCode">接班护士编码</param>
        /// <param name="successionNurseName">接班护士名称</param>
        /// <param name="handoverDate">交班日期</param>
        /// <param name="shiftSettingId">班次id</param>
        /// <param name="shiftSettingName">班次名称</param>
        /// <param name="status">交班状态，0：未提交，1：提交交班</param>
        /// <param name="creationCode">创建人编码</param>
        /// <param name="creationName">创建人名称</param>
        /// <param name="totalPatient">查询的全部患者</param>
        /// <param name="dutyNurseName"></param>
        public void Modify(string patientId, // 患者id
            int? visitNo, // 就诊号
            string patientName, // 患者姓名
            string sex, // 性别编码
            string sexName, // 性别名称
            string age, // 年龄
            string triageLevel, // 分诊级别
            string triageLevelName, // 分诊级别名称
            string areaCode, // 区域编码
            string areaName, // 区域名称
            DateTime inDeptTime, // 入科时间
            string diagnoseName, // 诊断
            string bed, // 床号
            bool isNoThree, // 是否三无
            bool criticallyIll, // 是否病危
            string content, // 交班内容
            string test, // 检验
            string inspect, // 检查
            string emr, // 电子病历
            string inOutVolume, // 出入量
            string vitalSigns, // 生命体征
            string medicine, // 药物
            string latestStatus, // 最新现状
            string background, // 背景
            string assessment, // 评估
            string proposal, // 建议
            string devices, // 设备
            DateTime handoverTime, // 交班时间
            [NotNull] string handoverNurseCode, // 交班护士编码
            [NotNull] string handoverNurseName, // 交班护士名称
            [NotNull] string successionNurseCode, // 接班护士编码
            [NotNull] string successionNurseName, // 接班护士名称
            [NotNull] DateTime handoverDate, // 交班日期
            [NotNull] Guid shiftSettingId, // 班次id
            [NotNull] string shiftSettingName, // 班次名称
            int status, // 交班状态，0：未提交，1：提交交班
            [NotNull] string creationCode, // 创建人编码
            [NotNull] string creationName, // 创建人名称
            int totalPatient, string dutyNurseName // 查询的全部患者
        )
        {
            //患者id
            PatientId = Check.Length(patientId, "患者id", NurseHandoverConsts.MaxPatientIdLength);

            //就诊号
            VisitNo = visitNo;

            //患者姓名
            PatientName = Check.Length(patientName, "患者姓名", NurseHandoverConsts.MaxPatientNameLength);

            //性别编码
            Sex = Check.Length(sex, "性别编码", NurseHandoverConsts.MaxSexLength);

            //性别名称
            SexName = Check.Length(sexName, "性别名称", NurseHandoverConsts.MaxSexNameLength);

            //年龄
            Age = Check.Length(age, "年龄", NurseHandoverConsts.MaxAgeLength);

            //分诊级别
            TriageLevel = Check.Length(triageLevel, "分诊级别", NurseHandoverConsts.MaxTriageLevelLength);

            //分诊级别名称
            TriageLevelName = Check.Length(triageLevelName, "分诊级别名称", NurseHandoverConsts.MaxTriageLevelNameLength);

            //区域编码
            AreaCode = Check.Length(areaCode, "区域编码", NurseHandoverConsts.MaxAreaCodeLength);

            //区域名称
            AreaName = Check.Length(areaName, "区域名称", NurseHandoverConsts.MaxAreaNameLength);

            //入科时间
            InDeptTime = inDeptTime;

            //诊断
            DiagnoseName = Check.Length(diagnoseName, "诊断", NurseHandoverConsts.MaxDiagnoseNameLength);

            //床号
            Bed = Check.Length(bed, "床号", NurseHandoverConsts.MaxBedLength);

            //是否三无
            IsNoThree = isNoThree;

            //是否病危
            CriticallyIll = criticallyIll;

            //交班内容
            Content = Check.Length(content, "交班内容", NurseHandoverConsts.MaxContentLength);

            //检验
            Test = Check.Length(test, "检验", NurseHandoverConsts.MaxTestLength);

            //检查
            Inspect = Check.Length(inspect, "检查", NurseHandoverConsts.MaxInspectLength);

            //电子病历
            Emr = Check.Length(emr, "电子病历", NurseHandoverConsts.MaxEmrLength);

            //出入量
            InOutVolume = Check.Length(inOutVolume, "出入量", NurseHandoverConsts.MaxInOutVolumeLength);

            //生命体征
            VitalSigns = Check.Length(vitalSigns, "生命体征", NurseHandoverConsts.MaxVitalSignsLength);

            //药物
            Medicine = Check.Length(medicine, "药物", NurseHandoverConsts.MaxMedicineLength);

            //最新现状
            LatestStatus = Check.Length(latestStatus, "最新现状", NurseHandoverConsts.MaxLatestStatusLength);

            //背景
            Background = Check.Length(background, "背景", NurseHandoverConsts.MaxBackgroundLength);

            //评估
            Assessment = Check.Length(assessment, "评估", NurseHandoverConsts.MaxAssessmentLength);

            //建议
            Proposal = Check.Length(proposal, "建议", NurseHandoverConsts.MaxProposalLength);

            //设备
            Devices = Check.Length(devices, "设备", NurseHandoverConsts.MaxDevicesLength);

            //交班时间
            HandoverTime = handoverTime;

            //交班护士编码
            HandoverNurseCode =
                Check.NotNull(handoverNurseCode, "交班护士编码", NurseHandoverConsts.MaxHandoverNurseCodeLength);

            //交班护士名称
            HandoverNurseName =
                Check.NotNull(handoverNurseName, "交班护士名称", NurseHandoverConsts.MaxHandoverNurseNameLength);

            //接班护士编码
            SuccessionNurseCode = Check.NotNull(successionNurseCode, "接班护士编码",
                NurseHandoverConsts.MaxSuccessionNurseCodeLength);

            //接班护士名称
            SuccessionNurseName = Check.NotNull(successionNurseName, "接班护士名称",
                NurseHandoverConsts.MaxSuccessionNurseNameLength);

            //交班日期
            HandoverDate = Check.NotNull(handoverDate, "交班日期");

            //班次id
            ShiftSettingId = Check.NotNull(shiftSettingId, "班次id");

            //班次名称
            ShiftSettingName = Check.NotNull(shiftSettingName, "班次名称", NurseHandoverConsts.MaxShiftSettingNameLength);

            //交班状态，0：未提交，1：提交交班
            Status = status;

            //创建人编码
            CreationCode = Check.NotNull(creationCode, "创建人编码", NurseHandoverConsts.MaxCreationCodeLength);

            //创建人名称
            CreationName = Check.NotNull(creationName, "创建人名称", NurseHandoverConsts.MaxCreationNameLength);

            //查询的全部患者
            TotalPatient = totalPatient;
            DutyNurseName = dutyNurseName;

        }

        #endregion

        #region constructor

        private NurseHandover(string dutyNurseName)
        {
            DutyNurseName = dutyNurseName;
            // for EFCore
        }

        #endregion
    }
}