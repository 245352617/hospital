using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;
using YiJian.EMR.Enums;

namespace YiJian.EMR.Writes.Entities
{
    /// <summary>
    /// 患者电子病历/文书...
    /// </summary>
    [Comment("患者电子病历/文书...")]
    public class PatientEmr : FullAuditedAggregateRoot<Guid>
    {
        /// <summary>
        /// 患者电子病历
        /// </summary>
        private PatientEmr()
        {
        }

        /// <summary>
        /// 患者电子病历
        /// </summary> 
        public PatientEmr(
            Guid id,
            Guid PiId,
            [NotNull] string patientNo,
            [NotNull] string patientName,
            [NotNull] string doctorCode,
            [NotNull] string doctorName,
            [NotNull] string deptCode,
            [NotNull] string deptName,
            [NotNull] string emrTitle,
            string categoryLv1,
            string categoryLv2,
            EClassify classify,
            Guid originalId,
            Guid? originId,
            DateTime? admissionTime,
            DateTime? dischargeTime,
            string code,
            string subTitle
        )
        {
            Id = id;
            PI_ID = PiId;
            PatientNo = Check.NotNullOrWhiteSpace(patientNo, nameof(patientNo), maxLength: 32);
            PatientName = Check.NotNullOrWhiteSpace(patientName, nameof(patientName), maxLength: 50);
            DoctorCode = Check.NotNullOrWhiteSpace(doctorCode, nameof(doctorCode), maxLength: 32);
            DoctorName = Check.NotNullOrWhiteSpace(doctorName, nameof(doctorName), maxLength: 50);
            DeptCode = Check.NotNullOrWhiteSpace(deptCode, nameof(deptCode), maxLength: 32);
            DeptName = Check.NotNullOrWhiteSpace(deptName, nameof(deptName), maxLength: 50);
            EmrTitle = Check.NotNullOrWhiteSpace(emrTitle, nameof(emrTitle), maxLength: 200);
            CategoryLv1 = categoryLv1;
            CategoryLv2 = categoryLv2;
            Classify = classify;
            OriginalId = originalId;
            OriginId = originId;
            AdmissionTime = admissionTime;
            DischargeTime = dischargeTime;
            IsArchived = false;
            Code = code;
            SubTitle = subTitle;
        }

        /// <summary>
        /// 患者唯一Id
        /// </summary>
        public Guid PI_ID { get; set; }

        /// <summary>
        /// 患者编号
        /// </summary>
        [Comment("患者编号")]
        [Required(ErrorMessage = "患者编号不能为空"), StringLength(32, ErrorMessage = "患者编号最大长度32字符")]
        public string PatientNo { get; set; }

        /// <summary>
        /// 患者名称
        /// </summary>
        [Comment("患者名称")]
        [Required(ErrorMessage = "患者名称不能为空"), StringLength(50, ErrorMessage = "患者名称最大长度50字符")]
        public string PatientName { get; set; }

        /// <summary>
        /// 医生编号
        /// </summary>
        [Comment("医生编号")]
        [Required(ErrorMessage = "医生编号不能为空"), StringLength(32, ErrorMessage = "医生编号最大长度32字符")]
        public string DoctorCode { get; set; }

        /// <summary>
        /// 医生名称
        /// </summary>
        [Comment("医生名称")]
        [Required(ErrorMessage = "医生名称不能为空"), StringLength(50, ErrorMessage = "医生名称最大长度50字符")]
        public string DoctorName { get; set; }


        [Required(ErrorMessage = "科室编号不能为空"), StringLength(32, ErrorMessage = "科室编号最大长度32字符")]
        public string DeptCode { get; set; }

        [Required(ErrorMessage = "科室名称不能为空"), StringLength(100, ErrorMessage = "科室名称最大长度100字符")]
        public string DeptName { get; set; }

        /// <summary>
        /// 病历名称
        /// </summary>
        [Comment("病历名称")]
        [Required(ErrorMessage = "病历名称不能为空"), StringLength(200, ErrorMessage = "病历名称最大长度200字符")]
        public string EmrTitle { get; set; }

        /// <summary>
        /// 一级分类
        /// </summary>
        [Comment("一级分类")]
        [StringLength(32, ErrorMessage = "一级分类最大长度32字符")]
        public string CategoryLv1 { get; set; }

        /// <summary>
        /// 二级分类
        /// </summary>
        [Comment("二级分类")]
        [StringLength(32, ErrorMessage = "二级分类最大长度32字符")]
        public string CategoryLv2 { get; set; }


        /// <summary>
        /// 入院时间
        /// </summary>
        [Comment("入院时间")]
        public DateTime? AdmissionTime { get; set; }

        /// <summary>
        /// 出院时间
        /// </summary>
        [Comment("出院时间")]
        public DateTime? DischargeTime { get; set; }

        /// <summary>
        /// 电子文书分类（0=电子病历，1=文书）
        /// </summary>
        [Comment("电子文书分类（0=电子病历，1=文书）")]
        public EClassify Classify { get; set; } = EClassify.EMR;

        /// <summary>
        /// 是否已归档，默认false=未归档
        /// </summary>
        [Comment("是否已归档，默认false=未归档")]
        public bool IsArchived { get; set; } = false;

        /// <summary>
        /// 原电子病历模板Id（上一级）
        /// </summary>
        [Comment("原电子病历模板Id（上一级）")]
        public Guid OriginalId { get; set; }

        /// <summary>
        /// 最初引入病历库的Id
        /// </summary>
        [Comment("最初引入病历库的Id")]
        public Guid? OriginId { get; set; }

        /// <summary>
        /// 电子病历模板目录名称编码
        /// </summary>  
        [Comment("电子病历模板目录名称编码")]
        public string Code { get; set; }

        /// <summary>
        /// 标签
        /// </summary>
        [StringLength(100)]
        public string SubTitle { get; set; }

        /// <summary>
        /// 患者电子病历
        /// </summary> 
        public void Update(
            [NotNull] string patientNo,
            [NotNull] string patientName,
            [NotNull] string doctorCode,
            [NotNull] string doctorName,
            [NotNull] string deptCode,
            [NotNull] string deptName,
            [NotNull] string emrTitle,
            string categoryLv1,
            string categoryLv2
        )
        {
            PatientNo = Check.NotNullOrWhiteSpace(patientNo, nameof(patientNo), maxLength: 32);
            PatientName = Check.NotNullOrWhiteSpace(patientName, nameof(patientName), maxLength: 50);
            DoctorCode = Check.NotNullOrWhiteSpace(doctorCode, nameof(doctorCode), maxLength: 32);
            DoctorName = Check.NotNullOrWhiteSpace(doctorName, nameof(doctorName), maxLength: 50);
            DeptCode = Check.NotNullOrWhiteSpace(deptCode, nameof(deptCode), maxLength: 32);
            DeptName = Check.NotNullOrWhiteSpace(deptName, nameof(deptName), maxLength: 50);
            EmrTitle = Check.NotNullOrWhiteSpace(emrTitle, nameof(emrTitle), maxLength: 200);
            CategoryLv1 = categoryLv1;
            CategoryLv2 = categoryLv2;
        }

        /// <summary>
        /// 归档
        /// </summary>
        public void Archive(bool archive = true)
        {
            IsArchived = archive;
        }

    }
}
