using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text.Json;
using System.Threading.Tasks;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SamJan.MicroService.PreHospital.Core;
using StackExchange.Redis;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Uow;
using JsonResult = SamJan.MicroService.PreHospital.Core.JsonResult;

namespace SamJan.MicroService.PreHospital.TriageService
{
    /// <summary>
    /// 院前分诊判定依据接口
    /// </summary>
    [Auth("Judgment")]
    [Authorize]
    public class JudgmentAppService : ApplicationService, IJudgmentAppService
    {
        private readonly IRepository<JudgmentType> _judgmentTypeRepository;
        private readonly IRepository<JudgmentMaster> _judgmentMasterRepository;
        private readonly IRepository<JudgmentItem> _judgmentItemRepository;
        private readonly ITriageConfigAppService _triageConfigService;
        private readonly IDatabase _redis;
        private readonly IConfiguration _configuration;
        private readonly NLogHelper _log;

        public JudgmentAppService(IRepository<JudgmentType> judgmentTypeRepository,
            IRepository<JudgmentMaster> judgmentMasterRepository,
            IRepository<JudgmentItem> judgmentItemRepository,
            NLogHelper log, RedisHelper redisHelper, IConfiguration configuration,
            ITriageConfigAppService triageConfigService)
        {
            _judgmentTypeRepository = judgmentTypeRepository;
            _judgmentMasterRepository = judgmentMasterRepository;
            _judgmentItemRepository = judgmentItemRepository;
            _log = log;
            _configuration = configuration;
            _triageConfigService = triageConfigService;
            _redis = redisHelper.GetDatabase();
        }


        /// <summary>
        /// 获取所有判定依据及子项
        /// </summary>
        /// <param name="isEnabled"></param>
        /// <returns></returns>
        [Auth("Judgment" + PermissionDefinition.Separator + PermissionDefinition.List)]
        public async Task<JsonResult<List<JudgmentTypeDto>>> GetJudgmentDetailListAsync(int isEnabled = -1)
        {
            try
            {
                var serviceName = _configuration.GetSection("ServiceName").Value;
                var dtos = new List<JudgmentTypeDto>();
                if (await _redis.KeyExistsAsync($"{serviceName}:Judgment"))
                {
                    var array = await _redis.HashValuesAsync($"{serviceName}:Judgment");
                    var json = $"[{string.Join(",", array)}]";
                    dtos.AddRange(JsonSerializer.Deserialize<List<JudgmentTypeDto>>(json));
                }
                else
                {
                    var judgmentTypes = await _judgmentTypeRepository.ToListAsync();
                    var judgmentMasters = await _judgmentMasterRepository.Where(x => !x.IsDeleted)
                        .Include(c => c.JudgmentItems)
                        .OrderBy(o => o.Sort)
                        .ToListAsync();
                    // TODO: 使用导航属性不会自动过滤 Deleted 软删除的数据，需要手动过滤

                    foreach (var item in judgmentTypes.OrderBy(o => o.Sort))
                    {
                        item.JudgmentMasters = judgmentMasters.FindAll(x => x.JudgmentTypeId == item.Id);
                    }

                    dtos = judgmentTypes.BuildAdapter().AdaptToType<List<JudgmentTypeDto>>();
                    var typeHashEntries = new HashEntry[dtos.Count];
                    var index = 0;
                    dtos.ForEach(item =>
                    {
                        var json = JsonSerializer.Serialize(item);
                        typeHashEntries[index] = new HashEntry(item.Id.ToString(), json);
                        index++;
                    });

                    await _redis.HashSetAsync($"{serviceName}:Judgment", typeHashEntries);
                    //不设置过期时间是因为每次数据更新会一起更新缓存，所以去掉过期时间观察有无问题
                    /*await _redis.KeyExpireAsync($"{serviceName}:Judgment",
                        TimeSpan.FromDays(7) + TimeSpan.FromHours(new Random().Next(0, 5)));*/
                }

                var dict = await _triageConfigService.GetTriageConfigByRedisAsync(TriageDict.TriageDepartment
                    .ToString());
                dtos = dtos.WhereIf(isEnabled != -1, x => x.IsEnabled == Convert.ToBoolean(isEnabled))?.ToList();
                dtos?.ForEach(item =>
                {
                    if (item.Children == null || item.Children.Count <= 0)
                    {
                        return;
                    }

                    item.Children.ForEach(master =>
                    {
                        //_log.Info($"【master：{JsonHelper.SerializeObject(master)}】");
                        if (master.Children == null || master.Children.Count <= 0) return;
                        var itemList = master.Children.WhereIf(isEnabled != -1,
                            x => x.IsEnabled == Convert.ToBoolean(isEnabled));
                        if (itemList != null)
                        {
                            master.Children = itemList.OrderBy(o => o.Sort).ToList();
                        }
                    });

                    var masterList = item.Children.WhereIf(isEnabled != -1,
                        x => x.IsEnabled == Convert.ToBoolean(isEnabled));
                    item.Children = masterList?.OrderBy(o => o.Sort).ToList();
                    item.TriageDeptName = dict[TriageDict.TriageDepartment.ToString()]
                        .FirstOrDefault(x => x.TriageConfigCode == item.TriageDeptCode)?.TriageConfigName;
                });

                return JsonResult<List<JudgmentTypeDto>>.Ok(data: dtos?.OrderBy(o => o.Sort).ToList());
            }
            catch (Exception e)
            {
                _log.Warning($"【JudgmentService】【GetJudgmentDetailListAsync】【获取判定依据详细错误】【Msg：{e}】");
                return JsonResult<List<JudgmentTypeDto>>.Fail(e.Message);
            }
        }

        /// <summary>
        /// 新增判定依据分类
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [Auth("Judgment" + PermissionDefinition.Separator + PermissionDefinition.Create)]
        public async Task<JsonResult> CreateJudgmentTypeAsync(List<CreateOrUpdateJudgmentTypeDto> dto)
        {
            try
            {
                var model = dto.BuildAdapter().AdaptToType<List<JudgmentType>>();
                var splicingName = "";
                model.ForEach(item =>
                {
                    item.AddUser = CurrentUser.UserName;
                    item.SetId(GuidGenerator.Create()).GetPy();
                    splicingName += item.DeptName + ",";
                });
                Expression<Func<JudgmentType, bool>> first = x => false;
                var param = Expression.Parameter(typeof(JudgmentType), "x");
                Expression body = Expression.Invoke(first, param);
                body = splicingName.Split(",")
                    .Select(s => (Expression<Func<JudgmentType, bool>>)(x => x.DeptName == s))
                    .Aggregate(body,
                        (current, expression) => Expression.OrElse(current, Expression.Invoke(expression, param)));
                var lambda = Expression.Lambda<Func<JudgmentType, bool>>(body, param);
                if (await _judgmentTypeRepository.CountAsync(lambda) > 0)
                {
                    return JsonResult.Fail("名称已存在");
                }

                await _judgmentTypeRepository.GetDbSet().AddRangeAsync(model);
                await _judgmentTypeRepository.GetDbContext().SaveChangesAsync();
                // 更新Redis缓存
                var typeDtos = model.BuildAdapter().AdaptToType<List<JudgmentTypeDto>>();
                await OperationJudgmentRedisCacheAsync(2, typeDtos: typeDtos);
                return JsonResult.Ok();
            }
            catch (Exception e)
            {
                _log.Warning("【JudgmentService】【GetAllJudgmentDetailsAsync】" +
                             $"【新增判定依据类型错误】【Msg：{e}】");
                return JsonResult.Fail(e.Message);
            }
        }

        /// <summary>
        /// 根据Id删除判定依据分类
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        //[UnitOfWork]
        [Auth("Judgment" + PermissionDefinition.Separator + PermissionDefinition.Delete)]
        public async Task<JsonResult> DeleteJudgmentTypeAsync(Guid id)
        {
            try
            {
                var model = await _judgmentTypeRepository.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
                if (model != null)
                {
                    await _judgmentTypeRepository.DeleteAsync(x => x.Id == id);
                    var masterIds = await _judgmentMasterRepository.Where(x => x.JudgmentTypeId == id)
                        .Select(s => s.Id)
                        .ToListAsync();
                    await _judgmentMasterRepository.DeleteAsync(x => masterIds.Contains(x.Id));
                    await _judgmentItemRepository.DeleteAsync(x => masterIds.Contains(x.JudgmentMasterId));
                    //await CurrentUnitOfWork.CompleteAsync();
                    await _redis.HashDeleteAsync("TriageService:Judgment", id.ToString());
                    return JsonResult.Ok();
                }

                _log.Error("【JudgmentService】【DeleteJudgmentTypeAsync】【根据Id删除判定依据类型失败】【Msg：该判定依据类型不存在！】");
                return JsonResult.Fail("该判定依据类型不存在！");
            }
            catch (Exception e)
            {
                _log.Warning($"【JudgmentService】【DeleteJudgmentTypeAsync】【根据Id删除判定依据类型错误】【Msg：{e}】");
                return JsonResult.Fail(e.Message);
            }
        }

        /// <summary>
        /// 根据Id更新判定依据分类
        /// </summary>
        /// <param name="id"></param>
        /// <param name="dto"></param>
        /// <returns></returns>
        //[UnitOfWork]
        [Auth("Judgment" + PermissionDefinition.Separator + PermissionDefinition.Update)]
        public async Task<JsonResult> UpdateJudgmentTypeAsync(Guid id, CreateOrUpdateJudgmentTypeDto dto)
        {
            try
            {

                var judgmentType = await _judgmentTypeRepository
                    .AsNoTracking()
                    .FirstOrDefaultAsync(x => x.Id == id);
                if (await _judgmentTypeRepository.CountAsync(t => dto.ItemName == t.DeptName && t.Id != id) > 0)
                {
                    return JsonResult.Fail("名称已存在");
                }

                if (judgmentType != null)
                {
                    var model = dto.BuildAdapter().AdaptToType<JudgmentType>();
                    model.SetId(id).GetPy();
                    model.AddUser = judgmentType.AddUser;
                    model.ModUser = CurrentUser.UserName;
                    await _judgmentTypeRepository.UpdateAsync(model);

                    // 若判定类型改变了启用状态，则判决依据主诉、项目也应跟随改变启用状态
                    if (Convert.ToBoolean(judgmentType.IsEnabled) != dto.IsEnabled)
                    {
                        var masters = await _judgmentMasterRepository.Where(x => x.JudgmentTypeId == id)
                            .ToListAsync();
                        var masterDbContext = _judgmentMasterRepository.GetDbContext();
                        masters.ForEach(item =>
                        {
                            item.IsEnabled = model.IsEnabled;
                            item.ModUser = CurrentUser.UserName;
                            masterDbContext.Entry(item).State = EntityState.Modified;
                        });

                        await masterDbContext.SaveChangesAsync();
                        var masterIds = masters.Select(s => s.Id).ToList();
                        var items = await _judgmentItemRepository.Where(x => masterIds.Contains(x.JudgmentMasterId))
                            .ToListAsync();
                        var itemDbContext = _judgmentItemRepository.GetDbContext();
                        items.ForEach(item =>
                        {
                            item.IsEnabled = model.IsEnabled;
                            item.ModUser = CurrentUser.UserName;
                            itemDbContext.Entry(item).State = EntityState.Modified;
                        });

                        await itemDbContext.SaveChangesAsync();
                    }

                    //await CurrentUnitOfWork.CompleteAsync();
                    var typeDto = model.BuildAdapter().AdaptToType<JudgmentTypeDto>();
                    var list = new List<JudgmentTypeDto> { typeDto };
                    await OperationJudgmentRedisCacheAsync(1, typeDtos: list);
                    return JsonResult.Ok();
                }

                _log.Error("【JudgmentService】【UpdateJudgmentTypeAsync】【根据Id更新判定依据类型失败】" +
                           $"【该判定依据类型不存在 ID：{id}】");
                return JsonResult.Fail("该判定依据类型不存在！");
            }
            catch (Exception e)
            {
                _log.Warning($"【JudgmentService】【UpdateJudgmentTypeAsync】【根据Id更新判定依据类型错误】【Msg：{e}】");
                return JsonResult.Fail(e.Message);
            }
        }

        /// <summary>
        /// 根据Id获取判定依据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Auth("Judgment" + PermissionDefinition.Separator + PermissionDefinition.Get)]
        public async Task<JsonResult<JudgmentTypeDto>> GetJudgmentDetailAsync(Guid id)
        {
            try
            {
                bool First(JudgmentTypeDto x) => x.Id == id;
                var dtos = await OperationJudgmentRedisCacheAsync(3, First);
                if (dtos != null)
                {
                    var dto = dtos.FirstOrDefault();
                    return JsonResult<JudgmentTypeDto>.Ok(data: dto);
                }

                _log.Error("【JudgmentService】【GetJudgmentDetailAsync】【根据Id获取判定依据类型错误】【Msg：从缓存获取判定依据错误】");
                return JsonResult<JudgmentTypeDto>.Fail("未查询到数据");
            }
            catch (Exception e)
            {
                _log.Warning($"【JudgmentService】【GetJudgmentDetailAsync】【根据Id获取判定依据类型错误】【Msg：{e}】");
                return JsonResult<JudgmentTypeDto>.Fail(e.Message);
            }
        }

        /// <summary>
        /// 新增判定依据主诉
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [Auth("Judgment" + PermissionDefinition.Separator + PermissionDefinition.Create)]
        public async Task<JsonResult> CreateJudgmentMasterAsync(List<CreateOrUpdateJudgmentMasterDto> dto)
        {
            try
            {
                var model = dto.BuildAdapter().AdaptToType<List<JudgmentMaster>>();
                model.ForEach(item =>
                {
                    item.AddUser = CurrentUser.UserName;
                    item.SetId(GuidGenerator.Create()).GetPy();
                });
                //判断同级（同一分类）下名称是否相等，相等则无法新增
                Expression<Func<JudgmentMaster, bool>> first = x => false;
                var param = Expression.Parameter(typeof(JudgmentMaster), "x");
                Expression body = Expression.Invoke(first, param);
                body = dto.Select(s => (Expression<Func<JudgmentMaster, bool>>)(x =>
                       x.ItemDescription == s.ItemName && x.JudgmentTypeId == s.JudgmentTypeId))
                    .Aggregate(body,
                        (current, expression) => Expression.OrElse(current, Expression.Invoke(expression, param)));
                var lambda = Expression.Lambda<Func<JudgmentMaster, bool>>(body, param);
                if (await _judgmentMasterRepository.CountAsync(lambda) > 0)
                {
                    return JsonResult.Fail("名称已存在");
                }

                await _judgmentMasterRepository.GetDbSet().AddRangeAsync(model);
                await _judgmentMasterRepository.GetDbContext().SaveChangesAsync();
                var dtos = model.BuildAdapter().AdaptToType<List<JudgmentMasterDto>>();
                await OperationJudgmentRedisCacheAsync(2, masterDtos: dtos);
                return JsonResult.Ok();
            }
            catch (Exception e)
            {
                _log.Warning($"【JudgmentService】【CreateJudgmentMasterAsync】【新增判定依据主诉分类错误】【Msg：{e}】");
                return JsonResult.Fail(e.Message);
            }
        }

        /// <summary>
        /// 根据Id删除判定依据主诉
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [UnitOfWork]
        [Auth("Judgment" + PermissionDefinition.Separator + PermissionDefinition.Delete)]
        public async Task<JsonResult> DeleteJudgmentMasterAsync(Guid id)
        {
            try
            {
                var model = await _judgmentMasterRepository.FirstOrDefaultAsync(x => x.Id == id);
                if (model != null)
                {
                    await _judgmentMasterRepository.DeleteAsync(x => x.Id == id);
                    await _judgmentItemRepository.DeleteAsync(x => x.JudgmentMasterId == id);
                    await CurrentUnitOfWork.CompleteAsync();
                    var dto = model.BuildAdapter().AdaptToType<JudgmentMasterDto>();
                    var list = new List<JudgmentMasterDto> { dto };
                    await OperationJudgmentRedisCacheAsync(0, masterDtos: list);
                    return JsonResult.Ok();
                }

                _log.Error("【JudgmentService】【DeleteJudgmentMasterAsync】【根据Id删除判定依据主诉分类失败】" +
                           $"【Msg：不存在该判定依据主诉分类 Id：{id}】");

                return JsonResult.Fail("不存在该判定依据主诉分类");
            }
            catch (Exception e)
            {
                _log.Warning($"【JudgmentService】【DeleteJudgmentMasterAsync】【根据Id删除判定依据主诉分类错误】【Msg：{e}】");
                return JsonResult.Fail(e.Message);
            }
        }

        /// <summary>
        /// 根据Id更新判定依据主诉
        /// </summary>
        /// <param name="id"></param>
        /// <param name="dto"></param>
        /// <returns></returns>
        [Auth("Judgment" + PermissionDefinition.Separator + PermissionDefinition.Update)]
        public async Task<JsonResult> UpdateJudgmentMasterAsync(Guid id, CreateOrUpdateJudgmentMasterDto dto)
        {
            try
            {
                var judgmentMaster = await _judgmentMasterRepository
                    .AsNoTracking()
                    .FirstOrDefaultAsync(x => x.Id == id);
                if (await _judgmentMasterRepository.CountAsync(t =>
                        dto.ItemName == t.ItemDescription && t.Id != id && t.JudgmentTypeId == dto.JudgmentTypeId) >
                    0)
                {
                    return JsonResult.Fail("名称已存在");
                }

                if (judgmentMaster != null)
                {
                    var model = dto.BuildAdapter().AdaptToType<JudgmentMaster>();
                    model.SetId(judgmentMaster.Id).GetPy();
                    model.ModUser = CurrentUser.UserName;
                    model.AddUser = judgmentMaster.AddUser;
                    await _judgmentMasterRepository.UpdateAsync(model, true);

                    // 若主诉改变了启用状态，则判决依据项目也应跟随改变启用状态
                    if (dto.IsEnabled != Convert.ToBoolean(judgmentMaster.IsEnabled))
                    {
                        var items = await _judgmentItemRepository.Where(x => x.JudgmentMasterId == id).ToListAsync();
                        var itemDbContext = _judgmentItemRepository.GetDbContext();
                        foreach (var item in items)
                        {
                            item.IsEnabled = model.IsEnabled;
                            item.ModUser = CurrentUser.UserName;
                            itemDbContext.Entry(item).State = EntityState.Modified;
                        }

                        await itemDbContext.SaveChangesAsync();
                    }


                    var masterDto = model.BuildAdapter().AdaptToType<JudgmentMasterDto>();
                    var list = new List<JudgmentMasterDto> { masterDto };
                    await OperationJudgmentRedisCacheAsync(1, masterDtos: list);
                    return JsonResult.Ok();
                }

                _log.Error("【JudgmentService】【UpdateJudgmentMasterAsync】【根据Id更新判定依据主诉分类失败】" +
                           $"【Msg：不存在该判定依据主诉分类 ID：{id}】");
                return JsonResult.Fail("不存在该判定依据主诉分类");
            }
            catch (Exception e)
            {
                _log.Warning($"【JudgmentService】【UpdateJudgmentMasterAsync】【根据Id更新判定依据主诉分类错误】【Msg：{e}】");
                return JsonResult.Fail(e.Message);
            }
        }

        /// <summary>
        /// 新增判定依据项目
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [Auth("Judgment" + PermissionDefinition.Separator + PermissionDefinition.Create)]
        public async Task<JsonResult> CreateJudgmentItemAsync(List<CreateOrUpdateJudgmentItemDto> dto)
        {
            try
            {
                var model = dto.BuildAdapter().AdaptToType<List<JudgmentItem>>();
                model.ForEach(item =>
                {
                    item.AddUser = CurrentUser.UserName;
                    item.SetId(GuidGenerator.Create()).GetPy();
                });
                //判断同级（同一主诉）下名称是否相等，相等则无法新增
                Expression<Func<JudgmentItem, bool>> first = x => false;
                var param = Expression.Parameter(typeof(JudgmentItem), "x");
                Expression body = Expression.Invoke(first, param);
                body = dto.Select(s => (Expression<Func<JudgmentItem, bool>>)(x =>
                       x.ItemDescription == s.ItemName && x.JudgmentMasterId == s.JudgmentMasterId))
                    .Aggregate(body,
                        (current, expression) => Expression.OrElse(current, Expression.Invoke(expression, param)));
                var lambda = Expression.Lambda<Func<JudgmentItem, bool>>(body, param);
                if (await _judgmentItemRepository.CountAsync(lambda) > 0)
                {
                    return JsonResult.Fail("名称已存在");
                }

                await _judgmentItemRepository.GetDbSet().AddRangeAsync(model);
                if (await _judgmentItemRepository.GetDbContext().SaveChangesAsync() > 0)
                {
                    var list = model.BuildAdapter().AdaptToType<List<JudgmentItemDto>>();
                    await OperationJudgmentRedisCacheAsync(2, itemDtos: list);
                    return JsonResult.Ok();
                }

                _log.Error("新增判定依据项目失败，原因：数据库保存数据失败");
                return JsonResult.Fail();
            }
            catch (Exception e)
            {
                _log.Warning($"【JudgmentService】【CreateJudgmentItemAsync】【新增判定依据项目错误】【Msg：{e}】");
                return JsonResult.Fail(e.Message);
            }
        }

        /// <summary>
        /// 根据Id删除判定依据项目
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Auth("Judgment" + PermissionDefinition.Separator + PermissionDefinition.Delete)]
        public async Task<JsonResult> DeleteJudgmentItemAsync(Guid id)
        {
            try
            {
                var model = await _judgmentItemRepository.FirstOrDefaultAsync(x => x.Id == id);
                if (model != null)
                {
                    await _judgmentItemRepository.DeleteAsync(x => x.Id == id, true);
                    var itemDto = model.BuildAdapter().AdaptToType<JudgmentItemDto>();
                    var list = new List<JudgmentItemDto> { itemDto };
                    await OperationJudgmentRedisCacheAsync(0, itemDtos: list);
                    return JsonResult.Ok();
                }

                _log.Error("【JudgmentService】【DeleteJudgmentItemAsync】【根据Id删除判定依据项目失败】" +
                           $"【Msg：不存在该判定依据项目 ID：{id}】");
                return JsonResult.Fail("不存在该判定依据项目");
            }
            catch (Exception e)
            {
                _log.Warning($"【JudgmentService】【DeleteJudgmentItemAsync】【根据Id删除判定依据项目错误】【Msg：{e}】");
                return JsonResult.Fail(e.Message);
            }
        }

        /// <summary>
        /// 根据Id更新判定依据项目
        /// </summary>
        /// <param name="id"></param>
        /// <param name="dto"></param>
        /// <returns></returns>
        [Auth("Judgment" + PermissionDefinition.Separator + PermissionDefinition.Update)]
        public async Task<JsonResult> UpdateJudgmentItemAsync(Guid id, CreateOrUpdateJudgmentItemDto dto)
        {
            try
            {
                var judgmentItem = await _judgmentItemRepository.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
                if (await _judgmentItemRepository.CountAsync(t => dto.ItemName == t.ItemDescription && t.Id != id && t.JudgmentMasterId == dto.JudgmentMasterId) > 0)
                {
                    return JsonResult.Fail("名称已存在");
                }

                if (judgmentItem != null)
                {
                    var model = dto.BuildAdapter().AdaptToType<JudgmentItem>();
                    model.SetId(judgmentItem.Id).GetPy();
                    model.ModUser = CurrentUser.UserName;
                    await _judgmentItemRepository.UpdateAsync(model, true);
                    var itemDto = model.BuildAdapter().AdaptToType<JudgmentItemDto>();
                    var list = new List<JudgmentItemDto> { itemDto };
                    await OperationJudgmentRedisCacheAsync(1, itemDtos: list);
                    return JsonResult.Ok();
                }

                _log.Error("【JudgmentService】【UpdateJudgmentItemAsync】【根据Id更新判定依据项目失败】" +
                           $"【Msg：不存在该判定依据项目 ID：{id}】");
                return JsonResult.Fail("不存在该判定依据项目");
            }
            catch (Exception e)
            {
                _log.Warning($"【JudgmentService】【UpdateJudgmentItemAsync】【根据Id更新判定依据项目错误】【Msg：{e}】");
                return JsonResult.Fail(e.Message);
            }
        }

        /// <summary>
        /// 操作判定依据Redis缓存
        /// </summary>
        /// <param name="flag">操作标识符；删除：0，1：更新，2：新增，3：查询</param>
        /// <param name="lambda">查询表达式，只支持JudgmentTypeDto表达式</param>
        /// <param name="typeDtos">判定依据类型Dto</param>
        /// <param name="masterDtos">判定依据主诉Dto</param>
        /// <param name="itemDtos">判定依据项目Dto</param>
        /// <returns></returns>
        [ApiExplorerSettings(IgnoreApi = true)]
        public async Task<List<JudgmentTypeDto>> OperationJudgmentRedisCacheAsync(int flag,
            Func<JudgmentTypeDto, bool> lambda = null, List<JudgmentTypeDto> typeDtos = null,
            List<JudgmentMasterDto> masterDtos = null, List<JudgmentItemDto> itemDtos = null)
        {
            try
            {
                var str = flag switch
                {
                    0 => "删除",
                    1 => "更新",
                    2 => "新增",
                    3 => "查询",
                    _ => ""
                };

                List<JudgmentTypeDto> res = null;
                var result = await GetJudgmentDetailListAsync();
                if (result.Code == 200 && result.Data != null)
                {
                    var serviceName = _configuration.GetSection("ServiceName").Value;
                    var cacheDtos = result.Data;
                    switch (flag)
                    {
                        case 0:
                            if (masterDtos != null)
                            {
                                var masterIds = masterDtos.Select(s => s.Id);
                                cacheDtos.ForEach(item =>
                                {
                                    var list = item.Children.Where(x => masterIds.Contains(x.Id))
                                        .ToList();
                                    if (list.Count <= 0) return;
                                    item.Children.RemoveAll(list);
                                });
                                cacheDtos.RemoveAll(cacheDtos.Where(t => masterIds.Contains(t.Id)));
                            }

                            if (itemDtos != null)
                            {
                                var itemIds = itemDtos.Select(s => s.Id);
                                foreach (var type in cacheDtos)
                                {
                                    foreach (var master in type.Children)
                                    {
                                        var list = master.Children.Where(x => itemIds.Contains(x.Id)).ToList();
                                        if (list.Count <= 0) continue;
                                        master.Children.RemoveAll(list);
                                    }
                                }
                            }

                            break;

                        case 1:


                            if (typeDtos != null)
                            {
                                cacheDtos.ForEach(type =>
                                {
                                    var newCache = typeDtos.FirstOrDefault(x => x.Id == type.Id);
                                    if (newCache == null) return;
                                    type.Py = newCache.Py;
                                    type.Sort = newCache.Sort;
                                    type.ItemName = newCache.ItemName;
                                    type.TriageDeptCode = newCache.TriageDeptCode;
                                    if (type.IsEnabled == newCache.IsEnabled) return;
                                    type.IsEnabled = newCache.IsEnabled;
                                    type.Children?.ForEach(master =>
                                    {
                                        master.IsEnabled = type.IsEnabled;
                                        master.Children?.ForEach(x => { x.IsEnabled = master.IsEnabled; });
                                    });
                                });
                            }

                            if (masterDtos != null)
                            {
                                cacheDtos.ForEach(type =>
                                {
                                    var newCache = masterDtos.FirstOrDefault(x => x.JudgmentTypeId == type.Id);
                                    if (newCache != null)
                                    {
                                        type.Children?.ForEach(mater =>
                                        {
                                            if (mater.Id != newCache.Id) return;
                                            mater.Py = newCache.Py;
                                            mater.Sort = newCache.Sort;
                                            mater.ItemName = newCache.ItemName;
                                            if (mater.IsEnabled == newCache.IsEnabled) return;
                                            mater.IsEnabled = newCache.IsEnabled;
                                            mater.Children?.ForEach(item => { item.IsEnabled = mater.IsEnabled; });
                                        });
                                    }
                                });
                            }

                            if (itemDtos != null)
                            {
                                //第三级菜单更新操作为，将旧值移除再插入新值
                                var itemIds = itemDtos.Select(s => s.JudgmentMasterId);
                                foreach (var type in cacheDtos)
                                {
                                    if (!type.Children.Exists(x => itemIds.Contains(x.Id))) continue;
                                    foreach (var master in type.Children)
                                    {
                                        var list = master.Children.FindAll(x => x.JudgmentMasterId == master.Id);
                                        if (list.Count <= 0) continue;
                                        master.Children = master.Children.Where(x => !itemDtos.Select(s => s.Id).Contains(x.Id)).ToList();
                                        master.Children.AddRange(itemDtos);
                                    }
                                }
                            }

                            break;

                        case 2:
                            if (typeDtos != null)
                            {
                                cacheDtos.AddRange(typeDtos);
                            }

                            if (masterDtos != null)
                            {
                                cacheDtos.ForEach(item =>
                                {
                                    var list = masterDtos.FindAll(x => x.JudgmentTypeId == item.Id);
                                    if (list.Count <= 0) return;
                                    item.Children ??= new List<JudgmentMasterDto>();
                                    item.Children.AddRange(list);
                                });
                            }

                            if (itemDtos != null)
                            {
                                var masterIds = itemDtos.Select(s => s.JudgmentMasterId).ToList();
                                foreach (var type in cacheDtos)
                                {
                                    if (type.Children == null || type.Children.Count <= 0) continue;
                                    foreach (var master in type.Children)
                                    {
                                        var list = itemDtos.FindAll(x => x.JudgmentMasterId == master.Id);
                                        if (list.Count <= 0) continue;
                                        master.Children ??= new List<JudgmentItemDto>();
                                        master.Children.AddRange(list);
                                    }
                                }
                            }

                            break;

                        case 3:
                            if (lambda != null)
                            {
                                res = cacheDtos.Where(lambda).ToList();
                            }

                            break;
                    }

                    if (flag == 3) return res;

                    var typeHashEntries = new HashEntry[cacheDtos.Count];
                    var index = 0;
                    cacheDtos.ForEach(item =>
                    {
                        var json = JsonSerializer.Serialize(item);
                        typeHashEntries[index++] = new HashEntry(item.Id.ToString(), json);
                    });

                    await _redis.HashSetAsync($"{serviceName}:Judgment", typeHashEntries);
                    return res;
                }

                _log.Error(
                    $"【JudgmentService】【OperationJudgmentRedisCacheAsync】【{str}判定依据Redis缓存失败】【Msg：Redis中没有缓存数据】");
                _log.Trace($"【JudgmentService】【OperationJudgmentRedisCacheAsync】【{str}判定依据Redis缓存结束】");
                return null;
            }
            catch (Exception e)
            {
                _log.Warning($"【JudgmentService】【OperationJudgmentRedisCacheAsync】【操作判定依据Redis缓存错误】【Msg：{e}】");
                return null;
            }
        }


        /// <summary>
        /// 更新主诉拼音
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [ApiExplorerSettings(IgnoreApi = true)]
        [HttpGet]
        public async Task UpdatePy()
        {
            var judgmentTypes = await _judgmentTypeRepository.ToListAsync();
            foreach (var item in judgmentTypes)
            {
                item.GetPy();
            }

            var judgmentMasters = await _judgmentMasterRepository.ToListAsync();
            foreach (var item in judgmentMasters)
            {
                item.GetPy();
            }

            var judgmentItems = await _judgmentItemRepository.ToListAsync();
            foreach (var item in judgmentItems)
            {
                item.GetPy();
            }

            var serviceName = _configuration.GetSection("ServiceName").Value;
            await _redis.KeyDeleteAsync($"{serviceName}:Judgment");
        }
    }
}