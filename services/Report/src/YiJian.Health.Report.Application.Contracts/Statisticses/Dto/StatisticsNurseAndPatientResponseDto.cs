namespace YiJian.Health.Report.Statisticses.Dto
{
    /// <summary>
    /// 急诊科护患比视图
    /// </summary>
    public class StatisticsNurseAndPatientResponseDto
    {

        /// <summary>
        /// 日期格式化
        /// </summary>
        public string FormatDate { get; set; }

        /// <summary>
        /// 在岗护士总数
        /// </summary> 
        public int NurseTotal { get; set; }

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
