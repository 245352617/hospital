namespace YiJian.Handover
{
    /// <summary>
    /// 交班患者 表字段长度常量
    /// </summary>
    public class NursePatientsConsts
    {  
        /// <summary>
        /// 患者id(50)
        /// </summary>
        public static int MaxPatientIdLength { get; set; } = 50;
        /// <summary>
        /// 患者姓名(100)
        /// </summary>
        public static int MaxPatientNameLength { get; set; } = 100;
        /// <summary>
        /// 性别(10)
        /// </summary>
        public static int MaxSexLength { get; set; } = 20;
        /// <summary>
        /// 年龄(20)
        /// </summary>
        public static int MaxAgeLength { get; set; } = 20;
        /// <summary>
        /// 分诊级别(50)
        /// </summary>
        public static int MaxTriageLevelLength { get; set; } = 50;
        /// <summary>
        /// 分诊级别名称(100)
        /// </summary>
        public static int MaxTriageLevelNameLength { get; set; } = 100;
        /// <summary>
        /// 区域编码(50)
        /// </summary>
        public static int MaxAreaCodeLength { get; set; } = 50;
        /// <summary>
        /// 区域名称(100)
        /// </summary>
        public static int MaxAreaNameLength { get; set; } = 100;
        /// <summary>
        /// 诊断(4000)
        /// </summary>
        public static int MaxDiagnoseNameLength { get; set; } = 4000;
        /// <summary>
        /// 床号(10)
        /// </summary>
        public static int MaxBedLength { get; set; } = 10;
        /// <summary>
        /// 交班内容(4000)
        /// </summary>
        public static int MaxContentLength { get; set; } = 4000;
        /// <summary>
        /// 检验(4000)
        /// </summary>
        public static int MaxTestLength { get; set; } = 4000;
        /// <summary>
        /// 检查(4000)
        /// </summary>
        public static int MaxInspectLength { get; set; } = 4000;
        /// <summary>
        /// 电子病历(4000)
        /// </summary>
        public static int MaxEmrLength { get; set; } = 4000;
        /// <summary>
        /// 出入量(4000)
        /// </summary>
        public static int MaxInOutVolumeLength { get; set; } = 4000;
        /// <summary>
        /// 生命体征(4000)
        /// </summary>
        public static int MaxVitalSignsLength { get; set; } = 4000;
        /// <summary>
        /// 药物(4000)
        /// </summary>
        public static int MaxMedicineLength { get; set; } = 4000;
        /// <summary>
        /// 最新现状(4000)
        /// </summary>
        public static int MaxLatestStatusLength { get; set; } = 4000;
        /// <summary>
        /// 背景(500)
        /// </summary>
        public static int MaxBackgroundLength { get; set; } = 500;
        /// <summary>
        /// 评估(500)
        /// </summary>
        public static int MaxAssessmentLength { get; set; } = 500;
        /// <summary>
        /// 建议(500)
        /// </summary>
        public static int MaxProposalLength { get; set; } = 500;
        /// <summary>
        /// 设备(200)
        /// </summary>
        public static int MaxDevicesLength { get; set; } = 200;
        /// <summary>
        /// 交班护士编码(50)
        /// </summary>
        public static int MaxHandoverNurseCodeLength { get; set; } = 50;
        /// <summary>
        /// 交班护士名称(100)
        /// </summary>
        public static int MaxHandoverNurseNameLength { get; set; } = 100;
        /// <summary>
        /// 接班护士编码(50)
        /// </summary>
        public static int MaxSuccessionNurseCodeLength { get; set; } = 50;
        /// <summary>
        /// 接班护士名称(100)
        /// </summary>
        public static int MaxSuccessionNurseNameLength { get; set; } = 100;
    }
}