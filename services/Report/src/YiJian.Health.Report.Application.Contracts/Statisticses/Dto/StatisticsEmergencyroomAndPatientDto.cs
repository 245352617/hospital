using Volo.Abp.Application.Dtos;

namespace YiJian.Health.Report.Statisticses.Dto
{
    /// <summary>
    /// 抢救室滞留时间中位数视图
    /// </summary> 
    public class StatisticsEmergencyroomAndPatientDto : EntityDto<int>
    {
        /// <summary>
        /// 抢救总数
        /// </summary> 
        public int RescueTotal { get; set; }

        /// <summary>
        /// 平均滞留时间
        /// </summary> 
        public int AvgDetentionTime { get; set; }

        /// <summary>
        /// 滞留时间中位数
        /// </summary> 
        public int MidDetentionTime { get; set; }

        /// <summary>
        /// 年份
        /// </summary> 
        public int Year { get; set; }

    }

}
