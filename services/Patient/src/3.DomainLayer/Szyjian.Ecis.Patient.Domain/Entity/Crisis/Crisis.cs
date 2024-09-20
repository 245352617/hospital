using FreeSql.DataAnnotations;
using System;
using System.ComponentModel.DataAnnotations;

namespace Szyjian.Ecis.Patient.Domain
{
    /// <summary>
    /// 描    述:危急值
    /// 创 建 人:杨凯
    /// 创建时间:2023/9/26 14:44:41
    /// </summary>
    [Table(Name = "Pat_Crisis")]
    public class Crisis
    {
        /// <summary>
        /// 主键
        /// </summary>
        [Column(IsPrimary = true)]
        public Guid Id { get; set; }

        /// <summary>
        /// 处理措施编码
        /// </summary>
        public string HandleCode { get; set; }

        /// <summary>
        /// 处理措施名称
        /// </summary>
        public string HandleName { get; set; }

        /// <summary>
        /// 患者ID
        /// </summary>
        public Guid PI_ID { get; set; }

        /// <summary>
        /// 病案号
        /// </summary>
        public string MedicalRecordNo { get; set; }

        /// <summary>
        /// 开单医生编码
        /// </summary>
        public string ApplyDoctorCode { get; set; }

        /// <summary>
        /// 开单医生
        /// </summary>
        public string ApplyDoctorName { get; set; }

        /// <summary>
        /// 危急值项目
        /// </summary>
        public string CrisisName { get; set; }

        /// <summary>
        /// 危急值数值
        /// </summary>
        public string CrisisValue { get; set; }

        /// <summary>
        /// 发报告号人编码
        /// </summary>
        public string ReporterCode { get; set; }

        /// <summary>
        /// 发报告号人
        /// </summary>
        public string ReporterName { get; set; }

        /// <summary>
        /// 发报告时间
        /// </summary>
        public DateTime ReporterTime { get; set; }

        /// <summary>
        /// 接收护士编码
        /// </summary>
        public string NursingCode { get; set; }

        /// <summary>
        /// 接收护士
        /// </summary>
        public string NursingName { get; set; }

        /// <summary>
        /// 护士接收时间
        /// </summary>
        public DateTime? NursingReceiveTime { get; set; }

        /// <summary>
        /// 接收医生编码
        /// </summary>
        public string DoctorCode { get; set; }

        /// <summary>
        /// 接收医生
        /// </summary>
        public string DoctorName { get; set; }

        /// <summary>
        /// 医生接收时间
        /// </summary>
        public DateTime? DoctorReceiveTime { get; set; }

        /// <summary>
        /// 是否处理
        /// </summary>
        public bool IsHandle { get; set; }

        /// <summary>
        /// 样本号
        /// </summary>
        public string SampleNo { get; set; }

        /// <summary>
        /// 危急值项目及数值
        /// </summary>
        [StringLength(4000)]
        public string CrisisDetails { get; set; }
    }
}
