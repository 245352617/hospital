using Mapster;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using SamJan.MicroService.PreHospital.Core;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using JsonResult = SamJan.MicroService.PreHospital.Core.JsonResult;
using StackExchange.Redis;
using Microsoft.AspNetCore.Mvc;

namespace SamJan.MicroService.PreHospital.TriageService
{
    /// <summary>
    /// 分诊设备信息接口
    /// </summary>
    [Auth("TriageDevice")]
    [Authorize]
    public class TriageDeviceAppService : ApplicationService, ITriageDeviceAppService
    {
        private readonly IRepository<TriageDevice> _triageDeviceRepository;
        private readonly IPatientInfoRepository _patientInfoRepository;
        private readonly NLogHelper _log;
        private readonly IConfiguration _configuration;
        private readonly IHttpClientHelper _httpClientHelper;
        private readonly IDatabase _redis;
        private ConsulHttpClient _consulHttpClient;
        private ConsulHttpClient ConsulHttpClient => LazyGetRequiredService(ref _consulHttpClient);
        private readonly IHisApi _hisApi;

        public TriageDeviceAppService(IRepository<TriageDevice> triageDeviceRepository,
            IPatientInfoRepository patientInfoRepository,
            NLogHelper log, RedisHelper redisHelper,
            IConfiguration configuration, IHttpClientHelper httpClientHelper, IHisApi hisApi)
        {
            _triageDeviceRepository = triageDeviceRepository;
            _patientInfoRepository = patientInfoRepository;
            _log = log;
            _redis = redisHelper.GetDatabase();
            _configuration = configuration;
            _httpClientHelper = httpClientHelper;
            _hisApi = hisApi;
        }

        /// <summary>
        /// 新增分诊设备信息
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [Auth("TriageDevice" + PermissionDefinition.Separator + PermissionDefinition.Create)]
        public async Task<JsonResult> CreateTriageDeviceAsync(CreateOrUpdateTriageDeviceDto dto)
        {
            try
            {
                var model = dto.BuildAdapter().AdaptToType<TriageDevice>();
                if (model == null)
                    return JsonResult.Fail("参数不能为空");
                model.SetId(GuidGenerator.Create());
                model.AddUser = CurrentUser.UserName;
                if (await _triageDeviceRepository.CountAsync(t => t.DeviceCode == model.DeviceCode) > 0)
                {
                    _log.Error("【TriageDeviceService】【CreateTriageDeviceAsync】【添加分诊设备信息错误：设备已存在】");
                    return JsonResult.Fail("设备已存在");
                }

                await _triageDeviceRepository.InsertAsync(model);
                return JsonResult.Ok();
            }
            catch (Exception e)
            {
                _log.Warning($"【TriageDeviceService】【CreateTriageDeviceAsync】【添加分诊设备信息错误】【Msg：{e}】");
                return JsonResult.Fail(e.Message);
            }
        }

        /// <summary>
        /// 修改分诊设备信息
        /// </summary>
        /// <param name="id"></param>
        /// <param name="dto"></param>
        /// <returns></returns>
        [Auth("TriageDevice" + PermissionDefinition.Separator + PermissionDefinition.Update)]
        public async Task<JsonResult> UpdateTriageDeviceAsync(Guid id, CreateOrUpdateTriageDeviceDto dto)
        {
            try
            {
                var triageDevice = await _triageDeviceRepository.AsNoTracking().FirstOrDefaultAsync(t => t.Id == id);
                if (triageDevice == null)
                    return JsonResult.Fail("数据不存在");
                if (await _triageDeviceRepository.CountAsync(t => t.DeviceCode == dto.DeviceCode && t.Id != id) > 0)
                {
                    _log.Error("【TriageDeviceService】【UpdateTriageDeviceAsync】【修改分诊设备信息错误：设备已存在】");
                    return JsonResult.Fail("设备已存在");
                }

                var model = dto.BuildAdapter().AdaptToType<TriageDevice>();
                model.SetId(id);
                model.ModUser = CurrentUser.UserName;
                model.AddUser = triageDevice.AddUser;
                await _triageDeviceRepository.UpdateAsync(model);
                return JsonResult.Ok();
            }
            catch (Exception e)
            {
                _log.Warning($"【TriageDeviceService】【UpdateTriageDeviceAsync】【修改分诊设备信息错误】【Msg：{e}】");
                return JsonResult.Fail(e.Message);
            }
        }

        /// <summary>
        /// 删除分诊设备信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Auth("TriageDevice" + PermissionDefinition.Separator + PermissionDefinition.Delete)]
        public async Task<JsonResult> DeleteTriageDeviceAsync(Guid id)
        {
            try
            {
                var triageDevice = await _triageDeviceRepository.FirstOrDefaultAsync(t => t.Id == id);
                if (triageDevice == null)
                {
                    return JsonResult.Fail("数据不存在");
                }

                triageDevice.DeleteUser = CurrentUser.UserName;
                await _triageDeviceRepository.DeleteAsync(triageDevice);
                return JsonResult.Ok();
            }
            catch (Exception e)
            {
                _log.Warning($"【TriageDeviceService】【DeleteTriageDeviceAsync】【删除分诊设备信息错误】【Msg：{e}】");
                return JsonResult.Fail(e.Message);
            }
        }

        /// <summary>
        /// 获取分诊设备信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Auth("TriageDevice" + PermissionDefinition.Separator + PermissionDefinition.Get)]
        public async Task<JsonResult<TriageDeviceDto>> GetTriageDeviceAsync(Guid id)
        {
            try
            {
                var triageDevice =
                    await _triageDeviceRepository.AsNoTracking().FirstOrDefaultAsync(t => t.Id == id);
                var dtos = triageDevice.BuildAdapter().AdaptToType<TriageDeviceDto>();
                return JsonResult<TriageDeviceDto>.Ok(data: dtos);
            }
            catch (Exception e)
            {
                _log.Warning($"【TriageDeviceService】【GetTriageDeviceAsync】【获取分诊设备信息错误】【Msg：{e}】");
                return JsonResult<TriageDeviceDto>.Fail(e.Message);
            }
        }

        /// <summary>
        /// 获取分诊设备信息列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [Auth("TriageDevice" + PermissionDefinition.Separator + PermissionDefinition.List)]
        public async Task<JsonResult<List<TriageDeviceDto>>> GetTriageDeviceListAsync(TriageDeviceWhereInput input)
        {
            try
            {
                var triageDeviceList = await _triageDeviceRepository
                    .WhereIf(!string.IsNullOrEmpty(input.DeviceCodeOrName),
                        t => t.DeviceName.Contains(input.DeviceCodeOrName) ||
                             input.DeviceCodeOrName.Contains(t.DeviceName) ||
                             t.DeviceCode.Contains(input.DeviceCodeOrName) ||
                             input.DeviceCodeOrName.Contains(t.DeviceCode))
                    .ToListAsync();
                var dtos = triageDeviceList.BuildAdapter().AdaptToType<List<TriageDeviceDto>>();
                return JsonResult<List<TriageDeviceDto>>.Ok(data: dtos);
            }
            catch (Exception e)
            {
                _log.Warning($"【TriageDeviceService】【GetTriageDeviceListAsync】【获取分诊设备信息列表错误】【Msg：{e}】");
                return JsonResult<List<TriageDeviceDto>>.Fail(e.Message);
            }
        }

        /// <summary>
        /// 调用iot接口获取设备信息
        /// </summary>
        /// <returns></returns>
        [Auth("TriageDevice" + PermissionDefinition.Separator + PermissionDefinition.Get)]
        public async Task<JsonResult<List<IotDeviceInfoDto>>> GetDeviceInfoByIot()
        {
            try
            {
                var url = _configuration["IotServerSettings:iotDeviceList"];
                var result = await _httpClientHelper.GetAsync(url);
                var list = JsonSerializer.Deserialize<IotResultDto>(result);
                var deviceList = await GetTriageDeviceListAsync(new TriageDeviceWhereInput());
                var deviceNotIot = deviceList.Data.Select(t => t.DeviceCode).ToArray();
                var returnInfo = list.datas?.Where(w => !deviceNotIot.Contains(w.id)).ToList();

                return JsonResult<List<IotDeviceInfoDto>>.Ok(data: returnInfo);
            }
            catch (Exception e)
            {
                _log.Warning($"【TriageDeviceService】【GetTriageDeviceListAsync】【调用iot接口获取设备信息错误】【Msg：{e}】");
                return JsonResult<List<IotDeviceInfoDto>>.Fail(data: e.Message);
            }
        }

        /// <summary>
        /// 获取监护仪生命体征信息
        /// </summary>
        /// <param name="deviceCode"></param>
        /// <returns></returns>
        [Auth("TriageDevice" + PermissionDefinition.Separator + PermissionDefinition.Get)]
        public async Task<string> GetIotVitalSignsInfo(string deviceCode)
        {
            try
            {
                var deviceInfo = await _triageDeviceRepository.FirstOrDefaultAsync(t => t.DeviceCode == deviceCode);
                if (deviceInfo == null)
                {
                    _log.Info("【TriageDeviceService】【GetIotVitalSignsInfo】【获取监护仪生命体征信息错误：设备不存在】");
                    return "{\"code\":\"500\",\"msg\":\"设备不存在\"}";
                }

                var url = _configuration["IotServerSettings:IotDeviceUrl"] +
                          $@"/api/iot/deviceData/recentData?NetAddress={deviceInfo.DeviceIPOrCom}&Vendor={deviceInfo.FactoryInfo}&Model={deviceInfo.DeviceModel}";
                var result = await _httpClientHelper.GetAsync(url);
                return result;
            }
            catch (Exception e)
            {
                _log.Warning($"【TriageDeviceService】【GetIotVitalSignsInfo】【获取监护仪生命体征信息错误】【Msg：{e}】");
                return "{\"code\":\"500\",\"msg\":\"" + e.Message + "\"}";
            }
        }


        /// <summary>
        /// 根据车牌号获取患者生命体征数据
        /// </summary>
        /// <param name="carNum"></param>
        /// <returns></returns>
        [Auth("TriageDevice" + PermissionDefinition.Separator + PermissionDefinition.Get)]
        public async Task<JsonResult> GetVitalSignInfoByCarNumAsync(string carNum)
        {
            try
            {
                var jr = await ConsulHttpClient.GetAsync("http", "PreHospitalEmrService",
                    $"/api/EmrService/taskInfo/currentTaskAmbulance?CarNum={carNum}");
                if (jr != null)
                {
                    var taskAmbulanceDtos =
                        (List<CurrentTaskAmbulanceDto>)JsonHelper.DeserializeObject<List<CurrentTaskAmbulanceDto>>(
                            jr.Data.ToString());
                    if (taskAmbulanceDtos != null && taskAmbulanceDtos.Count > 0)
                    {
                        var deviceCode = taskAmbulanceDtos.FirstOrDefault()?.Devices.FirstOrDefault()?.IotDeviceId;
                        if (deviceCode == null)
                        {
                            _log.Error("根据车牌号获取患者生命体征数据失败！原因：该设备不存在！");
                            return JsonResult.Fail("该设备不存在！");
                        }

                        //InfluxDB时间与服务器时间差一小时三分钟
                        var queryTime = DateTime.Now.AddHours(-8).AddMinutes(-3).AddMinutes(-1)
                            .ToString("yyyy-MM-ddTHH:mm:00.000Z");
                        //select * from iot_signs_data where iot_device_id='6b32a2e0-c16b-4ef4-b582-8c2cbd0791f7' and time>='2021-11-11T12:25:00.000Z' and time <='2021-11-11T12:27:00.000Z' and signs_data=~/NIBP_PR/
                        //获取监护仪体温数据
                        var url = _configuration["IotServerSettings:IotDeviceUrl"] +
                                  $"/api/iot/deviceData/resultBySql?sql=select * from iot_signs_data where iot_device_id='{deviceCode}' " +
                                  $" and signs_data=~/{IotCategory.Temp}/" +
                                  $" and time>='{queryTime}' " +
                                  $" order by time desc limit 1";
                        var result = await _httpClientHelper.GetAsync(url);
                        var temp = new ObservationData();
                        if (!string.IsNullOrWhiteSpace(result))
                        {
                            var json = JObject.Parse(result);
                            if (json["code"]?.ToString() == "200" && json["data"] != null &&
                                !json["data"].ToString().IsNullOrWhiteSpace())
                            {
                                var iotData = JsonHelper.DeserializeObject<IotDataDto>(json["data"]?.ToString())
                                    ?.signs_data;
                                temp = iotData?.ObservationDatas?.FirstOrDefault(x => x.Category == IotCategory.Temp);
                            }
                        }

                        //获取监护仪收缩压数据
                        url = _configuration["IotServerSettings:IotDeviceUrl"] +
                              $"/api/iot/deviceData/resultBySql?sql=select * from iot_signs_data where iot_device_id='{deviceCode}' " +
                              $" and signs_data=~/{IotCategory.Sbp}/" +
                              $" and time>='{queryTime}' " +
                              $" order by time desc limit 1";
                        result = await _httpClientHelper.GetAsync(url);
                        var sbp = new ObservationData();
                        if (!string.IsNullOrWhiteSpace(result))
                        {
                            var json = JObject.Parse(result);
                            if (json["data"] != null && !json["data"].ToString().IsNullOrWhiteSpace())
                            {
                                var iotData = JsonHelper.DeserializeObject<List<IotDataDto>>(json["data"]?.ToString());
                                sbp = iotData?.FirstOrDefault()?.signs_data?.ObservationDatas
                                    ?.FirstOrDefault(x => x.Category == IotCategory.Sbp);
                            }
                        }

                        //获取监护仪舒张压数据
                        url = _configuration["IotServerSettings:IotDeviceUrl"] +
                              $"/api/iot/deviceData/resultBySql?sql=select * from iot_signs_data where iot_device_id='{deviceCode}' " +
                              $" and signs_data=~/{IotCategory.Sdp}/" +
                              $" and time>='{queryTime}' " +
                              $" order by time desc limit 1";
                        result = await _httpClientHelper.GetAsync(url);
                        var sdp = new ObservationData();
                        if (!string.IsNullOrWhiteSpace(result))
                        {
                            var json = JObject.Parse(result);
                            if (json["data"] != null && !json["data"].ToString().IsNullOrWhiteSpace())
                            {
                                var iotData = JsonHelper.DeserializeObject<List<IotDataDto>>(json["data"]?.ToString());
                                sdp = iotData?.FirstOrDefault()?.signs_data?.ObservationDatas
                                    ?.FirstOrDefault(x => x.Category == IotCategory.Sdp);
                            }
                        }

                        //获取监护仪血氧饱和度数据
                        url = _configuration["IotServerSettings:IotDeviceUrl"] +
                              $"/api/iot/deviceData/resultBySql?sql=select * from iot_signs_data where iot_device_id='{deviceCode}' " +
                              $" and signs_data=~/{IotCategory.SpO2}/" +
                              $" and time>='{queryTime}' " +
                              $" order by time desc limit 1";
                        result = await _httpClientHelper.GetAsync(url);
                        var spo2 = new ObservationData();
                        if (!string.IsNullOrWhiteSpace(result))
                        {
                            var json = JObject.Parse(result);
                            if (json["data"] != null && !json["data"].ToString().IsNullOrWhiteSpace())
                            {
                                var iotData = JsonHelper.DeserializeObject<List<IotDataDto>>(json["data"]?.ToString());
                                spo2 = iotData?.FirstOrDefault()?.signs_data?.ObservationDatas
                                    ?.FirstOrDefault(x => x.Category == IotCategory.SpO2);
                            }
                        }

                        //获取监护仪血氧饱和度数据
                        url = _configuration["IotServerSettings:IotDeviceUrl"] +
                              $"/api/iot/deviceData/resultBySql?sql=select * from iot_signs_data where iot_device_id='{deviceCode}' " +
                              $" and signs_data=~/{IotCategory.BreathRate}/" +
                              $" and time>='{queryTime}' " +
                              $" order by time desc limit 1";
                        result = await _httpClientHelper.GetAsync(url);
                        var breathRate = new ObservationData();
                        if (!string.IsNullOrWhiteSpace(result))
                        {
                            var json = JObject.Parse(result);
                            if (json["data"] != null && !json["data"].ToString().IsNullOrWhiteSpace())
                            {
                                var iotData = JsonHelper.DeserializeObject<List<IotDataDto>>(json["data"]?.ToString());
                                breathRate = iotData?.FirstOrDefault()?.signs_data?.ObservationDatas
                                    ?.FirstOrDefault(x => x.Category == IotCategory.BreathRate);
                            }
                        }

                        //获取监护仪血氧饱和度数据
                        url = _configuration["IotServerSettings:IotDeviceUrl"] +
                              $"/api/iot/deviceData/resultBySql?sql=select * from iot_signs_data where iot_device_id='{deviceCode}' " +
                              $" and signs_data=~/{IotCategory.HeartRate}/" +
                              $" and time>='{queryTime}' " +
                              $" order by time desc limit 1";
                        result = await _httpClientHelper.GetAsync(url);
                        var heartRate = new ObservationData();
                        if (!string.IsNullOrWhiteSpace(result))
                        {
                            var json = JObject.Parse(result);
                            if (json["data"] != null && !json["data"].ToString().IsNullOrWhiteSpace())
                            {
                                var iotData = JsonHelper.DeserializeObject<List<IotDataDto>>(json["data"]?.ToString());
                                heartRate = iotData?.FirstOrDefault()?.signs_data?.ObservationDatas
                                    ?.FirstOrDefault(x => x.Category == IotCategory.HeartRate);
                            }
                        }

                        //获取监护仪无创平均血压数据
                        url = _configuration["IotServerSettings:IotDeviceUrl"] +
                              $"/api/iot/deviceData/resultBySql?sql=select * from iot_signs_data where iot_device_id='{deviceCode}' " +
                              $" and signs_data=~/{IotCategory.NIBP_MAP}/" +
                              $" and time>='{queryTime}' " +
                              $" order by time desc limit 1";
                        result = await _httpClientHelper.GetAsync(url);
                        var nibpMap = new ObservationData();
                        if (!string.IsNullOrWhiteSpace(result))
                        {
                            var json = JObject.Parse(result);
                            if (json["data"] != null && !json["data"].ToString().IsNullOrWhiteSpace())
                            {
                                var iotData = JsonHelper.DeserializeObject<List<IotDataDto>>(json["data"]?.ToString());
                                nibpMap = iotData?.FirstOrDefault()?.signs_data?.ObservationDatas
                                    ?.FirstOrDefault(x => x.Category == IotCategory.NIBP_MAP);
                            }
                        }

                        var dto = new IotDataDto
                        {
                            signs_data = new IotSignsDataToInflux
                            {
                                ObservationDatas = new List<ObservationData>
                                {
                                    sbp,
                                    sdp,
                                    spo2,
                                    temp,
                                    heartRate,
                                    breathRate,
                                    nibpMap
                                }
                            }
                        };

                        return JsonResult.Ok(data: JsonSerializer.Serialize(dto));
                    }
                }

                _log.Error("【TriageDeviceService】【GetVitalSignInfoByCarNumAsync】【根据车牌号获取患者生命体征数据失败】" +
                           "【Msg：未查询到设备！请联系管理员】");
                return JsonResult.Fail("未查询到设备！请联系管理员");
            }
            catch (Exception e)
            {
                _log.Warning("【TriageDeviceService】【GetVitalSignInfoByCarNumAsync】" +
                             $"【根据车牌号获取患者生命体征数据错误】【Msg：{e}】");
                return JsonResult.Fail(e.Message);
            }
        }

        /// <summary>
        /// 通过门诊病人二维码获取门诊病人信息
        /// 龙岗中心设备对接生命体征接口
        /// </summary>
        /// <param name="id">患者 Id</param>
        /// <returns></returns>
        [AllowAnonymous]
        [ApiExplorerSettings(IgnoreApi = true)]
        [HttpGet("/api/TriageService/TriageDevice/GetMzPatientInfo")]
        public async Task<IActionResult> GetMzPatientInfo([FromQuery] string id)
        {
            _log.Info($"通过门诊病人二维码获取门诊病人信息, Id: {id}");
            MZPatientInfo mZPatientInfo = new MZPatientInfo
            {
                TIME = DateTime.Now,
            };
            var patientInfo = await this._patientInfoRepository.AsQueryable()
                .FirstOrDefaultAsync(x => x.PatientId == id);
            if (patientInfo != null)
            {
                mZPatientInfo = new MZPatientInfo
                {
                    NAME = patientInfo?.PatientName,
                    OPID = patientInfo?.PatientId,
                    SEX = patientInfo?.SexName,
                    TIME = DateTime.Now,
                };
            }
            else
            {
                var cache = await _redis.StringGetAsync($"{_configuration["ServiceName"]}:PatientBuilt:{id}");
                if (!string.IsNullOrEmpty(cache))
                {
                    PatientOutput patientCache = JsonHelper.DeserializeObject<PatientOutput>(cache);
                    mZPatientInfo = new MZPatientInfo
                    {
                        NAME = patientCache.PatientName,
                        OPID = patientCache.PatientId,
                        SEX = patientCache.Sex switch
                        {
                            "Sex_Man" => "男",
                            "Sex_Woman" => "女",
                            _ => "未知"
                        },
                        TIME = DateTime.Now,
                    };
                }
            }

            return new Microsoft.AspNetCore.Mvc.JsonResult(mZPatientInfo,
                new Newtonsoft.Json.JsonSerializerSettings
                {
                    DateFormatString = "yyyy/MM/dd HH:mm:ss",
                    ContractResolver = new Newtonsoft.Json.Serialization.DefaultContractResolver()
                });
        }

        /// <summary>
        /// 实现门诊患者生命体征数据的保存功能
        /// </summary>
        /// <param name="Infos"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [ApiExplorerSettings(IgnoreApi = true)]
        [HttpPost("/api/TriageService/TriageDevice/SaveMzPatientVitalSignInfos")]
        public async Task<IActionResult> SaveMzPatientVitalSignInfos(IEnumerable<MZPatientVitalSignInfo> Infos)
        {
            _log.Info($"实现门诊患者生命体征数据的保存功能, Infos: {JsonSerializer.Serialize(Infos)}");
            var serviceName = _configuration.GetSection("ServiceName").Value;
            var key = $"{serviceName}:MZPatientInfoList";
            foreach (var info in Infos)
            {
                await _redis.HashSetAsync(key, info.OPID.ToString(), JsonSerializer.Serialize(info));
            }

            return new Microsoft.AspNetCore.Mvc.JsonResult(MZResult.Success(),
                new Newtonsoft.Json.JsonSerializerSettings
                {
                    DateFormatString = "yyyy/MM/dd HH:mm:ss",
                    ContractResolver = new Newtonsoft.Json.Serialization.DefaultContractResolver()
                });
        }

        /// <summary>
        /// 获取生命体征（龙岗设备）
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<JsonResult<object>> GetMzDeviceData(string id)
        {
            var serviceName = _configuration.GetSection("ServiceName").Value;
            var key = $"{serviceName}:MZPatientInfoList";
            var cache = await _redis.HashGetAsync(key, id);
            if (!cache.HasValue)
            {
                return JsonResult<object>.Fail("无相关数据");
            }

            var mzVitalSignInfo = JsonSerializer.Deserialize<MZPatientVitalSignInfo>(cache);

            return JsonResult<object>.Ok(data: new
            {
                signs_data = new
                {
                    ObservationDatas = new object[]
                    {
                        new { Category = "BreathRate", Result = mzVitalSignInfo.Mb.ToString() },
                        new { Category = "Temp", Result = mzVitalSignInfo.Tw > 0 ? mzVitalSignInfo.Tw.ToString() : "" },
                        new { Category = "RR", Result = mzVitalSignInfo.Hx > 0 ? mzVitalSignInfo.Hx.ToString() : "" },
                        new
                        {
                            Category = "Sbp", Result = mzVitalSignInfo.Ssy > 0 ? mzVitalSignInfo.Ssy.ToString() : ""
                        },
                        new
                        {
                            Category = "Sdp", Result = mzVitalSignInfo.Szy > 0 ? mzVitalSignInfo.Szy.ToString() : ""
                        },
                        new
                        {
                            Category = "Pjy", Result = mzVitalSignInfo.Pjy > 0 ? mzVitalSignInfo.Pjy.ToString() : ""
                        },
                        new
                        {
                            Category = "SpO2", Result = mzVitalSignInfo.Spo2 > 0 ? mzVitalSignInfo.Spo2.ToString() : ""
                        },
                        new
                        {
                            Category = "Xt", Result = mzVitalSignInfo.Xt > 0 ? mzVitalSignInfo.Xt.ToString() : ""
                        }, // 未采集血糖的情况下设备返回-1
                        new { Category = "XtType", Result = mzVitalSignInfo.XtType.ToString() }
                    }
                }
            });
        }

        /// <summary>
        /// 获取生命体征信息-金湾
        /// </summary>
        /// <param name="deviceCode"></param>
        /// <returns></returns>
        [Auth("TriageDevice" + PermissionDefinition.Separator + PermissionDefinition.Get)]
        public async Task<JsonResult<object>> GetVitalSignsInfoJinWanAsync(string deviceCode)
        {
            try
            {
                if (!await _triageDeviceRepository.AnyAsync(t => t.DeviceCode == deviceCode))
                {
                    _log.Info("【TriageDeviceService】【GetJWVitalSignsInfo】【获取生命体征信息错误：设备不存在】");
                    return JsonResult<object>.Fail("设备不存在");
                }

                var vitalSignsInfo = await _hisApi.GetHisVitalSignsAsync(deviceCode);
                if (vitalSignsInfo.Code == 200)
                {
                    var vitalSigns = vitalSignsInfo.Data;
                    if (vitalSigns == null)
                        return JsonResult<object>.Fail("暂无生命体征数据");
                    var data = new
                    {
                        signs_data = new
                        {
                            ObservationDatas = new object[]
                            {
                                new { Category = "PR", Result = vitalSigns.mb },
                                new { Category = "HR", Result = vitalSigns.mb },
                                new { Category = "SpO2", Result = vitalSigns.xy },
                                new { Category = "NIBP_SYS", Result = vitalSigns.ssy },
                                new { Category = "NIBP_DIA", Result = vitalSigns.szy },
                                new { Category = "Temp", Result = vitalSigns.tw },
                            }
                        }
                    };
                    return JsonResult<object>.Ok(data: data);
                }
                return JsonResult<object>.Fail(vitalSignsInfo.Msg);
            }
            catch (Exception e)
            {
                _log.Warning($"【TriageDeviceService】【GetJWVitalSignsInfo】【获取生命体征信息错误】【Msg：{e}】");
                return JsonResult<object>.Fail(e.Message);
            }
        }
    }
}