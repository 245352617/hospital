using FreeSql.DataAnnotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using Szyjian.BaseFreeSql;
using Szyjian.Ecis.Patient.Domain.Shared;

namespace Szyjian.Ecis.Patient.Domain
{
    /// <summary>
    /// 诊断记录表
    /// </summary>
    [Table(Name = "Pat_DiagnoseRecord")]
    public class DiagnoseRecord : AuditEntity, IEqualityComparer<DiagnoseRecord>
    {
        /// <summary>
        /// 自增序号
        /// </summary>
        [Column(IsPrimary = true, IsIdentity = true, Position = 1)]
        public int PD_ID { get; set; }

        /// <summary>
        /// Triage_PatientInfo表ID
        /// </summary>
        [Column(Position = 2)]
        public Guid PI_ID { get; set; }

        /// <summary>
        /// 患者唯一标识(HIS)
        /// </summary>
        [MaxLength(20)]
        [Column(IsNullable = true, Position = 3)]
        public string PatientID { get; set; }

        /// <summary>
        /// 就诊号
        /// </summary>
        [Column(Position = 4)]
        public string VisitNo { get; set; }

        /// <summary>
        /// 就诊时间
        /// </summary>
        [Column(IsNullable = true, Position = 5)]
        public DateTime? VisitDate { get; set; }

        /// <summary>
        /// 诊断分类编码
        /// </summary>
        [Column(Position = 6)]
        public DiagnoseClass DiagnoseClassCode { get; set; }

        /// <summary>
        /// 诊断分类名称
        /// </summary>
        [MaxLength(20)]
        [Column(Position = 7)]
        public string DiagnoseClass { get; set; }

        /// <summary>
        /// 诊断类型编码 
        /// </summary>
        [Column(Position = 8)]
        public string DiagnoseTypeCode { get; set; }

        /// <summary>
        /// 诊断类型名称
        /// </summary>
        [MaxLength(20)]
        [Column(Position = 9)]
        public string DiagnoseType { get; set; }

        /// <summary>
        /// 诊断代码
        /// </summary>
        [MaxLength(50)]
        [Column(Position = 10)]
        public string DiagnoseCode { get; set; }

        /// <summary>
        /// 诊断名称
        /// </summary>
        [MaxLength(200)]
        [Column(Position = 11)]
        public string DiagnoseName { get; set; }

        /// <summary>
        /// 拼音码
        /// </summary>
        [MaxLength(100)]
        [Column(Position = 12)]
        public string PyCode { get; private set; }

        /// <summary>
        /// 医生编码
        /// </summary>
        [MaxLength(20)]
        [Column(Position = 13)]
        public string DoctorCode { get; set; }

        /// <summary>
        /// 医生姓名
        /// </summary>
        [MaxLength(50)]
        [Column(Position = 14)]
        public string DoctorName { get; set; }

        /// <summary>
        /// 管培生编码
        /// </summary>
        [MaxLength(20)]
        [Column(Position = 15)]
        public string TraineeCode { get; set; }

        /// <summary>
        /// 管培生姓名
        /// </summary>
        [MaxLength(50)]
        [Column(Position = 16)]
        public string TraineeName { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        [Column(Position = 17)]
        public int Sort { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [MaxLength(256)]
        [Column(Position = 18)]
        public string Remark { get; set; }


        /// <summary>
        /// 医院编码
        /// </summary>
        [MaxLength(250)]
        [Column(Position = 19)]
        public string HospitalCode { get; set; }

        /// <summary>
        /// 医院名称
        /// </summary>
        [MaxLength(250)]
        [Column(Position = 20)]
        public string HospitalName { get; set; }

        /// <summary>
        /// 医学类型
        /// </summary>
        [MaxLength(10)]
        [Column(Position = 21)]
        public MedicalTypeEnum MedicalType { get; set; }

        /// <summary>
        /// 扩展字段1
        /// </summary>
        [MaxLength(1000)]
        [Column(Position = 22)]
        public string ExtensionField1 { get; set; }

        /// <summary>
        /// 扩展字段2
        /// </summary>
        [MaxLength(1000)]
        [Column(Position = 23)]
        public string ExtensionField2 { get; set; }

        /// <summary>
        /// 扩展字段3
        /// </summary>
        [MaxLength(1000)]
        [Column(Position = 24)]
        public string ExtensionField3 { get; set; }

        /// <summary>
        /// 扩展字段4
        /// </summary>
        [MaxLength(1000)]
        [Column(Position = 25)]
        public string ExtensionField4 { get; set; }

        /// <summary>
        /// 扩展字段5
        /// </summary>
        [MaxLength(1000)]
        [Column(Position = 26)]
        public string ExtensionField5 { get; set; }

        /// <summary>
        /// Icd10
        /// </summary>
        [StringLength(128)]
        [Column(Position = 27)]
        public string Icd10 { get; set; }

        /// <summary>
        /// 报卡类型
        /// </summary>
        [StringLength(20)]
        [Column(Position = 28)]
        public ECardReportingType CardRepType { get; set; }

        /// <summary>
        /// 覆盖创建时间
        /// </summary>
        public new DateTime CreationTime { get; set; }

        /// <summary>
        /// 被电子病历引用过的诊断信息
        /// </summary>
        public bool EmrUsed { get; set; } = false;

        /// <summary>
        /// 比较方法
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public bool Equals([AllowNull] DiagnoseRecord x, [AllowNull] DiagnoseRecord y)
        {
            return x.DiagnoseCode == y.DiagnoseCode && x.DoctorCode == y.DoctorCode;
        }

        /// <summary>
        /// 重写hashcode
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public int GetHashCode([DisallowNull] DiagnoseRecord obj)
        {
            return obj.DiagnoseCode.GetHashCode() ^ obj.DoctorCode.GetHashCode();
        }

        /// <summary>
        /// 设定时间节点名称
        /// </summary>
        /// <returns></returns>
        public DiagnoseRecord SetDiagnoseTypeName()
        {
            DiagnoseType name = Enum.Parse<DiagnoseType>(DiagnoseTypeCode);
            DiagnoseType = name.GetDescription();
            return this;
        }
    }
}