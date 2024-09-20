using System;

namespace YiJian.Health.Report.Statisticses.Dto
{
    /// <summary>
    /// 急诊科各级患者比例月度视图
    /// </summary>
    public class StatisticsMonthLevelAndPatientDto : StatisticsLevelAndPatient
    {
        /// <summary>
        /// 月份，方便查询用的字段
        /// <![CDATA[
        ///  2023-10-01 表示 2023年10月份的记录
        /// ]]>
        /// </summary> 
        public DateTime YearMonth { get; set; }


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
