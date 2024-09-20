using System;
using System.ComponentModel.DataAnnotations;
using Szyjian.Ecis.Patient.Domain.Shared;

namespace Szyjian.Ecis.Patient.Application.Contracts
{
    /// <summary>
    /// 诊断记录Dto
    /// </summary>
    public class DiagnoseRecordDto
    {
        /// <summary>
        /// 自增Id
        /// </summary>
        public int PD_ID { get; set; }

        /// <summary>
        /// Triage_PatientInfo表ID(PVID)
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
        /// 诊断分类编码  开立 = 1，收藏 = 2
        /// </summary>
        public DiagnoseClass DiagnoseClassCode { get; set; }

        /// <summary>
        /// 诊断分类名称
        /// </summary>
        [MaxLength(20)]
        public string DiagnoseClass { get; set; }

        /// <summary>
        /// 诊断类型编码 0 = 一般诊断，1 = 疑似诊断，2 = 主要诊断
        /// </summary>
        [MaxLength(20)]
        public string DiagnoseTypeCode { get; set; }

        /// <summary>
        /// 诊断类型名称
        /// </summary>
        [MaxLength(20)]
        public string DiagnoseType { get; set; }

        /// <summary>
        /// 诊断代码
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
        /// 排序
        /// </summary>
        public int Sort { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [MaxLength(256)]
        public string Remark { get; set; }

        /// <summary>
        /// 添加人
        /// </summary>
        [MaxLength(50)]
        public string AddUserName { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreationTime { get; set; }

        /// <summary>
        /// 修改人
        /// </summary>
        [MaxLength(50)]
        public string ModUserName { get; set; }

        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime? LastModificationTime { get; set; }

        /// <summary>
        /// 是否删除
        /// </summary>
        public bool IsDeleted { get; set; }

        /// <summary>
        /// 删除人
        /// </summary>
        [MaxLength(50)]
        public string DeleteUserName { get; set; }

        /// <summary>
        /// 删除时间
        /// </summary>
        public DateTime? DeletionTime { get; set; }

        /// <summary>
        /// 医院编码
        /// </summary>
        [MaxLength(250)]
        public string HospitalCode { get; set; }

        /// <summary>
        /// 医院名称
        /// </summary>
        [MaxLength(250)]
        public string HospitalName { get; set; }

        /// <summary>
        /// 扩展字段1
        /// </summary>
        [MaxLength(1000)]
        public string ExtensionField1 { get; set; }

        /// <summary>
        /// 扩展字段2
        /// </summary>
        [MaxLength(1000)]
        public string ExtensionField2 { get; set; }

        /// <summary>
        /// 扩展字段3
        /// </summary>
        [MaxLength(1000)]
        public string ExtensionField3 { get; set; }

        /// <summary>
        /// 扩展字段4
        /// </summary>
        [MaxLength(1000)]
        public string ExtensionField4 { get; set; }

        /// <summary>
        /// 扩展字段5
        /// </summary>
        [MaxLength(1000)]
        public string ExtensionField5 { get; set; }

        /// <summary>
        /// 医学类型
        /// </summary>
        public MedicalTypeEnum MedicalType { get; set; }

        /// <summary>
        /// Icd10
        /// </summary>
        public string Icd10 { get; set; }

        /// <summary>
        /// 报卡类型
        /// </summary>
        public ECardReportingType CardRepType { get; set; }

        /// <summary>
        /// 被电子病历引用过的诊断信息
        /// </summary>
        public bool EmrUsed { get; set; } = false;
    }
}