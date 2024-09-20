using Volo.Abp.Application.Dtos;

namespace YiJian.Health.Report.Statisticses.Dto
{
    /// <summary>
    /// 急诊抢救室患者死亡率视图
    /// </summary> 
    public class StatisticsEmergencyroomAndDeathPatient : EntityDto<int>
    {
        /// <summary>
        /// 抢救总数
        /// </summary> 
        public int RescueTotal { get; set; }

        /// <summary>
        /// 死亡总数
        /// </summary> 
        public int DeathToll { get; set; }

        /// <summary>
        /// 死亡率
        /// </summary> 
        public decimal DeathRate
        {
            get { return 100 * ((decimal)DeathToll / (decimal)RescueTotal); }
        }

        /// <summary>
        /// 格式化的死亡率
        /// </summary>
        public string FormatDeathRate
        {
            get
            {
                return (((decimal)DeathToll / (decimal)RescueTotal)).ToString("P3");
            }
        }

        /// <summary>
        /// 年份
        /// </summary> 
        public int Year { get; set; }
    }

}
