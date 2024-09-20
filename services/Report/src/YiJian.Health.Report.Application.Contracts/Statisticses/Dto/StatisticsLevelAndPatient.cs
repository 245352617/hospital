using Volo.Abp.Application.Dtos;

namespace YiJian.Health.Report.Statisticses.Dto
{
    /// <summary>
    /// 急诊科各级患者比例视图
    /// </summary> 
    public class StatisticsLevelAndPatient : EntityDto<int>
    { 
        /// <summary>
        /// 年份
        /// </summary> 
        public int Year { get; set; }

        /// <summary>
        /// I级
        /// </summary> 
        public int LI { get; set; }

        /// <summary>
        /// II级
        /// </summary> 
        public int LII { get; set; }

        /// <summary>
        /// III级
        /// </summary> 
        public int LIII { get; set; }

        /// <summary>
        /// IVa级
        /// </summary> 
        public int LIVa { get; set; }

        /// <summary>
        /// IVb级
        /// </summary> 
        public int LIVb { get; set; }

        /// <summary>
        /// 总数
        /// </summary>
        public int Total
        {
            get
            {
                return LI + LII + LIII + LIVa + LIVb;
            }
        }
    }


}
