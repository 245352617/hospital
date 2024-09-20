namespace Szyjian.Ecis.Patient.Application.Contracts
{
    /// <summary>
    /// 推送患者分诊生命体征信息队列Dto
    /// </summary>
    public class VitalSignInfoMqDto
    {
        // /// <summary>
        // /// VitalSignInfo表主键Id  不需要前端传值
        // /// </summary>
        // public Guid Id { get; set; }

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
        public string BloodGlucose { get; set; }

        /// <summary>
        /// 体重
        /// </summary>
        public string Weight { get; set; }
        /// <summary>
        /// 脉搏
        /// </summary>
        public string Pulse { get; set; }
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
        public string ConsciousnessCode { get; set; }

        /// <summary>
        /// 意识名称
        /// </summary>
        public string ConsciousnessName { get; set; }
    }
}