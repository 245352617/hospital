using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace YiJian.Health.Report.Enums
{
    /// <summary>
    /// 报表类型  0=急诊科医患比, 1=急诊科护患比, 2=急诊科各级患者比例, 3=抢救室滞留时间中位数, 4=急诊抢救室患者死亡率
    /// </summary>
    public enum EReportType : int
    {
        /// <summary>
        /// 急诊科医患比
        /// </summary>
        [Description("急诊科医患比")]
        DoctorAndPatient = 0,

        /// <summary>
        /// 急诊科护患比
        /// </summary>
        [Description("急诊科护患比")]
        NurseAndPatient = 1,

        /// <summary>
        /// 急诊科各级患者比例
        /// </summary>
        [Description("急诊科各级患者比例")]
        LevelAndPatient = 2,

        /// <summary>
        /// 抢救室滞留时间中位数
        /// </summary>
        [Description("抢救室滞留时间中位数")]
        EmergencyroomAndPatient = 3,

        /// <summary>
        /// 急诊抢救室患者死亡率
        /// </summary>
        [Description("急诊抢救室患者死亡率")]
        EmergencyroomAndDeathPatient = 4, 

    }
}
