namespace SamJan.MicroService.PreHospital.TriageService
{
    /// <summary>
    /// Iot设备数据分类
    /// </summary>
    public static class IotCategory
    {
        /// <summary>
        /// 心率(Heart Rate
        /// </summary>
        public static string HeartRate { get; set; } = "HR";

        /// <summary>
        /// 呼吸频率(Respiration Rate)
        /// </summary>
        public static string BreathRate { get; set; } = "RR";

        /// <summary>
        /// 血氧饱和度(Pulse Oximetry)
        /// </summary>
        public static string SpO2 { get; set; } = "SpO2";

        /// <summary>
        /// 脉率(Pulse Rate
        /// </summary>
        public static string PR { get; set; } = "PR";

        /// <summary>
        /// 无创收缩压(Systolic  Pressure of NIBP )
        /// </summary>
        public static string Sbp { get; set; } = "NIBP_SYS";

        /// <summary>
        /// 无创舒张压(Diastolic Pressure of NIBP )
        /// </summary>
        public static string Sdp { get; set; } = "NIBP_DIA";

        /// <summary>
        /// 无创平均压(Mean Pressure of NIBP )
        /// </summary>
        public static string NIBP_MAP { get; set; } = "NIBP_MAP";

        /// <summary>
        /// 无创脉率(Pulse Rate of NIBP)
        /// </summary>
        public static string NIBP_PR { get; set; } = "NIBP_PR";

        /// <summary>
        /// 有创收缩压(Systolic Pressure of IBP )
        /// </summary>
        public static string P1_SYS { get; set; } = "P1_SYS ";

        /// <summary>
        /// 有创舒张压(Diastolic Pressure of IBP)
        /// </summary>
        public static string P1_DIA { get; set; } = "P1_DIA";

        /// <summary>
        /// 有创舒张压(Diastolic Pressure of IBP)
        /// </summary>
        public static string P1_MAP { get; set; } = "P1_MAP";

        /// <summary>
        /// 中心静脉压
        /// </summary>
        public static string CVP { get; set; } = "CVP";

        /// <summary>
        /// 有创舒张压(Diastolic Pressure of IBP)
        /// </summary>
        public static string BIS { get; set; } = "BIS";

        /// <summary>
        /// 有创舒张压(Diastolic Pressure of IBP)
        /// </summary>
        public static string Temp { get; set; } = "Temp";
    }
}