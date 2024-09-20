using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SamJan.MicroService.PreHospital.Core;
using StackExchange.Redis;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace SamJan.MicroService.PreHospital.TriageService
{
    /// <summary>
    /// 快速通道Api
    /// </summary>
    [Auth("FastTrackRegisterInfo")]
    [Authorize]
    public class FastTrackRegisterInfoAppService : ApplicationService, IFastTrackRegisterInfoAppService
    {
        private readonly NLogHelper _log;
        private readonly IRepository<FastTrackRegisterInfo> _fastTrackRegisterInfoRepository;
        private readonly IConfiguration _configuration;
        private readonly IDatabase _redis;
        private readonly ITriageConfigAppService _triageConfigService;
        private readonly IFastTrackSettingAppService _iFastTrackSettingService;
        public FastTrackRegisterInfoAppService(NLogHelper log,
            IRepository<FastTrackRegisterInfo> fastTrackRegisterInfoRepository,
            RedisHelper redisHelper, IConfiguration configuration,
            ITriageConfigAppService triageConfigService, IFastTrackSettingAppService iFastTrackSettingService)
        {
            _log = log;
            _fastTrackRegisterInfoRepository = fastTrackRegisterInfoRepository;
            _configuration = configuration;
            _redis = redisHelper.GetDatabase();
            _triageConfigService = triageConfigService;
            _iFastTrackSettingService = iFastTrackSettingService;
        }

        /// <summary>
        /// 根据Id获取患者快速通道登记信息
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [Auth("FastTrackRegisterInfo" + PermissionDefinition.Separator + PermissionDefinition.Get)]
        public async Task<JsonResult<FastTrackRegisterInfoDto>> GetAsync(Guid id,
            CancellationToken cancellationToken = default)
        {
            try
            {
                _log.Trace("【FastTrackRegisterInfoAppService】【GetAsync】【根据Id获取患者快速通道登记信息开始】");
                var serviceName = _configuration.GetSection("ServiceName").Value;
                var cache = await _redis.HashGetAsync($"{serviceName}:FastTrackRegisterInfoList", id.ToString());
                FastTrackRegisterInfoDto dto;
                if (string.IsNullOrWhiteSpace(cache))
                {
                    _log.Trace("【FastTrackRegisterInfoAppService】【GetAsync】" +
                               "【根据Id获取患者快速通道登记信息】【Msg：Redis缓存未获取到该患者信息，开始从DB中获取】");
                    var model = await _fastTrackRegisterInfoRepository.GetAsync(x => x.Id == id,
                        cancellationToken: cancellationToken);
                    dto = model.BuildAdapter().AdaptToType<FastTrackRegisterInfoDto>();
                    if (dto != null)
                    {
                        _log.Trace("【FastTrackRegisterInfoAppService】【GetAsync】" +
                                   "【根据Id获取患者快速通道登记信息】【Msg：从DB中获取到该患者数据，开始存入Redis缓存】");
                        var json = JsonSerializer.Serialize(dto);
                        await _redis.HashSetAsync($"{serviceName}:FastTrackRegisterInfoList", dto.Id.ToString(), json);
                    }
                    else
                    {
                        _log.Error("【FastTrackRegisterInfoAppService】【GetAsync】" +
                                   "【根据Id获取患者快速通道登记信息】【Msg：Redis缓存与DB中都无该患者登记信息】");
                        return JsonResult<FastTrackRegisterInfoDto>.Fail("未查询到该患者登记信息");
                    }
                }
                else
                {
                    _log.Trace("【FastTrackRegisterInfoAppService】【GetAsync】" +
                               "【根据Id获取患者快速通道登记信息】【Msg：从Redis缓存中获取数据成功】");
                    dto = JsonSerializer.Deserialize<FastTrackRegisterInfoDto>(cache);
                }

                _log.Info("【FastTrackRegisterInfoAppService】【GetAsync】" +
                          "【根据Id获取患者快速通道登记信息成功】");
                _log.Trace("【FastTrackRegisterInfoAppService】【GetAsync】" +
                           "【根据Id获取患者快速通道登记信息结束】");
                return JsonResult<FastTrackRegisterInfoDto>.Ok(data: dto);
            }
            catch (Exception e)
            {
                _log.Warning("【FastTrackRegisterInfoAppService】【GetAsync】" +
                             $"【根据Id获取患者快速通道登记信息错误】【Msg：{e}】");
                return JsonResult<FastTrackRegisterInfoDto>.Fail();
            }
        }

        /// <summary>
        /// 根据输入项获取患者快速通道登记信息
        /// </summary>
        /// <param name="input"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [Auth("FastTrackRegisterInfo" + PermissionDefinition.Separator + PermissionDefinition.List)]
        public async Task<JsonResult<PagedResultDto<FastTrackRegisterInfoDto>>> GetListAsync(
            FastTrackRegisterInfoInput input, CancellationToken cancellationToken = default)
        {
            try
            {
                _log.Trace("【FastTrackRegisterInfoAppService】【GetListAsync】【根据输入项获取患者快速通道登记信息开始】");
                var serviceName = _configuration.GetSection("ServiceName").Value;
                List<FastTrackRegisterInfoDto> dtos;
                var configResult = await _triageConfigService.GetTriageConfigListAsync(TriageDict.Sex.ToString(), -1);
                var dict = configResult.Data[TriageDict.Sex.ToString()];
                if (!await _redis.KeyExistsAsync($"{serviceName}:FastTrackRegisterInfoList"))
                {
                    var list = await _fastTrackRegisterInfoRepository.GetListAsync(
                        cancellationToken: cancellationToken);
                    dtos = list.BuildAdapter().AdaptToType<List<FastTrackRegisterInfoDto>>();

                    var hashList = new HashEntry[dtos.Count];
                    var index = 0;
                    dtos.ForEach(item =>
                    {
                        var json = JsonSerializer.Serialize(item);
                        hashList[index++] = new HashEntry(item.Id.ToString(), json);
                    });

                    await _redis.HashSetAsync($"{serviceName}:FastTrackRegisterInfoList", hashList);
                }
                else
                {
                    var hashList = await _redis.HashValuesAsync($"{serviceName}:FastTrackRegisterInfoList");
                    var json = $"[{string.Join(",", hashList)}]";
                    dtos = JsonSerializer.Deserialize<List<FastTrackRegisterInfoDto>>(json);
                }

                dtos = dtos.WhereIf(
                            input.StartTime != null && input.EndTime != null && input.StartTime < input.EndTime,
                            x => x.CreationTime >= input.StartTime && x.CreationTime <= input.EndTime)
                        .WhereIf(!string.IsNullOrEmpty(input.ReceptionNurse),
                            x => x.ReceptionNurse == input.ReceptionNurse)
                        .WhereIf(!string.IsNullOrEmpty(input.PatientName),
                            x => x.PatientName.Contains(input.PatientName))
                        .WhereIf(!string.IsNullOrEmpty(input.PoliceStationCode),
                            x => x.PoliceStationId == Guid.Parse(input.PoliceStationCode))
                        .WhereIf(!string.IsNullOrEmpty(input.PoliceInfo),
                            x => x.PoliceCode.Contains(input.PoliceInfo) || x.PoliceName.Contains(input.PoliceInfo))
                        .OrderByDescending(o => o.CreationTime)
                        .ThenByDescending(o => o.LastModificationTime).ToList()
                    ;
                
                var totalCount = dtos.Count;
                dtos = dtos.Skip((input.SkipCount - 1) * input.MaxResultCount)
                    .Take(input.MaxResultCount)
                    .ToList();
                dtos.ForEach(t => { t.Sex = dict.FirstOrDefault(w => w.TriageConfigCode == t.Sex)?.TriageConfigName; });
                var res = new PagedResultDto<FastTrackRegisterInfoDto>
                {
                    TotalCount = totalCount,
                    Items = dtos
                };

                _log.Info("【FastTrackRegisterInfoAppService】【GetListAsync】【根据输入项获取患者快速通道登记信息成功】");
                _log.Trace("【FastTrackRegisterInfoAppService】【GetListAsync】【根据输入项获取患者快速通道登记信息结束】");
                return JsonResult<PagedResultDto<FastTrackRegisterInfoDto>>.Ok(data: res);
            }
            catch (Exception e)
            {
                _log.Warning($"【FastTrackRegisterInfoAppService】【GetListAsync】" +
                             $"【根据输入项获取患者快速通道登记信息错误】【Msg：{e}】");
                return JsonResult<PagedResultDto<FastTrackRegisterInfoDto>>.Fail(e.Message);
            }
        }

        /// <summary>
        /// 提交患者快速通道登记信息
        /// </summary>
        /// <param name="dto"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [Auth("FastTrackRegisterInfo" + PermissionDefinition.Separator + PermissionDefinition.Save)]
        public async Task<JsonResult> SaveFastTrackRegisterInfoAsync(CreateOrUpdateFastTrackRegisterInfoDto dto,
            CancellationToken cancellationToken = default)
        {
            try
            {
                _log.Trace("【FastTrackRegisterInfoAppService】【SaveFastTrackRegisterInfoAsync】【提交患者快速通道登记信息开始】");
                var model = dto.BuildAdapter().AdaptToType<FastTrackRegisterInfo>();
                var fastSetting =
                    await _iFastTrackSettingService.GetFastTrackSettingAsync(new FastTrackSettingWhereInput
                    { Id = dto.PoliceStationId });
                if (fastSetting.Data == null)
                {
                    _log.Error("【FastTrackRegisterInfoAppService】【SaveFastTrackRegisterInfoAsync】" +
                               "【提交患者快速通道登记信息失败】【Msg：不存在派出所信息】");
                    return JsonResult.Fail("不存在派出所信息");
                }

                var dbContext = _fastTrackRegisterInfoRepository.GetDbContext();
                if (dto.Id != Guid.Empty)
                {
                    if (await _fastTrackRegisterInfoRepository.AsNoTracking()
                        .CountAsync(x => x.Id == dto.Id, cancellationToken: cancellationToken) <= 0)
                    {
                        _log.Error("【FastTrackRegisterInfoAppService】【SaveFastTrackRegisterInfoAsync】" +
                                   "【提交患者快速通道登记信息失败】【Msg：不存在该患者的快速通道登记信息】");
                        return JsonResult.Fail("不存在该患者的快速通道登记信息");
                    }

                    model.SetId(dto.Id);
                    model.ModUser = CurrentUser.UserName;
                    model.PoliceStationName = fastSetting.Data.FastTrackName;
                    model.PoliceStationPhone = fastSetting.Data.FastTrackPhone;
                    dbContext.Entry(model).State = EntityState.Modified;
                }
                else
                {
                    model.AddUser = CurrentUser.UserName;
                    model.PoliceStationName = fastSetting.Data.FastTrackName;
                    model.PoliceStationPhone = fastSetting.Data.FastTrackPhone;
                    model.SetId(GuidGenerator.Create());
                    dbContext.Entry(model).State = EntityState.Added;

                }

                if (await dbContext.SaveChangesAsync(cancellationToken) > 0)
                {
                    _log.Trace("【FastTrackRegisterInfoAppService】【SaveFastTrackRegisterInfoAsync】" +
                               "【提交患者快速通道登记信息】【Msg：更新快速通道登记信息Redis缓存】");
                    var serviceName = _configuration.GetSection("ServiceName").Value;
                    var cache = model.BuildAdapter().AdaptToType<FastTrackRegisterInfoDto>();
                    var json = JsonSerializer.Serialize(cache);
                    await _redis.HashSetAsync($"{serviceName}:FastTrackRegisterInfoList", model.Id.ToString(), json);

                    _log.Info("【FastTrackRegisterInfoAppService】【SaveFastTrackRegisterInfoAsync】" +
                              "【提交患者快速通道登记信息结束】");
                    _log.Trace("【FastTrackRegisterInfoAppService】【SaveFastTrackRegisterInfoAsync】" +
                               "【提交患者快速通道登记信息结束】");
                    return JsonResult.Ok();
                }

                _log.Error("【FastTrackRegisterInfoAppService】【SaveFastTrackRegisterInfoAsync】" +
                           "【提交患者快速通道登记信息失败】【Msg：写入DB失败】");
                return JsonResult.Fail("提交失败！请检查后重试！");
            }
            catch (Exception e)
            {
                _log.Warning(
                    $"【FastTrackRegisterInfoAppService】【SaveFastTrackRegisterInfoAsync】【提交患者快速通道登记信息错误】【Msg：{e}】");
                return JsonResult.Fail(e.Message);
            }
        }
    }
}