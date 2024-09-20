using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SamJan.MicroService.PreHospital.TriageService
{
    /// <summary>
    /// 报表接口
    /// </summary>
    public interface IReportAppService
    {
        /// <summary>
        /// 死亡人数统计报表生成
        /// 目前在预检分诊中无法做到统计，接口统计数据不准
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        Task GenDeathCountReport(string date);

        /// <summary>
        /// 早八晚八报表生成
        /// </summary>
        /// <param name="beginDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        Task GenHotMorningAndNightReport(string beginDate, string endDate);

        /// <summary>
        /// 抢救区、留观区统计报表生成
        /// 目前在预检分诊中无法做到统计，接口统计数据不准
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        Task GenRescueAndViewReport(string date);

        /// <summary>
        /// 分诊工作量统计
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="triageUserCode"></param>
        /// <param name="pageNum"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        Task<WorkLoadStatisticsDto> GetWorkLoadStatisticsAsync(DateTime startDate, DateTime endDate,
            string triageUserCode, int pageNum = 1, int pageSize = 50);

        /// <summary>
        /// 急诊科每日统计
        /// </summary>
        /// <param name="mouth"></param>
        /// <returns></returns>
        Task<Dictionary<string, object>> GetEveryDayStatisticsAsync(string mouth);

        /// <summary>
        /// 发热人数统计报表生成
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        Task GenFeverCountReport(string date);

        /// <summary>
        /// 单日分诊人数统计报表生成
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        Task GenTriageCountReport(string date);
    }
}