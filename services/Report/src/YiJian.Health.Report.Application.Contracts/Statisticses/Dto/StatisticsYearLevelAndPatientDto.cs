using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Entities;

namespace YiJian.Health.Report.Statisticses.Dto
{

    /// <summary>
    /// 急诊科各级患者比例年度视图
    /// </summary>
    public class StatisticsYearLevelAndPatientDto : StatisticsLevelAndPatient
    {
        /// <summary>
        /// 年度格式化 yyyy
        /// </summary>
        public string FormatDate { get { return $"{Year}"; } }
    }

}
