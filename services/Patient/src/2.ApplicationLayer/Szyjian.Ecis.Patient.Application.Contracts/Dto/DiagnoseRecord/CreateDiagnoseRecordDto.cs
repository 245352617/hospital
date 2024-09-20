using System;
using System.ComponentModel.DataAnnotations;
using Szyjian.Ecis.Patient.Domain.Shared;

namespace Szyjian.Ecis.Patient.Application.Contracts
{
    /// <summary>
    /// 新增诊断记录Dto
    /// </summary>
    public class CreateDiagnoseRecordDto
    {
        public int PD_ID { get; set; } = -1;

        /// <summary>
        /// Triage_PatientInfo表ID
        /// </summary>
        public Guid PI_ID { get; set; }

        /// <summary>
        /// 患者唯一标识(HIS)
        /// </summary>
        [MaxLength(20)]
        public string PatientID { get; set; }

        /// <summary>
        /// 就诊号
        /// </summary>
        public string VisitNo { get; set; }

        /// <summary>
        /// 就诊时间
        /// </summary>
        public DateTime? VisitDate { get; set; }

        /// <summary>
        /// 诊断类型编码  Commonly = 一般诊断，Suspected = 疑似诊断，Main = 主要诊断
        /// </summary>
        public string DiagnoseTypeCode { get; set; }

        /// <summary>
        /// 诊断编码
        /// </summary>
        [MaxLength(50)]
        public string DiagnoseCode { get; set; }

        /// <summary>
        /// 诊断名称
        /// </summary>
        [MaxLength(100)]
        public string DiagnoseName { get; set; }

        /// <summary>
        /// 拼音码
        /// </summary>
        [MaxLength(100)]
        public string PyCode { get; set; }

        /// <summary>
        /// 医生编码
        /// </summary>
        [MaxLength(20)]
        public string DoctorCode { get; set; }

        /// <summary>
        /// 医生姓名
        /// </summary>
        [MaxLength(50)]
        public string DoctorName { get; set; }

        /// <summary>
        /// 管培生编码
        /// </summary>
        [MaxLength(20)]
        public string TraineeCode { get; set; }

        /// <summary>
        /// 管培生姓名
        /// </summary>
        [MaxLength(50)]
        public string TraineeName { get; set; }

        /// <summary>
        /// 医学类型
        /// </summary>
        public MedicalTypeEnum MedicalType { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int Sort { get; set; }

        /// <summary>
        /// 诊断说明
        /// </summary>
        [MaxLength(256)]
        public string Remark { get; set; }

        public DateTime? CreationTime { get; set; } = DateTime.Now;

        /// <summary>
        /// Icd10
        /// </summary>
        public string Icd10 { get; set; }

    }
}