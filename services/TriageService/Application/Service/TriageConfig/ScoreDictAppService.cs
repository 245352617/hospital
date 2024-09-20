using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SamJan.MicroService.PreHospital.Core;
using StackExchange.Redis;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using JsonResult = SamJan.MicroService.PreHospital.Core.JsonResult;

namespace SamJan.MicroService.PreHospital.TriageService
{
    /// <summary>
    /// 评分字典接口
    /// </summary>
    public class ScoreDictAppService:ApplicationService,IScoreDictAppService
    {
        private readonly ILogger<ScoreDictAppService> _log;
        private readonly IRepository<ScoreDict, Guid> _repository;
        private readonly IDatabase _redis;

        public ScoreDictAppService(ILogger<ScoreDictAppService> log, IRepository<ScoreDict, Guid> repository, RedisHelper redisHelper)
        {
            _log = log;
            _repository = repository;
            _redis = redisHelper.GetDatabase();
        }
        
        /// <summary>
        /// 获取评分字典树
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<JsonResult<List<ScoreDictDto>>> GetTreeAsync(ScoreDictInput input)
        {
            try
            {
                _log.LogInformation("获取评分字典树开始");
                List<ScoreDictDto> dtoList;
                var redisKey = AppSettings.TriageService + AppSettings.ScoreDictRedisKey;
                if (!await _redis.KeyExistsAsync(redisKey))
                {
                    var list = await _repository.GetListAsync();
                    dtoList = list.BuildAdapter().AdaptToType<List<ScoreDictDto>>();
                    dtoList = dtoList.OrderByDescending(o => o.Level)
                        .ThenBy(o => o.ParentId)
                        .ThenBy(o => o.Sort)
                        .ToList();
                    var hashList = new HashEntry [list.Count];
                    for (int i = 0; i < dtoList.Count; i++)
                    {
                        hashList[i] = new HashEntry(dtoList[i].Id.ToString(), JsonHelper.SerializeObject(dtoList[i]));
                    }

                    await _redis.HashSetAsync(redisKey, hashList);
                }
                else
                {
                    var values = await _redis.HashValuesAsync(redisKey);
                    string json = "[" + string.Join(",",values) + "]";
                    dtoList = JsonHelper.DeserializeObject<List<ScoreDictDto>>(json);
                }

                dtoList = dtoList.WhereIf(input.IsEnabled != -1, x => x.IsEnabled == Convert.ToBoolean(input.IsEnabled))
                    .WhereIf(input.Level > 0, x => x.Level == input.Level)
                    .ToList();
                dtoList = GetScoreDictTree(dtoList);
                dtoList = dtoList.WhereIf(!input.Category.IsNullOrWhiteSpace(), x => x.Category == input.Category)
                    .ToList();
                _log.LogInformation("获取评分字典树成功");
                return JsonResult<List<ScoreDictDto>>.Ok(data: dtoList);
            }
            catch (Exception e)
            {
                _log.LogError("获取评分字典树错误，原因：{Msg}",e);
                return JsonResult<List<ScoreDictDto>>.Fail(e.Message);
            }
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<JsonResult> SaveAsync(ScoreDictDto dto)
        {
            try
            {
                _log.LogInformation("保存评分字典开始");
                var scoreDict = await _repository.FirstOrDefaultAsync(x => x.Id == dto.Id);
                var dbContext = _repository.GetDbContext();
                if (scoreDict == null)
                {
                    scoreDict = dto.BuildAdapter().AdaptToType<ScoreDict>().SetId(GuidGenerator.Create());
                    dbContext.Entry(scoreDict).State = EntityState.Added;
                }
                else
                {
                    scoreDict = dto.BuildAdapter().AdaptToType<ScoreDict>().SetId(scoreDict.Id);
                    dbContext.Entry(scoreDict).State = EntityState.Modified;
                }

                if (await dbContext.SaveChangesAsync() > 0)
                {
                    dto.Id = scoreDict.Id;
                    await _redis.HashSetAsync(AppSettings.TriageService + AppSettings.ScoreDictRedisKey,
                        scoreDict.Id.ToString(), JsonHelper.SerializeObject(dto));
                    _log.LogInformation("保存评分字典成功");
                    return JsonResult.Ok();
                }
                
                _log.LogError("保存评分字典失败，原因：数据库保存数据失败");
                return JsonResult.Fail("保存失败，请重试！");
            }
            catch (Exception e)
            {
                _log.LogError("保存评分字典树错误，原因：{Msg}",e);
                return JsonResult.Fail(e.Message);
            }
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        [HttpPost]
        public async Task<JsonResult> DeleteAsync(Guid id)
        {
            try
            {
                _log.LogInformation("删除评分字典开始");
                var scoreDict = await _repository.FirstOrDefaultAsync(x => x.Id == id);
                if (scoreDict == null)
                {
                    _log.LogError("删除评分字典失败，原因：该评分字典项已经被删除");
                    return JsonResult.Fail("该评分字典项已经被删除");
                }

                var dbContext = _repository.GetDbContext();
                dbContext.Entry(scoreDict).State = EntityState.Deleted;
                if (await dbContext.SaveChangesAsync() > 0)
                {
                    await _redis.HashDeleteAsync(AppSettings.TriageService + AppSettings.ScoreDictRedisKey,
                        id.ToString());
                    _log.LogInformation("删除评分字典成功");
                    return JsonResult.Ok();
                }

                _log.LogError("删除评分字典失败，原因：数据库删除数据失败");
                return JsonResult.Fail("删除失败，请重试！");
            }
            catch (Exception e)
            {
                _log.LogError("删除评分字典树错误，原因：{Msg}",e);
                return JsonResult.Fail(e.Message);
            }
        }
        
        /// <summary>
        /// 递归生成评分字典树
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        private List<ScoreDictDto> GetScoreDictTree(IEnumerable<ScoreDictDto> list)
        {
            list = list.OrderByDescending(o => o.Level)
                .ThenBy(o=>o.ParentId)
                .ThenBy(o=>o.Sort)
                .ToList();
            var minLevel = int.MaxValue;
            foreach (var t in list)
            {
                var parent = list.FirstOrDefault(x => x.Id == t.ParentId);
                if (parent != null)
                {
                    parent.Children ??= new List<ScoreDictDto>();
                    parent.Children.Add(t);
                }

                if (t.Level <= minLevel)
                {
                    minLevel = t.Level;
                }
            }

            return list.Where(x => x.Level == minLevel).ToList();
        }
    }
}