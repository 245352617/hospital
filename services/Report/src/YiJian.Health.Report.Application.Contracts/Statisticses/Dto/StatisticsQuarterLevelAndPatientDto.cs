namespace YiJian.Health.Report.Statisticses.Dto
{
    /// <summary>
    /// 急诊科各级患者比例季度视图
    /// </summary>
    public class StatisticsQuarterLevelAndPatientDto : StatisticsLevelAndPatient
    {
        /// <summary>
        /// 季度
        /// </summary> 
        public int Quarter { get; set; }

        /// <summary>
        /// 季度格式化 yyyy年MM季
        /// </summary>
        public string FormatDate { get { return $"{Year}-Q{Quarter}"; } }
    }

}
