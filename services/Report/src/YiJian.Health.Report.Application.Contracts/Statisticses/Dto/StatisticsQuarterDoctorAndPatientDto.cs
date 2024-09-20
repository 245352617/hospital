using System;

namespace YiJian.Health.Report.Statisticses.Dto
{
    /// <summary>
    /// 急诊科医患季度
    /// </summary>
    public class StatisticsQuarterDoctorAndPatientDto : StatisticsDoctorAndPatientDto
    {
        /// <summary>
        /// 季度，方便查询用的字段
        /// eg:
        /// <![CDATA[
        /// 2022-01-01 表示第一季度 （1,2,3 月）
        /// 2022-04-01 表示第二季度 （4,5,6 月）
        /// 2022-07-01 表示第三季度 （7,8,9 月）
        /// 2022-10-01 表示第四季度 （10,11,12 月）
        /// ]]> 
        /// </summary> 
        public DateTime YearQuarter { get; set; }

        /// <summary>
        /// 季度
        /// </summary> 
        public int Quarter { get; set; }

        /// <summary>
        /// 月份格式化 yyyy-MM
        /// </summary>
        public string FormatDate
        {
            get { return $"{Year}-Q{Quarter}"; }
        }
    }



}
