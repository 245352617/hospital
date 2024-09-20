using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SamJan.MicroService.PreHospital.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using NPOI.HPSF;
using NUglify.Helpers;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using JsonResult = SamJan.MicroService.PreHospital.Core.JsonResult;

namespace SamJan.MicroService.PreHospital.TriageService
{
    /// <summary>
    /// 报表
    /// </summary>
    public class ReportAppService : ApplicationService, IReportAppService
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<ReportAppService> _log;
        private readonly IRepository<ReportHotMorningAndNight> _hotMorningAndNightsRepository;
        private readonly IRepository<ReportDeathCount> _reportDeathCountRepository;
        private readonly IRepository<ReportRescueAndView> _reportRescueAndViewRepository;
        private readonly IRepository<ReportFeverCount> _reportFeverCountsRepository;
        private readonly IRepository<ReportTriageCount> _reportTriageCountsRepository;

        private IDapperRepository __dapperRepository;
        private IDapperRepository _dapperRepository => LazyGetRequiredService(ref __dapperRepository);


        private DataTableExtensions _dataTableExtensions;
        private DataTableExtensions DataTableExtensions => LazyGetRequiredService(ref _dataTableExtensions);

        public ReportAppService(IConfiguration configuration, ILogger<ReportAppService> log,
            IRepository<ReportHotMorningAndNight> hotMorningAndNightsRepository,
            IRepository<ReportDeathCount> reportDeathCountRepository,
            IRepository<ReportRescueAndView> reportRescueAndViewRepository,
            IRepository<ReportFeverCount> reportFeverCountsRepository,
            IRepository<ReportTriageCount> reportTriageCountsRepository)
        {
            this._configuration = configuration;
            this._log = log;
            this._hotMorningAndNightsRepository = hotMorningAndNightsRepository;
            this._reportDeathCountRepository = reportDeathCountRepository;
            this._reportRescueAndViewRepository = reportRescueAndViewRepository;
            _reportFeverCountsRepository = reportFeverCountsRepository;
            _reportTriageCountsRepository = reportTriageCountsRepository;
        }

        /// <summary>
        /// 全院早八晚八发热统计
        /// </summary>
        /// <param name="yearmonth">月份（字符串如：2022-01）</param>
        /// <returns></returns>
        public async Task<JsonResult<IEnumerable<ReportHotMorningAndNightDto>>> GetAllHotMorningAndNight(
            string yearmonth)
        {
            DateTime beginDate;
            DateTime endDate;
            try
            {
                var yearAndMonth = yearmonth.Split(new char[] { '/', '-' }).Select(x => int.Parse(x)).ToList();
                // 开始日期
                beginDate = new DateTime(yearAndMonth[0], yearAndMonth[1], 1);
                // 结束日期
                endDate = beginDate.AddMonths(1);
            }
            catch (Exception)
            {
                return JsonResult<IEnumerable<ReportHotMorningAndNightDto>>.Fail(
                    msg: "月份格式错误，预期的格式如 2022-01 或者 2022-1 或者 2022/01 或者 2022/1 ");
            }

            try
            {
                // DeptCode 为 null 的表示全院
                var list = await this._hotMorningAndNightsRepository.AsNoTracking()
                    .Where(x => string.IsNullOrEmpty(x.DeptCode))
                    .Where(x => x.Date >= beginDate && x.Date < endDate)
                    .OrderBy(x => x.Date)
                    .ToListAsync();
                var result = list.BuildAdapter().AdaptToType<List<ReportHotMorningAndNightDto>>();

                return JsonResult<IEnumerable<ReportHotMorningAndNightDto>>.Ok(data: result);
            }
            catch (Exception ex)
            {
                return JsonResult<IEnumerable<ReportHotMorningAndNightDto>>.Fail(msg: ex.Message);
            }
        }

        /// <summary>
        /// 早八晚八数据修改
        /// </summary>
        /// <param name="id"></param>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<JsonResult> UdpateHotMorningAndNight(Guid id, UpdateHotMorningAndNightDto dto)
        {
            try
            {
                var item = await _hotMorningAndNightsRepository.FirstOrDefaultAsync(x => x.Id == id);
                item.MorningCountChanged = (int?)dto.MorningCountChanged;
                item.NightCountChanged = (int?)dto.NightCountChanged;

                await this._hotMorningAndNightsRepository.UpdateAsync(item);
            }
            catch (Exception ex)
            {
                return JsonResult.Fail(msg: ex.Message);
            }

            return JsonResult.Ok();
        }

        /// <summary>
        /// 导出早八晚八报表到Excel
        /// </summary>
        /// <param name="yearmonth"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<IActionResult> ExportHotMorningAndNightAsync(string yearmonth,
            CancellationToken cancellationToken = default)
        {
            try
            {
                DataTable table = this.GetMorningAndNightDataTable();
                List<ReportHeaderDto> reportHeaders = this.GetMorningAndNightHeader();
                var listResult = await this.GetAllHotMorningAndNight(yearmonth);
                if (listResult.Code != 200) return new OkObjectResult(listResult);
                foreach (var item in listResult.Data)
                {
                    this.FillDataTable(table, item);
                }

                var buffer = await this.DataTableExtensions.DataTableToExcelAsync(reportHeaders, table);
                var file = new FileContentResult(buffer, "application/octet-stream")
                {
                    FileDownloadName = $"早八晚八发热统计.{DateTime.Now:yyyyMMddHHmmss}.xls"
                };

                _log.LogInformation("【ExportHotMorningAndNightAsync】" +
                                    "【导出数据到Excel成功】");
                _log.LogTrace("【ExportHotMorningAndNightAsync】" +
                              "【导出数据到Excel结束】");
                return file;
            }
            catch (Exception e)
            {
                _log.LogWarning($"【导出数据到Excel错误】【Msg：{e}】");
                return null;
            }
        }

        /// <summary>
        /// 全院死亡人数统计
        /// </summary>
        /// <param name="date">日期（字符串如：2022-01-01）</param>
        /// <returns></returns>
        public async Task<JsonResult<IEnumerable<ReportDeathCountDto>>> GetDeathCount(string date)
        {
            if (!DateTime.TryParse(date, out DateTime triageDate))
            {
                return JsonResult<IEnumerable<ReportDeathCountDto>>.Fail(msg: "日期格式错误");
            }

            try
            {
                // DeptCode 为 null 的表示全院
                var list = await this._reportDeathCountRepository.AsNoTracking()
                    .Where(x => x.TriageDate == triageDate)
                    .OrderBy(x => x.TriageDate)
                    .ThenBy(x => x.Sort)
                    .ToListAsync();
                var result = list.BuildAdapter().AdaptToType<List<ReportDeathCountDto>>();

                return JsonResult<IEnumerable<ReportDeathCountDto>>.Ok(data: result);
            }
            catch (Exception ex)
            {
                return JsonResult<IEnumerable<ReportDeathCountDto>>.Fail(msg: ex.Message);
            }
        }


        /// <summary>
        /// 导出发热人数统计报表到Excel
        /// </summary>
        /// <param name="date">日期（字符串如：2022-01-01）</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<IActionResult> ExportFeverCountAsync(string date,
            CancellationToken cancellationToken = default)
        {
            try
            {
                DataTable table = this.GetFeverCountDataTable();
                List<ReportHeaderDto> reportHeaders = this.GetFeverCountHeader();
                var listResult = await this.GetFeverCount(date);
                if (listResult.Code != 200) return new OkObjectResult(listResult);
                foreach (var item in listResult.Data)
                {
                    this.FillDataTable(table, item);
                }

                var buffer = await this.DataTableExtensions.DataTableToExcelAsync(reportHeaders, table);
                var file = new FileContentResult(buffer, "application/octet-stream")
                {
                    FileDownloadName = $"死亡人数统计.{DateTime.Now:yyyyMMddHHmmss}.xls"
                };

                _log.LogInformation("【ExportFeverCountAsync】" +
                                    "【导出数据到Excel成功】");
                _log.LogTrace("【ExportFeverCountAsync】" +
                              "【导出数据到Excel结束】");
                return file;
            }
            catch (Exception e)
            {
                _log.LogWarning($"【导出数据到Excel错误】【Msg：{e}】");
                return null;
            }
        }

        /// <summary>
        /// 导出死亡人数统计报表到Excel
        /// </summary>
        /// <param name="date">日期（字符串如：2022-01-01）</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<IActionResult> ExportDeathCountAsync(string date,
            CancellationToken cancellationToken = default)
        {
            try
            {
                DataTable table = this.GetDeathCountDataTable();
                List<ReportHeaderDto> reportHeaders = this.GetDeathCountHeader();
                var listResult = await this.GetDeathCount(date);
                if (listResult.Code != 200) return new OkObjectResult(listResult);
                foreach (var item in listResult.Data)
                {
                    this.FillDataTable(table, item);
                }

                var buffer = await this.DataTableExtensions.DataTableToExcelAsync(reportHeaders, table);
                var file = new FileContentResult(buffer, "application/octet-stream")
                {
                    FileDownloadName = $"死亡人数统计.{DateTime.Now:yyyyMMddHHmmss}.xls"
                };

                _log.LogInformation("【ExportDeathCountAsync】" +
                                    "【导出数据到Excel成功】");
                _log.LogTrace("【ExportDeathCountAsync】" +
                              "【导出数据到Excel结束】");
                return file;
            }
            catch (Exception e)
            {
                _log.LogWarning($"【导出数据到Excel错误】【Msg：{e}】");
                return null;
            }
        }

        /// <summary>
        /// 死亡人数数据修改
        /// </summary>
        /// <param name="id"></param>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<JsonResult> UdpateDeathCount(Guid id, UpdateDeathCountDto dto)
        {
            try
            {
                var item = await _reportDeathCountRepository.FirstOrDefaultAsync(x => x.Id == id);
                item.DeathCountChanged = (int?)dto.DeathCountChanged;

                await this._reportDeathCountRepository.UpdateAsync(item);
            }
            catch (Exception ex)
            {
                return JsonResult.Fail(msg: ex.Message);
            }

            return JsonResult.Ok();
        }

        /// <summary>
        /// 抢救区统计
        /// </summary>
        /// <param name="date">日期（字符串如：2022-01-01）</param>
        /// <returns></returns>
        public async Task<JsonResult<IEnumerable<ReportRescueAndViewDto>>> GetRescueAsync(string date)
        {
            if (!DateTime.TryParse(date, out DateTime triageDate))
            {
                return JsonResult<IEnumerable<ReportRescueAndViewDto>>.Fail(msg: "日期格式错误");
            }

            try
            {
                // DeptCode 为 null 的表示全院
                var list = await this._reportRescueAndViewRepository.AsNoTracking()
                    .Where(x => x.AreaCode == "Rescue" && x.TriageDate == triageDate)
                    .OrderBy(x => x.TriageDate)
                    .ThenBy(x => x.Sort)
                    .ToListAsync();
                var result = list.BuildAdapter().AdaptToType<List<ReportRescueAndViewDto>>();

                return JsonResult<IEnumerable<ReportRescueAndViewDto>>.Ok(data: result);
            }
            catch (Exception ex)
            {
                return JsonResult<IEnumerable<ReportRescueAndViewDto>>.Fail(msg: ex.Message);
            }
        }

        /// <summary>
        /// 留观区统计
        /// </summary>
        /// <param name="date">日期（字符串如：2022-01-01）</param>
        /// <returns></returns>
        public async Task<JsonResult<IEnumerable<ReportRescueAndViewDto>>> GetViewAsync(string date)
        {
            if (!DateTime.TryParse(date, out DateTime triageDate))
            {
                return JsonResult<IEnumerable<ReportRescueAndViewDto>>.Fail(msg: "日期格式错误");
            }

            try
            {
                // DeptCode 为 null 的表示全院
                var list = await this._reportRescueAndViewRepository.AsNoTracking()
                    .Where(x => x.AreaCode == "View" && x.TriageDate == triageDate)
                    .OrderBy(x => x.TriageDate)
                    .ThenBy(x => x.Sort)
                    .ToListAsync();
                var result = list.BuildAdapter().AdaptToType<List<ReportRescueAndViewDto>>();

                return JsonResult<IEnumerable<ReportRescueAndViewDto>>.Ok(data: result);
            }
            catch (Exception ex)
            {
                return JsonResult<IEnumerable<ReportRescueAndViewDto>>.Fail(msg: ex.Message);
            }
        }

        /// <summary>
        /// 导出抢救区、留观区报表到Excel
        /// </summary>
        /// <param name="date">日期（字符串如：2022-01-01）</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<IActionResult> ExportRescueAndViewAsync(string date,
            CancellationToken cancellationToken = default)
        {
            try
            {
                DataTable table = this.GetRescueAndViewDataTable();
                List<ReportHeaderDto> reportHeaders = this.GetRescueAndViewHeader();
                var rescueResult = await this.GetRescueAsync(date);
                var viewResult = await this.GetViewAsync(date);
                if (rescueResult.Code != 200) return new OkObjectResult(rescueResult);
                if (viewResult.Code != 200) return new OkObjectResult(viewResult);
                foreach (var item in rescueResult.Data)
                {
                    this.FillDataTable(table, item);
                }

                foreach (var item in viewResult.Data)
                {
                    this.FillDataTable(table, item);
                }

                var buffer = await this.DataTableExtensions.DataTableToExcelAsync(reportHeaders, table);
                var file = new FileContentResult(buffer, "application/octet-stream")
                {
                    FileDownloadName = $"抢救区、留观区统计.{DateTime.Now:yyyyMMddHHmmss}.xls"
                };

                _log.LogInformation("【ExportRescueAndViewAsync】" +
                                    "【导出数据到Excel成功】");
                _log.LogTrace("【ExportRescueAndViewAsync】" +
                              "【导出数据到Excel结束】");
                return file;
            }
            catch (Exception e)
            {
                _log.LogWarning($"【导出数据到Excel错误】【Msg：{e}】");
                return null;
            }
        }

        /// <summary>
        /// 抢救区、留观区数据修改
        /// </summary>
        /// <param name="id"></param>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<JsonResult> UdpateRescueAndViewAsync(Guid id, UpdateRescueAndViewDto dto)
        {
            try
            {
                var item = await _reportRescueAndViewRepository.FirstOrDefaultAsync(x => x.Id == id);
                item.CountChanged = (int?)dto.CountChanged;

                await this._reportRescueAndViewRepository.UpdateAsync(item);
            }
            catch (Exception ex)
            {
                return JsonResult.Fail(msg: ex.Message);
            }

            return JsonResult.Ok();
        }

        /// <summary>
        /// 早八晚八报表生成
        /// </summary>
        /// <param name="beginDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public async Task GenHotMorningAndNightReport(string beginDate, string endDate)
        {
            try
            {
                DateTime dateBeginDate = !string.IsNullOrEmpty(beginDate)
                    ? DateTime.Parse(beginDate)
                    : DateTime.Today.AddDays(-1);
                DateTime dateEndDate = !string.IsNullOrEmpty(endDate) ? DateTime.Parse(endDate) : DateTime.Today;
                // 删除旧记录
                _ = this._hotMorningAndNightsRepository.DeleteAsync(
                    x => x.Date >= dateBeginDate && x.Date < dateEndDate);
                // 查询数据
                string sql = @"Select Date ,
                                Sum(Case Early8 When 1 Then 1 Else 0 End) MorningCount,
		                        Sum(Case Early8 When 2 Then 1 Else 0 End) NightCount
                            From(
                                Select Convert(varchar(10), DateAdd(Hour, -8, pat.TriageTime), 120) Date,
                                        Case When DatePart(Hour, pat.TriageTime) >= 8 And DatePart(Hour, pat.TriageTime) < 20 Then 1 Else 2 End Early8,
                                        pat.PatientId, pat.PatientName,
                                        conq.TriageDeptCode, conq.TriageDeptName
                                    from Triage_PatientInfo pat
                                        Left Join Triage_ConsequenceInfo conq On pat.Id = conq.PatientInfoId
                                        Left Join Triage_VitalSignInfo vit On pat.Id = vit.PatientInfoId
                                    Where pat.TriageStatus = 1-- 已分诊
                                        And Cast(vit.Temp as float) >= 37.3
                            ) tb
                            Where Date >= '" + dateBeginDate.ToString("yyyy-MM-dd") + @"' And Date < '" +
                             dateBeginDate.ToString("yyyy-MM-dd") + @"' 
                            Group By Date
                            Order By Date Desc";
                var connectionStringKey = _configuration.GetConnectionString("DefaultConnection");
                var list = await _dapperRepository.QueryListAsync<ReportHotMorningAndNight>(sql,
                    dbKey: "DefaultConnection", connectionStringKey: connectionStringKey);

                // 对日期递归处理
                var date = dateBeginDate;
                while (date < dateEndDate)
                {
                    var item = list.FirstOrDefault(x => x.Date == date);
                    if (item == null)
                    {
                        // 空则插入默认记录
                        item = new ReportHotMorningAndNight
                        {
                            Date = date,
                            Sort = 999
                        };
                    }

                    await this._hotMorningAndNightsRepository.InsertAsync(item);

                    // 循环递增
                    date = date.AddDays(1);
                }
            }
            catch (Exception ex)
            {
                _log.LogInformation("Error: {Message}", ex.Message);
                throw;
            }
        }

        /// <summary>
        /// 死亡人数统计报表生成
        /// 目前在预检分诊中无法做到统计，接口统计数据不准
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public async Task GenDeathCountReport(string date)
        {
            try
            {
                DateTime triageDate = !string.IsNullOrEmpty(date)
                    ? DateTime.Parse(date).Date
                    : DateTime.Today.AddDays(-1);
                // 删除旧记录
                _ = this._reportDeathCountRepository.DeleteAsync(x => x.TriageDate == triageDate);
                // 查询数据
                string sql =
                    @"Select cqi.TriageTargetName ItemName, Count(pat.PatientID) DeathCount, Convert(varchar(10), pat.TriageTime, 120) TriageDate
		                        from Triage_PatientInfo pat
			                        Left Join Triage_ConsequenceInfo cqi On pat.Id = cqi.PatientInfoID
		                        Where pat.TriageStatus = 1 And Convert(varchar(10), pat.TriageTime, 120) = '" +
                    triageDate.ToString("yyy-MM-dd") + @"' 
                                Group By cqi.TriageTargetName, Convert(varchar(10), pat.TriageTime, 120)";
                var connectionStringKey = _configuration.GetConnectionString("DefaultConnection");
                var list = await _dapperRepository.QueryListAsync<ReportDeathCount>(sql, dbKey: "DefaultConnection",
                    connectionStringKey: connectionStringKey);

                var firstAidAreaCount = list.FirstOrDefault(x => x.ItemName == "抢救区");
                var viewAreaCount = list.FirstOrDefault(x => x.ItemName == "留观区");
                var outPatientAreaCount = list.FirstOrDefault(x => x.ItemName == "就诊区");
                var preHospitalCount = list.FirstOrDefault(x => x.ItemName == "死亡");
                var firstAidRecord = new ReportDeathCount
                {
                    Sort = 1,
                    ItemName = "抢救区",
                    //DeathCount = firstAidAreaCount != null ? firstAidAreaCount.DeathCount : 0,
                    DeathCount = 0,
                    TriageDate = triageDate,
                };
                var viewRecord = new ReportDeathCount
                {
                    Sort = 2,
                    ItemName = "留观区",
                    //DeathCount = viewAreaCount != null ? viewAreaCount.DeathCount : 0,
                    DeathCount = 0,
                    TriageDate = triageDate,
                };
                var outPatientRecord = new ReportDeathCount
                {
                    Sort = 3,
                    ItemName = "急诊病房",
                    //DeathCount = outPatientAreaCount != null ? outPatientAreaCount.DeathCount : 0,
                    DeathCount = 0,
                    TriageDate = triageDate,
                };
                var preHospitalRecord = new ReportDeathCount
                {
                    Sort = 4,
                    ItemName = "院前",
                    DeathCount = preHospitalCount != null ? preHospitalCount.DeathCount : 0,
                    TriageDate = triageDate,
                };
                await this._reportDeathCountRepository.InsertAsync(firstAidRecord);
                await this._reportDeathCountRepository.InsertAsync(viewRecord);
                await this._reportDeathCountRepository.InsertAsync(outPatientRecord);
                await this._reportDeathCountRepository.InsertAsync(preHospitalRecord);
            }
            catch (Exception ex)
            {
                _log.LogInformation("Error: {Message}", ex.Message);
                throw;
            }
        }

        /// <summary>
        /// 抢救区、留观区统计报表生成
        /// 目前在预检分诊中无法做到统计，接口统计数据不准
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        //[ApiExplorerSettings(IgnoreApi = true)]
        public async Task GenRescueAndViewReport(string date)
        {
            try
            {
                DateTime triageDate = !string.IsNullOrEmpty(date)
                    ? DateTime.Parse(date).Date
                    : DateTime.Today.AddDays(-1);
                // 删除旧记录
                _ = this._reportRescueAndViewRepository.DeleteAsync(x => x.TriageDate == triageDate);
                // 查询数据
                string sql =
                    @"Select cqi.TriageTargetName ItemName, Count(pat.PatientID) DeathCount, Convert(varchar(10), pat.TriageTime, 120) TriageDate
		                        from Triage_PatientInfo pat
			                        Left Join Triage_ConsequenceInfo cqi On pat.Id = cqi.PatientInfoID
		                        Where pat.TriageStatus = 1 And Convert(varchar(10), pat.TriageTime, 120) = '" +
                    triageDate.ToString("yyy-MM-dd") + @"' 
                                Group By cqi.TriageTargetName, Convert(varchar(10), pat.TriageTime, 120)";
                var connectionStringKey = _configuration.GetConnectionString("DefaultConnection");
                var list = await _dapperRepository.QueryListAsync<ReportDeathCount>(sql, dbKey: "DefaultConnection",
                    connectionStringKey: connectionStringKey);

                await this._reportRescueAndViewRepository.InsertAsync(new ReportRescueAndView
                {
                    Sort = 1,
                    AreaCode = "Rescue",
                    AreaName = "抢救区",
                    ItemName = "抢救总数",
                    Count = 0,
                    TriageDate = triageDate,
                });
                await this._reportRescueAndViewRepository.InsertAsync(new ReportRescueAndView
                {
                    Sort = 2,
                    AreaCode = "Rescue",
                    AreaName = "抢救区",
                    ItemName = "病危",
                    Count = 0,
                    TriageDate = triageDate,
                });
                await this._reportRescueAndViewRepository.InsertAsync(new ReportRescueAndView
                {
                    Sort = 3,
                    AreaCode = "Rescue",
                    AreaName = "抢救区",
                    ItemName = "病重",
                    Count = 0,
                    TriageDate = triageDate,
                });
                await this._reportRescueAndViewRepository.InsertAsync(new ReportRescueAndView
                {
                    Sort = 4,
                    AreaCode = "Rescue",
                    AreaName = "抢救区",
                    ItemName = "抢救总数",
                    Count = 0,
                    TriageDate = triageDate,
                });
                await this._reportRescueAndViewRepository.InsertAsync(new ReportRescueAndView
                {
                    Sort = 5,
                    AreaCode = "Rescue",
                    AreaName = "抢救区",
                    ItemName = "送住院人数",
                    Count = 0,
                    TriageDate = triageDate,
                });
                await this._reportRescueAndViewRepository.InsertAsync(new ReportRescueAndView
                {
                    Sort = 6,
                    AreaCode = "Rescue",
                    AreaName = "抢救区",
                    ItemName = "开启绿色通道人数",
                    Count = 0,
                    TriageDate = triageDate,
                });
                await this._reportRescueAndViewRepository.InsertAsync(new ReportRescueAndView
                {
                    Sort = 7,
                    AreaCode = "Rescue",
                    AreaName = "抢救区",
                    ItemName = "滞留≥6h人数",
                    Count = 0,
                    TriageDate = triageDate,
                });
                await this._reportRescueAndViewRepository.InsertAsync(new ReportRescueAndView
                {
                    Sort = 8,
                    AreaCode = "Rescue",
                    AreaName = "抢救区",
                    ItemName = "核酸采样人数",
                    Count = 0,
                    TriageDate = triageDate,
                });
                await this._reportRescueAndViewRepository.InsertAsync(new ReportRescueAndView
                {
                    Sort = 9,
                    AreaName = "抢救区",
                    ItemName = "72h重返抢救室人数",
                    Count = 0,
                    TriageDate = triageDate,
                });
                await this._reportRescueAndViewRepository.InsertAsync(new ReportRescueAndView
                {
                    Sort = 1,
                    AreaCode = "View",
                    AreaName = "留观区",
                    ItemName = "输液人数",
                    Count = 0,
                    TriageDate = triageDate,
                });
                await this._reportRescueAndViewRepository.InsertAsync(new ReportRescueAndView
                {
                    Sort = 2,
                    AreaCode = "View",
                    AreaName = "留观区",
                    ItemName = "留观人数",
                    Count = 0,
                    TriageDate = triageDate,
                });
                await this._reportRescueAndViewRepository.InsertAsync(new ReportRescueAndView
                {
                    Sort = 3,
                    AreaCode = "View",
                    AreaName = "留观区",
                    ItemName = "输液留观",
                    Count = 0,
                    TriageDate = triageDate,
                });
                await this._reportRescueAndViewRepository.InsertAsync(new ReportRescueAndView
                {
                    Sort = 4,
                    AreaCode = "View",
                    AreaName = "留观区",
                    ItemName = "病危",
                    Count = 0,
                    TriageDate = triageDate,
                });
                await this._reportRescueAndViewRepository.InsertAsync(new ReportRescueAndView
                {
                    Sort = 5,
                    AreaCode = "View",
                    AreaName = "留观区",
                    ItemName = "病重",
                    Count = 0,
                    TriageDate = triageDate,
                });
                await this._reportRescueAndViewRepository.InsertAsync(new ReportRescueAndView
                {
                    Sort = 6,
                    AreaCode = "View",
                    AreaName = "留观区",
                    ItemName = "留观≥72h",
                    Count = 0,
                    TriageDate = triageDate,
                });
                await this._reportRescueAndViewRepository.InsertAsync(new ReportRescueAndView
                {
                    Sort = 7,
                    AreaCode = "View",
                    AreaName = "留观区",
                    ItemName = "抢救",
                    Count = 0,
                    TriageDate = triageDate,
                });
            }
            catch (Exception ex)
            {
                _log.LogInformation("Error: {Message}", ex.Message);
                throw;
            }
        }

        /// <summary>
        /// 获取早八晚八报表表头
        /// </summary>
        /// <returns></returns>
        private List<ReportHeaderDto> GetMorningAndNightHeader()
        {
            var headerDtoList = new List<ReportHeaderDto>
            {
                new ReportHeaderDto
                {
                    HeaderName = "日期时间",
                    HeaderField = "Date",
                    HeaderSort = "1",
                    HeaderWidth = "220"
                },
                new ReportHeaderDto
                {
                    HeaderName = "早八 (08:00 ~ 20:00)",
                    HeaderField = "MorningCount",
                    HeaderSort = "2",
                    HeaderWidth = "100"
                },
                new ReportHeaderDto
                {
                    HeaderName = "晚八 (20:00 ~ 08:00)",
                    HeaderField = "NightCount",
                    HeaderSort = "3",
                    HeaderWidth = "120"
                },
            };
            return headerDtoList;
        }

        /// <summary>
        /// 获取早八晚八DataTable
        /// </summary>
        /// <returns></returns>
        private DataTable GetMorningAndNightDataTable()
        {
            DataTable table = new DataTable();
            DataColumn dc = table.Columns.Add("Index", typeof(int));
            dc.AutoIncrement = true;
            dc.AutoIncrementSeed = 1;
            table.Columns.Add("Date", typeof(DateTime));
            table.Columns.Add("MorningCount", typeof(int));
            table.Columns.Add("NightCount", typeof(int));

            return table;
        }

        /// <summary>
        /// DataTable 填充数据
        /// </summary>
        /// <param name="table"></param>
        /// <param name="item"></param>
        private void FillDataTable(DataTable table, ReportHotMorningAndNightDto item)
        {
            DataRow row = table.NewRow();
            row.SetField("Date", item.Date);
            row.SetField("MorningCount", item.MorningCountResult);
            row.SetField("NightCount", item.NightCountResult);

            table.Rows.Add(row);
        }

        /// <summary>
        /// 获取死亡人数统计报表表头
        /// </summary>
        /// <returns></returns>
        private List<ReportHeaderDto> GetDeathCountHeader()
        {
            var headerDtoList = new List<ReportHeaderDto>
            {
                new ReportHeaderDto
                {
                    HeaderName = "项目",
                    HeaderField = "ItemName",
                    HeaderSort = "1",
                    HeaderWidth = "220"
                },
                new ReportHeaderDto
                {
                    HeaderName = "统计人数",
                    HeaderField = "DeathCount",
                    HeaderSort = "2",
                    HeaderWidth = "100"
                },
            };
            return headerDtoList;
        }

        /// <summary>
        /// 获取死亡人数统计DataTable
        /// </summary>
        /// <returns></returns>
        private DataTable GetDeathCountDataTable()
        {
            DataTable table = new DataTable();
            DataColumn dc = table.Columns.Add("Index", typeof(int));
            dc.AutoIncrement = true;
            dc.AutoIncrementSeed = 1;
            table.Columns.Add("ItemName", typeof(string));
            table.Columns.Add("DeathCount", typeof(int));

            return table;
        }

        /// <summary>
        /// 全院发热人数统计
        /// </summary>
        /// <param name="date">日期（字符串如：2022-01-01）</param>
        /// <returns></returns>
        public async Task<JsonResult<IEnumerable<ReportFeverCountDto>>> GetFeverCount(string date)
        {
            if (!DateTime.TryParse(date, out DateTime triageDate))
            {
                return JsonResult<IEnumerable<ReportFeverCountDto>>.Fail(msg: "日期格式错误");
            }

            try
            {
                // DeptCode 为 null 的表示全院
                var list = await this._reportFeverCountsRepository.AsNoTracking()
                    .Where(x => x.TriageDate == triageDate)
                    .OrderBy(x => x.TriageDate)
                    .ThenBy(x => x.Sort)
                    .ToListAsync();
                var result = list.BuildAdapter().AdaptToType<List<ReportFeverCountDto>>();
                result.Add(new ReportFeverCountDto()
                {
                    DeptName = "全院",
                    TriageDate = triageDate,
                    FeverCount = list.Sum(l => l.FeverCount),
                    FeverCountChanged = list.Sum(l => l.FeverCountChanged),
                    Sort = list.Any() ? list.Max(m => m.Sort) + 1 : 1
                });
                return JsonResult<IEnumerable<ReportFeverCountDto>>.Ok(data: result);
            }
            catch (Exception ex)
            {
                return JsonResult<IEnumerable<ReportFeverCountDto>>.Fail(msg: ex.Message);
            }
        }

        /// <summary>
        /// 发热人数数据修改
        /// </summary>
        /// <param name="id"></param>
        /// <param name="feverCountChanged"></param>
        /// <returns></returns>
        public async Task<JsonResult> UpdateFeverCount(Guid id, int? feverCountChanged)
        {
            try
            {
                var item = await _reportFeverCountsRepository.FirstOrDefaultAsync(x => x.Id == id);
                item.FeverCountChanged = feverCountChanged;
                await this._reportFeverCountsRepository.UpdateAsync(item);
            }
            catch (Exception ex)
            {
                return JsonResult.Fail(msg: ex.Message);
            }

            return JsonResult.Ok();
        }

        /// <summary>
        /// 发热人数统计报表生成
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public async Task GenFeverCountReport(string date)
        {
            try
            {
                DateTime triageDate = !string.IsNullOrEmpty(date)
                    ? DateTime.Parse(date).Date
                    : DateTime.Today.AddDays(-1);
                // 删除旧记录
                // _ = this._reportFeverCountsRepository.DeleteAsync(x => x.TriageDate == triageDate);
                // 查询数据
                string sql = @"delete [dbo].[Rpt_FeverCount] where Convert(varchar(10), TriageDate, 120)= '" + triageDate.ToString("yyy-MM-dd") + @"';
                                INSERT INTO [dbo].[Rpt_FeverCount]
                                       ([Id]
                                       ,[DeptName]
                                       ,[TriageDate]
                                       ,[Sort]
                                       ,[FeverCount]
                                       ,[FeverCountChanged])
                            Select NEWID(),
		                            CASE tc.TriageConfigName	WHEN '' THEN N'未知'	ELSE tc.TriageConfigName END DeptName,
		                            ISNULL(Convert(varchar(10), pat.TriageTime, 120) , '" + triageDate.ToString("yyy-MM-dd") + @"') TriageDate,
		                             ROW_NUMBER() over ( order by COUNT(PatientId)) Sort,
		                            Count(PatientId) FeverCount,0
	                            from  Triage_PatientInfo pat   
							Left Join Triage_ConsequenceInfo conq On pat.Id = conq.PatientInfoId	and	  pat.TriageStatus = 1	-- 已分诊                               
                             And Convert(varchar(10), pat.TriageTime, 120) = '" + triageDate.ToString("yyy-MM-dd") +
                             @"' RIGHT JOIN Dict_TriageConfig tc on conq.TriageDeptCode=tc.TriageConfigCode 
                            Left Join Triage_VitalSignInfo vit On pat.Id = vit.PatientInfoId  And Cast(vit.Temp as float) >= 37.3
                            where  tc.TriageConfigType='1005' and tc.IsDisable=1 and tc.IsDeleted=0 Group By Convert(varchar(10), pat.TriageTime, 120), tc.TriageConfigName
                            Order By TriageDate, DeptName ";
                var connectionStringKey = _configuration.GetConnectionString("DefaultConnection");
                var result = await _dapperRepository.ExecuteSqlAsync(sql, dbKey: "DefaultConnection",
                    connectionStringKey: connectionStringKey);
            }
            catch (Exception ex)
            {
                _log.LogInformation("Error: {Message}", ex.Message);
                throw;
            }
        }

        /// <summary>
        /// 获取发热人数统计报表表头
        /// </summary>
        /// <returns></returns>
        private List<ReportHeaderDto> GetFeverCountHeader()
        {
            var headerDtoList = new List<ReportHeaderDto>
            {
                new ReportHeaderDto
                {
                    HeaderName = "科室",
                    HeaderField = "DeptName",
                    HeaderSort = "1",
                    HeaderWidth = "220"
                },
                new ReportHeaderDto
                {
                    HeaderName = "统计人数",
                    HeaderField = "FeverCount",
                    HeaderSort = "2",
                    HeaderWidth = "100"
                },
            };
            return headerDtoList;
        }

        /// <summary>
        /// 获取发热人数统计DataTable
        /// </summary>
        /// <returns></returns>
        private DataTable GetFeverCountDataTable()
        {
            DataTable table = new DataTable();
            DataColumn dc = table.Columns.Add("Index", typeof(int));
            dc.AutoIncrement = true;
            dc.AutoIncrementSeed = 1;
            table.Columns.Add("DeptName", typeof(string));
            table.Columns.Add("FeverCount", typeof(int));

            return table;
        }

        /// <summary>
        /// DataTable 填充数据
        /// </summary>
        /// <param name="table"></param>
        /// <param name="item"></param>
        private void FillDataTable(DataTable table, ReportFeverCountDto item)
        {
            DataRow row = table.NewRow();
            row.SetField("DeptName", item.DeptName);
            row.SetField("FeverCount", item.FeverCount);
            table.Rows.Add(row);
        }

        /// <summary>
        /// 全院单日分诊人数统计
        /// </summary>
        /// <param name="date">日期（字符串如：2022-01-01）</param>
        /// <returns></returns>
        public async Task<JsonResult<IEnumerable<ReportTriageCountDto>>> GetTriageCount(string date)
        {
            if (!DateTime.TryParse(date, out DateTime triageDate))
            {
                return JsonResult<IEnumerable<ReportTriageCountDto>>.Fail(msg: "日期格式错误");
            }

            try
            {
                var list = await this._reportTriageCountsRepository.AsNoTracking()
                    .Where(x => x.TriageDate == triageDate)
                    .OrderBy(x => x.TriageDate)
                    .ThenBy(x => x.Sort)
                    .ToListAsync();
                var result = list.BuildAdapter().AdaptToType<List<ReportTriageCountDto>>();
                result.Add(new ReportTriageCountDto()
                {
                    DeptName = "全院",
                    TriageDate = triageDate,
                    TriageCount = list.Sum(l => l.TriageCount),
                    TriageCountChanged = list.Sum(l => l.TriageCountChanged),
                    Sort = list.Any() ? list.Max(m => m.Sort) + 1 : 1
                });
                return JsonResult<IEnumerable<ReportTriageCountDto>>.Ok(data: result);
            }
            catch (Exception ex)
            {
                return JsonResult<IEnumerable<ReportTriageCountDto>>.Fail(msg: ex.Message);
            }
        }

        /// <summary>
        /// 单日分诊人数数据修改
        /// </summary>
        /// <param name="id"></param>
        /// <param name="triageCountChanged"></param>
        /// <returns></returns>
        public async Task<JsonResult> UpdateTriageCount(Guid id, int? triageCountChanged)
        {
            try
            {
                var item = await _reportTriageCountsRepository.FirstOrDefaultAsync(x => x.Id == id);
                item.TriageCountChanged = triageCountChanged;
                await this._reportTriageCountsRepository.UpdateAsync(item);
            }
            catch (Exception ex)
            {
                return JsonResult.Fail(msg: ex.Message);
            }

            return JsonResult.Ok();
        }

        /// <summary>
        /// 单日分诊人数统计报表生成
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public async Task GenTriageCountReport(string date)
        {
            try
            {
                DateTime triageDate = !string.IsNullOrEmpty(date)
                    ? DateTime.Parse(date).Date
                    : DateTime.Today.AddDays(-1);
                // 删除旧记录
                // _ = this._reportTriageCountsRepository.DeleteAsync(x => x.TriageDate == triageDate);
                // 查询数据
                string sql = @"delete [dbo].[Rpt_TriageCount] where Convert(varchar(10), TriageDate, 120)= '" + triageDate.ToString("yyy-MM-dd") + @"';
                            INSERT INTO [dbo].[Rpt_TriageCount]
                                       ([Id]
                                       ,[DeptName]
                                       ,[TriageDate]
                                       ,[Sort]
                                       ,[TriageCount]
                                       ,[TriageCountChanged])
                              Select NEWID(),
		                            CASE tc.TriageConfigName	WHEN '' THEN N'未知'	ELSE tc.TriageConfigName END DeptName,
		                             ISNULL(Convert(varchar(10), pat.TriageTime, 120) , '" + triageDate.ToString("yyy-MM-dd") + @"') TriageDate,
		                             ROW_NUMBER() over ( order by COUNT(PatientId)) Sort,
		                            Count(PatientId) FeverCount,0
	                          from  Triage_PatientInfo pat   
							 Left Join Triage_ConsequenceInfo conq On pat.Id = conq.PatientInfoId	and	  pat.TriageStatus = 1	-- 已分诊                               
                              And Convert(varchar(10), pat.TriageTime, 120) = '" + triageDate.ToString("yyy-MM-dd") +
                             @"'RIGHT JOIN Dict_TriageConfig tc on conq.TriageDeptCode=tc.TriageConfigCode 
                               where  tc.TriageConfigType='1005' and tc.IsDisable=1 and tc.IsDeleted=0 Group By Convert(varchar(10), pat.TriageTime, 120), tc.TriageConfigName
                                    Order By TriageDate, DeptName";
                var connectionStringKey = _configuration.GetConnectionString("DefaultConnection");
                await _dapperRepository.ExecuteSqlAsync(sql, dbKey: "DefaultConnection",
                    connectionStringKey: connectionStringKey);
            }
            catch (Exception ex)
            {
                _log.LogInformation("Error: {Message}", ex.Message);
                throw;
            }
        }

        /// <summary>
        /// 获取单日分诊人数统计报表表头
        /// </summary>
        /// <returns></returns>
        private List<ReportHeaderDto> GetTriageCountHeader()
        {
            var headerDtoList = new List<ReportHeaderDto>
            {
                new ReportHeaderDto
                {
                    HeaderName = "科室",
                    HeaderField = "DeptName",
                    HeaderSort = "1",
                    HeaderWidth = "220"
                },
                new ReportHeaderDto
                {
                    HeaderName = "统计人数",
                    HeaderField = "TriageCount",
                    HeaderSort = "2",
                    HeaderWidth = "100"
                },
            };
            return headerDtoList;
        }

        /// <summary>
        /// 获取单日分诊人数统计DataTable
        /// </summary>
        /// <returns></returns>
        private DataTable GetTriageCountDataTable()
        {
            DataTable table = new DataTable();
            DataColumn dc = table.Columns.Add("Index", typeof(int));
            dc.AutoIncrement = true;
            dc.AutoIncrementSeed = 1;
            table.Columns.Add("DeptName", typeof(string));
            table.Columns.Add("TriageCount", typeof(int));

            return table;
        }

        /// <summary>
        /// DataTable 填充数据
        /// </summary>
        /// <param name="table"></param>
        /// <param name="item"></param>
        private void FillDataTable(DataTable table, ReportTriageCountDto item)
        {
            DataRow row = table.NewRow();
            row.SetField("DeptName", item.DeptName);
            row.SetField("TriageCount", item.TriageCount);
            table.Rows.Add(row);
        }

        /// <summary>
        /// DataTable 填充数据
        /// </summary>
        /// <param name="table"></param>
        /// <param name="item"></param>
        private void FillDataTable(DataTable table, ReportDeathCountDto item)
        {
            DataRow row = table.NewRow();
            row.SetField("ItemName", item.ItemName);
            row.SetField("DeathCount", item.DeathCountResult);

            table.Rows.Add(row);
        }


        /// <summary>
        /// 抢救区、留观区报表表头
        /// </summary>
        /// <returns></returns>
        private List<ReportHeaderDto> GetRescueAndViewHeader()
        {
            var headerDtoList = new List<ReportHeaderDto>
            {
                new ReportHeaderDto
                {
                    HeaderName = "区域",
                    HeaderField = "AreaName",
                    HeaderSort = "1",
                    HeaderWidth = "220"
                },
                new ReportHeaderDto
                {
                    HeaderName = "项目",
                    HeaderField = "ItemName",
                    HeaderSort = "2",
                    HeaderWidth = "220"
                },
                new ReportHeaderDto
                {
                    HeaderName = "统计人数",
                    HeaderField = "Count",
                    HeaderSort = "3",
                    HeaderWidth = "100"
                },
            };
            return headerDtoList;
        }

        /// <summary>
        /// 抢救区、留观区统计DataTable
        /// </summary>
        /// <returns></returns>
        private DataTable GetRescueAndViewDataTable()
        {
            DataTable table = new DataTable();
            DataColumn dc = table.Columns.Add("Index", typeof(int));
            dc.AutoIncrement = true;
            dc.AutoIncrementSeed = 1;
            table.Columns.Add("AreaName", typeof(string));
            table.Columns.Add("ItemName", typeof(string));
            table.Columns.Add("Count", typeof(int));

            return table;
        }

        /// <summary>
        /// DataTable 填充数据
        /// </summary>
        /// <param name="table"></param>
        /// <param name="item"></param>
        private void FillDataTable(DataTable table, ReportRescueAndViewDto item)
        {
            DataRow row = table.NewRow();
            row.SetField("AreaName", item.AreaName);
            row.SetField("ItemName", item.ItemName);
            row.SetField("Count", item.CountResult);

            table.Rows.Add(row);
        }

        /// <summary>
        /// 获取分诊工作量表头
        /// </summary>
        /// <returns></returns>
        private List<ReportHeaderDto> GetWorkLoadHeader()
        {
            var headerDtoList = new List<ReportHeaderDto>
            {
                new ReportHeaderDto
                {
                    HeaderName = "编号",
                    HeaderField = "rowNum",
                    HeaderSort = "1",
                    HeaderWidth = "100"
                },
                new ReportHeaderDto
                {
                    HeaderName = "分诊人员",
                    HeaderField = "triageUserName",
                    HeaderSort = "2",
                    HeaderWidth = "200"
                },
                new ReportHeaderDto
                {
                    HeaderName = "急诊科室名称",
                    HeaderField = "triageDeptName",
                    HeaderSort = "3",
                    HeaderWidth = "200"
                },
                new ReportHeaderDto
                {
                    HeaderName = "人次",
                    HeaderField = "personCount",
                    HeaderSort = "4",
                    HeaderWidth = "100"
                },
            };
            return headerDtoList;
        }


        /// <summary>
        /// 分诊工作量统计
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="triageUserCode"></param>
        /// <param name="pageNum"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public async Task<WorkLoadStatisticsDto> GetWorkLoadStatisticsAsync(DateTime startDate, DateTime endDate,
            string triageUserCode, int pageNum = 1, int pageSize = 50)
        {
            var str = string.IsNullOrEmpty(triageUserCode) ? " and 1=1" : $"and TriageUserCode='{triageUserCode}'";
            // 查询数据
            string sql =
                $"select  ROW_NUMBER() over ( order by COUNT(TriageUserName)) RowNum, TriageUserName,TriageDeptName,COUNT(TriageUserName) PersonCount  from Triage_PatientInfo p left join Triage_ConsequenceInfo  c on p.Id=c.PatientInfoId where p.TriageStatus=1 and TriageTime>='{startDate}' and TriageTime<='{endDate}' " +
                str + " GROUP BY p.TriageUserName,c.TriageDeptName";
            var connectionStringKey = _configuration.GetConnectionString("DefaultConnection");
            var list = await _dapperRepository.QueryListAsync<WorkLoadInfoDto>(sql, dbKey: "DefaultConnection",
                connectionStringKey: connectionStringKey);
            var result = new WorkLoadStatisticsDto()
            {
                Header = GetWorkLoadHeader(),
                Body = list.Skip((pageNum - 1) * pageSize).Take(pageSize).ToList(),
                TotalCount = list.Count()
            };
            return result;
        }

        /// <summary>
        /// 急诊科每日统计
        /// </summary>
        /// <param name="mouth"></param>
        /// <returns></returns>
        public async Task<Dictionary<string, object>> GetEveryDayStatisticsAsync([Required] string mouth)
        {
            try
            {
                var beginDate = DateTime.Parse(mouth + "-01");
                var endDate = DateTime.Parse(beginDate.AddMonths(1).AddDays(-1).ToShortDateString());
                // 查询数据 科室
                string sql =
                    @"select TriageDeptCode,TriageDeptName,TriageTime,TriageTargetCode,GreenRoadCode,ToHospitalWayCode into #t from Triage_PatientInfo  p left join Triage_ConsequenceInfo c on p.Id=c.PatientInfoId WHERE  p.TriageStatus=1 and  TriageTime>='" +
                    beginDate + @"' and TriageTime<='" + endDate + @"'
                            select 'AllCount' TriageDeptCode,N'急诊分诊总人数' TriageDeptName,CONVERT(VARCHAR(10),TriageTime,120) Date,COUNT(1) PersonCount from #t 
                            GROUP BY CONVERT(VARCHAR(10),TriageTime,120)
                            UNION all
                            SELECT TriageDeptCode,TriageDeptName,CONVERT(VARCHAR(10),TriageTime,120) Date,COUNT(TriageDeptCode) PersonCount from #t  GROUP BY TriageDeptCode,TriageDeptName,CONVERT(VARCHAR(10),TriageTime,120) 
                            UNION all
                            select 'RescueArea' TriageDeptCode,N'进抢救区人数' TriageDeptName,CONVERT(VARCHAR(10),TriageTime,120) Date,COUNT(1) PersonCount from #t 
                            WHERE TriageTargetCode='TriageDirection_003'
                            GROUP BY CONVERT(VARCHAR(10),TriageTime,120) 
                            UNION all
                            select 'GreenRoad' TriageDeptCode,N'开启绿通人数' TriageDeptName,CONVERT(VARCHAR(10),TriageTime,120) Date,COUNT(1) PersonCount from #t where ISNULL(GreenRoadCode,'')!=''
                            GROUP BY CONVERT(VARCHAR(10),TriageTime,120) 
                            UNION all
                            select 'ToHospitalWay' TriageDeptCode,N'120来院次数' TriageDeptName,CONVERT(VARCHAR(10),TriageTime,120) Date,COUNT(1) PersonCount from #t 
                            where  ToHospitalWayCode in ('ToHospitalWay_001','ToHospitalWay_006')
                            GROUP BY CONVERT(VARCHAR(10),TriageTime,120) ";
                var connectionStringKey = _configuration.GetConnectionString("DefaultConnection");
                var list = await _dapperRepository.QueryListAsync<EveryDayStatisticsDto>(sql,
                    dbKey: "DefaultConnection",
                    connectionStringKey: connectionStringKey);
                var dictList = new List<Dictionary<string, object>>();
                list.GroupBy(g => g.TriageDeptName).ForEach(x =>
                {
                    var dict = new Dictionary<string, object> { { "ItemAndDate", x.Key } };
                    x.ForEach(f =>
                    {
                        // 检查是否存在日期键
                        if (!dict.ContainsKey(f.Date))
                        {
                            dict.Add(f.Date, f.PersonCount.ToString());
                        }
                    });
                    dictList.Add(dict);
                });
                var dictResult = new Dictionary<string, object>
                {
                    { "Header", GetEveryDayStatisticsHeader(mouth) },
                    { "Body", dictList }
                };
                return dictResult;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }


        /// <summary>
        /// 急诊每日统计表头
        /// </summary>
        /// <returns></returns>
        private List<ReportHeaderDto> GetEveryDayStatisticsHeader(string mouth)
        {
            var headerDtoList = new List<ReportHeaderDto>
            {
                new ReportHeaderDto
                {
                    HeaderName = "项目/日期",
                    HeaderField = "ItemAndDate",
                    HeaderSort = "1",
                    HeaderWidth = "100"
                }
            };
            var dateList = GetEveryDay(mouth);
            int sort = 1;
            foreach (var date in dateList)
            {
                sort++;
                headerDtoList.Add(new ReportHeaderDto()
                {
                    HeaderName = date,
                    HeaderField = mouth.Split('-')[0] + "-" + date,
                    HeaderSort = sort.ToString(),
                    HeaderWidth = "100"
                });
            }

            return headerDtoList;
        }

        /// <summary>
        /// 根据年月获取月的每日
        /// </summary>
        /// <param name="mouth"></param>
        /// <returns></returns>
        private List<string> GetEveryDay(string mouth)
        {
            var beginDate = DateTime.Parse(mouth + "-01");
            var endDate = DateTime.Parse(beginDate.AddMonths(1).AddDays(-1).ToShortDateString());
            var dateList = new List<string>();
            for (DateTime i = beginDate; i <= endDate; i = i.AddDays(1))
            {
                dateList.Add(i.ToString("MM-dd"));
            }

            return dateList;
        }

    }
}