using Volo.Abp.Application.Dtos;

namespace YiJian.Health.Report.Statisticses.Dto
{
    /// <summary>
    /// 急诊科医患视图
    /// </summary>
    public class StatisticsDoctorAndPatientDto : EntityDto<int>
    {
        /// <summary>
        /// 在岗医师总数
        /// </summary>
        public int DoctorTotal { get; set; }

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
                return 100 * (decimal)DoctorTotal / (decimal)ReceptionTotal;
            }
        }

        /// <summary>
        /// 格式化的医患比
        /// </summary>
        public string FormatRatio { get { return $"{((decimal)DoctorTotal / (decimal)ReceptionTotal).ToString("P3")}"; } }

        /// <summary>
        /// 年
        /// </summary>
        public int Year { get; set; }

    }


}
