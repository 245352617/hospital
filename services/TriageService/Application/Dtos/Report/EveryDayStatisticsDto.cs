namespace SamJan.MicroService.PreHospital.TriageService
{
    public class EveryDayStatisticsDto
    {
        /// <summary>
        /// 分诊科室编码
        /// </summary>
        public string TriageDeptCode { get; set; }
        /// <summary>
        /// 分诊科室
        /// </summary>
        public string TriageDeptName { get; set; }

        /// <summary>
        /// 分诊日期
        /// </summary>
        public string Date { get; set; }

        /// <summary>
        /// 分诊人数
        /// </summary>
        public int PersonCount { get; set; }
    }
}