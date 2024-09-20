using DotNetCore.CAP;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using Volo.Abp.Uow;
using YiJian.ECIS.ShareModel.Etos.Reports;
using YiJian.Health.Report.Enums;
using YiJian.Health.Report.Statisticses.Contracts;

namespace YiJian.Health.Report.Statisticses
{
    /// <summary>
    /// (订阅)报表数据采集作业服务
    /// </summary>
    public class JobAppService : ReportAppService, IJobAppService, ICapSubscribe
    {
        private readonly ILogger<JobAppService> _logger;
        private readonly IRpMonthDoctorAndPatientRepository _rpMonthDoctorAndPatientRepository;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="rpMonthDoctorAndPatientRepository"></param>
        public JobAppService(
            ILogger<JobAppService> logger,
            IRpMonthDoctorAndPatientRepository rpMonthDoctorAndPatientRepository)
        {
            _logger = logger;
            _rpMonthDoctorAndPatientRepository = rpMonthDoctorAndPatientRepository;
        }

        /// <summary>
        /// 统计接口
        /// </summary>
        /// <param name="reportType"></param>
        /// <param name="veidoo"></param>
        /// <param name="formatDate"></param>
        /// <returns></returns>
        public async Task<int> StatisticsAsync(EReportType reportType, EVeidoo veidoo, string formatDate)
        {
            return await _rpMonthDoctorAndPatientRepository.CallUspStatisticsAsync((int)reportType, (int)veidoo, formatDate);
        }

        #region 采集急诊科医患比数据

        /// <summary>
        /// (订阅)采集急诊科医患比数据（月度）
        /// </summary>
        /// <param name="eto"></param>
        /// <returns></returns>
        [CapSubscribe("report.statistics.month.DoctorAndPatient")]
        public async Task CollectMonthDoctorAndPatientAsync(MonthReportEto eto)
        {
            using var uow = UnitOfWorkManager.Begin();
            try
            {
                //采集医患比月度数据
                var formatDate = $"{eto.Year}M{eto.Month}";
                _ = await _rpMonthDoctorAndPatientRepository.CallUspStatisticsAsync(reportType: (int)EReportType.DoctorAndPatient, veidoo: (int)EVeidoo.Month, formatDate: formatDate);

                await uow.CompleteAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"采集医患关系数据时异常:{ex.Message},请求参数：{Newtonsoft.Json.JsonConvert.SerializeObject(eto)}");
                await uow.RollbackAsync();
                throw;
            }
        }

        /// <summary>
        /// (订阅)采集急诊科医患比数据（季度）
        /// </summary>
        /// <param name="eto"></param>
        /// <returns></returns>
        [CapSubscribe("report.statistics.quarter.DoctorAndPatient")]
        public async Task CollectQuarterDoctorAndPatientAsync(QuarterReportEto eto)
        {
            using var uow = UnitOfWorkManager.Begin();
            try
            {

                // 采集医患比季度数据
                var formatDate = $"{eto.Year}Q{eto.Quarter}";
                _ = await _rpMonthDoctorAndPatientRepository.CallUspStatisticsAsync(reportType: (int)EReportType.DoctorAndPatient, veidoo: (int)EVeidoo.Quarter, formatDate: formatDate);

                await uow.CompleteAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"采集医患关系数据时异常:{ex.Message},请求参数：{Newtonsoft.Json.JsonConvert.SerializeObject(eto)}");
                await uow.RollbackAsync();
                throw;
            }
        }

        /// <summary>
        /// (订阅)采集急诊科医患比数据（年度）
        /// </summary>
        /// <param name="eto"></param>
        /// <returns></returns>
        [CapSubscribe("report.statistics.year.DoctorAndPatient")]
        public async Task CollectYearDoctorAndPatientAsync(DateTime eto)
        {
            using var uow = UnitOfWorkManager.Begin();
            try
            {
                //采集医患比年度数据
                _ = await _rpMonthDoctorAndPatientRepository.CallUspStatisticsAsync(reportType: (int)EReportType.DoctorAndPatient, veidoo: (int)EVeidoo.Year, formatDate: $"{eto.Year}");

                await uow.CompleteAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"采集医患关系数据时异常:{ex.Message},请求参数：{Newtonsoft.Json.JsonConvert.SerializeObject(eto)}");
                await uow.RollbackAsync();
                throw;
            }
        }

        #endregion

        #region 采集急诊科护患比数据

        /// <summary>
        /// (订阅)采集急诊科护患比数据（月度）
        /// </summary>
        /// <param name="eto"></param>
        /// <returns></returns>
        [CapSubscribe("report.statistics.month.NurseAndPatient")]
        public async Task CollectMonthNurseAndPatientAsync(MonthReportEto eto)
        {
            using var uow = UnitOfWorkManager.Begin();
            try
            {
                // 采集医患比月度数据
                var formatDate = $"{eto.Year}M{eto.Month}";
                _ = await _rpMonthDoctorAndPatientRepository.CallUspStatisticsAsync(reportType: (int)EReportType.NurseAndPatient, veidoo: (int)EVeidoo.Month, formatDate: formatDate);

                await uow.CompleteAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"采集医患关系数据时异常:{ex.Message},请求参数：{Newtonsoft.Json.JsonConvert.SerializeObject(eto)}");
                await uow.RollbackAsync();
                throw;
            }
        }

        /// <summary>
        /// (订阅)采集急诊科护患比数据（季度）
        /// </summary>
        /// <param name="eto"></param>
        /// <returns></returns>
        [CapSubscribe("report.statistics.quarter.NurseAndPatient")]
        public async Task CollectQuarterNurseAndPatientAsync(QuarterReportEto eto)
        {
            using var uow = UnitOfWorkManager.Begin();
            try
            {

                // 采集医患比季度数据
                var formatDate = $"{eto.Year}Q{eto.Quarter}";
                _ = await _rpMonthDoctorAndPatientRepository.CallUspStatisticsAsync(reportType: (int)EReportType.NurseAndPatient, veidoo: (int)EVeidoo.Quarter, formatDate: formatDate);

                await uow.CompleteAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"采集医患关系数据时异常:{ex.Message},请求参数：{Newtonsoft.Json.JsonConvert.SerializeObject(eto)}");
                await uow.RollbackAsync();
                throw;
            }
        }

        /// <summary>
        /// (订阅)采集急诊科护患比数据（年度）
        /// </summary>
        /// <param name="eto"></param>
        /// <returns></returns>
        [CapSubscribe("report.statistics.year.NurseAndPatient")]
        public async Task CollectYearNurseAndPatientAsync(DateTime eto)
        {
            using var uow = UnitOfWorkManager.Begin();
            try
            {

                // 采集医患比年度数据
                _ = await _rpMonthDoctorAndPatientRepository.CallUspStatisticsAsync(reportType: (int)EReportType.NurseAndPatient, veidoo: (int)EVeidoo.Year, formatDate: $"{eto.Year}");

                await uow.CompleteAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"采集医患关系数据时异常:{ex.Message},请求参数：{Newtonsoft.Json.JsonConvert.SerializeObject(eto)}");
                await uow.RollbackAsync();
                throw;
            }
        }

        #endregion

        #region 采集急诊科各级患者比例数据

        /// <summary>
        /// (订阅)采集急诊科各级患者比例数据（月度）
        /// </summary>
        /// <param name="eto"></param>
        /// <returns></returns>
        [CapSubscribe("report.statistics.month.LevelAndPatient")]
        public async Task CollectMonthLevelAndPatientAsync(MonthReportEto eto)
        {
            using var uow = UnitOfWorkManager.Begin();
            try
            {

                // 采集急诊科各级患者比例数据（月度）
                var formatDate = $"{eto.Year}M{eto.Month}";
                _ = await _rpMonthDoctorAndPatientRepository.CallUspStatisticsAsync(reportType: (int)EReportType.LevelAndPatient, veidoo: (int)EVeidoo.Month, formatDate: formatDate);

                await uow.CompleteAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"采集医患关系数据时异常:{ex.Message},请求参数：{Newtonsoft.Json.JsonConvert.SerializeObject(eto)}");
                await uow.RollbackAsync();
                throw;
            }
        }

        /// <summary>
        /// (订阅)采集急诊科各级患者比例数据（季度）
        /// </summary>
        /// <param name="eto"></param>
        /// <returns></returns>
        [CapSubscribe("report.statistics.quarter.LevelAndPatient")]
        public async Task CollectQuarterLevelAndPatientAsync(QuarterReportEto eto)
        {
            using var uow = UnitOfWorkManager.Begin();
            try
            {

                // 采集急诊科各级患者比例数据（季度）
                var formatDate = $"{eto.Year}Q{eto.Quarter}";
                _ = await _rpMonthDoctorAndPatientRepository.CallUspStatisticsAsync(reportType: (int)EReportType.LevelAndPatient, veidoo: (int)EVeidoo.Quarter, formatDate: formatDate);

                await uow.CompleteAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"采集医患关系数据时异常:{ex.Message},请求参数：{Newtonsoft.Json.JsonConvert.SerializeObject(eto)}");
                await uow.RollbackAsync();
                throw;
            }
        }

        /// <summary>
        /// (订阅)采集急诊科各级患者比例数据（年度）
        /// </summary>
        /// <param name="eto"></param>
        /// <returns></returns>
        [CapSubscribe("report.statistics.year.LevelAndPatient")]
        public async Task CollectYearLevelAndPatientAsync(DateTime eto)
        {
            using var uow = UnitOfWorkManager.Begin();
            try
            {
                // 采集急诊科各级患者比例数据（年度）
                _ = await _rpMonthDoctorAndPatientRepository.CallUspStatisticsAsync(reportType: (int)EReportType.LevelAndPatient, veidoo: (int)EVeidoo.Year, formatDate: $"{eto.Year}");

                await uow.CompleteAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"采集医患关系数据时异常:{ex.Message},请求参数：{Newtonsoft.Json.JsonConvert.SerializeObject(eto)}");
                await uow.RollbackAsync();
                throw;
            }
        }

        #endregion

        #region 采集抢救室滞留时间中位数数据

        /// <summary>
        /// (订阅)采集抢救室滞留时间中位数数据（月度）
        /// </summary>
        /// <param name="eto"></param>
        /// <returns></returns>
        [CapSubscribe("report.statistics.month.EmergencyroomAndPatient")]
        public async Task CollectMonthEmergencyroomAndPatientAsync(MonthReportEto eto)
        {
            using var uow = UnitOfWorkManager.Begin();
            try
            {

                // 采集抢救室滞留时间中位数数据（月度）
                var formatDate = $"{eto.Year}M{eto.Month}";
                _ = await _rpMonthDoctorAndPatientRepository.CallUspStatisticsAsync(reportType: (int)EReportType.EmergencyroomAndPatient, veidoo: (int)EVeidoo.Month, formatDate: formatDate);

                await uow.CompleteAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"采集医患关系数据时异常:{ex.Message},请求参数：{Newtonsoft.Json.JsonConvert.SerializeObject(eto)}");
                await uow.RollbackAsync();
                throw;
            }
        }

        /// <summary>
        /// (订阅)采集抢救室滞留时间中位数数据（季度）
        /// </summary>
        /// <param name="eto"></param>
        /// <returns></returns>
        [CapSubscribe("report.statistics.quarter.EmergencyroomAndPatient")]
        public async Task CollectQuarterEmergencyroomAndPatientAsync(QuarterReportEto eto)
        {
            using var uow = UnitOfWorkManager.Begin();
            try
            {
                // 采集抢救室滞留时间中位数数据（季度）
                var formatDate = $"{eto.Year}Q{eto.Quarter}";
                _ = await _rpMonthDoctorAndPatientRepository.CallUspStatisticsAsync(reportType: (int)EReportType.EmergencyroomAndPatient, veidoo: (int)EVeidoo.Quarter, formatDate: formatDate);

                await uow.CompleteAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"采集医患关系数据时异常:{ex.Message},请求参数：{Newtonsoft.Json.JsonConvert.SerializeObject(eto)}");
                await uow.RollbackAsync();
                throw;
            }
        }

        /// <summary>
        /// (订阅)采集抢救室滞留时间中位数数据（年度）
        /// </summary>
        /// <param name="eto"></param>
        /// <returns></returns>
        [CapSubscribe("report.statistics.year.EmergencyroomAndPatient")]
        public async Task CollectYearEmergencyroomAndPatientAsync(DateTime eto)
        {
            using var uow = UnitOfWorkManager.Begin();
            try
            {
                //TODO 采集抢救室滞留时间中位数数据（年度）
                _ = await _rpMonthDoctorAndPatientRepository.CallUspStatisticsAsync(reportType: (int)EReportType.EmergencyroomAndPatient, veidoo: (int)EVeidoo.Year, formatDate: $"{eto.Year}");

                await uow.CompleteAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"采集医患关系数据时异常:{ex.Message},请求参数：{Newtonsoft.Json.JsonConvert.SerializeObject(eto)}");
                await uow.RollbackAsync();
                throw;
            }
        }

        #endregion

        #region 采集急诊抢救室患者死亡率数据

        /// <summary>
        /// (订阅)采集急诊抢救室患者死亡率数据（月度）
        /// </summary>
        /// <param name="eto"></param>
        /// <returns></returns>
        [CapSubscribe("report.statistics.month.EmergencyroomAndDeathPatient")]
        public async Task CollectMonthEmergencyroomAndDeathPatientAsync(MonthReportEto eto)
        {
            using var uow = UnitOfWorkManager.Begin();
            try
            {
                // 采集急诊抢救室患者死亡率数据（月度）
                var formatDate = $"{eto.Year}M{eto.Month}";
                _ = await _rpMonthDoctorAndPatientRepository.CallUspStatisticsAsync(reportType: (int)EReportType.EmergencyroomAndDeathPatient, veidoo: (int)EVeidoo.Month, formatDate: formatDate);

                await uow.CompleteAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"采集医患关系数据时异常:{ex.Message},请求参数：{Newtonsoft.Json.JsonConvert.SerializeObject(eto)}");
                await uow.RollbackAsync();
                throw;
            }
        }

        /// <summary>
        /// (订阅)采集急诊抢救室患者死亡率数据（季度）
        /// </summary>
        /// <param name="eto"></param>
        /// <returns></returns>
        [CapSubscribe("report.statistics.quarter.EmergencyroomAndDeathPatient")]
        public async Task CollectQuarterEmergencyroomAndDeathPatientAsync(QuarterReportEto eto)
        {
            using var uow = UnitOfWorkManager.Begin();
            try
            {
                //  采集急诊抢救室患者死亡率数据（季度）
                var formatDate = $"{eto.Year}Q{eto.Quarter}";
                _ = await _rpMonthDoctorAndPatientRepository.CallUspStatisticsAsync(reportType: (int)EReportType.EmergencyroomAndDeathPatient, veidoo: (int)EVeidoo.Quarter, formatDate: formatDate);

                await uow.CompleteAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"采集医患关系数据时异常:{ex.Message},请求参数：{Newtonsoft.Json.JsonConvert.SerializeObject(eto)}");
                await uow.RollbackAsync();
                throw;
            }
        }

        /// <summary>
        /// (订阅)采集急诊抢救室患者死亡率数据（年度）
        /// </summary>
        /// <param name="eto"></param>
        /// <returns></returns>
        [CapSubscribe("report.statistics.year.EmergencyroomAndDeathPatient")]
        public async Task CollectYearEmergencyroomAndDeathPatientAsync(DateTime eto)
        {
            using var uow = UnitOfWorkManager.Begin();
            try
            {
                //TODO 采集急诊抢救室患者死亡率数据（年度）
                _ = await _rpMonthDoctorAndPatientRepository.CallUspStatisticsAsync(reportType: (int)EReportType.EmergencyroomAndDeathPatient, veidoo: (int)EVeidoo.Year, formatDate: $"{eto.Year}");

                await uow.CompleteAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"采集医患关系数据时异常:{ex.Message},请求参数：{Newtonsoft.Json.JsonConvert.SerializeObject(eto)}");
                await uow.RollbackAsync();
                throw;
            }
        }

        #endregion



    }
}
