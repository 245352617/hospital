using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MiniExcelLibs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using YiJian.ECIS.ShareModel.Enums;
using YiJian.ECIS.ShareModel.Responses;
using YiJian.Health.Report.Statisticses.Contracts;
using YiJian.Health.Report.Statisticses.Entities;
using YiJian.Health.Report.Statisticses.Models;

namespace YiJian.Health.Report.Statisticses
{
    /// <summary>
    /// 统计分析报表
    /// </summary>
    [Authorize]
    public class StatisticAppService : ReportAppService, IStatisticsAppService
    {
        private readonly IStatisticsRepository _rpStatisticsRepository;

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="rpStatisticsRepository"></param>
        public StatisticAppService(IStatisticsRepository rpStatisticsRepository)
        {
            _rpStatisticsRepository = rpStatisticsRepository;
        }

        /// <summary>
        /// 分页获取患者，诊断，病历查询
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        public async Task<ResponseBase<List<AdmissionRecord>>> AdmissionRecordListAsync(InputAdmissionRecordByPageModel param)
        {
            if (!param.BeginDate.HasValue)
            {
                param.BeginDate = new DateTime(2000, 1, 1);
            }

            if (!param.EndDate.HasValue)
            {
                param.EndDate = new DateTime(2030, 1, 1);
            }

            var count = await _rpStatisticsRepository.GetAdmissionRecordCountAsync(param);
            var report = await _rpStatisticsRepository.GetAdmissionRecordPageAsync(param);
            return new ResponseBase<List<AdmissionRecord>>(EStatusCode.COK, data: report, totalCount: count);
        }

        /// <summary>
        /// 导出-患者，诊断，病历查
        /// </summary> 
        /// <returns></returns>
        [AllowAnonymous]
        public async Task<IActionResult> DownloadExcelAdmissionRecordAsync(InputAdmissionRecordModel param)
        {
            var data = await _rpStatisticsRepository.GetAdmissionRecordAsync(param);
            int index = 1;
            foreach (var item in data)
            {
                item.Row = index++;
            }

            var tempPath = Path.GetTempPath();
            if (!Directory.Exists(tempPath)) Directory.CreateDirectory(tempPath);

            var templateName = "AdmissionRecord.xlsx";

            var path = Path.Combine(tempPath, $"{templateName}");
            if (File.Exists(path))
            {
                var dest = Path.Combine(tempPath, $"{DateTime.Now.ToString("yyyy-MM-dd_HHmmss")}_{templateName}");
                File.Move(path, dest);
            }
            var templatePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Templates", "Statisticses", $"{templateName}");
            var value = new
            {
                Title = "病历查询",
                Data = data
            };
            MiniExcel.SaveAsByTemplate(path, templatePath, value);
            var stream = new FileStream(path, FileMode.Open, FileAccess.Read);
            var filename = $"病历查询.xlsx";

            if (filename.IsNullOrEmpty()) throw new Exception("没有查找到你需要的报表文件");
            if (stream == null) throw new Exception("导出文件异常");

            string mimeType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            return new FileStreamResult(stream, mimeType)
            {
                FileDownloadName = filename
            };
        }
    }
}
