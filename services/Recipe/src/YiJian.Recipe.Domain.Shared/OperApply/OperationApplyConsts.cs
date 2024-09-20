namespace YiJian.Recipe
{
    /// <summary>
    /// 手术申请 表字段长度常量
    /// </summary>
    public class OperationApplyConsts
    {
        /// <summary>
        /// 患者唯一标识(HIS)(4000)
        /// </summary>
        public static int MaxPatientIdLength { get; set; } = 20;
        /// <summary>
        /// 患者姓名(4000)
        /// </summary>
        public static int MaxPatientNameLength { get; set; } = 50;
        /// <summary>
        /// 患者性别(4000)
        /// </summary>
        public static int MaxSexLength { get; set; } = 10;
        /// <summary>
        /// 年龄(4000)
        /// </summary>
        public static int MaxAgeLength { get; set; } = 20;
        /// <summary>
        /// 身份证号(4000)
        /// </summary>
        public static int MaxIDNoLength { get; set; } = 20;
        /// <summary>
        /// 申请单号(4000)
        /// </summary>
        public static int MaxApplyNumLength { get; set; } = 20;
        /// <summary>
        /// 申请人Id(4000)
        /// </summary>
        public static int MaxApplicantIdLength { get; set; } = 50;
        /// <summary>
        /// 申请人名称(4000)
        /// </summary>
        public static int MaxApplicantNameLength { get; set; } = 100;
        /// <summary>
        /// 血型(4000)
        /// </summary>
        public static int MaxBloodTypeLength { get; set; } = 50;
        /// <summary>
        /// RH阴性阳性(4000)
        /// </summary>
        public static int MaxRHCodeLength { get; set; } = 50;
        /// <summary>
        /// 拟施手术编码(4000)
        /// </summary>
        public static int MaxProposedOperationCodeLength { get; set; } = 1000;
        /// <summary>
        /// 拟施手术名称(4000)
        /// </summary>
        public static int MaxProposedOperationNameLength { get; set; } = 4000;
        /// <summary>
        /// 手术等级(4000)
        /// </summary>
        public static int MaxOperationLevelLength { get; set; } = 50;
        /// <summary>
        /// 申请科室编码(4000)
        /// </summary>
        public static int MaxApplyDeptCodeLength { get; set; } = 50;
        /// <summary>
        /// 申请科室名称(4000)
        /// </summary>
        public static int MaxApplyDeptNameLength { get; set; } = 100;
        /// <summary>
        /// 手术位置(4000)
        /// </summary>
        public static int MaxOperationLocationLength { get; set; } = 100;
        /// <summary>
        /// 手术医生编码(4000)
        /// </summary>
        public static int MaxDoctorCodeLength { get; set; } = 50;
        /// <summary>
        /// 手术医生名称(4000)
        /// </summary>
        public static int MaxDoctorNameLength { get; set; } = 100;
        /// <summary>
        /// 手术助手(4000)
        /// </summary>
        public static int MaxOperationAssistantLength { get; set; } = 200;
        /// <summary>
        /// 手术时长(4000)
        /// </summary>
        public static int MaxOperationDurationLength { get; set; } = 50;
        /// <summary>
        /// 备注(4000)
        /// </summary>
        public static int MaxRemarkLength { get; set; } = 4000;
        /// <summary>
        /// 手术类型(4000)
        /// </summary>
        public static int MaxOperationTypeLength { get; set; } = 50;
        /// <summary>
        /// 术前诊断编码(4000)
        /// </summary>
        public static int MaxDiagnoseCodeLength { get; set; } = 200;
        /// <summary>
        /// 术前诊断名称(4000)
        /// </summary>
        public static int MaxDiagnoseNameLength { get; set; } = 1000;
        /// <summary>
        /// 麻醉医生(4000)
        /// </summary>
        public static int MaxAnesthesiologistLength { get; set; } = 50;
        /// <summary>
        /// 麻醉助手(4000)
        /// </summary>
        public static int MaxAnesthesiologistAssistantLength { get; set; } = 50;
        /// <summary>
        /// 巡回护士(4000)
        /// </summary>
        public static int MaxTourNurseLength { get; set; } = 50;
        /// <summary>
        /// 器械护士(4000)
        /// </summary>
        public static int MaxInstrumentNurseLength { get; set; } = 50;
        /// <summary>
        /// 麻醉方式编码(4000)
        /// </summary>
        public static int MaxAnaestheticCodeLength { get; set; } = 50;
        /// <summary>
        /// 麻醉方式名称(4000)
        /// </summary>
        public static int MaxAnaestheticNameLength { get; set; } = 100;
    }
}