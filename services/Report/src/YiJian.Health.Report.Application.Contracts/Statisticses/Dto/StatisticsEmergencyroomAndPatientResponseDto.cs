namespace YiJian.Health.Report.Statisticses.Dto
{
    /// <summary>
    /// 抢救室滞留时间中位数比视图
    /// </summary>
    public class StatisticsEmergencyroomAndPatientResponseDto
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
        /// 平均滞留时间
        /// </summary> 
        public int AvgDetentionTime { get; set; }

        /// <summary>
        /// 滞留时间中位数
        /// </summary> 
        public int MidDetentionTime { get; set; }

    }

}
