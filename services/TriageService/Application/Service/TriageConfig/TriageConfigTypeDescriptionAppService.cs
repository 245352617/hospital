using Mapster;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using JsonResult = SamJan.MicroService.PreHospital.Core.JsonResult;
using SamJan.MicroService.PreHospital.Core;

namespace SamJan.MicroService.PreHospital.TriageService
{
    /// <summary>
    /// 院前分诊设置类型接口
    /// </summary>
    [Auth("TriageConfigTypeDescription")]
    [Authorize]
    public class TriageConfigTypeDescriptionAppService : ApplicationService, ITriageConfigTypeDescriptionAppService
    {
        private readonly IRepository<TriageConfigTypeDescription> _triageConfigTypeDescriptionRepository;
        private readonly NLogHelper _log;
        public TriageConfigTypeDescriptionAppService(IRepository<TriageConfigTypeDescription> triageConfigTypeDescriptionRepository,
            NLogHelper log)
        {
            _triageConfigTypeDescriptionRepository = triageConfigTypeDescriptionRepository;
            _log = log;
        }

        /// <summary>
        /// 新增院前分诊设置类型描述
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [Auth("TriageConfigTypeDescription" + PermissionDefinition.Separator + PermissionDefinition.Save)]
        public async Task<JsonResult> SaveTriageConfigTypeDescriptionAsync(CreateTriageConfigTypeDescriptionDto dto)
        {
            try
            {
                var model = dto.BuildAdapter().AdaptToType<TriageConfigTypeDescription>();
                if (model == null) return JsonResult.Fail("数据错误");
                model.SetId(GuidGenerator.Create());
                model.AddUser = CurrentUser.UserName;
                await _triageConfigTypeDescriptionRepository.InsertAsync(model);
                return JsonResult.Ok("成功");

            }
            catch (Exception e)
            {
                _log.Warning($"【TriageConfigTypeDescriptionService】【SaveTriageConfigTypeDescriptionAsync】【保存院前分诊设置类型描述错误】【Msg：{e}】");
                return JsonResult.Fail(e.Message);
            }
        }

        /// <summary>
        /// 修改院前分诊设置类型描述
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [Auth("TriageConfigTypeDescription" + PermissionDefinition.Separator + PermissionDefinition.Update)]
        public async Task<JsonResult> UpdateTriageConfigTypeDescriptionAsync(TriageConfigTypeDescriptionDto dto)
        {
            try
            {
                _log.Info("【TriageConfigTypeDescriptionService】【UpdateTriageConfigTypeDescriptionAsync】【修改院前分诊设置类型描述开始】");
                var model = dto.BuildAdapter().AdaptToType<TriageConfigTypeDescription>();
                if (await _triageConfigTypeDescriptionRepository.AsNoTracking()
                    .FirstOrDefaultAsync(x => x.Id == dto.Id) == null) return JsonResult.Fail("不存在此类型字典");
                model.ModUser = CurrentUser.UserName;
                await _triageConfigTypeDescriptionRepository.UpdateAsync(model);
                return JsonResult.Ok("成功");

            }
            catch (Exception e)
            {
                _log.Warning($"【TriageConfigTypeDescriptionService】【UpdateTriageConfigTypeDescriptionAsync】【修改院前分诊设置类型描述错误】【Msg：{e}】");
                return JsonResult.Fail(e.Message);
            }
        }

        /// <summary>
        /// 删除院前分诊设置类型描述
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Auth("TriageConfigTypeDescription" + PermissionDefinition.Separator + PermissionDefinition.Delete)]
        public async Task<JsonResult> DeleteTriageConfigTypeDescriptionAsync(Guid id)
        {
            try
            {
                _log.Info("【TriageConfigTypeDescriptionService】【DeleteTriageConfigTypeDescriptionAsync】【删除院前分诊设置类型描述开始】");
                var triageConfigTypeDescriptions = await _triageConfigTypeDescriptionRepository.FirstOrDefaultAsync(t => t.Id == id);
                if (triageConfigTypeDescriptions == null) return JsonResult.Fail("数据不存在");
                triageConfigTypeDescriptions.DeleteUser = CurrentUser.UserName;
                await _triageConfigTypeDescriptionRepository.DeleteAsync(triageConfigTypeDescriptions, true);
                return JsonResult.Ok("成功");

            }
            catch (Exception e)
            {
                _log.Warning($"【TriageConfigTypeDescriptionService】【DeleteTriageConfigTypeDescriptionAsync】【删除院前分诊设置类型描述错误】【Msg：{e}】");
                return JsonResult.Fail(e.Message);
            }
        }

        /// <summary>
        /// 获取院前分诊设置类型描述集合
        /// </summary>
        /// <returns></returns>
        [Auth("TriageConfigTypeDescription" + PermissionDefinition.Separator + PermissionDefinition.Get)]
        public async Task<JsonResult> GetTriageConfigTypeDescriptionListAsync()
        {
            try
            {
                var triageConfigTypeDescriptions = await _triageConfigTypeDescriptionRepository.ToListAsync();
                var list = triageConfigTypeDescriptions.OrderBy(t => t.Sort).ToList().BuildAdapter().AdaptToType<List<TriageConfigTypeDescriptionDto>>();
                return JsonResult.Ok(data: list);
            }
            catch (Exception e)
            {
                _log.Warning($"【TriageConfigTypeDescriptionService】【GetTriageConfigTypeDescriptionListAsync】【获取所有的院前分诊设置类型描述错误】【Msg：{e}】");
                return JsonResult.Fail(e.Message);
            }

        }

        /// <summary>
        /// 获取院前分诊设置类型描述详情
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Auth("TriageConfigTypeDescription" + PermissionDefinition.Separator + PermissionDefinition.Get)]
        public async Task<JsonResult> GetTriageConfigTypeDescriptionDetailAsync(Guid id)
        {
            try
            {
                _log.Info("【TriageConfigTypeDescriptionService】【GetTriageConfigTypeDescriptionDetailAsync】【获取院前分诊设置类型描述详情开始】");
                var triageConfigTypeDescriptions = await _triageConfigTypeDescriptionRepository.FirstOrDefaultAsync(t => t.Id == id);
                var dto = triageConfigTypeDescriptions.BuildAdapter().AdaptToType<TriageConfigTypeDescriptionDto>();
                _log.Info("【TriageConfigTypeDescriptionService】【GetTriageConfigTypeDescriptionDetailAsync】【获取院前分诊设置类型描述详情结束】");
                return JsonResult.Ok(data: dto);
            }
            catch (Exception e)
            {
                _log.Warning($"【TriageConfigTypeDescriptionService】【GetTriageConfigTypeDescriptionDetailAsync】【获取院前分诊设置类型描述详情错误】【Msg：{e}】");
                return JsonResult.Fail(e.Message);
            }
        }
    }
}
