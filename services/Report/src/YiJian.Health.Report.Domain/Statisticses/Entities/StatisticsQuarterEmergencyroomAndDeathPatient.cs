using Microsoft.EntityFrameworkCore;
using System;

namespace YiJian.Health.Report.Statisticses.Entities
{
    /// <summary>
    /// 急诊抢救室患者死亡率季度视图
    /// </summary>
    [Comment("急诊抢救室患者死亡率季度视图")]
    public class StatisticsQuarterEmergencyroomAndDeathPatient : StatisticsEmergencyroomAndDeathPatient
    {

        /// <summary>
        /// 季度，方便查询用的字段
        /// eg:
        /// <![CDATA[
        /// 2022-01-01 表示第一季度 （1,2,3 月）
        /// 2022-04-01 表示第二季度 （4,5,6 月）
        /// 2022-07-01 表示第三季度 （7,8,9 月）
        /// 2022-10-01 表示第四季度 （10,11,12 月）
        /// ]]> 
        /// </summary>
        [Comment("季度，方便查询用的字段")]
        public DateTime YearQuarter { get; set; }

        /// <summary>
        /// 年份
        /// </summary>
        [Comment("年份")]
        public int Year { get; set; }

        /// <summary>
        /// 季度
        /// </summary>
        [Comment("季度")]
        public int Quarter { get; set; }

    }

}
