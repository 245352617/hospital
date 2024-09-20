using Hangfire;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace SamJan.MicroService.PreHospital.TriageService
{
    public static class HangfireBackgroundJobsExtensions
    {
        /// <summary>
        /// 日报表生成后台作业
        /// </summary>
        public static void UseGenDailyReportsBackgroundJob(this IApplicationBuilder app, IConfiguration configuration)
        {
            var genReportCronb = configuration["HangfireCronb:GenDailyReport"];
            var job = app.ApplicationServices.GetRequiredService<GenReportJob>();
            RecurringJob.AddOrUpdate("GenDailyReportJob", () => job.Execute(), genReportCronb, TimeZoneInfo.Local);
        }
    }
}