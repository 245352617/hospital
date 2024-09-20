using Hangfire;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Szyjian.Ecis.Patient.BackgroundJob
{
    public static class HangfireBackgroundJobsExtensions
    {
        public static void UseHangfireJobs(this IServiceProvider services, IConfiguration config)
        {
            var job = services.GetRequiredService<AutoExpireVisitStatusJob>();
            var endVisitJob = services.GetRequiredService<AutoEndVisitJob>();
            RecurringJob.AddOrUpdate("自动过号作业", () => job.ExecuteAsync(), config["Cron:ExpireVisitStatus"]);
            RecurringJob.AddOrUpdate("自动结束就诊", () => endVisitJob.ExecuteAsync(), config["Cron:EndVisit"]);

            if (config["HospitalCode"] == "PKU")
            {// 北大定时任务
                var hisPatientInfoJob = services.GetRequiredService<AsyncHisPatientInfoJob>();
                RecurringJob.AddOrUpdate("同步诊断作业", () => hisPatientInfoJob.ExecuteAsync(),
                    config["Cron:SyncDiagnosis"]);

                RecurringJob.AddOrUpdate("同步患者入科时间信息", () => hisPatientInfoJob.SyncPatientBasicInfoAsync(),
                    config["Cron:SyncPatientBasicInfo"]);
            }
        }
    }
}