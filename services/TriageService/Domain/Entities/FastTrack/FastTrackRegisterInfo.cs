using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SamJan.MicroService.PreHospital.Core.BaseEntities;

namespace SamJan.MicroService.PreHospital.TriageService
{
    /// <summary>
    /// 快速通道登记信息表
    /// </summary>
    public class FastTrackRegisterInfo : BaseEntity<Guid>
    {
        public FastTrackRegisterInfo SetId(Guid id)
        {
            Id = id;
            return this;
        }

        /// <summary>
        /// 患者姓名
        /// </summary>
        [Description("患者姓名")]
        [StringLength(50)]
        public string PatientName { get; set; }

        /// <summary>
        /// 性别
        /// </summary>T
        [Description("性别")]
        [StringLength(20)]
        public string Sex { get; set; }

        /// <summary>
        /// 患者年龄
        /// </summary>
        [Description("患者年龄")]
        [StringLength(20)]
        public string Age { get; set; }

        /// <summary>
        /// 所属派出所Id
        /// </summary>
        [Description("所属派出所Id")]
        public Guid PoliceStationId { get; set; }
        
        /// <summary>
        /// 所属派出所电话号码
        /// </summary>
        [Description("所属派出所电话号码")]
        [StringLength(20)]
        public string PoliceStationPhone { get; set; }

        /// <summary>
        /// 所处派出所名称
        /// </summary>
        [Description("所处派出所名称")]
        [StringLength(50)]
        public string PoliceStationName { get; set; }

        /// <summary>
        /// 警务人员警号
        /// </summary>
        [Description("警务人员警号")]
        [StringLength(20)]
        public string PoliceCode { get; set; }

        /// <summary>
        /// 警务人员姓名
        /// </summary>
        [Description("警务人员姓名")]
        [StringLength(20)]
        public string PoliceName { get; set; }

        /// <summary>
        /// 接诊护士
        /// </summary>
        [Description("接诊护士")]
        [StringLength(20)]
        public string ReceptionNurse { get; set; }
        /// <summary>
        /// 接诊护士名称
        /// </summary>
        [Description("接诊护士名称")]
        [StringLength(20)]
        public string ReceptionNurseName { get; set; }
    }
}