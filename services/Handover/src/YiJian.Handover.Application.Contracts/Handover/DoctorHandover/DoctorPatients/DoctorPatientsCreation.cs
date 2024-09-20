using System.ComponentModel.DataAnnotations.Schema;

namespace YiJian.Handover
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using Volo.Abp.Validation;

    [Serializable]
    public class DoctorPatientsCreation
    {
   

        /// <summary>
        /// triage分诊患者id
        /// </summary> 
        public Guid PI_ID { get; set; }

        ///  <summary>
        /// 患者id
        /// </summary>
        [DynamicStringLength(typeof(DoctorPatientsConsts), nameof(DoctorPatientsConsts.MaxPatientIdLength),
            ErrorMessage = "患者id最大长度不能超过{1}!")]
        public string PatientId { get; set; }

        /// <summary>
        /// 就诊号
        /// </summary>

        public int? VisitNo { get; set; }

        /// <summary>
        /// 患者姓名
        /// </summary>
        [DynamicStringLength(typeof(DoctorPatientsConsts), nameof(DoctorPatientsConsts.MaxPatientNameLength),
            ErrorMessage = "患者姓名最大长度不能超过{1}!")]
        public string PatientName { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        [DynamicStringLength(typeof(DoctorPatientsConsts), nameof(DoctorPatientsConsts.MaxSexLength),
            ErrorMessage = "性别最大长度不能超过{1}!")]
        public string Sex { get; set; }
        public string  SexName { get; set; }
        /// <summary>
        /// 年龄
        /// </summary>
        [DynamicStringLength(typeof(DoctorPatientsConsts), nameof(DoctorPatientsConsts.MaxAgeLength),
            ErrorMessage = "年龄最大长度不能超过{1}!")]
        public string Age { get; set; }

        /// <summary>
        /// 分诊级别
        /// </summary>
        [DynamicStringLength(typeof(DoctorPatientsConsts), nameof(DoctorPatientsConsts.MaxTriageLevelLength),
            ErrorMessage = "分诊级别最大长度不能超过{1}!")]
        public string TriageLevelName { get; set; }

        /// <summary>
        /// 诊断
        /// </summary>
        [DynamicStringLength(typeof(DoctorPatientsConsts), nameof(DoctorPatientsConsts.MaxDiagnoseLength),
            ErrorMessage = "诊断最大长度不能超过{1}!")]
        public string DiagnoseName { get; set; }

        /// <summary>
        /// 床号
        /// </summary>
        [DynamicStringLength(typeof(DoctorPatientsConsts), nameof(DoctorPatientsConsts.MaxBedLength),
            ErrorMessage = "床号最大长度不能超过{1}!")]
        public string Bed { get; set; }

        /// <summary>
        /// 交班内容
        /// </summary>
        [Column(TypeName = "text")]
        public string Content { get; set; }

        /// <summary>
        /// 检验
        /// </summary>
        [DynamicStringLength(typeof(DoctorPatientsConsts), nameof(DoctorPatientsConsts.MaxTestLength),
            ErrorMessage = "检验最大长度不能超过{1}!")]
        public string Test { get; set; }

        /// <summary>
        /// 检查
        /// </summary>
        [DynamicStringLength(typeof(DoctorPatientsConsts), nameof(DoctorPatientsConsts.MaxInspectLength),
            ErrorMessage = "检查最大长度不能超过{1}!")]
        public string Inspect { get; set; }

        /// <summary>
        /// 电子病历
        /// </summary>
        [DynamicStringLength(typeof(DoctorPatientsConsts), nameof(DoctorPatientsConsts.MaxEmrLength),
            ErrorMessage = "电子病历最大长度不能超过{1}!")]
        public string Emr { get; set; }

        /// <summary>
        /// 出入量
        /// </summary>
        [DynamicStringLength(typeof(DoctorPatientsConsts), nameof(DoctorPatientsConsts.MaxInOutVolumeLength),
            ErrorMessage = "出入量最大长度不能超过{1}!")]
        public string InOutVolume { get; set; }

        /// <summary>
        /// 生命体征
        /// </summary>
        [DynamicStringLength(typeof(DoctorPatientsConsts), nameof(DoctorPatientsConsts.MaxVitalSignsLength),
            ErrorMessage = "生命体征最大长度不能超过{1}!")]
        public string VitalSigns { get; set; }

        /// <summary>
        /// 药物
        /// </summary>
        [DynamicStringLength(typeof(DoctorPatientsConsts), nameof(DoctorPatientsConsts.MaxMedicineLength),
            ErrorMessage = "药物最大长度不能超过{1}!")]
        public string Medicine { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public bool Status { get; set; }
    }
}