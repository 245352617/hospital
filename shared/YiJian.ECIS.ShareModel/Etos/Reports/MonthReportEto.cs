namespace YiJian.ECIS.ShareModel.Etos.Reports
{
    /// <summary>
    /// 希望采集的月份参数
    /// </summary>
    public class MonthReportEto
    {
        /// <summary>
        /// 希望采集的月份参数
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        public MonthReportEto(int year, int month)
        {
            Year = year;
            Month = month;
        }

        /// <summary>
        /// 年份
        /// </summary>
        public int Year { get; set; }

        /// <summary>
        /// 月份
        /// </summary>
        public int Month { get; set; }
    }


}
