using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace YiJian.Health.Report.Statisticses.Entities
{
    /// <summary>
    /// 患者查询报表对象
    /// </summary>
    public class AdmissionRecord
    {
        /// <summary>
        /// 序号
        /// </summary>
        [NotMapped]
        public int Row { get; set; }

        /// <summary>
        /// 患者Gid
        /// </summary>
        [Key]
        public Guid PI_ID { get; set; }

        /// <summary>
        /// 就诊号
        /// </summary>
        public string VisitNo { get; set; }

        /// <summary>
        /// 患者Id
        /// </summary>
        public string PatientId { get; set; }

        /// <summary>
        /// 患者姓名
        /// </summary>
        public string PatientName { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public string SexName { get; set; }

        /// <summary>
        /// 年龄
        /// </summary>
        public string Age { get; set; }

        /// <summary>
        /// 挂号时间
        /// </summary>
        public DateTime? RegisterTime { get; set; }

        /// <summary>
        /// 接诊科室
        /// </summary>
        public string DeptName { get; set; }

        /// <summary>
        /// 接诊医生
        /// </summary>
        public string FirstDoctorName { get; set; }

        /// <summary>
        /// 诊断
        /// </summary>
        public string Diagnoses { get; set; }

        /// <summary>
        /// 病历
        /// </summary>
        public string EmrTitles { get; set; }

        /// <summary>
        /// 医嘱
        /// </summary>
        public string DoctorsAdvice { get; set; }

        /// <summary>
        /// 特殊病人
        /// </summary>
        public string SpecialName { get; set; }
    }
}
