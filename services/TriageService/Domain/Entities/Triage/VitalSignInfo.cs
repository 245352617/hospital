using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SamJan.MicroService.PreHospital.Core.BaseEntities;

namespace SamJan.MicroService.PreHospital.TriageService
{
    /// <summary>
    /// 院前分诊生命体征信息
    /// </summary>
    public class VitalSignInfo : BaseEntity<Guid>
    {
        public VitalSignInfo SetId(Guid id)
        {
            Id = id;
            return this;
        }
        
        /// <summary>
        /// 院前分诊患者建档表主键Id
        /// </summary>
        [Description("院前分诊患者建档表主键Id")]
        public Guid PatientInfoId { get; set; }

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
        /// 血糖（单位 mmol/L）
        /// </summary>
        [Description("血糖（单位 mmol/L）")]
        public float? BloodGlucose { get; set; }

        /// <summary>
        /// 生命体征备注名称
        /// </summary>
        [Description("生命体征备注名称")]
        [StringLength(100)]
        public string RemarkName { get; set; }

        /// <summary>
        /// 设备编码
        /// </summary>
        [Description("设备编码")]
        [StringLength(100)]
        public string DeviceCode { get; set; }

        /// <summary>
        /// 心电图 Code
        /// </summary>
        [Description("心电图 Code")]
        [StringLength(100)]
        public string CardiogramCode { get; set; }

        /// <summary>
        /// 心电图 名称
        /// </summary>
        [Description("心电图 名称")]
        [StringLength(100)]
        public string CardiogramName { get; set; }

        /// <summary>
        /// 意识Code
        /// </summary>
        [Description("意识Code")]
        [StringLength(100)]
        public string ConsciousnessCode { get; set; }

        /// <summary>
        /// 意识名称
        /// </summary>
        [Description("意识名称")]
        [StringLength(100)]
        public string ConsciousnessName { get; set; }

        /// <summary>
        /// 判断生命体征是否为Null或空
        /// </summary>
        /// <returns></returns>
        public bool CheckVitalSignIsNullOrEmpty()
        {
            return string.IsNullOrWhiteSpace(Sbp +
                                             Sdp +
                                             Temp +
                                             BreathRate +
                                             HeartRate +
                                             SpO2 +
                                             Remark +
                                             CardiogramCode +
                                             ConsciousnessCode +
                                             BloodGlucose);
        }
        
    }
}