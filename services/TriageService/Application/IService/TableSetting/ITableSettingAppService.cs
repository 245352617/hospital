using System.Collections.Generic;
using System.Threading.Tasks;
using SamJan.MicroService.PreHospital.Core;
using Volo.Abp.Application.Services;

namespace SamJan.MicroService.PreHospital.TriageService
{
    public interface ITableSettingAppService : IApplicationService
    {
        /// <summary>
        /// 通过表格名称，查询单个表格配置
        /// </summary>
        /// <param name="input">表格名称（不含中文）</param>
        /// <returns></returns>
        Task<JsonResult<List<TableSettingOutput>>> QueryTableSetting(QueryTableSettingInput input);

        /// <summary>
        /// 保存表格配置集合
        /// </summary>
        /// <returns></returns>
        Task<JsonResult> SaveTableSetting(List<TableSettingOutput> inputs);

        /// <summary>
        /// 重置表格配置
        /// </summary>
        /// <returns></returns>
        Task<JsonResult> ResetTableSetting(QueryTableSettingInput input);
    }
}