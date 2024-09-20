using DotNetCore.CAP;
using YiJian.Job.BackgroundService.Contract;
using YiJian.Job.Models;

namespace YiJian.Job.BackgroundService.RabbitMQ;

/// <summary>
/// 报表作业服务
/// </summary>
public class ReportJobService : IReportJobService
{
    #region const

    //DoctorAndPatient
    const string DOCTOR_AND_PATIENT_MONTH = "report.statistics.month.DoctorAndPatient";
    const string DOCTOR_AND_PATIENT_QUARTER = "report.statistics.quarter.DoctorAndPatient";
    const string DOCTOR_AND_PATIENT_YEAR = "report.statistics.year.DoctorAndPatient";

    //NurseAndPatient
    const string NURSE_AND_PATIENT_MONTH = "report.statistics.month.NurseAndPatient";
    const string NURSE_AND_PATIENT_QUARTER = "report.statistics.quarter.NurseAndPatient";
    const string NURSE_AND_PATIENT_YEAR = "report.statistics.year.NurseAndPatient";

    //LevelAndPatient
    const string LEVEL_AND_PATIENT_MONTH = "report.statistics.month.LevelAndPatient";
    const string LEVEL_AND_PATIENT_QUARTER = "report.statistics.quarter.LevelAndPatient";
    const string LEVEL_AND_PATIENT_YEAR = "report.statistics.year.LevelAndPatient";


    //EmergencyroomAndPatient
    const string EMERGENCYROOM_AND_PATIENT_MONTH = "report.statistics.month.EmergencyroomAndPatient";
    const string EMERGENCYROOM_AND_PATIENT_QUARTER = "report.statistics.quarter.EmergencyroomAndPatient";
    const string EMERGENCYROOM_AND_PATIENT_YEAR = "report.statistics.year.EmergencyroomAndPatient";

    //EmergencyroomAndDeathPatient
    const string EMERGENCYROOM_AND_DEATH_PATIENT_MONTH = "report.statistics.month.EmergencyroomAndDeathPatient";
    const string EMERGENCYROOM_AND_DEATH_PATIENT_QUARTER = "report.statistics.quarter.EmergencyroomAndDeathPatient";
    const string EMERGENCYROOM_AND_DEATH_PATIENT_YEAR = "report.statistics.year.EmergencyroomAndDeathPatient";

    #endregion
    private readonly ICapPublisher _capBus;
    private readonly ILogger<ReportJobService> _logger;

    public ReportJobService(ICapPublisher capBus, ILogger<ReportJobService> logger)
    {
        _capBus = capBus;
        _logger = logger;
    }

    /// <summary>
    /// 月度
    /// </summary> 
    public void PublishMonth()
    {
        var date = DateTime.Now.AddMonths(-1); //上个月 
        var eto = new MonthReportEto(date.Year, date.Month);
        _logger.LogInformation($"月度报表采集时间：{DateTime.Now.ToString("yyyy年MM月dd日 HH:mm:ss")}");
        _capBus.Publish(DOCTOR_AND_PATIENT_MONTH, eto);
        _capBus.Publish(NURSE_AND_PATIENT_MONTH, eto);
        _capBus.Publish(LEVEL_AND_PATIENT_MONTH, eto);
        _capBus.Publish(EMERGENCYROOM_AND_PATIENT_MONTH, eto);
        _capBus.Publish(EMERGENCYROOM_AND_DEATH_PATIENT_MONTH, eto);
    }

    /// <summary>
    /// 季度
    /// </summary> 
    public void PublishQuarter()
    {
        var date = DateTime.Now.AddMonths(-3); //上季度 
        QuarterReportEto? eto = null; 
        switch (date.Month)
        {
            case 1:
                eto = new QuarterReportEto(date.Year, 1); //第一季度
                break;
            case 4:
                eto = new QuarterReportEto(date.Year, 2); //第二季度
                break;
            case 7:
                eto = new QuarterReportEto(date.Year, 3);//第三季度
                break;
            case 10:
                eto = new QuarterReportEto(date.Year, 4); //第四季度
                break;
            default:
                break;
        }

        //var eto = GetQuarter(date);
        if (eto == null) return;

        _logger.LogInformation($"季度报表采集时间：{DateTime.Now.ToString("yyyy年MM月dd日 HH:mm:ss")},你采集的是第{eto.Year}年度，第{eto.Quarter}季度的数据。");
        _capBus.Publish(DOCTOR_AND_PATIENT_QUARTER, eto);
        _capBus.Publish(NURSE_AND_PATIENT_QUARTER, eto);
        _capBus.Publish(LEVEL_AND_PATIENT_QUARTER, eto);
        _capBus.Publish(EMERGENCYROOM_AND_PATIENT_QUARTER, eto);
        _capBus.Publish(EMERGENCYROOM_AND_DEATH_PATIENT_QUARTER, eto);
    }

    //static QuarterReportEto? GetQuarter(DateTime date) => date.Month switch
    //{
    //    1 => new QuarterReportEto(date.Year, 1),//第一季度
    //    4 => new QuarterReportEto(date.Year, 2),//第二季度
    //    7 => new QuarterReportEto(date.Year, 3),//第三季度
    //    10 => new QuarterReportEto(date.Year, 4),//第四季度
    //    _ => null
    //};

    /// <summary>
    /// 年度
    /// </summary> 
    public void PublishYear()
    {
        var year = DateTime.Now.AddYears(-1);

        _logger.LogInformation($"年度报表采集时间：{DateTime.Now.ToString("yyyy年MM月dd日 HH:mm:ss")}，你采集的是第{year}年度的数据");
        _capBus.Publish(DOCTOR_AND_PATIENT_YEAR, year);
        _capBus.Publish(NURSE_AND_PATIENT_YEAR, year);
        _capBus.Publish(LEVEL_AND_PATIENT_YEAR, year);
        _capBus.Publish(EMERGENCYROOM_AND_PATIENT_YEAR, year);
        _capBus.Publish(EMERGENCYROOM_AND_DEATH_PATIENT_YEAR, year);
    }
}
