namespace YiJian.Health.Report.Statisticses.Dto
{
    /// <summary>
    /// 急诊科医患年度视图
    /// </summary>
    public class StatisticsYearDoctorAndPatientDto : StatisticsDoctorAndPatientDto
    {
        /// <summary>
        /// 月份格式化 yyyy-MM
        /// </summary>
        public string FormatDate
        {
            get { return $"{Year}"; }
        }
    }



}
