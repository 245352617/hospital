namespace YiJian.Health.Report.Statisticses.Dto
{
    /// <summary>
    /// 急诊抢救室患者死亡率视图
    /// </summary> 
    public class StatisticsEmergencyroomAndDeathPatientResponseDto
    {
        /// <summary>
        /// 月份格式化 yyyy-MM
        /// </summary>
        public string FormatDate { get; set; }

        /// <summary>
        /// 抢救总数
        /// </summary> 
        public int RescueTotal { get; set; }

        /// <summary>
        /// 死亡总数
        /// </summary> 
        public int DeathToll { get; set; }

        /// <summary>
        /// 死亡率
        /// </summary> 
        public decimal DeathRate { get; set; }

        /// <summary>
        /// 格式化的死亡率
        /// </summary>
        public string FormatDeathRate { get; set; }

    }

}
