using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Domain.Entities;

namespace YiJian.Health.Report.Statisticses.Dto
{
    /// <summary>
    /// 年统计数据Dto
    /// </summary>
    public class StatisticsYearEmergencyroomAndDeathPatientDto : StatisticsEmergencyroomAndDeathPatient
    {

        /// <summary>
        /// 年度格式化 yyyy
        /// </summary>
        public string FormatDate { get { return $"{Year}"; } }
    }

}
