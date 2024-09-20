using Mapster;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using JsonResult = SamJan.MicroService.PreHospital.Core.JsonResult;
using SamJan.MicroService.PreHospital.Core;

namespace SamJan.MicroService.PreHospital.TriageService
{
    /// <summary>
    /// 快速通道设置API
    /// </summary>
    [Auth("FastTrackSetting")]
    [Authorize]
    public class FastTrackSettingAppService : ApplicationService, IFastTrackSettingAppService
    {
        private readonly IRepository<FastTrackSetting> _fastTrackSettingRepository;
        private readonly ILogger<FastTrackSettingAppService> _log;

        public FastTrackSettingAppService(IRepository<FastTrackSetting> fastTrackSettingRepository, ILogger<FastTrackSettingAppService> log)
        {
            _fastTrackSettingRepository = fastTrackSettingRepository;
            _log = log;
        }

        /// <summary>
        /// 保存快速通道设置
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [Auth("FastTrackSetting" + PermissionDefinition.Separator + PermissionDefinition.Create,"保存快速通道设置")]
        public async Task<JsonResult> CreateFastTrackSettingAsync(CreateOrUpdateFastTrackSettingDto dto)
        {
            try
            {
                _log.LogInformation("保存快速通道设置开始");
                var model = dto.BuildAdapter().AdaptToType<FastTrackSetting>();
                if (model == null)
                {
                    _log.LogError("保存快速通道设置失败！原因：{Msg}","数据为空");
                    return JsonResult.Fail("不能保存空数据！");
                }
                var entity = await _fastTrackSettingRepository.IgnoreQueryFilters().AsNoTracking()
                    .FirstOrDefaultAsync(x => x.FastTrackName == model.FastTrackName);
                var dbContext = _fastTrackSettingRepository.GetDbContext();
                model.PhoneAndName = model.FastTrackName + ":" + model.FastTrackPhone;
                if (entity != null)
                {
                    model.SetId(entity.Id);
                    model.AddUser = entity.AddUser;
                    model.CreationTime = entity.CreationTime;
                    model.LastModificationTime = DateTime.Now;
                    model.ModUser = CurrentUser.UserName;
                    dbContext.Entry(model).State = EntityState.Modified;
                }
                else
                {
                    model.AddUser = CurrentUser.UserName;
                    dbContext.Entry(model).State = EntityState.Added;
                }

                if (await dbContext.SaveChangesAsync() > 0)
                {
                    _log.LogInformation("保存快速通道设置成功");
                    return JsonResult.Ok();
                }

                _log.LogError("保存快速通道设置失败！原因：{Msg}","数据库保存数据失败");
                return JsonResult.Fail("保存数据失败！请检查后重试！");
            }
            catch (Exception e)
            {
                _log.LogError("保存快速通道设置错误！原因：{Msg}",e);
                return JsonResult.Fail(e.Message);
            }
        }

        /// <summary>
        /// 更新快速通道设置
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [Auth("FastTrackSetting" + PermissionDefinition.Separator + PermissionDefinition.Update,"更新快速通道设置")]
        public async Task<JsonResult> UpdateFastTrackSettingAsync(CreateOrUpdateFastTrackSettingDto dto)
        {
            try
            {
                _log.LogInformation("更新快速通道设置开始");
                var fastTrackSetting = await _fastTrackSettingRepository.AsNoTracking()
                    .FirstOrDefaultAsync(t => t.Id == dto.Id);
                if (fastTrackSetting == null)
                {
                    _log.LogError("更新快速通道设置失败！原因：{Msg}  Id：{Id}","不存在此快速通道设置",dto.Id);
                    return JsonResult.Fail("不存在此快速通道设置！");
                }
                    
                var model = dto.BuildAdapter().AdaptToType<FastTrackSetting>();
                model.PhoneAndName = model.FastTrackName + ":" + model.FastTrackPhone;
                model.ModUser = CurrentUser.UserName;
                await _fastTrackSettingRepository.UpdateAsync(model);
                _log.LogInformation("更新快速通道设置成功");
                return JsonResult.Ok();
            }
            catch (Exception e)
            {
                _log.LogError("更新快速通道设置错误！原因：{Msg}",e);
                return JsonResult.Fail(e.Message);
            }
        }

        /// <summary>
        /// 快速通道设置启用禁用
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [Auth("FastTrackSetting" + PermissionDefinition.Separator + PermissionDefinition.Update)]
        public async Task<JsonResult> IsEnableFastTrackSettingAsync(CreateOrUpdateFastTrackSettingDto dto)
        {
            try
            {
                _log.LogInformation("快速通道设置{IsEnabled}开始", dto.IsEnable ? "启用" : "禁用");
                var fastTrackSetting = await _fastTrackSettingRepository.FirstOrDefaultAsync(t => t.Id == dto.Id);
                if (fastTrackSetting == null)
                {
                    _log.LogInformation("快速通道设置{IsEnabled}失败！原因：{Msg} Id：{Id}", dto.IsEnable ? "启用" : "禁用",
                        "不存在此快速通道设置", dto.Id);
                    return JsonResult.Fail("不存在此快速通道设置！");
                }

                fastTrackSetting.IsEnable = dto.IsEnable;
                fastTrackSetting.ModUser = CurrentUser.UserName;
                await _fastTrackSettingRepository.UpdateAsync(fastTrackSetting);
                _log.LogInformation("快速通道设置{IsEnabled}成功", dto.IsEnable ? "启用" : "禁用");
                return JsonResult.Ok();
            }
            catch (Exception e)
            {
                _log.LogError(e,"快速通道设置{IsEnabled}错误！原因：{Msg}", dto.IsEnable ? "启用" : "禁用",e);
                return JsonResult.Fail(e.Message);
            }
        }

        /// <summary>
        /// 删除快速通道设置
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [Auth("FastTrackSetting" + PermissionDefinition.Separator + PermissionDefinition.Delete)]
        public async Task<JsonResult> DeleteFastTrackSettingAsync(FastTrackSettingWhereInput input)
        {
            try
            {
                _log.LogInformation("删除快速通道设置开始");
                var fastTrackSetting = await _fastTrackSettingRepository.FirstOrDefaultAsync(t => t.Id == input.Id);
                if (fastTrackSetting == null)
                {
                    _log.LogError("删除快速通道设置失败！原因：{Msg} Id：{Id}", "不存在此快速通道设置", input.Id);
                    return JsonResult.Fail("不存在此快速通道设置");
                }

                fastTrackSetting.DeleteUser = CurrentUser.UserName;
                await _fastTrackSettingRepository.DeleteAsync(fastTrackSetting);
                _log.LogInformation("删除快速通道设置成功");
                return JsonResult.Ok();
            }
            catch (Exception e)
            {
                _log.LogError("删除快速通道设置错误！原因：{Msg}",e);
                return JsonResult.Fail(e.Message);
            }
        }

        /// <summary>
        /// 查询快速通道设置
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [Auth("FastTrackSetting" + PermissionDefinition.Separator + PermissionDefinition.Get)]
        public async Task<JsonResult<CreateOrUpdateFastTrackSettingDto>> GetFastTrackSettingAsync(FastTrackSettingWhereInput input)
        {
            try
            {
                _log.LogInformation("查询快速通道设置开始");
                var fastTrackSetting = await _fastTrackSettingRepository.AsNoTracking()
                    .FirstOrDefaultAsync(t => t.Id == input.Id);
                if (fastTrackSetting == null)
                {
                    _log.LogError("查询快速通道设置失败！原因：{Msg}","未查询到相应的快速通道设置");
                    return JsonResult<CreateOrUpdateFastTrackSettingDto>.Fail("未查询到相应的快速通道设置");
                }

                var dtos = fastTrackSetting.BuildAdapter().AdaptToType<CreateOrUpdateFastTrackSettingDto>();
                _log.LogInformation("查询快速通道设置成功");
                return JsonResult<CreateOrUpdateFastTrackSettingDto>.Ok(data: dtos);
            }
            catch (Exception e)
            {
                _log.LogError("查询快速通道设置错误！原因：{Msg}",e);
                return JsonResult<CreateOrUpdateFastTrackSettingDto>.Fail(e.Message);
            }
        }

        /// <summary>
        /// 查询快速通道设置列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [Auth("FastTrackSetting" + PermissionDefinition.Separator + PermissionDefinition.List)]
        public async Task<JsonResult<List<CreateOrUpdateFastTrackSettingDto>>> GetFastTrackSettingListAsync(
            FastTrackSettingWhereInput input)
        {
            try
            {
                _log.LogInformation("查询快速通道设置列表开始");
                var fastTrackSettingList = await _fastTrackSettingRepository
                    .WhereIf(!string.IsNullOrEmpty(input.PhoneOrName),
                        t => t.FastTrackName.Contains(input.PhoneOrName) ||
                             t.FastTrackPhone.Contains(input.PhoneOrName))
                    .WhereIf(!string.IsNullOrEmpty(input.IsEnable), t => t.IsEnable == bool.Parse(input.IsEnable))
                    .ToListAsync();
                var dtos = fastTrackSettingList.BuildAdapter().AdaptToType<List<CreateOrUpdateFastTrackSettingDto>>();
                _log.LogInformation("查询快速通道设置列表成功");
                return JsonResult<List<CreateOrUpdateFastTrackSettingDto>>.Ok(data: dtos);
            }
            catch (Exception e)
            {
                _log.LogError("查询快速通道设置列表错误！原因：{Msg}", e);
                return JsonResult<List<CreateOrUpdateFastTrackSettingDto>>.Fail(e.Message);
            }
        }
    }
}