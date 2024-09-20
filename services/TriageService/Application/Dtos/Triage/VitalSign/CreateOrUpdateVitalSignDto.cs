using System;
using System.ComponentModel.DataAnnotations;

namespace SamJan.MicroService.PreHospital.TriageService
{
    public class CreateOrUpdateVitalSignDto
    {
        /// <summary>
        /// VitalSignInfo表Id（注：修改时传入）
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// 收缩压
        /// </summary>
        public string Sbp { get; set; }

        /// <summary>
        /// 舒张压
        /// </summary>
        public string Sdp { get; set; }

        /// <summary>
        /// 血氧饱和度
        /// </summary>
        public string SpO2 { get; set; }

        /// <summary>
        /// 呼吸
        /// </summary>
        public string BreathRate { get; set; }

        /// <summary>
        /// 体温
        /// </summary>
        public string Temp { get; set; }

        /// <summary>
        /// 心率
        /// </summary>
        public string HeartRate { get; set; }

        /// <summary>
        /// 血糖（单位 mmol/L）
        /// </summary>
        public float? BloodGlucose { get; set; }

        /// <summary>
        /// 体重
        /// </summary>
        public string Weight { get; set; }

        /// <summary>
        /// 备注Code
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 备注名称
        /// </summary>
        public string RemarkName { get; set; }

        /// <summary>
        /// 设备编码
        /// </summary>
        public string DeviceCode { get; set; }

        /// <summary>
        /// 心电图 Code
        /// </summary>
        public string CardiogramCode { get; set; }

        /// <summary>
        /// 心电图 名称
        /// </summary>
        public string CardiogramName { get; set; }

        /// <summary>
        /// 意识Code
        /// </summary>
        [MaxLength(100,ErrorMessage = "意识Code的最大长度为{1}")]
        public string ConsciousnessCode { get; set; }

        /// <summary>
        /// 意识名称
        /// </summary>
        [MaxLength(100,ErrorMessage = "意识名称的最大长度为{1}")]
        public string ConsciousnessName { get; set; }
    }
}