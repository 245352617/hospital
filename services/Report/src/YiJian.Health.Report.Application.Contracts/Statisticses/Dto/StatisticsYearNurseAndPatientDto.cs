using Microsoft.EntityFrameworkCore;

namespace YiJian.Health.Report.Statisticses.Dto
{

    /// <summary>
    /// 急诊科护患年度视图
    /// </summary>
    public class StatisticsYearNurseAndPatientDto : StatisticsNurseAndPatientDto
    {
        /// <summary>
        /// 格式化 yyyy-MM
        /// </summary>
        public string FormatDate
        {
            get { return $"{Year}"; }
        }
    }

}
