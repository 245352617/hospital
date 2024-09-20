namespace YiJian.Handover
{
    using System;

    [Serializable]
    public class DoctorPatientsData
    {        
        public Guid Id { get; set; }
        
        /// <summary>
        /// 医生交班id
        /// </summary>
        public Guid  DoctorHandoverId { get; set; }
        
        
        public Guid  PI_ID { get; set; }
        
        /// <summary>
        /// tiage分诊患者id </summary> <summary> 患者id
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
        public string  TriageLevelName { get; set; }
        
        /// <summary>
        /// 诊断
        /// </summary>
        public string  DiagnoseName { get; set; }
        
        /// <summary>
        /// 床号
        /// </summary>
        public string  Bed { get; set; }
        
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
        /// 状态
        /// </summary>
        public bool Status { get; set; }

    }
}