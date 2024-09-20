using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using DotNetCore.CAP;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SamJan.MicroService.PreHospital.Core;
using StackExchange.Redis;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Uow;
using JsonResult = SamJan.MicroService.PreHospital.Core.JsonResult;
using JsonSerializerOptions = System.Text.Json.JsonSerializerOptions;

namespace SamJan.MicroService.PreHospital.TriageService
{
    /// <summary>
    /// SMS 接口
    /// </summary>
    [Authorize]
    [Auth("SMS")]
    public partial class SmsAppService : ApplicationService, ISmsAppService, ICapSubscribe
    {
        private readonly ILogger<SmsAppService> _log;
        private readonly IDatabase _redis;
        private readonly IConfiguration _configuration;
        private static readonly long Jan1St1970Ms = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).Ticks;
        

        private IHttpClientFactory _httpClientFactory;
        private IHttpClientFactory HttpClientFactory => LazyGetRequiredService(ref _httpClientFactory);
        

        private IRepository<TextMessageRecord, Guid> _textMessageRecordRepository;
        private IRepository<TextMessageRecord, Guid> TextMessageRecordRepository => LazyGetRequiredService(ref _textMessageRecordRepository);


        private IRepository<TextMessageTemplate, Guid> _textMessageTemplateRepository;
        private IRepository<TextMessageTemplate, Guid> TextMessageTemplateRepository => LazyGetRequiredService(ref _textMessageTemplateRepository);

        private const string TextMessageTemplateRedisKey = AppSettings.TriageService + ":TextMessageTemplate";


        public SmsAppService(ILogger<SmsAppService> log, RedisHelper redisHelper, IConfiguration configuration)
        {
            _log = log;
            _configuration = configuration;
            _redis = redisHelper.GetDatabase();
        }


        /// <summary>
        /// 查询短信模板
        /// </summary>
        /// <returns></returns>
        [Auth("SMS" + PermissionDefinition.Separator + "TextMessageTemplate" + PermissionDefinition.Separator +
              PermissionDefinition.Get)]
        public async Task<JsonResult<TextMessageTemplateDto>> GetTextMessageTemplateAsync()
        {
            try
            {
                TextMessageTemplateDto res;
                if (!await _redis.KeyExistsAsync(TextMessageTemplateRedisKey))
                {
                    var settingsList = await TextMessageTemplateRepository.FirstOrDefaultAsync();
                    res = settingsList.Adapt<TextMessageTemplate, TextMessageTemplateDto>();
                    await _redis.StringSetAsync(TextMessageTemplateRedisKey, JsonHelper.SerializeObject(res));
                }
                else
                {
                    var json = await _redis.StringGetAsync(TextMessageTemplateRedisKey);
                    res = JsonHelper.DeserializeObject<TextMessageTemplateDto>(json);
                }

                _log.LogInformation("查询短信模板成功");
                return JsonResult<TextMessageTemplateDto>.Ok(data: res);
            }
            catch (Exception e)
            {
                _log.LogError("查询短信模板错误，原因：{Msg}", e);
                return JsonResult<TextMessageTemplateDto>.Fail(e.Message);
            }
        }

        /// <summary>
        /// 保存短信模板
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [Auth("SMS" + PermissionDefinition.Separator + "TextMessageTemplate" + PermissionDefinition.Separator + PermissionDefinition.Save)]
        public async Task<JsonResult> SaveTextMessageTemplateAsync(TextMessageTemplateDto dto)
        {
            try
            {
                TextMessageTemplate settings;
                var db = TextMessageTemplateRepository.GetDbContext();
                if (dto.Id == Guid.Empty)
                {
                    settings = new TextMessageTemplate();
                    settings = dto.BuildAdapter().AdaptToType<TextMessageTemplate>();
                    db.Entry(settings).State = EntityState.Added;
                }
                else
                {
                    settings = await TextMessageTemplateRepository.FirstOrDefaultAsync(x => x.Id == dto.Id);
                    if (settings == null)
                    {
                        _log.LogError("保存短信模板失败，原因：不存在此标签设置");
                        return JsonResult.Fail("不存在此短信模板！！！");
                    }

                    settings.Content = dto.Content;
                    settings.ConcurrencyStamp = dto.ConcurrencyStamp;
                    db.Entry(settings).State = EntityState.Modified;
                }

                if (await db.SaveChangesAsync() > 0)
                {
                    _log.LogInformation("保存标签设置成功");
                    dto.Id = settings.Id;
                    dto.Time = settings.LastModificationTime ?? settings.CreationTime;
                    dto.ConcurrencyStamp = settings.ConcurrencyStamp;
                    await _redis.HashSetAsync(TextMessageTemplateRedisKey, dto.Id.ToString(),
                        JsonHelper.SerializeObject(dto));
                    return JsonResult.Ok();
                }

                _log.LogError("保存标签设置失败，原因：保存数据失败");
                return JsonResult.Fail(msg: "保存失败，请检查后重试！！！");
            }
            catch (Exception e)
            {
                _log.LogError("保存短信模板错误，原因：{Msg}", e);
                return JsonResult.Fail(e.Message);
            }
        }

        /// <summary>
        /// 查询短信记录
        /// </summary>
        /// <param name="startTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <param name="taskInfoNum">任务单号</param>
        /// <param name="pageIndex" example="1">>当前页码</param>
        /// <param name="pageSize" example="50">当前页大小</param>
        /// <param name="isOrderByDesc" example="false">>是否降序排序</param>
        /// <returns></returns>
        [Auth("SMS" + PermissionDefinition.Separator + "TextMessageRecord" + PermissionDefinition.Separator + PermissionDefinition.List)]
        public async Task<JsonResult<PagedResultDto<TextMessageRecordDto>>> GetTextMessageRecordListAsync(
            DateTime? startTime, DateTime? endTime, string taskInfoNum, int pageIndex = 1,
            int pageSize = 50, bool isOrderByDesc = true)
        {
            try
            {
                if (startTime.HasValue && endTime.HasValue)
                {
                    startTime = startTime.Value.Date;
                    endTime = endTime.Value.Date.AddDays(1).AddSeconds(-1);
                }

                var query = TextMessageRecordRepository.WhereIf(!taskInfoNum.IsNullOrWhiteSpace(),
                            x => x.TaskInfoNum.Contains(taskInfoNum))
                        .WhereIf(startTime < endTime, x => x.SendTime >= startTime && x.SendTime <= endTime)
                    ;
                query = isOrderByDesc ? query.OrderByDescending(o => o.SendTime) : query.OrderBy(o => o.SendTime);

                var paged = await query.Select(s => new TextMessageRecordDto
                    {
                        TextMessage = s.TextMessage,
                        SendTime = s.SendTime,
                        TaskInfoNum = s.TaskInfoNum,
                        SendToPhone = s.SendToPhone
                    })
                    .PageListAsync(pageIndex, pageSize);

                _log.LogInformation("查询短信记录成功");
                return JsonResult<PagedResultDto<TextMessageRecordDto>>.Ok(data: paged);
            }
            catch (Exception e)
            {
                _log.LogError("查询短信记录错误，原因：{Msg}", e);
                return JsonResult<PagedResultDto<TextMessageRecordDto>>.Fail(e.Message);
            }
        }

        /// <summary>
        /// 删除短信模板
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<JsonResult> DelTextMessageTemplateAsync(Guid id)
        {
            try
            {
                var phone = await TextMessageTemplateRepository.FirstOrDefaultAsync(x => x.Id == id);
                if (phone == null)
                {
                    _log.LogError("删除短信模板失败，原因：不存在此短信模板");
                    return JsonResult.Fail("不存在此短信模板！！！");
                }

                var db = TextMessageTemplateRepository.GetDbContext();
                db.Entry(phone).State = EntityState.Deleted;
                if (await db.SaveChangesAsync() > 0)
                {
                    _log.LogInformation("删除短信模板成功");
                    await _redis.KeyDeleteAsync(TextMessageTemplateRedisKey);
                    return JsonResult.Ok();
                }
                
                _log.LogError("删除短信模板错误，原因：数据库删除数据失败");
                return JsonResult.Fail("删除失败，请稍后重试！！！");
            }
            catch (Exception e)
            {
                _log.LogError("删除短信模板错误，原因：{Msg}",e);
                return JsonResult.Fail(e.Message);
            }
        }

        /// <summary>
        /// 发送开通绿通短信
        /// </summary>
        [ApiExplorerSettings(IgnoreApi = true)]
        [CapSubscribe("send.sms.when.opening.greenRoad")]
        public async Task SendOpenGreenRoadTextMessageAsync(OpenGreenRoadSmsDto dto)
        {
            try
            {
                using var uow = UnitOfWorkManager.Begin(new AbpUnitOfWorkOptions());
                var jr = await GetTextMessageTemplateAsync();
                if (jr.Code != 200 && jr.Data == null)
                {
                    return;
                }

                var templateContent = jr.Data.Content; //"${name},${phone}"
                var strList = MidStrEx_New(templateContent, "{", "}");
                var tagSettingsJr = await GetTagSettingsListAsync("");
                if (tagSettingsJr.Code != 200 && tagSettingsJr.Data.Any(x => x.IsSendMessage))
                {
                    _log.LogInformation("没有对应的标签管理");
                    return;
                }

                var dutyTelephoneJr = await GetDutyTelephoneListAsync("","","");
                if (dutyTelephoneJr.Code != 200 && dutyTelephoneJr.Data.Count <= 0)
                {
                    _log.LogInformation("没有配置值班电话号码");
                    return;
                }

                var phoneList = dutyTelephoneJr.Data.Where(x=> x.GreenRoads.Split(',').Any(xx=>xx==dto.GreenRoadName))
                    .Where(x => tagSettingsJr.Data.Any(xx => xx.IsSendMessage && xx.Id == x.TagSettingId))
                    .ToList();
                if (phoneList.Count <= 0)
                {
                    _log.LogInformation("没有需要发送短信的值班电话号码");
                    return;
                }

                var sendPhone = string.Join(",", phoneList.Select(s=>s.Telephone).ToList());
                var timestamp = (DateTime.UtcNow.Ticks - Jan1St1970Ms) / 10000;
                var properties = typeof(OpenGreenRoadSmsDto).GetProperties();
                foreach (var str in strList)
                {
                    var property = properties.FirstOrDefault(x => x.Name == str);
                    if (property != null)
                    {
                        templateContent = templateContent.Replace("{" + str + "}",
                            property.GetValue(dto, null)?.ToString());
                    }
                }

                var smsContent = HttpUtility.UrlEncode(templateContent,Encoding.UTF8);
                var key = Get32MD5(_configuration["Settings:SMS:EprId"] + _configuration["Settings:SMS:UserId"] +
                                   _configuration["Settings:SMS:Pwd"] +
                                   timestamp);

                var sb = new StringBuilder();
                sb.Append(_configuration["Settings:SMS:Uri"] + "?");
                sb.Append("cmd=" + _configuration["Settings:SMS:Cmd"]);
                sb.Append("&eprId=" + _configuration["Settings:SMS:EprId"]);
                sb.Append("&userId=" + _configuration["Settings:SMS:UserId"]);
                sb.Append("&key=" + key);
                sb.Append("&timestamp=" + timestamp);
                sb.Append("&format=" + 1);
                sb.Append("&mobile=" + sendPhone);
                sb.Append("&msgId=" + new Random().Next(1, 9999));
                sb.Append("&content=" + smsContent);
                var client = HttpClientFactory.CreateClient();
                client.Timeout = TimeSpan.FromSeconds(10);
                var result = "";
                try
                {
                    var response = await client.PostAsync(sb.ToString(),null);
                    if (response.IsSuccessStatusCode)
                    {
                        result = await response.Content.ReadAsStringAsync();
                        _log.LogInformation("调用短信接口成功，接口返回：{Result}", result);
                    }
                }
                catch (Exception ex)
                {
                    _log.LogError("调用短信接口错误，原因：{Msg}", ex);
                    result = ex.ToString();
                }

                var textMessageRecord = new TextMessageRecord
                {
                    TextMessage = templateContent,
                    TaskInfoId = dto.TaskInfoId,
                    TaskInfoNum = dto.TaskInfoNum,
                    SendTime = DateTime.Now,
                    SendToPhone = sendPhone,
                    Response = result
                };

                _log.LogInformation("写入短信记录表开始");
                await TextMessageRecordRepository.InsertAsync(textMessageRecord);
                await uow.CompleteAsync();
            }
            catch (Exception e)
            {
                _log.LogError("发送开通绿通短信错误，原因：{Msg}", e);
            }
        }
        
        /// <summary>
        /// 正则获取字符
        /// </summary>
        /// <param name="source"></param>
        /// <param name="startStr"></param>
        /// <param name="endStr"></param>
        /// <returns></returns>
        private List<string> MidStrEx_New(string source, string startStr, string endStr)
        {
            
            var rg = new Regex("(?<=(" + startStr + "))[.\\s\\S]*?(?=(" + endStr + "))", RegexOptions.Multiline | RegexOptions.Singleline);

            return rg.Matches(source).Select(s=>s.Value).ToList();
        }
        
        /// <summary>
        /// MD5 32位加密
        /// </summary>
        /// <param name="strMd"></param>
        /// <returns></returns>
        private string Get32MD5(string strMd)
        {
            MD5 md = new MD5CryptoServiceProvider();

            var b = md.ComputeHash(Encoding.Default.GetBytes(strMd));

            var md5Str = BitConverter.ToString(b).Replace("-", "");

            return md5Str;
        }
    }
}