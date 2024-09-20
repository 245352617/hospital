using Mapster;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using StackExchange.Redis;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using SamJan.MicroService.PreHospital.Core;

namespace SamJan.MicroService.PreHospital.TriageService
{
    /// <summary>
    /// 评分管理API
    /// </summary>
    [Auth("ScoreManage")]
    [Authorize]
    public class ScoreManageAppService : ApplicationService, IScoreManageAppService
    {
        private readonly IRepository<ScoreManage> _scoreManageRepository;
        private readonly NLogHelper _log;
        private readonly IDatabase _redis;
        private readonly IConfiguration _configuration;

        public ScoreManageAppService(IRepository<ScoreManage> scoreManageRepository,
            NLogHelper log, RedisHelper redisHelper, IConfiguration configuration)
        {
            _scoreManageRepository = scoreManageRepository;
            _log = log;
            _configuration = configuration;
            _redis = redisHelper.GetDatabase();
        }

        /// <summary>
        /// 新增评分管理
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [Auth("ScoreManage" + PermissionDefinition.Separator + PermissionDefinition.Create)]
        public async Task<JsonResult> CreateScoreManageAsync(CreateOrUpdateScoreManageDto dto)
        {
            try
            {
                var model = dto.BuildAdapter().AdaptToType<ScoreManage>();
                if (model == null)
                    return JsonResult.Fail("参数不能为空");
                model.AddUser = CurrentUser.UserName;
                var dbContext = _scoreManageRepository.GetDbContext();
                dbContext.Entry(model).State = EntityState.Added;
                if (await dbContext.SaveChangesAsync() > 0)
                {
                    var cache = model.BuildAdapter().AdaptToType<ScoreManageDto>();
                    var serviceName = _configuration.GetSection("ServiceName").Value;
                    var json = JsonSerializer.Serialize(cache);
                    await _redis.HashSetAsync($"{serviceName}:ScoreManageList", model.Id.ToString(), json);

                    if (await _scoreManageRepository.AsNoTracking().AnyAsync(x => x.Sort == dto.Sort && x.Id != model.Id))
                    {
                        // 对其他的进行重排
                        var listAfter = await _scoreManageRepository.Where(x => x.Sort >= dto.Sort && x.Id != model.Id)
                            .ToListAsync();
                        foreach (var item in listAfter)
                        {
                            item.Sort += 1;
                            var cacheItem = item.BuildAdapter().AdaptToType<ScoreManageDto>();
                            var jsonItem = JsonSerializer.Serialize(cacheItem);
                            await _redis.HashSetAsync($"{serviceName}:ScoreManageList", item.Id.ToString(), jsonItem);
                        }
                    }

                    return JsonResult.Ok();
                }

                _log.Error("【ScoreManageService】【EnableScoreManageAsync】【新增评分管理失败】【Msg：数据库保存数据失败！】");
                return JsonResult.Fail(msg: "新增评分管理失败！请检查或重试！");
            }
            catch (Exception e)
            {
                _log.Warning($"【ScoreManageService】【CreateScoreManageAsync】【添加评分管理错误】【Msg：{e}】");
                return JsonResult.Fail(e.Message);
            }
        }

        /// <summary>
        /// 评分启用禁用
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [Auth("ScoreManage" + PermissionDefinition.Separator + PermissionDefinition.Update)]
        public async Task<JsonResult> EnableScoreManageAsync(ScoreManageInput input)
        {
            try
            {
                _log.Info("【ScoreManageService】【EnableScoreManageAsync】【评分启用禁用开始】");
                var scoreManage =
                    await _scoreManageRepository.AsNoTracking().FirstOrDefaultAsync(t => t.Id == input.Id);
                if (scoreManage == null)
                    return JsonResult.Fail("数据不存在");
                scoreManage.IsEnable = input.IsEnable;
                scoreManage.ModUser = CurrentUser.UserName;
                var dbContext = _scoreManageRepository.GetDbContext();
                dbContext.Entry(scoreManage).State = EntityState.Modified;
                if (await dbContext.SaveChangesAsync() > 0)
                {
                    var cache = scoreManage.BuildAdapter().AdaptToType<ScoreManageDto>();
                    var serviceName = _configuration.GetSection("ServiceName").Value;
                    var json = JsonSerializer.Serialize(cache);
                    await _redis.HashSetAsync($"{serviceName}:ScoreManageList", scoreManage.Id.ToString(), json);
                    _log.Info("【ScoreManageService】【EnableScoreManageAsync】【评分启用禁用结束】");
                    return JsonResult.Ok();
                }

                _log.Error("【ScoreManageService】【EnableScoreManageAsync】【评分启用禁用失败】【Msg：数据库保存数据失败！】");
                return JsonResult.Fail(msg: "评分启用禁用失败！请检查或重试！");
            }
            catch (Exception e)
            {
                _log.Warning($"【ScoreManageService】【EnableScoreManageAsync】【评分启用禁用错误】【Msg：{e}】");
                return JsonResult.Fail(e.Message);
            }
        }

        /// <summary>
        /// 修改评分管理
        /// </summary>
        /// <param name="id"></param>
        /// <param name="dto"></param>
        /// <returns></returns>
        [Auth("ScoreManage" + PermissionDefinition.Separator + PermissionDefinition.Update)]
        public async Task<JsonResult> UpdateScoreManageAsync(Guid id, CreateOrUpdateScoreManageDto dto)
        {
            try
            {
                var scoreManage = await _scoreManageRepository.AsNoTracking().FirstOrDefaultAsync(t => t.Id == id);
                if (scoreManage == null)
                    return JsonResult.Fail("数据不存在");
                var model = dto.BuildAdapter().AdaptToType<ScoreManage>();
                model.SetId(id);
                model.AddUser = scoreManage.AddUser;
                model.ModUser = CurrentUser.UserName;
                var dbContext = _scoreManageRepository.GetDbContext();
                dbContext.Entry(model).State = EntityState.Modified;
                if (await dbContext.SaveChangesAsync() > 0)
                {
                    var cache = model.BuildAdapter().AdaptToType<ScoreManageDto>();
                    var serviceName = _configuration.GetSection("ServiceName").Value;
                    var json = JsonSerializer.Serialize(cache);
                    await _redis.HashSetAsync($"{serviceName}:ScoreManageList", scoreManage.Id.ToString(), json);

                    if (await _scoreManageRepository.AsNoTracking().AnyAsync(x => x.Sort == dto.Sort && x.Id != id))
                    {
                        // 对其他的进行重排
                        var listAfter = await _scoreManageRepository.Where(x => x.Sort >= dto.Sort && x.Id != id)
                        .ToListAsync();
                        foreach (var item in listAfter)
                        {
                            item.Sort += 1;
                            var cacheItem = item.BuildAdapter().AdaptToType<ScoreManageDto>();
                            var jsonItem = JsonSerializer.Serialize(cacheItem);
                            await _redis.HashSetAsync($"{serviceName}:ScoreManageList", item.Id.ToString(), jsonItem);
                        }
                    }
                    return JsonResult.Ok();
                }

                _log.Error("【ScoreManageService】【EnableScoreManageAsync】【修改评分管理失败】【Msg：数据库保存数据失败！】");
                return JsonResult.Fail(msg: "修改评分管理失败！请检查或重试！");
            }
            catch (Exception e)
            {
                _log.Warning($"【ScoreManageService】【UpdateScoreManageAsync】【修改评分管理错误】【Msg：{e}】");
                return JsonResult.Fail(e.Message);
            }
        }

        /// <summary>
        /// 删除评分管理
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Auth("ScoreManage" + PermissionDefinition.Separator + PermissionDefinition.Delete)]
        public async Task<JsonResult> DeleteScoreManageAsync(Guid id)
        {
            try
            {
                _log.Info("【ScoreManageService】【DeleteScoreManageAsync】【删除评分管理开始】");
                var scoreManage = await _scoreManageRepository.AsNoTracking().FirstOrDefaultAsync(t => t.Id == id);
                if (scoreManage == null)
                    return JsonResult.Fail("数据不存在");
                scoreManage.DeleteUser = CurrentUser.UserName;
                var dbContext = _scoreManageRepository.GetDbContext();
                dbContext.Entry(scoreManage).State = EntityState.Deleted;
                if (await dbContext.SaveChangesAsync() > 0)
                {
                    var serviceName = _configuration.GetSection("ServiceName").Value;
                    await _redis.HashDeleteAsync($"{serviceName}:ScoreManageList", scoreManage.Id.ToString());
                    _log.Info("【ScoreManageService】【DeleteScoreManageAsync】【删除评分管理结束】");

                    // 对其他的进行重排
                    var listAfter = await _scoreManageRepository.Where(x => x.Sort >= scoreManage.Sort)
                        .ToListAsync();
                    foreach (var item in listAfter)
                    {
                        item.Sort -= 1;
                        await _redis.HashDeleteAsync($"{serviceName}:ScoreManageList", item.Id.ToString());
                    }

                    return JsonResult.Ok();
                }

                _log.Error("【ScoreManageService】【EnableScoreManageAsync】【删除评分管理失败】【Msg：数据库保存数据失败！】");
                return JsonResult.Fail(msg: "删除评分管理失败！请检查或重试！");
            }
            catch (Exception e)
            {
                _log.Warning($"【ScoreManageService】【DeleteScoreManageAsync】【删除评分管理错误】【Msg：{e}】");
                return JsonResult.Fail(e.Message);
            }
        }


        /// <summary>
        /// 获取评分管理
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Auth("ScoreManage" + PermissionDefinition.Separator + PermissionDefinition.Get)]
        public async Task<JsonResult<ScoreManageDto>> GetScoreManageAsync(Guid id)
        {
            try
            {
                var serviceName = _configuration.GetSection("ServiceName").Value;
                var hasCache = await _redis.HashExistsAsync("{serviceName}:ScoreManageList", id.ToString());
                ScoreManageDto dtos;
                if (hasCache)
                {
                    var json = await _redis.HashGetAsync($"{serviceName}:ScoreManageList", id.ToString());
                    dtos = JsonSerializer.Deserialize<ScoreManageDto>(json);
                }
                else
                {
                    var scoreManage = await _scoreManageRepository.FirstOrDefaultAsync(t => t.Id == id);
                    dtos = scoreManage.BuildAdapter().AdaptToType<ScoreManageDto>();
                    var json = JsonSerializer.Serialize(dtos);
                    await _redis.HashSetAsync($"{serviceName}:ScoreManageList", dtos.Id.ToString(), json);
                }

                return JsonResult<ScoreManageDto>.Ok(data: dtos);
            }
            catch (Exception e)
            {
                _log.Warning($"【ScoreManageService】【GetScoreManageAsync】【获取评分管理错误】【Msg：{e}】");
                return JsonResult<ScoreManageDto>.Fail(e.Message);
            }
        }

        /// <summary>
        /// 获取评分管理列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [Auth("ScoreManage" + PermissionDefinition.Separator + PermissionDefinition.List)]
        public async Task<JsonResult<List<ScoreManageDto>>> GetScoreManageListAsync(ScoreManageWhereInput input)
        {
            try
            {
                var serviceName = _configuration.GetSection("ServiceName").Value;
                List<ScoreManageDto> dtos;
                if (!await _redis.KeyExistsAsync($"{serviceName}:ScoreManageList"))
                {
                    var scoreManageList = await _scoreManageRepository.GetListAsync();
                    dtos = scoreManageList.BuildAdapter().AdaptToType<List<ScoreManageDto>>();
                    var hashList = new HashEntry[dtos.Count];
                    var index = 0;
                    dtos.ForEach(item =>
                    {
                        var json = JsonSerializer.Serialize(item);
                        hashList[index++] = new HashEntry(item.Id.ToString(), json);
                    });

                    await _redis.HashSetAsync($"{serviceName}:ScoreManageList", hashList);
                }
                else
                {
                    var hashList = await _redis.HashValuesAsync($"{serviceName}:ScoreManageList");
                    var json = $"[{string.Join(",", hashList)}]";
                    dtos = JsonSerializer.Deserialize<List<ScoreManageDto>>(json);
                }

                dtos = dtos.WhereIf(!string.IsNullOrEmpty(input.ScoreName),
                        t => t.ScoreName.Contains(input.ScoreName) || input.ScoreName.Contains(t.ScoreName))
                    .WhereIf(!string.IsNullOrEmpty(input.IsEnable), t => t.IsEnable == bool.Parse(input.IsEnable))
                    .OrderBy(o => o.Sort)
                    .ToList();
                return JsonResult<List<ScoreManageDto>>.Ok(data: dtos);
            }
            catch (Exception e)
            {
                _log.Warning($"【ScoreManageService】【GetScoreManageListAsync】【获取评分管理列表错误】【Msg：{e}】");
                return JsonResult<List<ScoreManageDto>>.Fail(e.Message);
            }
        }
    }
}