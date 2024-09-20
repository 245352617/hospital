using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SamJan.MicroService.PreHospital.Core;
using Volo.Abp.Application.Services;
using JsonResult = SamJan.MicroService.PreHospital.Core.JsonResult;

namespace SamJan.MicroService.PreHospital.TriageService
{
    public interface IReportSettingAppService : IApplicationService
    {
        /// <summary>
        /// 获取所有分诊报表设置
        /// </summary>
        /// <param name="isEnabled"> -1:查询所有；0:禁用；1:启用 </param>
        /// <param name="platform">平台标识，1:院前急救，2：预检分诊，3：急诊管理</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<JsonResult<List<ReportSettingListDto>>> GetListAsync(int isEnabled, int type = 0, int platform = -1, CancellationToken cancellationToken = default);

        /// <summary>
        /// 根据报表Id获取报表设置
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<JsonResult<ReportSettingDto>> GetAsync(Guid id, CancellationToken cancellationToken = default);

        /// <summary>
        /// 保存分诊报表设置
        /// </summary>
        /// <param name="dto"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<JsonResult> SaveReportSettingAsync(ReportSettingDto dto, CancellationToken cancellationToken = default);

        /// <summary>
        /// 根据Id删除分诊报表设置
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<JsonResult> DeleteAsync(Guid id, CancellationToken cancellationToken = default);

        /// <summary>
        /// 根据报表设置返回数据
        /// </summary>
        /// <param name="dto"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>返回结果为json字符串</returns>
        Task<JsonResult<PagedReportDataDto>> GetReportDataAsync(ReportQueryOptionsDto dto,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// 根据报表Id初始化报表
        /// </summary>
        /// <param name="reportId">报表Id</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<JsonResult<InitReportDto>> InitialReportSettingAsync(Guid reportId, CancellationToken cancellationToken = default);

        /// <summary>
        /// 到处到Excel
        /// </summary>
        /// <param name="dto"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<IActionResult> ImportDataToExcelAsync(ReportQueryOptionsDto dto, CancellationToken cancellationToken = default);
    }
}