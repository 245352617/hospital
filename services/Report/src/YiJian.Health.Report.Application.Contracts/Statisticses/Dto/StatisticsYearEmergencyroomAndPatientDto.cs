using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Domain.Entities;

namespace YiJian.Health.Report.Statisticses.Dto
{

    /// <summary>
    /// 抢救室滞留时间中位数年度视图
    /// </summary>
    public class StatisticsYearEmergencyroomAndPatientDto : StatisticsEmergencyroomAndPatientDto
    {

        /// <summary>
        /// 年度格式化 yyyy
        /// </summary>
        public string FormatDate { get { return $"{Year}"; } }
    }

}
