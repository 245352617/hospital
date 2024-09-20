namespace YiJian.Handover
{
    using System;

    /// <summary>
    /// 交班患者 读取输出
    /// </summary>
    [Serializable]
    public class NursePatientsData
    {        
        public Guid Id { get; set; }
        
        /// <summary>
        /// 护士交班id
        /// </summary>
        public Guid  NurseHandoverId { get; set; }
        
        /// <summary>
        /// triage分诊患者id
        /// </summary>
        public Guid  PI_ID { get; set; }
        
        /// <summary>
        /// 患者id
        /// </summary>
        public string  PatientId { get; set; }
        
        /// <summary>
        /// 就诊号
        /// </summary>
        public int?  VisitNo { get; set; }
        
        /// <summary>
        /// 患者姓名
        /// </summary>
        public string  PatientName { get; set; }
        
        /// <summary>
        /// 性别
        /// </summary>
        public string  Sex { get; set; }
        
        /// <summary>
        /// 年龄
        /// </summary>
        public string  Age { get; set; }
        
        /// <summary>
        /// 分诊级别
        /// </summary>
        public string  TriageLevel { get; set; }
        
        /// <summary>
        /// 分诊级别名称
        /// </summary>
        public string  TriageLevelName { get; set; }
        
        /// <summary>
        /// 区域编码
        /// </summary>
        public string  AreaCode { get; set; }
        
        /// <summary>
        /// 区域名称
        /// </summary>
        public string  AreaName { get; set; }
        
        /// <summary>
        /// 入科时间
        /// </summary>
        public DateTime  InDeptTime { get; set; }
        
        /// <summary>
        /// 诊断
        /// </summary>
        public string  DiagnoseName { get; set; }
        
        /// <summary>
        /// 床号
        /// </summary>
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
        public string  Content { get; set; }
        
        /// <summary>
        /// 检验
        /// </summary>
        public string  Test { get; set; }
        
        /// <summary>
        /// 检查
        /// </summary>
        public string  Inspect { get; set; }
        
        /// <summary>
        /// 电子病历
        /// </summary>
        public string  Emr { get; set; }
        
        /// <summary>
        /// 出入量
        /// </summary>
        public string  InOutVolume { get; set; }
        
        /// <summary>
        /// 生命体征
        /// </summary>
        public string  VitalSigns { get; set; }
        
        /// <summary>
        /// 药物
        /// </summary>
        public string  Medicine { get; set; }
        
        /// <summary>
        /// 最新现状
        /// </summary>
        public string  LatestStatus { get; set; }
        
        /// <summary>
        /// 背景
        /// </summary>
        public string  Background { get; set; }
        
        /// <summary>
        /// 评估
        /// </summary>
        public string  Assessment { get; set; }
        
        /// <summary>
        /// 建议
        /// </summary>
        public string  Proposal { get; set; }
        
        /// <summary>
        /// 设备
        /// </summary>
        public string  Devices { get; set; }
        
        /// <summary>
        /// 交班时间
        /// </summary>
        public DateTime  HandoverTime { get; set; }
        
        /// <summary>
        /// 交班护士编码
        /// </summary>
        public string  HandoverNurseCode { get; set; }
        
        /// <summary>
        /// 交班护士名称
        /// </summary>
        public string  HandoverNurseName { get; set; }
        
        /// <summary>
        /// 接班护士编码
        /// </summary>
        public string  SuccessionNurseCode { get; set; }
        
        /// <summary>
        /// 接班护士名称
        /// </summary>
        public string  SuccessionNurseName { get; set; }
        /// <summary>
        /// 责任护士
        /// </summary>
        public string DutyNurseName { get;  set; }
        
    }
}