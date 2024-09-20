using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using NPOI.Util.Collections;
using NUglify.Helpers;
using SamJan.MicroService.PreHospital.Core;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Uow;
using JsonResult = SamJan.MicroService.PreHospital.Core.JsonResult;

namespace SamJan.MicroService.PreHospital.TriageService
{
    /// <summary>
    /// 分诊报表设置Api
    /// </summary>
    [Auth("ReportSetting")]
    [Authorize]
    public class ReportSettingAppService : ApplicationService, IReportSettingAppService
    {
        private readonly ILogger<ReportSettingAppService> _log;
        private readonly IDatabase _redis;
        private readonly string _serviceName;
        private readonly IRepository<ReportSetting> _reportSettingRepository;
        private readonly IConfiguration _configuration;
        private readonly IReportPermissionRepository _reportPermission;


        private ITriageConfigAppService _triageConfigService;
        private ITriageConfigAppService TriageConfigService => LazyGetRequiredService(ref _triageConfigService);

        private IDapperRepository _dapperRepository;
        private IDapperRepository DapperRepository => LazyGetRequiredService(ref _dapperRepository);


        private DataTableExtensions _dataTableExtensions;
        private DataTableExtensions DataTableExtensions => LazyGetRequiredService(ref _dataTableExtensions);

        public ReportSettingAppService(ILogger<ReportSettingAppService> log, IRepository<ReportSetting> reportSettingRepository,
            RedisHelper redisHelper, IConfiguration configuration, IReportPermissionRepository reportPermission)
        {
            _log = log;
            _reportSettingRepository = reportSettingRepository;
            _redis = redisHelper.GetDatabase();
            _configuration = configuration;
            _serviceName = configuration.GetSection("ServiceName").Value;
            _reportPermission = reportPermission;
        }

        /// <summary>
        /// 获取所有分诊报表设置
        /// </summary>
        /// <param name="isEnabled"> -1:查询所有；0:禁用；1:启用 </param>
        /// <param name="platform">平台标识，1:院前急救，2：预检分诊，3：急诊管理</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [Auth("ReportSetting" + PermissionDefinition.Separator + PermissionDefinition.List)]
        public async Task<JsonResult<List<ReportSettingListDto>>> GetListAsync(int isEnabled = -1, int type = 0, int platform = -1, CancellationToken cancellationToken = default)
        {
            try
            {
                List<ReportSettingDto> dtos;
                if (await _redis.KeyExistsAsync($"{_serviceName}:ReportSetting"))
                {
                    _log.LogTrace("【ReportSettingAppService】【GetListAsync】【获取所有分诊报表】【从Redis缓存中捕捉到数据】");
                    var values = await _redis.HashValuesAsync($"{_serviceName}:ReportSetting");
                    var json = $"[{string.Join(",", values)}]";
                    dtos = JsonSerializer.Deserialize<List<ReportSettingDto>>(json);
                }
                else
                {
                    _log.LogTrace("【ReportSettingAppService】【GetListAsync】【获取所有分诊报表】【从Redis缓存中未捕捉到数据，开始从DB中获取数据】");
                    List<ReportSetting> list = await _reportSettingRepository.Include(c => c.ReportSettingQueryOption)
                        .ToListAsync(cancellationToken: cancellationToken);
                    dtos = list.BuildAdapter().AdaptToType<List<ReportSettingDto>>();

                    _log.LogTrace("【ReportSettingAppService】【GetListAsync】【获取所有分诊报表】" +
                               "【从DB中获取数据成功，开始写入Redis缓存】");
                    HashEntry[] hashList = new HashEntry[dtos.Count];
                    int index = 0;
                    dtos.ForEach(item =>
                    {
                        var json = JsonSerializer.Serialize(item);
                        hashList[index++] = new HashEntry(item.Id.ToString(), json);
                    });

                    await _redis.HashSetAsync($"{_serviceName}:ReportSetting", hashList);
                    _log.LogTrace("【ReportSettingAppService】【GetListAsync】【获取所有分诊报表】" +
                               "【写入Redis缓存成功】");
                }

                var dict = await TriageConfigService.GetTriageConfigByRedisAsync(TriageDict.TriageReportType
                    .ToString(), 1);
                var configDtos = dict[TriageDict.TriageReportType.ToString()];
                dtos = dtos.WhereIf(isEnabled > -1, x => x.IsEnabled == isEnabled)
                 .OrderBy(o => o.Sort)
                 .ToList();
                var res = configDtos.WhereIf(platform != -1, x => x.Platform == platform || x.Platform == 0).Select(item => new ReportSettingListDto
                {
                    Id = item.Id,
                    ReportTypeName = item.TriageConfigName,
                    ReportTypeCode = item.TriageConfigCode,
                    Sort = item.Sort,
                    ReportSettingDto = dtos.FindAll(x => x.ReportTypeCode == item.TriageConfigCode)
                })
                    .OrderBy(o => o.Sort)
                    .ToList();

                string userName = CurrentUser.UserName;
                if (type == 1)
                {
                    List<ReportPermission> reportPermissions = _reportPermission.GetList(userName);
                    List<string> reportNames = reportPermissions.Select(x => x.ReportName).ToList();
                    foreach (ReportSettingListDto item in res)
                    {

                        item.ReportSettingDto = item.ReportSettingDto.Where(x => reportNames.Contains(x.ReportName)).ToList();
                    }
                }
                else
                {
                    List<ReportPermission> reportPermissions = await _reportPermission.GetListAsync();
                    List<string> hasPermissionReportNames = reportPermissions.Where(x => x.UserName == userName).Select(x => x.ReportName).Distinct().ToList();
                    List<string> noPermissions = reportPermissions.Where(x => !hasPermissionReportNames.Contains(x.ReportName)).Select(x => x.ReportName).Distinct().ToList();
                    foreach (ReportSettingListDto item in res)
                    {

                        item.ReportSettingDto = item.ReportSettingDto.Where(x => !noPermissions.Contains(x.ReportName)).ToList();
                    }
                }

                return JsonResult<List<ReportSettingListDto>>.Ok(data: res);
            }
            catch (Exception e)
            {
                _log.LogWarning($"【ReportSettingAppService】【GetListAsync】【获取所有分诊报表错误】【Msg：{e}】");
                return JsonResult<List<ReportSettingListDto>>.Fail(e.Message);
            }
        }

        /// <summary>
        /// 根据报表Id获取报表设置
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [Auth("ReportSetting" + PermissionDefinition.Separator + PermissionDefinition.Get)]
        public async Task<JsonResult<ReportSettingDto>> GetAsync(Guid id, CancellationToken cancellationToken = default)
        {
            try
            {
                _log.LogTrace("【ReportSettingAppService】【GetAsync】【根据报表Id获取报表设置开始】");
                ReportSettingDto dto;
                if (await _redis.KeyExistsAsync($"{_serviceName}:ReportSetting"))
                {
                    var value = await _redis.HashGetAsync($"{_serviceName}:ReportSetting", id.ToString());
                    if (!string.IsNullOrWhiteSpace(value))
                    {
                        _log.LogTrace("【ReportSettingAppService】【GetAsync】【根据报表Id获取报表设置】【从Redis缓存捕捉到数据】");
                        dto = JsonSerializer.Deserialize<ReportSettingDto>(value);
                        _log.LogInformation("【ReportSettingAppService】【GetAsync】【根据报表Id获取报表设置成功】");
                        return JsonResult<ReportSettingDto>.Ok(data: dto);
                    }
                }

                _log.LogTrace("【ReportSettingAppService】【GetAsync】【根据报表Id获取报表设置】【未从Redis缓存捕捉到数据，开始从DB中获取数据】");
                var setting =
                    await _reportSettingRepository.Include(c => c.ReportSettingQueryOption)
                        .FirstOrDefaultAsync(x => x.Id == id, cancellationToken: cancellationToken);
                dto = setting.BuildAdapter().AdaptToType<ReportSettingDto>();
                _log.LogTrace("【ReportSettingAppService】【GetAsync】【根据报表Id获取报表设置】【从DB中获取数据成功开始写入Redis缓存】");
                var json = JsonSerializer.Serialize(dto);
                await _redis.HashSetAsync($"{_serviceName}:ReportSetting", dto.Id.ToString(), json);
                _log.LogTrace("【ReportSettingAppService】【GetAsync】【根据报表Id获取报表设置】【写入Redis缓存成功】");
                _log.LogInformation($"【ReportSettingAppService】【GetAsync】【根据报表Id获取报表设置成功】");
                return JsonResult<ReportSettingDto>.Ok(data: dto);
            }
            catch (Exception e)
            {
                _log.LogWarning($"【ReportSettingAppService】【GetAsync】【根据报表Id获取报表设置错误】【Msg：{e}】");
                return JsonResult<ReportSettingDto>.Fail(e.Message);
            }
        }

        /// <summary>
        /// 保存分诊报表设置
        /// </summary>
        /// <param name="dto"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [UnitOfWork]
        [Auth("ReportSetting" + PermissionDefinition.Separator + PermissionDefinition.Save)]
        public async Task<JsonResult> SaveReportSettingAsync(ReportSettingDto dto, CancellationToken cancellationToken = default)
        {
            try
            {
                _log.LogTrace("【ReportSettingAppService】【SaveReportSettingAsync】【保存分诊报表设置开始】");
                ReportSetting rptSettingDto = dto.BuildAdapter().AdaptToType<ReportSetting>();
                string conString = _configuration.GetSection("ConnectionStrings:ReportConnection").Value; //获取分诊报表数据库连接字符串，配置受限数据库用户连接
                rptSettingDto.ConnectionString = !string.IsNullOrWhiteSpace(conString) ? conString : _configuration.GetSection("ConnectionStrings:DefaultConnection").Value; //次选默认数据库连接字符串

                var masterDbContext = _reportSettingRepository.GetDbContext();
                if (dto.Id == Guid.Empty)
                {
                    rptSettingDto.SetId(GuidGenerator.Create());
                    rptSettingDto.AddUser = CurrentUser.UserName;
                    masterDbContext.Entry(rptSettingDto).State = EntityState.Added;
                }
                else
                {
                    var entityDB = await _reportSettingRepository.Include(c => c.ReportSettingQueryOption)
                        .AsNoTracking()
                        .FirstOrDefaultAsync(x => x.Id == dto.Id, cancellationToken);
                    if (entityDB == null)
                    {
                        _log.LogError("【ReportSettingAppService】【SaveReportSettingAsync】【保存分诊报表设置失败】" +
                                   "【Msg：该报表设置不存在】");
                        return JsonResult.Fail("该报表设置不存在");
                    }

                    if (rptSettingDto.ReportSettingQueryOption != null && rptSettingDto.ReportSettingQueryOption.Count > 0)
                    {
                        rptSettingDto.ReportSettingQueryOption.ToList().ForEach(queryOpt =>
                        {
                            queryOpt.QueryFiled = queryOpt.QueryFiled.ToLower(CultureInfo.InvariantCulture);
                            queryOpt.ReportSettingId = rptSettingDto.Id;
                            /*if (item.DataSource.Contains("from", StringComparison.OrdinalIgnoreCase) 
                                && item.DataSource.Contains("select", StringComparison.OrdinalIgnoreCase))
                            {
                                item.DataSourceFlag = 1;
                            }*/

                            if (queryOpt.Id == Guid.Empty)
                            {
                                queryOpt.SetId(GuidGenerator.Create());
                                queryOpt.AddUser = CurrentUser.UserName;
                                masterDbContext.Entry(queryOpt).State = EntityState.Added;
                            }
                            else
                            {
                                queryOpt.AddUser = entityDB.ReportSettingQueryOption.FirstOrDefault(x => x.Id == queryOpt.Id)
                                    ?.AddUser;
                                queryOpt.ModUser = CurrentUser.UserName;
                                masterDbContext.Entry(queryOpt).State = EntityState.Modified;
                            }
                        });
                    }

                    // 删除（设置IsDeleted）不需要的查询条件
                    if (entityDB.ReportSettingQueryOption != null && entityDB.ReportSettingQueryOption.Count > 0)
                    {
                        entityDB.ReportSettingQueryOption.ForEach(queryOpt =>
                        {
                            if (rptSettingDto.ReportSettingQueryOption.Where(x => x.Id == queryOpt.Id).Count() == 0)
                            {
                                rptSettingDto.ReportSettingQueryOption.Add(queryOpt);
                                masterDbContext.Entry(queryOpt).State = EntityState.Deleted;
                            }
                        });
                    }

                    rptSettingDto.AddUser = entityDB.AddUser;
                    rptSettingDto.ModUser = CurrentUser.UserName;
                    masterDbContext.Entry(rptSettingDto).State = EntityState.Modified;
                }

                if (await masterDbContext.SaveChangesAsync(cancellationToken) > 0)
                {
                    _log.LogInformation("【ReportSettingAppService】【SaveReportSettingAsync】【保存分诊报表设置成功】");
                    _log.LogTrace("【ReportSettingAppService】【SaveReportSettingAsync】【保存分诊报表设置结束】【开始更新Redis缓存】");
                    // 过滤掉IsDeleted为true的数据
                    if (rptSettingDto.ReportSettingQueryOption != null && rptSettingDto.ReportSettingQueryOption.Count > 0)
                    {
                        rptSettingDto.ReportSettingQueryOption = rptSettingDto.ReportSettingQueryOption.Where(x => !x.IsDeleted).ToList();
                    }
                    var newDto = rptSettingDto.BuildAdapter().AdaptToType<ReportSettingDto>();
                    var json = JsonSerializer.Serialize(newDto);
                    await _redis.HashSetAsync($"{_serviceName}:ReportSetting", newDto.Id.ToString(), json);
                    _log.LogTrace("【ReportSettingAppService】【SaveReportSettingAsync】【保存分诊报表设置结束】【更新Redis缓存成功】");
                    return JsonResult.Ok();
                }

                _log.LogError("【ReportSettingAppService】【SaveReportSettingAsync】【保存分诊报表设置失败】【Msg：写入DB失败】");
                return JsonResult.Fail("保存失败！请检查后重试");
            }
            catch (Exception e)
            {
                _log.LogWarning($"【ReportSettingAppService】【SaveReportSettingAsync】【保存分诊报表设置错误】【Msg：{e}】");
                return JsonResult.Fail(e.Message);
            }
        }

        /// <summary>
        /// 根据Id删除分诊报表设置
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost]
        [Auth("ReportSetting" + PermissionDefinition.Separator + PermissionDefinition.Delete)]
        public async Task<JsonResult> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            try
            {
                _log.LogTrace("【ReportSettingAppService】【DeleteAsync】【保存分诊报表设置开始】");
                var masterDbContext = _reportSettingRepository.GetDbContext();
                var entity = await _reportSettingRepository.Include(c => c.ReportSettingQueryOption)
                    .FirstOrDefaultAsync(x => x.Id == id, cancellationToken: cancellationToken);
                if (entity == null)
                {
                    _log.LogError("【ReportSettingAppService】【DeleteAsync】【保存分诊报表设置失败】【Msg：该报表设置已经被删除】");
                    return JsonResult.Fail("该报表设置已经被删除");
                }

                entity.DeleteUser = CurrentUser.UserName;
                masterDbContext.Entry(entity).State = EntityState.Deleted;
                if (entity.ReportSettingQueryOption != null && entity.ReportSettingQueryOption.Count > 0)
                {
                    foreach (var item in entity.ReportSettingQueryOption)
                    {
                        item.IsDeleted = true;
                    }
                }

                if (await masterDbContext.SaveChangesAsync(cancellationToken) > 0)
                {
                    _log.LogInformation("【ReportSettingAppService】【DeleteAsync】【删除分诊报表设置成功】");
                    _log.LogTrace("【ReportSettingAppService】【DeleteAsync】【删除分诊报表设置结束】【开始更新Redis缓存】");
                    await _redis.HashDeleteAsync($"{_serviceName}:ReportSetting", id.ToString());
                    return JsonResult.Ok();
                }

                _log.LogError("【ReportSettingAppService】【DeleteAsync】【删除分诊报表设置失败】【Msg：删除DB数据失败】");
                return JsonResult.Fail("删除分诊报表设置失败！请检查后重试！");
            }
            catch (Exception e)
            {
                _log.LogWarning($"【ReportSettingAppService】【DeleteAsync】【根据Id删除分诊报表设置错误】【Msg：{e}】");
                return JsonResult.Fail(e.Message);
            }
        }

        /// <summary>
        /// 根据报表设置返回数据
        /// </summary>
        /// <param name="dto"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>返回结果为json字符串</returns>
        [Auth("ReportSetting" + PermissionDefinition.Separator + "GetReportData")]
        public async Task<JsonResult<PagedReportDataDto>> GetReportDataAsync(ReportQueryOptionsDto dto,
            CancellationToken cancellationToken = default)
        {
            var res = new PagedReportDataDto();
            try
            {
                _log.LogTrace("【ReportSettingAppService】【GetReportDataAsync】【根据报表设置返回数据开始");
                var jsonResult = await GetAsync(dto.ReportId, cancellationToken);
                ReportSettingDto setting = jsonResult.Data;
                if (string.IsNullOrEmpty(setting.ConnectionString))
                {
                    return JsonResult<PagedReportDataDto>.Fail("报表未设置数据库连接字符串！", res);
                }

                if (setting != null)
                {
                    var queryStr = "";
                    var isNeedAnd = false;  // where条件开头是否不存在and
                    var isAddWhere = false;
                    if (setting.ReportSql.Contains("where", StringComparison.OrdinalIgnoreCase))
                    {
                        queryStr += " and ";
                    }
                    else
                    {
                        queryStr += " where ";
                    }

                    if (!string.IsNullOrEmpty(dto.QueryData))
                    {
                        var json = JObject.Parse(dto.QueryData);

                        _log.LogTrace("【ReportSettingAppService】【GetReportDataAsync】【根据报表设置返回数据】【开始拼接SQL查询条件】");

                        var queryOptionList = setting.ReportSettingQueryOption.ToList();
                        foreach (var item in queryOptionList.Where(item =>
                            !string.IsNullOrWhiteSpace(json[item.QueryFiled]?.ToString())))
                        {
                            var queryFiledValue = json[item.QueryFiled.ToLower(CultureInfo.InvariantCulture)]?.ToString();

                            if (string.IsNullOrWhiteSpace(queryFiledValue))
                                continue;

                            var queryType = item.QueryType.ToLowerInvariant();
                            isAddWhere = true;

                            switch (queryType)
                            {
                                case "datetime":
                                    {
                                        var timeArray = queryFiledValue.Split('~');
                                        bool haveStartTime = !string.IsNullOrWhiteSpace(timeArray[0]);
                                        bool haveEndTime = !string.IsNullOrWhiteSpace(timeArray[1]);
                                        //where 条件后是否存在 and 不存在则此where条件以 and 开头
                                        if (isNeedAnd)
                                        {
                                            if (haveStartTime)
                                                queryStr += $" AND {item.QueryFiled} >= '" + Convert.ToDateTime(timeArray[0]).ToString("yyyy-MM-dd HH:mm:ss.fff") + "' ";
                                            if (haveEndTime)
                                                queryStr += $"AND {item.QueryFiled} <= '" + Convert.ToDateTime(timeArray[1]).ToString("yyyy-MM-dd HH:mm:ss.fff") + "' ";
                                        }
                                        else
                                        {
                                            if (haveStartTime)
                                                queryStr += $" {item.QueryFiled} >= '" + Convert.ToDateTime(timeArray[0]).ToString("yyyy-MM-dd HH:mm:ss.fff") + "' ";
                                            if (haveEndTime)
                                            {
                                                if (haveStartTime)
                                                    queryStr += " AND ";
                                                queryStr += $" {item.QueryFiled} <= '" + Convert.ToDateTime(timeArray[1]).ToString("yyyy-MM-dd HH:mm:ss.fff") + "' ";
                                            }
                                        }

                                        break;
                                    }
                                // 文本框支持多选
                                case "input":

                                    if (isNeedAnd)
                                    {
                                        queryStr += $" AND {item.QueryFiled}  LIKE N'%{queryFiledValue}%' ";
                                    }
                                    else
                                    {
                                        queryStr += $" {item.QueryFiled}  LIKE N'%{queryFiledValue}%' ";
                                    }

                                    break;

                                case "checkbox":
                                    var jsonArray = JArray.Parse(queryFiledValue);
                                    // 如果查询条件数组为空，则不需要拼接查询条件
                                    if (!jsonArray.IsEmptyOrNull())
                                    {
                                        if (isNeedAnd)
                                        {
                                            queryStr += $" AND {item.QueryFiled} in (";
                                            var str = jsonArray.Aggregate("", (current, array) => current + $" N'{array}',");
                                            str = str.TrimEnd(',');
                                            queryStr += str + ") ";
                                        }
                                        else
                                        {
                                            queryStr += $" {item.QueryFiled} in (";
                                            var str = jsonArray.Aggregate("", (current, array) => current + $" N'{array}',");
                                            str = str.TrimEnd(',');
                                            queryStr += str + ") ";
                                        }
                                    }

                                    break;

                                default:

                                    if (isNeedAnd)
                                    {
                                        queryStr += $" AND {item.QueryFiled}=N'{queryFiledValue}' ";
                                    }
                                    else
                                    {
                                        queryStr += $" {item.QueryFiled}=N'{queryFiledValue}' ";
                                    }


                                    break;
                            }

                            isNeedAnd = true;
                        }
                    }

                    //若未添加where条件则把 where 去掉
                    if (!isAddWhere)
                    {
                        queryStr = "";
                    }
                    //若存在占位符{where}
                    if (setting.ReportSql.Contains("{where}"))
                    {
                        setting.ReportSql = setting.ReportSql.Replace("{where}", queryStr);
                    }
                    else
                    {
                        setting.ReportSql += queryStr;
                    }
                    _log.LogTrace("【ReportSettingAppService】【GetReportDataAsync】【根据报表设置返回数据】" +
                               $"【拼接SQL查询条件结束，开始根据SQL查询数据. Sql：{setting.ReportSql}】");
                    _log.LogInformation($"【sql：{setting.ReportSql}】");
                    var dbKey = "TriageService";
                    if (setting.ConnectionString.IsNotEmptyOrNull())
                    {
                        var conn = setting.ConnectionString.Split(";");
                        foreach (var item in conn)
                        {
                            if (item.Contains("Database"))
                            {
                                dbKey = item.Split("=")?[1];
                            }
                        }
                    }
                    var dt = await DapperRepository.GetDataTableExecuteReaderAsync(setting.ReportSql, null, dbKey, setting.ConnectionString);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        var dictRes = await TriageConfigService.GetTriageConfigListAsync("");
                        if (dictRes.Data != null)
                        {
                            _log.LogTrace("【ReportSettingAppService】【GetReportDataAsync】【根据报表设置返回数据】" +
                                       "【根据SQL查询数据结束，开始将字典数据Code转换为Name】");
                            var dictList = new List<TriageConfigDto>();
                            foreach (var dict in dictRes.Data)
                            {
                                dictList.AddRange(dict.Value);
                            }

                            for (var i = 0; i < dt.Rows.Count; i++)
                            {
                                for (var j = 0; j < dt.Columns.Count; j++)
                                {
                                    var value = dt.Rows[i][j].ToString();
                                    var result = "";
                                    if (string.IsNullOrWhiteSpace(value)) continue;
                                    if (!value.Contains("_")) continue;
                                    foreach (var item in value.Split(','))
                                    {
                                        var array = item.Split('_');
                                        var parseRes = Enum.TryParse(array[0],
                                            out TriageDict triageConfigEnum);
                                        if (!parseRes) continue;
                                        var dict = dictList.FirstOrDefault(x =>
                                            x.TriageConfigType == (int)triageConfigEnum &&
                                            x.TriageConfigCode == item);
                                        if (dict != null)
                                        {
                                            result += dict.TriageConfigName + ",";
                                        }
                                    }

                                    dt.Rows[i][j] = result.TrimEnd(',');
                                }
                            }

                            _log.LogTrace("【ReportSettingAppService】【GetReportDataAsync】【根据报表设置返回数据】" +
                                       "【将字典数据Code转换为Name结束】");
                        }
                    }

                    if (dt != null)
                    {
                        dt.DefaultView.Sort =
                            $"{setting.ReportSortFiled} {(Convert.ToBoolean(setting.OrderType) ? "DESC" : "ASC")}";
                        _log.LogInformation("【ReportSettingAppService】【GetReportDataAsync】【根据报表设置返回数据成功】");
                        res.TotalCount = dt.Rows.Count;
                        if (dto.IsNeedPaged)
                        {
                            dt = GetPageToDataTable(dt, dto.SkipCount, dto.MaxResultCount);
                        }

                        res.Dt = dt.DefaultView.ToTable();
                        return JsonResult<PagedReportDataDto>.Ok(data: res);
                    }
                }

                _log.LogError(
                    $"【ReportSettingAppService】【GetReportDataAsync】【根据报表设置返回数据开始】【Msg：未查询到该报表设置。Id：{dto.ReportId}】");
                return JsonResult<PagedReportDataDto>.Fail("未查询到该报表设置，请检查重试！", res);
            }
            catch (Exception e)
            {
                _log.LogWarning($"【ReportSettingAppService】【GetReportDataAsync】【根据Id删除分诊报表设置错误】【Msg：{e}】");
                return JsonResult<PagedReportDataDto>.Fail(e.Message, res);
            }
        }


        /// <summary>
        /// Select 页面 数据分页
        /// </summary>
        /// <param name="dt">数据源</param>
        /// <param name="pageIndex">第几页</param>
        /// <param name="pageSize">每页多少条</param>
        /// <returns></returns>
        [ApiExplorerSettings(IgnoreApi = true)]
        private DataTable GetPageToDataTable(DataTable dt, int pageIndex, int pageSize)
        {
            if (pageIndex == 0)
                return dt; //0页代表每页数据，直接返回

            if (dt == null)
            {
                var table = new DataTable();
                return table;
            }

            var newDt = dt.Copy();
            newDt.Clear(); //copy dt的框架

            var rowBegin = (pageIndex - 1) * pageSize;
            var rowEnd = pageIndex * pageSize; //要展示的数据条数

            if (rowBegin >= dt.Rows.Count)
                return newDt; //源数据记录数小于等于要显示的记录，直接返回dt

            if (rowEnd > dt.Rows.Count)
                rowEnd = dt.Rows.Count;
            for (var i = rowBegin; i <= rowEnd - 1; i++)
            {
                var newDr = newDt.NewRow();
                var dr = dt.Rows[i];
                foreach (DataColumn column in dt.Columns)
                {
                    newDr[column.ColumnName] = dr[column.ColumnName];
                }

                newDt.Rows.Add(newDr);
            }

            return newDt;
        }


        /// <summary>
        /// 根据报表Id初始化报表
        /// </summary>
        /// <param name="reportId">报表Id</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [Auth("ReportSetting" + PermissionDefinition.Separator + "InitReportSetting")]
        [HttpGet]
        public async Task<JsonResult<InitReportDto>> InitialReportSettingAsync(Guid reportId,
            CancellationToken cancellationToken = default)
        {
            try
            {
                _log.LogTrace("【ReportSettingAppService】【GetReportDataAsync】【根据报表Id初始化报表开始】");
                var jsonResult = await GetAsync(reportId, cancellationToken);
                if (jsonResult.Code == 200 && jsonResult.Data != null)
                {
                    var setting = jsonResult.Data;
                    var dto = new InitReportDto
                    {
                        ReportHead = setting.ReportHead,
                    };

                    if (setting.ReportSettingQueryOption != null && setting.ReportSettingQueryOption.Count > 0)
                    {
                        var dbKey = "TriageService";
                        if (setting.ConnectionString.IsNotEmptyOrNull())
                        {
                            var conn = setting.ConnectionString.Split(";");
                            foreach (var c in conn)
                            {
                                if (c.Contains("Database"))
                                {
                                    dbKey = c.Split("=")?[1];
                                }
                            }
                        }
                        dto.InitReportQuery ??= new List<InitReportQueryDto>();
                        var item = setting.ReportSettingQueryOption.OrderBy(o => o.Sort).ToList();
                        foreach (ReportSettingQueryOptionDto t in item)
                        {
                            var queryDto = new InitReportQueryDto
                            {
                                QueryName = string.IsNullOrWhiteSpace(t.DisplayName) ? t.QueryName : t.DisplayName,
                                QueryType = t.QueryType,
                                QueryFiled = t.QueryFiled,
                                DefaultValue = t.DefaultValue
                            };

                            if (!string.IsNullOrWhiteSpace(t.DataSource))
                            {
                                var dt = await DapperRepository.GetDataTableExecuteReaderAsync(t.DataSource, null, dbKey, setting.ConnectionString);
                                queryDto.QueryData = dt;
                            }

                            dto.InitReportQuery.Add(queryDto);
                        }
                    }

                    _log.LogInformation("【ReportSettingAppService】【GetReportDataAsync】【根据报表Id初始化报表成功】");
                    return JsonResult<InitReportDto>.Ok(data: dto);
                }

                _log.LogError($"【ReportSettingAppService】【GetReportDataAsync】【根据报表Id初始化报表成功】" +
                           $"【Msg：未查询到该报表设置。Id：{reportId}】");
                return JsonResult<InitReportDto>.Fail("未查询到该报表设置");
            }
            catch (Exception e)
            {
                _log.LogWarning($"【ReportSettingAppService】【GetReportDataAsync】【根据报表Id初始化报表错误】【Msg：{e}】");
                return JsonResult<InitReportDto>.Fail(e.Message);
            }
        }

        /// <summary>
        /// 导出数据到Excel
        /// </summary>
        /// <param name="dto"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        [Auth("ReportSetting" + PermissionDefinition.Separator + "ImportToExcel")]
        public async Task<IActionResult> ImportDataToExcelAsync(ReportQueryOptionsDto dto,
            CancellationToken cancellationToken = default)
        {
            try
            {
                var settingRes = await GetAsync(dto.ReportId, cancellationToken);
                if (settingRes.Code == 200 && settingRes.Data != null)
                {
                    dto.IsNeedPaged = false;
                    var jsonObject = JObject.Parse(dto.QueryData);
                    var properties = jsonObject.Properties().ToList();
                    string startdate = "start1", endDate = "end1";
                    string key = "", value = "";
                    for (int i = 0; i < properties.Count(); i++)
                    {
                        // 获取属性
                        JProperty property = properties[i];

                        if (property.Name.Contains(startdate))
                        {
                            key = property.Name.Replace(startdate, "");
                            value = property.Value.ToString();
                        }
                        if (property.Name.Contains(endDate))
                        {
                            value += " ~ " + property.Value.ToString();
                        }
                    }
                    jsonObject.Add(key, value);
                    dto.QueryData = jsonObject.ToString();
                    var dtRes = await GetReportDataAsync(dto, cancellationToken);
                    if (dtRes.Code == 200 && dtRes.Data != null)
                    {
                        var headerDtos = JsonSerializer.Deserialize<List<ReportHeaderDto>>(settingRes.Data.ReportHead,
                            new JsonSerializerOptions
                            {
                                PropertyNameCaseInsensitive = true
                            });
                        var buffer = await DataTableExtensions.DataTableToExcelAsync(headerDtos, dtRes.Data.Dt);
                        var file = new FileContentResult(buffer, "application/octet-stream")
                        {
                            FileDownloadName = $"{settingRes.Data.ReportName}.{DateTime.Now:yyyyMMddHHmmss}.xls"
                        };


                        return file;
                    }

                    _log.LogError("【ReportSettingAppService】【ImportDataToExcelAsync】" +
                               "【导出数据到Excel错误】【Msg：未查询到该报表设置】");
                    return null;
                }

                _log.LogError("【ReportSettingAppService】【ImportDataToExcelAsync】" +
                           "【导出数据到Excel错误】【Msg：未查询到数据！】");
                return null;
            }
            catch (Exception e)
            {
                _log.LogWarning($"【ReportSettingAppService】【ImportDataToExcelAsync】【导出数据到Excel错误】【Msg：{e}】");
                return null;
            }
        }
    }
}