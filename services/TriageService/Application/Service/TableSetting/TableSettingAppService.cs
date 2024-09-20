using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using SamJan.MicroService.PreHospital.Core;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using JsonResult = SamJan.MicroService.PreHospital.Core.JsonResult;

namespace SamJan.MicroService.PreHospital.TriageService
{
    /// <summary>
    /// 表格配置接口
    /// </summary>
    [Auth("TableSetting")]
    [Authorize]
    public class TableSettingAppService : ApplicationService, ITableSettingAppService
    {
        private readonly IRepository<TableSetting> _tableSettingRepository;
        private readonly NLogHelper _log;


        public TableSettingAppService(IRepository<TableSetting> tableSettingRepository, NLogHelper log)
        {
            _tableSettingRepository = tableSettingRepository;
            _log = log;
            //_redis = redisHelper.GetDatabase();
        }
        
        /// <summary>
        /// 通过表格名称，查询单个表格配置
        /// </summary>
        /// <param name="input">表格名称（不含中文）</param>
        /// <returns></returns>
        [Auth("TableSetting"+PermissionDefinition.Separator+PermissionDefinition.List)]
        // [AllowAnonymous]
        public async Task<JsonResult<List<TableSettingOutput>>> QueryTableSetting(QueryTableSettingInput input)
        {
            try
            {
                var output = await _tableSettingRepository.Where(p => p.TableCode == input.TableCode.Trim())
                    .OrderBy(p => p.SequenceNo)
                    .ToListAsync();
                return JsonResult<List<TableSettingOutput>>.Ok(data: output.BuildAdapter().AdaptToType<List<TableSettingOutput>>());
            }
            catch (Exception e)
            {
                _log.Warning($"【TableSettingService】【GetTableSettingAsync】【获取表格配置 {input.TableCode} 失败】【Msg：{e}】");
                return JsonResult<List<TableSettingOutput>>.Fail(e.Message);
            }
        }

        /// <summary>
        /// 保存表格配置集合
        /// </summary>
        /// <returns></returns>
        [Auth("TableSetting"+PermissionDefinition.Separator+PermissionDefinition.Save)]
        public async Task<JsonResult> SaveTableSetting(List<TableSettingOutput> inputs)
        {
            try
            {
                var dbContext = _tableSettingRepository.GetDbContext();
                foreach (var input in inputs)
                {
                    var index = inputs.IndexOf(input);
                    var item = await _tableSettingRepository.FindAsync(p => p.Id == input.Id);
                    item.SequenceNo = index + 1;
                    item.ColumnName = input.ColumnName;
                    item.ColumnWidth = input.ColumnWidth;
                    item.Visible = input.Visible;
                    item.ShowOverflowTooltip = input.ShowOverflowTooltip;
                    item.LastModificationTime = DateTime.Now;
                    item.ModUser = CurrentUser.UserName;
                    dbContext.Entry(item).State = EntityState.Modified;
                }

                await _tableSettingRepository.GetDbContext().SaveChangesAsync();

                return JsonResult.Ok();
            }
            catch (Exception e)
            {
                _log.Warning($"【TableSettingService】【SaveTableSetting】【保存表格配置错误{inputs.FirstOrDefault()?.TableCode}】【Msg：{e}】");
                return JsonResult.Fail(e.Message);
            }
        }

        /// <summary>
        /// 重置表格配置集合
        /// </summary>
        /// <returns></returns>
        [Auth("TableSetting"+PermissionDefinition.Separator+PermissionDefinition.Save)]
        public async Task<JsonResult> ResetTableSetting(QueryTableSettingInput input)
        {
            try
            {
                List<TableSetting> list = await _tableSettingRepository.Where(p => p.TableCode == input.TableCode.Trim()).ToListAsync();

                foreach (TableSetting item in list)
                {
                    item.SequenceNo = item.DefaultSequenceNo;
                    item.ColumnName = item.DefaultColumnName;
                    item.ColumnWidth = item.DefaultColumnWidth;
                    item.Visible = item.DefaultVisible;
                    item.ShowOverflowTooltip = item.DefaultShowOverflowTooltip;
                    item.LastModificationTime = DateTime.Now;
                    item.ModUser = CurrentUser.UserName;
                    await _tableSettingRepository.UpdateAsync(item);
                }

                await _tableSettingRepository.GetDbContext().SaveChangesAsync();

                return JsonResult.Ok();
            }
            catch (Exception e)
            {
                _log.Warning($"【TableSettingService】【SaveTableSetting】【重置表格配置错误{input.TableCode}】【Msg：{e}】");
                return JsonResult.Fail(e.Message);
            }
        }
    }
}