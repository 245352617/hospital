namespace YiJian.ECIS.ShareModel.Etos.Reports
{

    /// <summary>
    /// 希望采集的季度参数
    /// </summary>
    public class QuarterReportEto
    {
        /// <summary>
        /// 希望采集的季度参数
        /// </summary>
        /// <param name="year"></param>
        /// <param name="quarter"></param>
        public QuarterReportEto(int year, int quarter)
        {
            Year = year;
            Quarter = quarter;
        }

        /// <summary>
        /// 年份
        /// </summary>
        public int Year { get; set; }

        /// <summary>
        /// 季度
        /// </summary>
        public int Quarter { get; set; }
    }


}
