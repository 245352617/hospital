using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using YiJian.ECIS.ShareModel.Etos.Reports;
using YiJian.Health.Report.Enums;

namespace YiJian.Health.Report.Statisticses
{
    /// <summary>
    /// 报表数据采集作业服务
    /// </summary>
    public interface IJobAppService : IApplicationService
    {

        /// <summary>
        /// 统计接口
        /// </summary>
        /// <param name="reportType"></param>
        /// <param name="veidoo"></param>
        /// <param name="formatDate"></param>
        /// <returns></returns>
        public Task<int> StatisticsAsync(EReportType reportType, EVeidoo veidoo, string formatDate);

        /// <summary>
        /// (订阅)采集急诊科医患比数据（月度）
        /// </summary>
        /// <param name="eto"></param>
        /// <returns></returns> 
        public Task CollectMonthDoctorAndPatientAsync(MonthReportEto eto);

        /// <summary>
        /// (订阅)采集急诊科医患比数据（季度）
        /// </summary>
        /// <param name="eto"></param>
        /// <returns></returns> 
        public Task CollectQuarterDoctorAndPatientAsync(QuarterReportEto eto);

        /// <summary>
        /// (订阅)采集急诊科医患比数据（年度）
        /// </summary>
        /// <param name="eto"></param>
        /// <returns></returns> 
        public Task CollectYearDoctorAndPatientAsync(DateTime eto);

        /// <summary>
        /// (订阅)采集急诊科护患比数据（月度）
        /// </summary>
        /// <param name="eto"></param>
        /// <returns></returns> 
        public Task CollectMonthNurseAndPatientAsync(MonthReportEto eto);

        /// <summary>
        /// (订阅)采集急诊科护患比数据（季度）
        /// </summary>
        /// <param name="eto"></param>
        /// <returns></returns> 
        public Task CollectQuarterNurseAndPatientAsync(QuarterReportEto eto);

        /// <summary>
        /// (订阅)采集急诊科护患比数据（年度）
        /// </summary>
        /// <param name="eto"></param>
        /// <returns></returns> 
        public Task CollectYearNurseAndPatientAsync(DateTime eto);

        /// <summary>
        /// (订阅)采集急诊科各级患者比例数据（月度）
        /// </summary>
        /// <param name="eto"></param>
        /// <returns></returns> 
        public Task CollectMonthLevelAndPatientAsync(MonthReportEto eto);

        /// <summary>
        /// (订阅)采集急诊科各级患者比例数据（季度）
        /// </summary>
        /// <param name="eto"></param>
        /// <returns></returns> 
        public Task CollectQuarterLevelAndPatientAsync(QuarterReportEto eto);

        /// <summary>
        /// (订阅)采集急诊科各级患者比例数据（年度）
        /// </summary>
        /// <param name="eto"></param>
        /// <returns></returns> 
        public Task CollectYearLevelAndPatientAsync(DateTime eto);

        /// <summary>
        /// (订阅)采集抢救室滞留时间中位数数据（月度）
        /// </summary>
        /// <param name="eto"></param>
        /// <returns></returns> 
        public Task CollectMonthEmergencyroomAndPatientAsync(MonthReportEto eto);

        /// <summary>
        /// (订阅)采集抢救室滞留时间中位数数据（季度）
        /// </summary>
        /// <param name="eto"></param>
        /// <returns></returns> 
        public Task CollectQuarterEmergencyroomAndPatientAsync(QuarterReportEto eto);

        /// <summary>
        /// (订阅)采集抢救室滞留时间中位数数据（年度）
        /// </summary>
        /// <param name="eto"></param>
        /// <returns></returns> 
        public Task CollectYearEmergencyroomAndPatientAsync(DateTime eto);

        /// <summary>
        /// (订阅)采集急诊抢救室患者死亡率数据（月度）
        /// </summary>
        /// <param name="eto"></param>
        /// <returns></returns> 
        public Task CollectMonthEmergencyroomAndDeathPatientAsync(MonthReportEto eto);

        /// <summary>
        /// (订阅)采集急诊抢救室患者死亡率数据（季度）
        /// </summary>
        /// <param name="eto"></param>
        /// <returns></returns>
        public Task CollectQuarterEmergencyroomAndDeathPatientAsync(QuarterReportEto eto);

        /// <summary>
        /// (订阅)采集急诊抢救室患者死亡率数据（年度）
        /// </summary>
        /// <param name="eto"></param>
        /// <returns></returns> 
        public Task CollectYearEmergencyroomAndDeathPatientAsync(DateTime eto);

    }

}
