namespace YiJian.Health.Report.Statisticses.Dto
{
    /// <summary>
    ///  急诊科护患月度视图
    /// </summary>
    public class StatisticsMonthNurseAndPatientDto : StatisticsNurseAndPatientDto
    {
        /// <summary>
        /// 月份
        /// </summary>
        public int Month { get; set; }

        /// <summary>
        /// 月份格式化 yyyy-MM
        /// </summary>
        public string FormatDate { get { return $"{Year.ToString("0000")}-{Month.ToString("00")}"; } }

    }

}
