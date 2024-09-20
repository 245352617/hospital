using Volo.Abp.Application.Dtos;

namespace YiJian.Health.Report.Statisticses.Dto
{
    /// <summary>
    /// 急诊科护患比视图
    /// </summary> 
    public class StatisticsNurseAndPatientDto : EntityDto<int>
    {
        /// <summary>
        /// 在岗护士总数
        /// </summary> 
        public int NurseTotal { get; set; }

        /// <summary>
        /// 接诊总数
        /// </summary> 
        public int ReceptionTotal { get; set; }

        /// <summary>
        /// 医患比
        /// </summary>
        public decimal Ratio
        {
            get
            {
                return 100 * (decimal)NurseTotal / (decimal)ReceptionTotal;
            }
        }

        /// <summary>
        /// 格式化的医患比
        /// </summary>
        public string FormatRatio { get { return ((decimal)NurseTotal / (decimal)ReceptionTotal).ToString("P3"); } }

        /// <summary>
        /// 年份
        /// </summary> 
        public int Year { get; set; }

    }

}
