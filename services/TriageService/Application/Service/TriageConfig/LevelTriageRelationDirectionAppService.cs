using Mapster;
using Microsoft.AspNetCore.Mvc;
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
    /// 院前分诊级别关联分诊去向和其他去向接口
    /// </summary>
    [Auth("LevelTriageRelationDirection")]
    [Authorize]
    public class LevelTriageRelationDirectionAppService : ApplicationService, ILevelTriageRelationDirectionAppService
    {
        private readonly IRepository<LevelTriageRelationDirection> _levelTriageRelationDirectionsRepository;
        private readonly NLogHelper _log;

        public LevelTriageRelationDirectionAppService(
            IRepository<LevelTriageRelationDirection> levelTriageRelationDirectionsRepository,
            NLogHelper log)
        {
            _levelTriageRelationDirectionsRepository = levelTriageRelationDirectionsRepository;
            _log = log;
        }

        /// <summary>
        /// 修改分诊级别关联关联去向
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [Auth("LevelTriageRelationDirection" + PermissionDefinition.Separator + PermissionDefinition.Update)]
        public async Task<JsonResult> UpdateLevelTriageRelationDirectionAsync(List<LevelTriageRelationDirectionDto> dto)
        {
            try
            {
                var model = dto.BuildAdapter().AdaptToType<List<LevelTriageRelationDirection>>();
                if (model.Any(w => string.IsNullOrEmpty(w.TriageLevelCode)))
                {
                    return JsonResult.Fail("保存失败，级别编码不能为空");
                }

                model.ForEach(item => { item.ModUser = CurrentUser.UserName; });

                _levelTriageRelationDirectionsRepository.GetDbSet().UpdateRange(model);
                await _levelTriageRelationDirectionsRepository.GetDbContext().SaveChangesAsync();
            
                return JsonResult.Ok("保存成功");
            }
            catch (Exception e)
            {
                _log.Warning("【LevelTriageRelationDirectionService】【UpdateLevelTriageRelationDirectionAsync】" +
                             $"【修改分诊级别关联关联去向错误】【Msg：{e}】");
                return JsonResult.Fail("保存失败，" + e.Message);
            }
        }

        /// <summary>
        /// 获取分诊级别关联去向
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Auth("LevelTriageRelationDirection" + PermissionDefinition.Separator + PermissionDefinition.List)]
        public async Task<JsonResult<List<LevelTriageRelationDirectionDto>>> SelectLevelTriageRelationDirectionAsync()
        {
            try
            {
                var levelTriageRelationDirection = await _levelTriageRelationDirectionsRepository.GetListAsync();
                var dto = levelTriageRelationDirection.OrderBy(t => t.Sort).BuildAdapter()
                    .AdaptToType<List<LevelTriageRelationDirectionDto>>();
                return JsonResult<List<LevelTriageRelationDirectionDto>>.Ok(data: dto);
            }
            catch (Exception e)
            {
                _log.Warning(
                    $"【LevelTriageRelationDirectionService】【UpdateLevelTriageRelationDirectionAsync】【获取分诊级别关联关联去向错误】【Msg：{e}】");
                return JsonResult<List<LevelTriageRelationDirectionDto>>.Fail(e.Message);
            }
        }
    }
}