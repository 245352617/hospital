namespace YiJian.Health.Report.Statisticses.Dto
{
    /// <summary>
    /// 急诊科医患视图
    /// </summary>
    public class StatisticsDoctorAndPatientResponseDto
    {

        /// <summary>
        /// 日期格式化
        /// </summary>
        public string FormatDate { get; set; }

        /// <summary>
        /// 在岗医师总数
        /// </summary>
        public int DoctorTotal { get; set; }

        /// <summary>
        /// 接诊总数
        /// </summary>
        public int ReceptionTotal { get; set; }

        /// <summary>
        /// 格式化的医患比
        /// </summary>
        public string FormatRatio { get; set; }

    }

}
