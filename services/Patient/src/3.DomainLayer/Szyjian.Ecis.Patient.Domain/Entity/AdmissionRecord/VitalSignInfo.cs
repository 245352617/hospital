using FreeSql.DataAnnotations;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Domain.Entities;

namespace Szyjian.Ecis.Patient
{
    /// <summary>
    /// 生命体征
    /// </summary>
    [Table(Name = "Pat_VitalSignInfo")]
    public class VitalSignInfo : Entity<Guid>
    {
        /// <summary>
        /// 设置主键
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public VitalSignInfo SetId(Guid id)
        {
            Id = id;
            return this;
        }
        /// <summary>
        /// 院前分诊患者建档表主键Id
        /// </summary>
        [Description("院前分诊患者建档表主键Id")]
        public Guid PI_ID { get; set; }

        /// <summary>
        /// 收缩压
        /// </summary>
        [Description("收缩压")]
        [StringLength(20)]
        public string Sbp { get; set; }

        /// <summary>
        /// 舒张压
        /// </summary>
        [Description("舒张压")]
        [StringLength(20)]
        public string Sdp { get; set; }

        /// <summary>
        /// 血氧饱和度
        /// </summary>
        [Description("血氧饱和度")]
        [StringLength(20)]
        public string SpO2 { get; set; }

        /// <summary>
        /// 呼吸
        /// </summary>
        [Description("呼吸")]
        [StringLength(20)]
        public string BreathRate { get; set; }

        /// <summary>
        /// 体温
        /// </summary>
        [Description("体温")]
        [StringLength(20)]
        public string Temp { get; set; }

        /// <summary>
        /// 心率
        /// </summary>
        [Description("心率")]
        [StringLength(20)]
        public string HeartRate { get; set; }

        /// <summary>
        /// 血糖
        /// </summary>
        [Description("血糖")]
        [StringLength(20)]
        public string BloodGlucose { get; set; }

        /// <summary>
        /// 设备编码
        /// </summary>
        [Description("设备编码")]
        [StringLength(100)]
        public string DeviceCode { get; set; }
    }
}