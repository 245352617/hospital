using DotNetCore.CAP;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using SamJan.MicroService.PreHospital.Core;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TriageService.Application.Dtos.TriageConfig.TriageConfig;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Uow;
using JsonResult = SamJan.MicroService.PreHospital.Core.JsonResult;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace SamJan.MicroService.PreHospital.TriageService
{
    /// <summary>
    /// 院前分诊设置接口
    /// </summary>
    [Auth("TriageConfig")]
    [Authorize]
    public class TriageConfigAppService : ApplicationService, ITriageConfigAppService, ICapSubscribe
    {
        private readonly IConfiguration _configuration;
        private readonly IRepository<TriageConfig> _triageConfigRepository;
        private readonly IDatabase _redis;
        private readonly NLogHelper _log;
        private readonly ICapPublisher _capPublisher;
        private readonly IUnitOfWorkManager _unitOfWorkManager;

        public TriageConfigAppService(IRepository<TriageConfig> triageConfigRepository, NLogHelper log,
            RedisHelper redisHelper, IConfiguration configuration, ICapPublisher capPublisher, IUnitOfWorkManager unitOfWorkManager)
        {
            _triageConfigRepository = triageConfigRepository;
            _log = log;
            _configuration = configuration;
            _redis = redisHelper.GetDatabase();
            _capPublisher = capPublisher;
            _unitOfWorkManager = unitOfWorkManager;
        }


        /// <summary>
        /// 清除Redis指定Key缓存数据，默认清除当前服务TriageConfig
        /// </summary>
        /// <param name="redisKey"></param>
        /// <returns></returns>
        public async Task<JsonResult> ClearRedisByKeyAsync(string redisKey)
        {
            _log.Warning($"【TriageConfigService】->ClearRedisByKeyAsync->清除Redis指定Key->:{redisKey}】");
            try
            {
                var serviceName = _configuration.GetSection("ServiceName").Value;
                var delKey = !string.IsNullOrEmpty(redisKey) ? redisKey : $"{serviceName}:TriageConfig";
                if (await _redis.KeyExistsAsync(delKey))
                {
                    // 删除指定key
                    _redis.KeyDelete(delKey);

                    return JsonResult.Ok("Redis清除成功！");
                }
                else
                {
                    return JsonResult.Ok("Redis指定Key不存在，清除失败！");
                }
            }
            catch (Exception e)
            {
                _log.Warning($"【TriageConfigService】【SaveTriageConfigAsync】【保存院前分诊设置错误】【Msg：{e}】");
                return JsonResult.Fail(e.Message);
            }
        }


        /// <summary>
        /// 新增院前分诊设置
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [Auth("TriageConfig" + PermissionDefinition.Separator + PermissionDefinition.Save)]
        public async Task<JsonResult> SaveTriageConfigAsync(CreateTriageConfigDto dto)
        {
            try
            {
                var model = dto.BuildAdapter().AdaptToType<TriageConfig>().GetNamePy();
                if (model != null)
                {
                    var entities = await _triageConfigRepository
                        .Where(x => x.TriageConfigType == dto.TriageConfigType)
                        .IgnoreQueryFilters()
                        .ToListAsync();
                    // 如果没有传入编码，则自动生成编码，否则按传入的编码存储
                    if (string.IsNullOrWhiteSpace(model.TriageConfigCode))
                    {
                        // 自动生成字典编码，类型名+”_“+顺序生成序号000~999
                        var maxId = 0;
                        var orderedEntities = entities.OrderBy(o => o.TriageConfigCode);
                        foreach (var item in orderedEntities)
                        {
                            var array = item.TriageConfigCode.Split('_');
                            if (array.Length > 1 && int.TryParse(array[1], out var _))
                            {
                                maxId = int.Parse(array[1]);
                            }
                        }

                        var code = $"{(TriageDict)dto.TriageConfigType}_{(maxId + 1):D3}";
                        model.TriageConfigCode = code;

                        // var typeEng = (TriageDict)model.TriageConfigType;
                        // if (!model.TriageConfigCode.Contains(typeEng + "_"))
                        // {
                        //     model.TriageConfigCode = typeEng + "_" + model.TriageConfigCode;
                        // }
                    }

                    if (string.IsNullOrWhiteSpace(model.TriageConfigCode))
                    {
                        _log.Error("【TriageConfigService】【SaveTriageConfigAsync】" +
                                   "【保存院前分诊设置失败】【Code不能为空】");
                        return JsonResult.Fail("Code不能为空");
                    }

                    var dbContext = _triageConfigRepository.GetDbContext();
                    int result;

                    //若存在相同名称字典，则更新
                    var sameNameEntity = entities.FirstOrDefault(c => c.TriageConfigName == model.TriageConfigName);
                    switch (sameNameEntity)
                    {
                        case { IsDeleted: true }:

                            sameNameEntity.IsDeleted = false;
                            dbContext.Entry(sameNameEntity).State = EntityState.Modified;
                            result = await dbContext.SaveChangesAsync();
                            if (result > 0)
                            {
                                await OperationRedisCache(1,
                                    sameNameEntity.BuildAdapter().AdaptToType<TriageConfigDto>());
                                _log.Info("【TriageConfigService】【SaveTriageConfigAsync】【保存院前分诊设置成功】");
                                return JsonResult.Ok();
                            }

                            break;

                        case { IsDeleted: false }:

                            _log.Error("【TriageConfigService】【SaveTriageConfigAsync】【保存院前分诊设置失败】【Msg：名称已存在，不能重复！】");
                            return JsonResult.Fail("名称已存在，不能重复！");
                    }

                    if (entities.Count(c => c.TriageConfigCode == model.TriageConfigCode && !c.IsDeleted) > 0)
                    {
                        _log.Error("【TriageConfigService】【SaveTriageConfigAsync】【保存院前分诊设置失败】【Msg：编码已存在，不能重复】");
                        return JsonResult.Fail("编码已存在，不能重复！" + model.TriageConfigCode);
                    }

                    model.AddUser = CurrentUser.UserName;
                    dbContext.Entry(model).State = EntityState.Added;
                    result = await dbContext.SaveChangesAsync();
                    if (result > 0)
                    {
                        await OperationRedisCache(2, model.BuildAdapter().AdaptToType<TriageConfigDto>());
                        _log.Info("【TriageConfigService】【SaveTriageConfigAsync】【保存院前分诊设置成功】");
                        //同步信息，新增急诊masterdata信息
                        var dict = model.BuildAdapter().AdaptToType<DictionariesDto>();
                        dict.Id = Guid.Empty;
                        await _capPublisher.PublishAsync("masterdata.dictionaries.addorupdate.from.triage.config",
                            dict);
                        return JsonResult.Ok();
                    }

                    _log.Error("【TriageConfigService】【SaveTriageConfigAsync】【保存院前分诊设置失败】【Msg：保存失败！】");
                    return JsonResult.Fail("保存失败！");
                }

                _log.Error("【TriageConfigService】【SaveTriageConfigAsync】【保存院前分诊设置失败】【Msg：数据校验失败！】");
                return JsonResult.Fail("数据校验失败！");
            }
            catch (Exception e)
            {
                _log.Warning($"【TriageConfigService】【SaveTriageConfigAsync】【保存院前分诊设置错误】【Msg：{e}】");
                return JsonResult.Fail(e.Message);
            }
        }

        /// <summary>
        /// 修改院前分诊设置-post
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [Auth("TriageConfig" + PermissionDefinition.Separator + PermissionDefinition.Update)]
        public async Task<JsonResult> PostTriageConfig2Async(TriageConfigDto dto) => await UpdateTriageConfigAsync(dto);

        /// <summary>
        /// 修改院前分诊设置
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [Auth("TriageConfig" + PermissionDefinition.Separator + PermissionDefinition.Update)]
        public async Task<JsonResult> UpdateTriageConfigAsync(TriageConfigDto dto)
        {
            try
            {
                var model = dto.BuildAdapter().AdaptToType<TriageConfig>().GetNamePy();
                if (await _triageConfigRepository.CountAsync(c =>
                        c.TriageConfigName == dto.TriageConfigName && c.TriageConfigType == dto.TriageConfigType &&
                        c.Id != dto.Id) > 0)
                {
                    return JsonResult.Fail("名称已存在，不能重复！");
                }

                if (await _triageConfigRepository.CountAsync(x => x.Id == dto.Id) > 0)
                {
                    model.ModUser = CurrentUser.UserName;
                    var dbContext = _triageConfigRepository.GetDbContext();
                    dbContext.Entry(model).State = EntityState.Modified;
                    var result = await dbContext.SaveChangesAsync();
                    if (result > 0)
                    {
                        await OperationRedisCache(1, dto);
                        //同步信息，修改急诊masterdata字典信息
                        var dict = model.BuildAdapter().AdaptToType<DictionariesDto>();
                        await _capPublisher.PublishAsync("masterdata.dictionaries.addorupdate.from.triage.config",
                            dict);
                        return JsonResult.Ok();
                    }

                    _log.Error("【TriageConfigService】【UpdateTriageConfigAsync】【修改院前分诊设置失败】【Msg：修改失败！】");
                    return JsonResult.Fail("修改失败！");
                }

                _log.Error("【TriageConfigService】【UpdateTriageConfigAsync】【修改院前分诊设置失败】【Msg：数据不存在！】");
                return JsonResult.Fail("数据不存在");
            }
            catch (Exception e)
            {
                _log.Warning($"【TriageConfigService】【UpdateTriageConfigAsync】【修改院前分诊设置错误】【Msg：{e}】");
                return JsonResult.Fail(e.Message);
            }
        }


        /// <summary>
        ///     删除院前分诊设置
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Auth("TriageConfig" + PermissionDefinition.Separator + PermissionDefinition.Delete)]
        public async Task<JsonResult> DeleteTriageConfigAsync(Guid id)
        {
            try
            {
                var triageConfigs = await _triageConfigRepository.FirstOrDefaultAsync(t => t.Id == id);
                if (triageConfigs != null)
                {
                    if (triageConfigs.UnDeletable)
                    {
                        return JsonResult.Fail("该项不允许删除");
                    }

                    triageConfigs.DeleteUser = CurrentUser.UserName;
                    await _triageConfigRepository.DeleteAsync(triageConfigs, true);
                    var dbContext = _triageConfigRepository.GetDbContext();
                    dbContext.Entry(triageConfigs).State = EntityState.Deleted;
                    var result = await dbContext.SaveChangesAsync();
                    if (result > 0)
                    {
                        await OperationRedisCache(0, triageConfigs.BuildAdapter().AdaptToType<TriageConfigDto>());
                        //同步信息，删除急诊masterdata的字典信息
                        await _capPublisher.PublishAsync("masterdata.dictionaries.delete.from.triage.config",
                            triageConfigs.TriageConfigCode);

                        return JsonResult.Ok("成功");
                    }

                    _log.Error("【TriageConfigService】【DeleteTriageConfigAsync】【删除院前分诊设置失败】【Msg：失败失败！】");
                    return JsonResult.Fail("失败！");
                }

                _log.Info("【TriageConfigService】【DeleteTriageConfigAsync】【删除院前分诊设置失败】【Msg：数据已经被删除！】");
                return JsonResult.Fail("数据已经被删除！请检查");
            }
            catch (Exception e)
            {
                _log.Warning($"【TriageConfigService】【DeleteTriageConfigAsync】【删除院前分诊设置错误】【Msg：{e}】");
                return JsonResult.Fail(e.Message);
            }
        }


        /// <summary>
        /// 获取院前分诊设置集合
        /// </summary>
        /// <param name="triageConfigType"></param>
        /// <param name="appIsUse">app端建档是否需要使用此证件类型，对应扩展字段2</param>
        /// <param name="isEnable">-1不查询，0：禁用，1：启用</param>
        /// <returns></returns>
        [Auth("TriageConfig" + PermissionDefinition.Separator + PermissionDefinition.List)]
        public async Task<JsonResult<Dictionary<string, List<TriageConfigDto>>>> GetTriageConfigListAsync(string triageConfigType, int appIsUse = -1, int isEnable = 1)
        {
            try
            {
                var dtos = await GetTriageConfigByRedisAsync(triageConfigType, isEnable);

                if (appIsUse != -1)
                {
                    var list = new List<TriageConfigDto>();
                    foreach (var dict in dtos)
                    {
                        list.AddRange(dict.Value);
                    }

                    dtos = list.Where(x => x.ExtensionField2 == appIsUse.ToString() || x.ExtensionField2.IsNullOrWhiteSpace()).GroupBy(g => g.TriageConfigType)
                        .ToDictionary(k => ((TriageDict)k.Key).ToString(), v => v.ToList());
                }

                return JsonResult<Dictionary<string, List<TriageConfigDto>>>.Ok(msg: CurrentUser.UserName, data: dtos);
            }
            catch (Exception e)
            {
                _log.Warning($"【TriageConfigService】【GetTriageConfigListAsync】【获取所有的院前分诊设置错误】【Msg：{e}】");
                return JsonResult<Dictionary<string, List<TriageConfigDto>>>.Fail(e.Message);
            }
        }

        /// <summary>
        /// 获取特约记账类型(龙岗)
        /// </summary>
        /// <param name="triageConfigType"></param>
        /// <returns></returns>
        [Auth("TriageConfig" + PermissionDefinition.Separator + PermissionDefinition.List)]
        public async Task<JsonResult<Dictionary<string, List<TriageConfigDto>>>> GetTriageConfigSpecialAccountTypeListAsync(string triageConfigType)
        {
            try
            {
                var dtos = new Dictionary<string, List<TriageConfigDto>>();
                var hisCode = _configuration.GetValue<string>("HospitalCode");
                if (hisCode == "Longgang")
                {
                    dtos = await GetTriageConfigByRedisAsync(triageConfigType);
                }

                return JsonResult<Dictionary<string, List<TriageConfigDto>>>.Ok(msg: CurrentUser.UserName, data: dtos);
            }
            catch (Exception e)
            {
                _log.Warning($"【TriageConfigService】【GetTriageConfigSpecialAccountTypeListAsync】【获取特约记账类型(龙岗)错误】【Msg：{e}】");
                return JsonResult<Dictionary<string, List<TriageConfigDto>>>.Fail(e.Message);
            }
        }


        /// <summary>
        ///     获取院前分诊设置详情
        /// </summary>
        /// <param name="type">字典类别代码</param>
        /// <param name="code">字典代码</param>
        /// <returns></returns>
        [Auth("TriageConfig" + PermissionDefinition.Separator + PermissionDefinition.Get)]
        public async Task<JsonResult<Dictionary<string, List<TriageConfigDto>>>> GetTriageConfigDetailAsync(string type,
            string code)
        {
            try
            {
                var list = await GetTriageConfigByRedisAsync(type);

                if (list != null)
                {
                    var dtos = list[type].FindAll(x => x.TriageConfigCode == code);
                    list[type] = dtos;
                    return JsonResult<Dictionary<string, List<TriageConfigDto>>>.Ok(data: list);
                }

                _log.Error("【TriageConfigService】【GetTriageConfigDetailAsync】【获取院前分诊设置详情失败】【Msg：从缓存中读取字典数据失败】");
                return JsonResult<Dictionary<string, List<TriageConfigDto>>>.Fail("获取字典数据失败！");
            }
            catch (Exception e)
            {
                _log.Warning($"【TriageConfigService】【GetTriageConfigDetailAsync】【获取院前分诊设置详情错误】【Msg：{e}】");
                return JsonResult<Dictionary<string, List<TriageConfigDto>>>.Fail(e.Message);
            }
        }


        /// <summary>
        ///     根据分页获取分诊配置列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [Auth("TriageConfig" + PermissionDefinition.Separator + PermissionDefinition.Page)]
        public async Task<JsonResult<PagedResultDto<TriageConfigDto>>> GetTriageConfigPageListAsync(
            TriageConfigWhereInput input)
        {
            try
            {
                var dtos = await GetTriageConfigByRedisAsync(input.TriageConfigType);
                var triageList = new List<TriageConfigDto>();
                foreach (var item in dtos) triageList.AddRange(item.Value.ToList());

                var pageList = new PagedResultDto<TriageConfigDto>
                {
                    TotalCount = triageList.Count,
                    Items = triageList.OrderBy(t => t.Sort).Skip(input.MaxResultCount * (input.SkipCount - 1))
                        .Take(input.MaxResultCount).ToList()
                };
                return JsonResult<PagedResultDto<TriageConfigDto>>.Ok(data: pageList);
            }
            catch (Exception e)
            {
                _log.Warning($"【TriageConfigService】【GetTriageConfigListAsync】【获取所有的院前分诊设置错误】【Msg：{e}】");
                return JsonResult<PagedResultDto<TriageConfigDto>>.Fail(e.Message);
            }
        }

        /// <summary>
        /// 分诊服务初始化种子数据
        /// </summary>
        /// <returns></returns>
        public async Task<JsonResult<string>> InitDataSeedAsync()
        {
            try
            {
                var dir = new DirectoryInfo("./DataSeedSqlFiles");
                var files = dir.GetFiles();
                var fsql = ServiceProvider.GetRequiredService<IFreeSql>();
                foreach (var file in files)
                {
                    var tableName = file.Name.Split('.')[0];
                    var sql = "select count(1) from " + tableName;
                    var res = await fsql.Ado.QuerySingleAsync<int>(sql);
                    if (Convert.ToInt32(res) > 0)
                    {
                        continue;
                    }

                    var text = "";
                    await using (var stream = file.Open(FileMode.Open, FileAccess.Read))
                    {
                        var buffers = new byte[stream.Length];
                        await stream.ReadAsync(buffers, 0, buffers.Length);
                        text = Encoding.UTF8.GetString(buffers, 0, buffers.Length);
                    }

                    if (text.IsNullOrWhiteSpace()) continue;

                    text = text.Replace("GO", "");
                    await fsql.Ado.ExecuteNonQueryAsync(text);
                }

                _log.Info("分诊服务初始化种子数据结束");
                return JsonResult<string>.Ok();
            }
            catch (Exception e)
            {
                _log.Error($"分诊服务初始化种子数据错误，原因：{e}");
                return JsonResult<string>.Fail(e.Message);
            }
        }

        public async Task GetTestConfig()
        {
            try
            {
                var list = await _triageConfigRepository.OrderBy(o => o.Sort).IgnoreQueryFilters().ToListAsync();
                Console.WriteLine("success");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        /// <summary>
        /// 从Redis缓存中获取字典
        /// </summary>
        /// <param name="input"></param>
        /// <param name="isEnable">-1不查询，0：禁用，1：启用</param>
        /// <param name="isDeleted">-1：全部  0；未删除  1：已删除</param>
        /// <returns></returns>
        [ApiExplorerSettings(IgnoreApi = true)]
        [AllowAnonymous]
        public async Task<Dictionary<string, List<TriageConfigDto>>> GetTriageConfigByRedisAsync(string input = "",
            int isEnable = -1, int isDeleted = 0)
        {
            /*
             * 1.先判断Redis是否已经将需要缓存的字典类别缓存，如若没有则先缓存类别
             * 2.将缓存中的字典类别取出，循环判断是否已缓存了该类别字典
             * 3.如果没有缓存，则将未缓存类别拼接成字符串；如果缓存了则取出
             * 4.判断未缓存类别字符串是否不为空，如果不为空则将这些类别数据查询缓存出来
             */

            //_log.Trace("【TriageConfigService】【GetTriageConfigByRedisAsync】【从Redis缓存中获取字典开始】");
            var result = new Dictionary<string, List<TriageConfigDto>>();
            var serviceName = _configuration.GetSection("ServiceName").Value;
            try
            {
                if (await _redis.KeyExistsAsync($"{serviceName}:TriageConfig"))
                {
                    if (!string.IsNullOrWhiteSpace(input))
                    {
                        var sb = new StringBuilder();
                        foreach (var item in input.Split(','))
                        {
                            var type = item;
                            if (int.TryParse(item, out var str)) type = ((TriageDict)str).ToString();
                            if (await _redis.HashExistsAsync($"{serviceName}:TriageConfig", type))
                            {
                                var json = await _redis.HashGetAsync($"{serviceName}:TriageConfig", type);
                                if (!string.IsNullOrWhiteSpace(json))
                                {
                                    result.Add(type,
                                        JsonSerializer.Deserialize<List<TriageConfigDto>>(json).OrderBy(o => o.Sort)
                                            .WhereIf(isEnable != -1, t => t.IsDisable == isEnable)
                                            .WhereIf(isDeleted != -1, x => x.IsDeleted == isDeleted)
                                            .ToList());
                                }
                            }
                            else
                            {
                                sb.Append(str + ",");
                            }
                        }

                        if (!string.IsNullOrWhiteSpace(sb.ToString()))
                        {
                            #region 动态拼接 Or 查询表达式

                            Expression<Func<TriageConfig, bool>> first = x => false;
                            var param = Expression.Parameter(typeof(TriageConfig), "x");
                            Expression body = Expression.Invoke(first, param);
                            body = sb.ToString().TrimEnd(',').Split(',')
                                .Select(s =>
                                    (Expression<Func<TriageConfig, bool>>)(x =>
                                        x.TriageConfigType == Convert.ToInt32(s)))
                                .Aggregate(body,
                                    (current, expression) =>
                                        Expression.OrElse(current, Expression.Invoke(expression, param)));

                            #endregion

                            var lambda = Expression.Lambda<Func<TriageConfig, bool>>(body, param);
                            var list = await _triageConfigRepository.Where(lambda).IgnoreQueryFilters().ToListAsync();
                            var dtoList = list.BuildAdapter().AdaptToType<List<TriageConfigDto>>();
                            var dict = dtoList.OrderBy(s => s.Sort)
                                .GroupBy(g => g.TriageConfigType)
                                .ToDictionary(k => ((TriageDict)k.Key).ToString(), v => v.ToList());

                            foreach (var (key, value) in dict)
                            {
                                await _redis.HashSetAsync($"{serviceName}:TriageConfig", key,
                                    JsonSerializer.Serialize(value));
                                if (result.ContainsKey(key))
                                {
                                    result[key] = value;
                                    break;
                                }

                                var newValues = value.OrderBy(o => o.Sort)
                                    .WhereIf(isEnable != -1, t => t.IsDisable == isEnable)
                                    .WhereIf(isDeleted != -1, x => x.IsDeleted == isDeleted)
                                    .ToList();

                                result.Add(key, newValues);
                            }
                        }
                    }
                    else
                    {
                        var values = await _redis.HashValuesAsync($"{serviceName}:TriageConfig");
                        var dtos = new List<TriageConfigDto>();
                        foreach (var item in values)
                        {
                            var dtoList = JsonSerializer.Deserialize<List<TriageConfigDto>>(item)
                                .WhereIf(isEnable != -1, t => t.IsDisable == isEnable).OrderBy(o => o.Sort).ToList();
                            dtos.AddRange(dtoList);
                        }

                        result = dtos.WhereIf(isEnable != -1, t => t.IsDisable == isEnable)
                            .WhereIf(isDeleted != -1, x => x.IsDeleted == isDeleted)
                            .OrderBy(o => o.Sort)
                            .GroupBy(g => g.TriageConfigType).ToDictionary(
                                k => ((TriageDict)k.Key).ToString(),
                                v => v.ToList());
                    }
                }
                else
                {
                    await GetTriageConfigByDB(serviceName, input, isEnable, isDeleted);
                }

                //_log.Trace("【TriageConfigService】【GetTriageConfigByRedisAsync】【从Redis缓存中获取字典结束】" +
                //           $"【Result：{JsonSerializer.Serialize(result, new JsonSerializerOptions { Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping })}】");
                //_log.Info("【TriageConfigService】【GetTriageConfigByRedisAsync】【从Redis缓存中获取字典成功】");
                return result;
            }
            catch (Exception e)
            {
                _log.Warning($"【TriageConfigService】【GetTriageConfigByRedisAsync】【从Redis缓存中获取字典错误】【Msg：{e}】");
                //如果取redis报错的话直接容数据库中读取
                return await GetTriageConfigByDB(serviceName, input, isEnable, isDeleted, false);
            }
        }


        /// <summary>
        /// 从数据库中获取字典
        /// </summary>
        /// <param name="serviceName"></param>
        /// <param name="input"></param>
        /// <param name="isEnable"></param>
        /// <param name="isDeleted"></param>
        /// <param name="isSetRedis"></param>
        /// <returns></returns>
        public async Task<Dictionary<string, List<TriageConfigDto>>> GetTriageConfigByDB(string serviceName, string input = "",
            int isEnable = -1, int isDeleted = 0, bool isSetRedis = true)
        {
            var result = new Dictionary<string, List<TriageConfigDto>>();
            var list = await _triageConfigRepository.OrderBy(o => o.Sort).IgnoreQueryFilters().ToListAsync();
            var dtos = list.BuildAdapter().AdaptToType<List<TriageConfigDto>>();
            var cache = dtos.GroupBy(g => g.TriageConfigType)
                .ToDictionary(k => ((TriageDict)k.Key).ToString(),
                    v => v.ToList());
            if (isSetRedis)
            {
                async void Action(KeyValuePair<string, List<TriageConfigDto>> item)
                {
                    var (key, value) = item;
                    var json = JsonSerializer.Serialize(value);
                    await _redis.HashSetAsync($"{serviceName}:TriageConfig", key, json);
                }
                cache.ToList().ForEach(Action);
            }

            if (!string.IsNullOrWhiteSpace(input))
            {
                foreach (var item in input.Split(','))
                {
                    if (!int.TryParse(item, out var type)) continue;
                    if (!Enum.TryParse(item, out TriageDict value)) continue;
                    var dtoList = dtos.WhereIf(isEnable != -1, t => t.IsDisable == isEnable)
                        .WhereIf(isDeleted != -1, x => x.IsDeleted == isDeleted)
                        .Where(x => x.TriageConfigType == type)
                        .OrderBy(o => o.Sort)
                        .ToList();

                    result.Add(value.ToString(), dtoList);
                }
            }
            else
            {
                var dicts = dtos.WhereIf(isEnable != -1, t => t.IsDisable == isEnable)
                    .WhereIf(isDeleted != -1, x => x.IsDeleted == isDeleted)
                    .OrderBy(o => o.Sort)
                    .ToList();

                if (dicts.Count > 0)
                {
                    result = dtos.GroupBy(g => g.TriageConfigType)
                        .ToDictionary(k => ((TriageDict)k.Key).ToString(),
                            v => v.ToList());
                }
            }
            return result;
        }

        /// <summary>
        /// 获取医院信息
        /// </summary>
        /// <returns></returns>
        public Task<JsonResult<HospitalInfoDto>> GetHospitalInfoAsync()
        {
            // 从配置文件获取医院代码
            var hisCode = _configuration.GetValue<string>("HospitalCode");

            return Task.FromResult(JsonResult<HospitalInfoDto>.Ok(data: new HospitalInfoDto
            { HospitalCode = hisCode }));
        }

        /// <summary>
        /// 是否演示环境
        /// </summary>
        /// <returns></returns>
        public JsonResult<IsDemoDto> GetIsDemo()
        {
            //是否为演示环境
            var isDemo = _configuration["HospitalCode"]?.ToString() == "Mock";
            return JsonResult<IsDemoDto>.Ok(data: new IsDemoDto { IsDemo = isDemo });
        }

        /// <summary>
        ///     操作Redis缓存
        /// </summary>
        /// <param name="flag">操作标识符；删除：0，1：更新，2：新增</param>
        /// <param name="dto">缓存数据</param>
        /// <returns></returns>
        [ApiExplorerSettings(IgnoreApi = true)]
        private async Task OperationRedisCache(int flag, TriageConfigDto dto)
        {
            try
            {
                var str = flag switch
                {
                    0 => "删除",
                    1 => "更新",
                    2 => "新增",
                    _ => ""
                };

                var type = ((TriageDict)dto.TriageConfigType).ToString();
                _log.Trace($"【TriageConfigService】【OperationRedisCache】【操作Redis缓存开始】【操作：{str}");
                var cache = await GetTriageConfigByRedisAsync(dto.TriageConfigType.ToString());
                var list = new List<TriageConfigDto>();
                if (cache.Count > 0)
                {
                    list = cache[type];
                }

                switch (flag)
                {
                    case 0:
                        list.Remove(list.FirstOrDefault(x => x.Id == dto.Id));
                        break;
                    case 1:
                        list.Remove(list.FirstOrDefault(x => x.Id == dto.Id));
                        list.Add(dto);
                        break;
                    case 2:
                        list.Remove(list.FirstOrDefault(x => x.Id == dto.Id));
                        list.Add(dto);
                        break;
                }

                var serviceName = _configuration.GetSection("ServiceName").Value;
                var json = JsonSerializer.Serialize(list);
                await _redis.HashSetAsync($"{serviceName}:TriageConfig", type, json);
                _log.Trace("【TriageConfigService】【OperationRedisCache】" +
                           $"【操作Redis缓存---{str} Key:{serviceName}:TriageConfig:{type}成功】");
                _log.Info("【TriageConfigService】【OperationRedisCache】【操作Redis缓存结束】");
            }
            catch (Exception e)
            {
                _log.Warning($"【TriageConfigService】【OperationRedisCache】【操作Redis缓存错误】【Msg：{e}】");
            }
        }

        /// <summary>
        /// 同步费别列表(龙岗)
        /// </summary>
        /// <param name="faberEventData"></param>
        /// <returns></returns>
        [CapSubscribe("sync.faber.from.masterdata")]
        [ApiExplorerSettings(IgnoreApi = true)]
        //[AllowAnonymous]
        //[HttpPost]
        public async Task SyncFaberListAsync(FaberSyncHis faberEventData)
        {
            using var uow = this._unitOfWorkManager.Begin();
            _log.Info($"【TriageConfigService】【SyncFaberListAsync】【通过CAP sync.faber.from.masterdata 接收到Faber同步数据】：{JsonConvert.SerializeObject(faberEventData)}");
            try
            {
                Stopwatch stopwatch = Stopwatch.StartNew();
                if (faberEventData.DicType != 5)
                {
                    return;
                }

                if (faberEventData.DicDatas.Count == 0)
                {
                    return;
                }

                List<TriageConfig> newFaberList = new List<TriageConfig>();
                List<FaberSyncDto> hisFaberList = JsonConvert.DeserializeObject<List<FaberSyncDto>>(JsonConvert.SerializeObject(faberEventData.DicDatas));
                // 由his数据获得新的费别列表newFaberList
                hisFaberList?.ForEach(x =>
                {
                    newFaberList.Add(new TriageConfig()
                    {
                        HisConfigCode = x.FeibieCode, //如：202
                        TriageConfigName = x.FeibieName, //如：医保二档（成人）
                        ExtraCode = x.MedFeibieCode, //如：A31002

                        TriageConfigType = (int)TriageDict.Faber, // 1003

                        IsDisable = 1,
                        UnDeletable = false,
                        IsDeleted = false,

                        //TODO：目前该获得拼音方法有问题，由于使用的是单字获得拼音方式，因此对多音字会出现拿错拼音码问题，以后需要修改，用数据库或redis等方式记录词语和拼音的对应关系
                        Py = PyHelper.GetFirstPy(x.FeibieName), //如：YBED（CR）
                    });
                });
                int newFaberListCount = newFaberList.Count;
                stopwatch.Stop();
                long timeJson2List = stopwatch.ElapsedMilliseconds;

                stopwatch.Restart();
                var currentFaberList = await _triageConfigRepository
                                               .Where(x => x.TriageConfigType == (int)TriageDict.Faber)
                                               .Where(x => !x.IsDeleted)
                                               .OrderBy(o => o.TriageConfigCode)
                                               .IgnoreQueryFilters().ToListAsync();
                int currentFaberListCount = currentFaberList.Count;
                stopwatch.Stop();
                long timeGetCurrentFaberList = stopwatch.ElapsedMilliseconds;

                stopwatch.Restart();
                int addCount = 0;
                if (newFaberList.Count > 0)
                {
                    // 获得现在费别最大序号
                    int maxId = 0;
                    foreach (var item in currentFaberList)
                    {
                        if (int.TryParse(item.TriageConfigCode.Split('_')[1], out var id))
                        {
                            maxId = id;
                        }
                    }

                    foreach (var newFaberItem in newFaberList)
                    {
                        var currentSelectFaber = currentFaberList.Find(x => x.HisConfigCode == newFaberItem.HisConfigCode);
                        if (currentSelectFaber == null) //在现有列表中不存在，需要新增
                        {
                            maxId++;
                            // 自动生成字典编码，类型名+”_“+顺序生成序号000~999
                            string code = $"{TriageDict.Faber.ToString()}_{(maxId):D3}";

                            TriageConfig addFaber = new TriageConfig()
                            {
                                HisConfigCode = newFaberItem.HisConfigCode,
                                TriageConfigName = newFaberItem.TriageConfigName,
                                ExtraCode = newFaberItem.ExtraCode,
                                TriageConfigCode = code,

                                TriageConfigType = (int)TriageDict.Faber,
                                Py = PyHelper.GetFirstPy(newFaberItem.TriageConfigName),
                                Sort = maxId,
                                IsDisable = 1,
                                UnDeletable = false,
                                IsDeleted = false,
                                Platform = 0,

                                CreationTime = DateTime.Now,
                                AddUser = CurrentUser.UserName,
                            };
                            await _triageConfigRepository.InsertAsync(addFaber);
                            addCount++;
                            _log.Info($"{DateTime.Now}费别列表新增内容,{JsonConvert.SerializeObject(addFaber)}");
                        }
                        #region 在现有列表中存在，需要更新，目前怕出现问题暂时不更新
                        //else
                        //{
                        //    //currentSelectFaber.HisConfigCode = newFaberItem.HisConfigCode;
                        //    currentSelectFaber.TriageConfigName = newFaberItem.TriageConfigName;
                        //    currentSelectFaber.ExtraCode = newFaberItem.ExtraCode;
                        //    currentSelectFaber.TriageConfigCode = newFaberItem.TriageConfigCode;
                        //    currentSelectFaber.IsDisable = 1;
                        //    currentSelectFaber.UnDeletable = false;
                        //    currentSelectFaber.IsDeleted = false;
                        //    currentSelectFaber.ModUser = CurrentUser.UserName;
                        //    currentSelectFaber.LastModificationTime = DateTime.Now;
                        //    currentSelectFaber.DeleteUser = null;
                        //    currentSelectFaber.DeletionTime = null;

                        //    await _triageConfigRepository.UpdateAsync(currentSelectFaber);
                        //    _log.Info($"{DateTime.Now}费别列表更新内容,{JsonConvert.SerializeObject(currentSelectFaber)}");
                        //}
                        #endregion
                    }
                }
                stopwatch.Stop();
                long timeAddAndUpdateDb = stopwatch.ElapsedMilliseconds;

                #region 把新列表中没有的费别，在数据库中设置删除，目前怕出现问题暂时不设置软删除
                stopwatch.Restart();
                //var toDeleteFaberList = currentFaberList.FindAll(x => !newFaberList.Exists(y => y.HisConfigCode == x.HisConfigCode));
                //foreach (var item in toDeleteFaberList)
                //{
                //    item.IsDeleted = true;
                //    item.DeleteUser = CurrentUser.UserName;
                //    item.DeletionTime = DateTime.Now;

                //    //await _triageConfigRepository.UpdateAsync(item);
                //    _log.Info($"{DateTime.Now}费别列表设置软删除内容,{JsonConvert.SerializeObject(item)}");
                //}
                stopwatch.Stop();
                long timeDeleteDb = stopwatch.ElapsedMilliseconds;
                #endregion

                _log.Info($"【TriageConfigService】【SyncFaberListAsync】【同步Fabert数据成功】");

                { //清楚费别Redis缓存
                    string delKey = $"{_configuration.GetSection("ServiceName").Value}:TriageConfig";
                    bool isDel = _redis.HashDelete(delKey, TriageDict.Faber.ToString());
                    _log.Info($"【TriageConfigService】【SyncFaberListAsync】【清除费别Redis缓存】key:{delKey}, field:{TriageDict.Faber.ToString()}，redis是否删除成功：{isDel}");
                }

                _log.Info($@"
                    把MQ获得数据转换为费别列表耗时: {timeJson2List}ms，该列表数量：{newFaberListCount}，
                    获得现有费别列表耗时: {timeGetCurrentFaberList}ms，现有数量：{currentFaberListCount}，
                    新增及更新诊断耗时: {timeAddAndUpdateDb}ms，新增数量：{addCount}，
                    软删除诊断耗时: {timeDeleteDb}ms，
                    共耗时：{timeJson2List + timeGetCurrentFaberList + timeAddAndUpdateDb + timeDeleteDb}ms
                ");

                await uow.CompleteAsync();
            }
            catch (Exception e)
            {
                await uow.RollbackAsync();
                _log.Warning($"【TriageConfigService】【SyncFaberListAsync】【同步Fabert数据错误】【Msg：{e}】");
                throw;
            }   
        }
    }
}