using System;
using System.Collections.Generic;
using System.Text;

namespace YiJian.BodyParts.Dtos
{
    /// <summary>
    /// 患者费用信息
    /// </summary>
    public class PatientChargeDto
    {
        /// <summary>
        /// 患者id
        /// </summary>
        public string PI_ID { get; set; }

        /// <summary>
        /// 体温
        /// </summary>
        /// <example></example>
        public string Temp { get; set; }

        /// <summary>
        /// 心率
        /// </summary>
        /// <example></example>
        public string HeartRate { get; set; }

        /// <summary>
        /// 血压
        /// </summary>
        /// <example></example>
        public string BloodPressure { get; set; }

        /// <summary>
        /// RR
        /// </summary>
        /// <example></example>
        public string Breathing { get; set; }

        /// <summary>
        /// SPO2
        /// </summary>
        /// <example></example>
        public string Spo2 { get; set; }

        /// <summary>
        /// 待执行医嘱列表
        /// </summary>
        public List<Orders> Orders { get; set; }

        /// <summary>
        /// 总费用
        /// </summary>
        /// <example></example>
        public string Consume { get; set; }
    }

    /// <summary>
    /// 待执行医嘱
    /// </summary>
    public class Orders
    {
        /// <summary>
        /// 医嘱名称
        /// </summary>
        /// <example></example>
        public string OrderText { get; set; }

        /// <summary>
        /// 频次
        /// </summary>
        /// <example></example>
        public string Frequency { get; set; }

        /// <summary>
        /// 预计执行时间
        /// </summary>
        /// <example></example>
        public DateTime? PlanExcuteTime { get; set; }
    }
}
