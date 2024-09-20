using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SamJan.MicroService.PreHospital.Core.BaseEntities;

namespace SamJan.MicroService.PreHospital.TriageService
{
    /// <summary>
    /// 患者挂号记录表 IsDelete 用作挂号状态 0：已挂号 1：已退号
    /// </summary>
    public class RegisterInfo : BaseEntity<Guid>
    {
        public RegisterInfo SetId(Guid id)
        {
            Id = id;
            return this;
        }

        public RegisterInfo()
        {

        }

        public RegisterInfo(Guid id, Guid patientInfoId)
        {
            this.Id = id;
            this.PatientInfoId = patientInfoId;
        }

        /// <summary>
        /// 挂号流水号
        /// </summary>
        [Description("挂号流水号")]
        [StringLength(50)]
        public string RegisterNo { get; set; }

        /// <summary>
        /// 挂号科室编码
        /// </summary>
        [Description("挂号科室编码")]
        [StringLength(50)]
        public string RegisterDeptCode { get; set; }

        /// <summary>
        /// 挂号医生编码
        /// </summary>
        [Description("挂号医生编码")]
        [StringLength(50)]
        public string RegisterDoctorCode { get; set; }

        /// <summary>
        /// 挂号医生姓名
        /// </summary>
        [Description("挂号医生姓名")]
        [StringLength(50)]
        public string RegisterDoctorName { get; set; }


        /// <summary>
        /// 挂号时间
        /// </summary>
        [Description("挂号时间")]
        public DateTime RegisterTime { get; set; }

        /// <summary>
        /// 分诊患者基本信息表Id
        /// </summary>
        [Description("分诊患者基本信息表Id")]
        public Guid PatientInfoId { get; set; }

        /// <summary>
        /// 就诊次数
        /// </summary>
        [Description("就诊次数")]
        [StringLength(20)]
        public string VisitNo { get; set; }

        /// <summary>
        /// 是否取消挂号
        /// </summary>
        [Description("是否已取消挂号")]
        public bool IsCancelled { get; set; }

        /// <summary>
        /// 取消挂号时间
        /// </summary>
        [Description("取消挂号时间")]
        public DateTime? CancellationTime { get; set; }
    }
}