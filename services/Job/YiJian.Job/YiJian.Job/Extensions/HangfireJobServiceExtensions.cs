using DotNetCore.CAP;
using Hangfire;
using Hangfire.Common;
using Microsoft.Extensions.Options;
using YiJian.Job.BackgroundService.Contract;
using YiJian.Job.Models;

namespace YiJian.Job.Extensions;

public static class HangfireJobServiceExtensions
{
    public static IApplicationBuilder UseHangfireJob(this IApplicationBuilder app, string hospitalCode)
    {
        if (hospitalCode == "LDC")
        {
            //注入MQ作业
            RecurringJob.AddOrUpdate<ICheckPatientExistJobService>("job.check.patient.exist", x => x.Publish(), "0 */1 * * * ?");

            //从His更新患者状态
            RecurringJob.AddOrUpdate<IPatientStatusFromHisService>("job.update.patientstatus.fromhis", x => x.Publish(), "0 */5 * * * ?");

            RecurringJob.AddOrUpdate<ISynctoxicJobService>("job.masterdata.synctoxic", x => x.Publish(), "0 */5 * * * ?");
            RecurringJob.AddOrUpdate<IRecipeSplitJobService>("job.recipe.split", x => x.Publish(), "0 59 23 ? * *");

            //等发包了再打开 报表采集作业
            //RecurringJob.AddOrUpdate<IReportJobService>("job.report.statistics.month", x => x.PublishMonth(), "1 0 0 1 * ?"); //月度 （这个月的第一天 00:00:01 统计上个月的数据）
            //RecurringJob.AddOrUpdate<IReportJobService>("job.report.statistics.quarter", x => x.PublishQuarter(), "1 5 0 1 1,4,7,10 *"); //季度 （这个季度的第一天 00:05:01 统计上个季度的数据）
            //RecurringJob.AddOrUpdate<IReportJobService>("job.report.statistics.year", x => x.PublishYear(), "1 10 0 1 1 *"); //年度 （今年的第一天 00:10:01 统计上一年的数据）

            //十分钟采集一次minio病历数据
            RecurringJob.AddOrUpdate<IGatherMinioEmrJobService>("emr.gather.minio.emr", x => x.Publish(), "0 0/10 * * * ?");
        }
        return app;
    }

}
